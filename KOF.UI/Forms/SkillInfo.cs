using System.Diagnostics;
using KOF.Core.Handlers;
using KOF.Core;
using KOF.Core.Models;

namespace KOF.UI.Forms
{
    public partial class SkillInfo : Form
    {
        public Client Client { get; set; } = default!;
        private Character Character { get { return Client.Character; } }
        private CharacterHandler CharacterHandler { get { return Client.CharacterHandler; } }

        public SkillInfo(Client client)
        {
            Client = client;

            InitializeComponent();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                SkillPointLabel.Text = $"Point : {Character.Skills[0]}";

                switch (Character.Job)
                {
                    case "Warrior":
                        {
                            Skill1Label.Text = $"Attack : {Character.Skills[5]}";
                            Skill2Label.Text = $"Defense : {Character.Skills[6]}";
                            Skill3Label.Text = $"Passion / Berserker : {Character.Skills[7]}";
                        }
                        break;
                    case "Rogue":
                        {
                            Skill1Label.Text = $"Archery : {Character.Skills[5]}";
                            Skill2Label.Text = $"Assasinate : {Character.Skills[6]}";
                            Skill3Label.Text = $"Search : {Character.Skills[7]}";
                        }
                        break;
                    case "Mage":
                        {
                            Skill1Label.Text = $"Fire : {Character.Skills[5]}";
                            Skill2Label.Text = $"Glacier : {Character.Skills[6]}";
                            Skill3Label.Text = $"Lightning : {Character.Skills[7]}";
                        }
                        break;
                    case "Priest":
                        {
                            Skill1Label.Text = $"Heal : {Character.Skills[5]}";
                            Skill2Label.Text = $"Aura : {Character.Skills[6]}";
                            Skill3Label.Text = $"Holy : {Character.Skills[7]}";
                        }
                        break;

                    default:
                        break;
                }
            
                MasterLabel.Text = $"Master : {Character.Skills[8]}";

                if (Character.Skills[0] == 0)
                {
                    Skill1UpButton.Enabled = false;
                    Skill2UpButton.Enabled = false;
                    Skill3UpButton.Enabled = false;
                    MasterUpButton.Enabled = false;
                }
                else
                {
                    Skill1UpButton.Enabled = true;
                    Skill2UpButton.Enabled = true;
                    Skill3UpButton.Enabled = true;
                    MasterUpButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void Skill1UpButton_Click(object sender, EventArgs e)
        {
            Character.Skills[0]--;
            Character.Skills[5]++;

            CharacterHandler.SkillPointChange(5);
            CharacterHandler.InitializeSkillList();
        }

        private void Skill2UpButton_Click(object sender, EventArgs e)
        {
            Character.Skills[0]--;
            Character.Skills[6]++;

            CharacterHandler.SkillPointChange(6);
            CharacterHandler.InitializeSkillList();
        }

        private void Skill3UpButton_Click(object sender, EventArgs e)
        {
            Character.Skills[0]--;
            Character.Skills[7]++;

            CharacterHandler.SkillPointChange(7);
            CharacterHandler.InitializeSkillList();
        }

        private void MasterUpButton_Click(object sender, EventArgs e)
        {
            Character.Skills[0]--;
            Character.Skills[8]++;

            CharacterHandler.SkillPointChange(8);
            CharacterHandler.InitializeSkillList();
        }

        private void SkillInfo_Load(object sender, EventArgs e)
        {

        }

        private void Skill1StJobButton_Click(object sender, EventArgs e)
        {
            CharacterHandler.StatChangeReq();
        }
    }
}
