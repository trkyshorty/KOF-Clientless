using System.Reflection;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using KOF.Database.Models;
using KOF.Core.Communications;
using KOF.Core.Exceptions;
using KOF.Core.Win32;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace KOF.Core;

public class Session : IDisposable
{
    public delegate Task MessageHandler(Session s, Message m);

    private readonly List<Tuple<byte, MessageHandler>> _handlers = new();

    private readonly Dictionary<Type, object> _services = new();

    private Socket _socket { get; set; }

    internal readonly MessageProtocol Protocol;

    public Account Account { get; private set; } = new();

    public Client Client { get; private set; } = default!;

    public Session(Account account, Client client)
    {
        Account = account;
        Client = client;

        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        /*{
            NoDelay = false,
            Blocking = true
        };*/

        Protocol = new ClientMessageProtocol();
    }

    public bool Connected => _socket.Connected;

    public bool Ready => Protocol.Ready;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();

        if (disposing)
            _socket.Dispose();
    }

    ~Session()
    {
        Dispose(false);
    }

    private void ReleaseUnmanagedResources()
    {
        _services.Clear();
        _handlers.Clear();
    }

    public void RegisterService<T>(T service) where T : class
    {
        if (FindService<T>() != null)
            return;

        _services.Add(typeof(T), service);

        foreach (var (method, attr) in from method in service.GetType()
                     .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                       let attr = method.GetCustomAttribute<MessageHandlerAttribute>()
                                       where attr != null
                                       select (method, attr))
        {
            var hnd = (MessageHandler)Delegate.CreateDelegate(typeof(MessageHandler), service, method);
            _handlers.Add(Tuple.Create(attr.ID, hnd));
        }
    }

    public void RegisterService<T>() where T : class
    {
        RegisterService(Activator.CreateInstance<T>());
    }

    public void RemoveService<T>(bool removeHandlers = true) where T : class
    {
        var service = FindService<T>();
        if (service == null)
            return;

        _services.Remove(typeof(T));

        if (!removeHandlers)
            return;

        foreach (var handler in _handlers.Where(x => x.Item2.Target?.GetType() == typeof(T)))
            _handlers.Remove(handler);
    }

    public T? FindService<T>() where T : class
    {
        return (T?)_services.GetValueOrDefault(typeof(T));
    }

    public void RegisterHandler(MessageHandler handler)
    {
        if (_handlers.FirstOrDefault(x => x.Item2 == handler) != null)
            return;

        var attr = handler.GetMethodInfo().GetCustomAttribute<MessageHandlerAttribute>();
        if (attr == null)
            return;

        _handlers.Add(Tuple.Create(attr.ID, handler));
    }

    public Task ConnectAsync(string ip, int port)
    {
        // When attempting to reconnect we have to reset the protocol
        // or we will put it in a invalid status.
        Protocol.Option = MessageProtocolOption.None;
        Protocol.State = MessageProtocolState.WaitSetup;

        return _socket.ConnectAsync(ip, port);
    }

    public async Task SendAsync(Message msg)
    {
        if (Client.ClientProcess == null)
        {
            try
            {
                if (!_socket.Connected) return;
                await _socket.SendAsync(Protocol.Encode(this, msg), SocketFlags.None).ConfigureAwait(false);
            }
            catch (SocketException)
            {
                throw;
            }
        }
        else
        {
            if (Client.ClientProcess.HasExited) return;

            IntPtr PacketPtr = Win32Api.VirtualAllocEx(Client.ClientProcess.Handle, IntPtr.Zero, 1, Win32Api.MEM_COMMIT, Win32Api.PAGE_EXECUTE_READWRITE);

            var packet = msg.AsDataSpan().ToArray();

            Debug.WriteLine($"{DateTime.Now:HH:mm:ss} SEND: {Convert.ToHexString(msg.AsDataSpan()).ToLower()}");

            Win32Api.WriteProcessMemory(Client.ClientProcess.Handle, PacketPtr, packet, packet.Length, 0);

            Client.ExecuteRemoteCode("608B0D"
                + Win32Api.AlignDWORD(0xF7E32C)  //KO_PTR_PKT
                + "68"
                + Win32Api.AlignDWORD(packet.Length)
                + "68"
                + Win32Api.AlignDWORD(PacketPtr)
                + "BF"
                + Win32Api.AlignDWORD(0x5ED620) //KO_SND_FNC
                + "FFD7C605"
                + Win32Api.AlignDWORD(0xF7E32C + 0xC5) //KO_PTR_PKT
                + "0061C3");

            Win32Api.VirtualFreeEx(Client.ClientProcess.Handle, PacketPtr, 0, Win32Api.MEM_RELEASE);
        }
    }

    public Task DisconnectAsync()
    {
        return !_socket.Connected
            ? Task.CompletedTask
            : Task.Factory.FromAsync(_socket.BeginDisconnect, _socket.EndDisconnect, true, null);
    }

    public async Task<Message> ReceiveAsync()
    {
        // instead of calling the method recursively on itself.
        // because doing so is a bad idea, and performance killer. (Ask Google if you don't know why)

        var header = new byte[2];
        await ReceiveExactAsync(header.AsMemory(), SocketFlags.None).ConfigureAwait(false);

        var sizeBuffer = new byte[2];
        await ReceiveExactAsync(sizeBuffer.AsMemory(), SocketFlags.None).ConfigureAwait(false);

        var size = MemoryMarshal.Read<MessageSize>(sizeBuffer);
        var remaining = size.Value;

        var buffer = new byte[remaining];
        await ReceiveExactAsync(buffer.AsMemory(), SocketFlags.None).ConfigureAwait(false);

        var tail = new byte[2];
        await ReceiveExactAsync(tail.AsMemory(), SocketFlags.None).ConfigureAwait(false);

        var msg = Protocol.Decode(this, size, buffer.AsSpan());

        return msg;
    }

    private async Task ReceiveExactAsync(Memory<byte> buffer, SocketFlags flags)
    {
        var received = 0;
        var remaining = buffer.Length;
        
        while (received < remaining)
        {
            if (!_socket.Connected)
                return;

            var receivedChunk = await _socket.ReceiveAsync(buffer[received..], flags)
                .ConfigureAwait(false);

            if (receivedChunk == 0)
                continue;

            received += receivedChunk;
        }
    }

    public async Task RespondAsync(Message msg)
    {
        foreach (var (id, handler) in _handlers.ToList())
            if (msg.ID.Equals(id))
                await handler.Invoke(this, msg).ConfigureAwait(false);
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        if (Client.ClientProcess == null)
        {
            while (!cancellationToken.IsCancellationRequested && _socket.Connected)
            {
                var msg = await ReceiveAsync().ConfigureAwait(false);
                await RespondAsync(msg).ConfigureAwait(false);
            }
        }
        else
        {
            while (!cancellationToken.IsCancellationRequested && !Client.ClientProcess.HasExited)
            {
                Int32 MailslotRecvMessageSize = 0; Int32 MailslotRecvMessageLeft = 0;

                Win32Api.GetMailslotInfo(Client.MailslotRecvPtr, IntPtr.Zero, out MailslotRecvMessageSize, out MailslotRecvMessageLeft, IntPtr.Zero);

                if (MailslotRecvMessageSize > 0)
                {
                    byte[] MessageBuffer = new byte[MailslotRecvMessageSize];

                    do
                    {
                        Int32 MessageReadByte = 0;

                        Win32Api.ReadFile(Client.MailslotRecvPtr, MessageBuffer, MailslotRecvMessageSize, out MessageReadByte, IntPtr.Zero);
                        
                        if (MessageReadByte > 0)
                        {
                            var msg = new Message(MessageBuffer.Length, MessageBuffer);
                            await RespondAsync(msg).ConfigureAwait(false);
                            //Debug.WriteLine($"{DateTime.Now:HH:mm:ss} RECV: {Convert.ToHexString(msg.AsDataSpan()).ToLower()}");
                        }

                        MailslotRecvMessageLeft -= 1;

                        await Task.Delay(1);

                    } while (MailslotRecvMessageLeft != 0);
                }

                await Task.Delay(1);
            }
        }
    }
}