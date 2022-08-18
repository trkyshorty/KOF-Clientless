using KOF.Core.Communications;
using KOF.Core.Handlers;
using KOF.Core.Models;
using KOF.Database;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace KOF.UI.Forms;

public partial class CharacterCreate : Form
{
    private long AccountId { get; set; }

    public CharacterCreate(long accountId)
    {
        AccountId = accountId;

        InitializeComponent();
    }

    private void CharacterCreate_Load(object sender, EventArgs e)
    {
        var account = ClientHandler.AccountList.FirstOrDefault(x => x.Id == AccountId)!;

        if (account.NationId == 1)
        {
            var raceKarus = new List<KeyValuePair<byte, string>>
            {
                new KeyValuePair<byte, string>(byte.MaxValue, "Select race"),
                new KeyValuePair<byte, string>(1, "Big"),
                new KeyValuePair<byte, string>(2, "Middle"),
                new KeyValuePair<byte, string>(3, "Small"),
                new KeyValuePair<byte, string>(4, "Woman" ),
                new KeyValuePair<byte, string>(6, "Kurian")
            };

            Race.DataSource = raceKarus;
            Race.DisplayMember = "Value";
            Race.ValueMember = "Key";
            Race.SelectedValue = byte.MaxValue;

            var classkarus = new List<KeyValuePair<ushort, string>>
            {
                new KeyValuePair<ushort, string>(ushort.MaxValue, "Select class"),
                new KeyValuePair<ushort, string>(101, "Warrior"),
                new KeyValuePair<ushort, string>(102, "Rogue"),
                new KeyValuePair<ushort, string>(103, "Wizard"),
                new KeyValuePair<ushort, string>(104, "Priest"),
                new KeyValuePair<ushort, string>(113, "Kurian")
            };

            Class.DataSource = classkarus;
            Class.DisplayMember = "Value";
            Class.ValueMember = "Key";
            Class.SelectedValue = ushort.MaxValue;
        }
        else
        {
            var raceHuman = new List<KeyValuePair<byte, string>>
            {
                new KeyValuePair<byte, string>(byte.MaxValue, "Select race"),

                new KeyValuePair<byte, string>(11, "Barbarian"),
                new KeyValuePair<byte, string>(12, "Man"),
                new KeyValuePair<byte, string>(13, "Woman"),
                new KeyValuePair<byte, string>(14, "Porutu")
            };

            Race.DataSource = raceHuman;
            Race.DisplayMember = "Value";
            Race.ValueMember = "Key";
            Race.SelectedValue = byte.MaxValue;

            var classHuman = new List<KeyValuePair<ushort, string>>
            {
                new KeyValuePair<ushort, string>(ushort.MaxValue, "Select class"),

                new KeyValuePair<ushort, string>(201, "Warrior"),
                new KeyValuePair<ushort, string>(202, "Rogue"),
                new KeyValuePair<ushort, string>(203, "Wizard"),
                new KeyValuePair<ushort, string>(204, "Priest"),
                new KeyValuePair<ushort, string>(213, "Porutu")
            };

            Class.DataSource = classHuman;
            Class.DisplayMember = "Value";
            Class.ValueMember = "Key";
            Class.SelectedValue = ushort.MaxValue;
        }

    }

    public bool NewCharacterValid(Lobby lobby)
    {
        if (lobby.Slot > 3 || lobby.Slot < 0)
            return false;

        //if (!Regex.Match(lobby.Name, "^[a-zA-Z0-9]{3,20}$", RegexOptions.IgnoreCase).Success)
        //    return false;

        return true;
    }

    private void CreateButton_Click(object sender, EventArgs e)
    {
        if ((byte)Race.SelectedValue == byte.MaxValue || (ushort)Class.SelectedValue == ushort.MaxValue || Nick.Text == "") return;

        var account = ClientHandler.AccountList.FirstOrDefault(x => x.Id == AccountId)!;

        if (account == null) return;

        var characterData = JsonSerializer.Deserialize<List<Lobby>>(account.CharacterData)!;

        if (characterData.Count == 0) return;

        var character = characterData.FirstOrDefault(x => x.Name == "")!;

        var existCharacterCount = characterData.FindAll(x => x.Name != "").Count();

        if (character != null)
        {
            character.Name = Nick.Text;

            character.Race = (byte)Race.SelectedValue;
            character.Class = (ushort)Class.SelectedValue;

            character.Face = 4;
            character.Hair = 14;

            character.R = 44;
            character.G = 50;
            character.B = 25;

            if(NewCharacterValid(character) == false)
            {
                MessageBox.Show("Check the entered character information is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            account.Character = character.Name;

            account.CharacterData = JsonSerializer.Serialize(characterData);

            SQLiteHandler.Update(account);
            ClientHandler.AccountList.ResetBindings();

            var client = ClientHandler.ClientList.FirstOrDefault(x => x != null && x.Account.Id == account.Id);

            if(client != null && existCharacterCount == 0)
            {
                client.Session.SendAsync(MessageBuilder.MsgSend_SelectNation(account.NationId)).ConfigureAwait(false);
                client.Session.SendAsync(MessageBuilder.MsgSend_NewCharacterCreate(character)).ConfigureAwait(false);
                client.Session.SendAsync(MessageBuilder.MsgSend_SelectedCharacter(account.Login, account.Character)).ConfigureAwait(false);
            }

            Nick.Text = "";

            MessageBox.Show($"Character {character.Name} created", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
