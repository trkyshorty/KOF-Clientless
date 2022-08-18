namespace KOF.Core.Communications;

/// <summary>
///     A <see cref="Message" /> service endpoint.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class MessageHandlerAttribute : Attribute
{
    public readonly byte ID;
    public MessageHandlerAttribute(byte id)
    {
        ID = id;
    }
}