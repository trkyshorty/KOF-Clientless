using KOF.Core;
using KOF.Core.Enums;
using KOF.Core.Handlers;
using System.Diagnostics;
using KOF.Core.Models;

namespace KOF.UI.Forms;

public partial class CharacterInfo : Form
{
    public Client Client { get; set; } = default!;
    private Character Character { get { return Client.Character; } }
    private CharacterHandler CharacterHandler { get { return Client.CharacterHandler; } }

    public CharacterInfo(Client client)
    {
        Client = client;

        InitializeComponent();
    }

    private void UpdateTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            LevelTextLabel.Text = $"Level : {Character.Level}";

            if(Character.NationId == 1)
                NationTextLabel.Text = $"Nation : Karus";
            else
                NationTextLabel.Text = $"Nation : El Morad";

            ExperienceTextLabel.Text = $"Experience : {Character.Experience} / {Character.MaxExperience}";
            NationalPointTextLabel.Text = $"National Point : {Character.Loyalty} / {Character.LoyaltyMonthly}";
            AttackTextLabel.Text = $"Attack : {Character.TotalHit}";
            DefenseTextLabel.Text = $"Defense : {Character.TotalAc}";
            MannerTextLabel.Text = $"Manner : 0";
            StatPointTextLabel.Text = $"Stat Point : {Character.Points}";

            if(Character.StatsItemBonuses[0] > 0)
                StrTextLabel.Text = $"STR : {Character.Stats[0]} (+{Character.StatsItemBonuses[0]})";
            else
                StrTextLabel.Text = $"STR : {Character.Stats[0]}";

            if (Character.StatsItemBonuses[1] > 0)
                HpTextLabel.Text = $"HP : {Character.Stats[1]} (+{Character.StatsItemBonuses[1]})";
            else
                HpTextLabel.Text = $"HP : {Character.Stats[1]}";

            if (Character.StatsItemBonuses[2] > 0)
                DexTextLabel.Text = $"DEX : {Character.Stats[2]} (+{Character.StatsItemBonuses[2]})";
            else
                DexTextLabel.Text = $"DEX : {Character.Stats[2]}";

            if (Character.StatsItemBonuses[3] > 0)
                MpTextLabel.Text = $"INT : {Character.Stats[3]} (+{Character.StatsItemBonuses[3]})";
            else
                MpTextLabel.Text = $"INT : {Character.Stats[3]}";

            if (Character.StatsItemBonuses[4] > 0)
                IntTextLabel.Text = $"MP : {Character.Stats[4]} (+{Character.StatsItemBonuses[4]})";
            else
                IntTextLabel.Text = $"MP : {Character.Stats[4]}";

            FireTextLabel.Text = $"Fire : {Character.FireR}";
            IceTextLabel.Text = $"Ice : {Character.ColdR}";
            LightningTextLabel.Text = $"Lightning : {Character.LightningR}";

            MagicTextLabel.Text = $"Magic : {Character.MagicR}";
            DarkTextLabel.Text = $"Dark : {Character.DiseaseR}";
            PoisonTextLabel.Text = $"Poison : {Character.PoisonR}";

            if(Character.Points == 0)
            {
                StrUpButton.Enabled = false;
                HpUpButton.Enabled = false;
                DexUpButton.Enabled = false;
                IntUpButton.Enabled = false;
                MpUpButton.Enabled = false;
            }
            else
            {
                StrUpButton.Enabled = true;
                HpUpButton.Enabled = true;
                DexUpButton.Enabled = true;
                IntUpButton.Enabled = true;
                MpUpButton.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void StrUpButton_Click(object sender, EventArgs e)
    {
        CharacterHandler.AbilityPointChange((byte)StatType.STAT_STR, 1);
    }

    private void HpUpButton_Click(object sender, EventArgs e)
    {
        CharacterHandler.AbilityPointChange((byte)StatType.STAT_HP, 1);
    }

    private void DexUpButton_Click(object sender, EventArgs e)
    {
        CharacterHandler.AbilityPointChange((byte)StatType.STAT_DEX, 1);
    }

    private void MpUpButton_Click(object sender, EventArgs e)
    {
        // Client side bug
        CharacterHandler.AbilityPointChange((byte)StatType.STAT_INT, 1); 
    }

    private void IntUpButton_Click(object sender, EventArgs e)
    {
        // Client side bug
        CharacterHandler.AbilityPointChange((byte)StatType.STAT_MP, 1);
    }

    private void CharacterInfo_Load(object sender, EventArgs e)
    {

    }
}
