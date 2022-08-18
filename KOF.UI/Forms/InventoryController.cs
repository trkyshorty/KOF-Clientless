using System.Diagnostics;
using System.ComponentModel;
using KOF.Core.Models;
using KOF.Core;
using KOF.Core.Handlers;
using System.Reflection;
using KOF.Database.Models;
using KOF.Core.Enums;
using KOF.Database;

namespace KOF.UI.Forms;

public partial class InventoryController : Form
{
    public Client Client { get; set; } = default!;
    private Character Character { get { return Client.Character; } }
    private CharacterHandler CharacterHandler { get { return Client.CharacterHandler; } }
    private BindingList<Inventory> InventoryItemList { get; set; } = new();

    public InventoryController(Client client)
    {
        Client = client;

        InitializeComponent();

        typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.SetProperty, null,
        ItemListDataGridView, new object[] { true });
    }

    private void Inventory_Load(object sender, EventArgs e)
    {
        InventoryItemList = new BindingList<Inventory>(Character.Inventory);

        ItemListDataGridView.DataSource = InventoryItemList;

        InitializeInventoryRowColor();
    }

    private void InitializeInventoryRowColor()
    {
        foreach (DataGridViewRow row in ItemListDataGridView.Rows)
        {
            var inventory = (Inventory)row.DataBoundItem;

            switch (inventory.SupplyFlag)
            {
                case (byte)SupplyFlagType.FLAG_NONE:
                        row.DefaultCellStyle.BackColor = Color.White;
                    break;

                case (byte)SupplyFlagType.FLAG_SELL:
                        row.DefaultCellStyle.BackColor = Color.ForestGreen;
                    break;

                case (byte)SupplyFlagType.FLAG_DELETE:
                        row.DefaultCellStyle.BackColor = Color.Red;
                    break;

                case (byte)SupplyFlagType.FLAG_INN_STORE:
                        row.DefaultCellStyle.BackColor = Color.Blue;
                    break;
            }
        }
    }

    private void InventoryTimer_Tick(object sender, EventArgs e)
    {
        try
        {
            InitializeInventoryRowColor();

            ItemListDataGridView.Refresh();

            NoahTextLabel.Text = $"Noah : {Character.Gold.ToString("N0")}";
            WeightLabel.Text = $"Weight : {Character.Weight.ToString("F1")} / {Character.MaxWeight.ToString("F1")}";
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in ItemListDataGridView.SelectedRows)
        {
            byte pos = (byte)row.Cells[0].Value;

            if(pos < 14)
                CharacterHandler.RemoveItem(1, pos, (uint)row.Cells[1].Value);
            else
                CharacterHandler.RemoveItem(2, (byte)(pos - 14), (uint)row.Cells[1].Value);
        }
    }

    private void UseButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in ItemListDataGridView.SelectedRows)
            CharacterHandler.UseItem((uint)row.Cells[1].Value);
    }

    private void EquipButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in ItemListDataGridView.SelectedRows)
            CharacterHandler.EquipItem((uint)row.Cells[1].Value, (byte)row.Cells[0].Value);
    }

    private void AutoDeleteButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in ItemListDataGridView.SelectedRows)
        {
            var inventory = (Inventory)row.DataBoundItem;

            if (inventory.ItemID == 0)
                continue;

            var supplyFlag = SQLiteHandler.Table<SupplyFlag>().SingleOrDefault(x => x.ItemId == inventory.ItemID);

            if (supplyFlag == null)
            {
                supplyFlag = new SupplyFlag()
                {
                    Flag = (int)SupplyFlagType.FLAG_DELETE,
                    ItemId = (int)inventory.ItemID
                };

                supplyFlag.Id = SQLiteHandler.Insert(supplyFlag);
            }
            else
            {
                supplyFlag.Flag = (byte)SupplyFlagType.FLAG_DELETE;
                SQLiteHandler.Update(supplyFlag);
            }

            Character.Inventory[inventory.Pos].SupplyFlag = (byte)SupplyFlagType.FLAG_DELETE;
        }
    }

    private void AutoInnStore_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in ItemListDataGridView.SelectedRows)
        {
            var inventory = (Inventory)row.DataBoundItem;

            if (inventory.ItemID == 0)
                continue;

            var supplyFlag = SQLiteHandler.Table<SupplyFlag>().SingleOrDefault(x => x.ItemId == inventory.ItemID);

            if (supplyFlag == null)
            {
                supplyFlag = new SupplyFlag()
                {
                    Flag = (int)SupplyFlagType.FLAG_INN_STORE,
                    ItemId = (int)inventory.ItemID
                };

                supplyFlag.Id = SQLiteHandler.Insert(supplyFlag);
            }
            else
            {
                supplyFlag.Flag = (byte)SupplyFlagType.FLAG_INN_STORE;
                SQLiteHandler.Update(supplyFlag);
            }

            Character.Inventory[inventory.Pos].SupplyFlag = (byte)SupplyFlagType.FLAG_INN_STORE;
        }

    }

    private void AutoSellButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in ItemListDataGridView.SelectedRows)
        {
            var inventory = (Inventory)row.DataBoundItem;

            if (inventory.ItemID == 0)
                continue;

            var supplyFlag = SQLiteHandler.Table<SupplyFlag>().SingleOrDefault(x => x.ItemId == inventory.ItemID);

            if (supplyFlag == null)
            {
                supplyFlag = new SupplyFlag()
                {
                    Flag = (int)SupplyFlagType.FLAG_SELL,
                    ItemId = (int)inventory.ItemID
                };

                supplyFlag.Id = SQLiteHandler.Insert(supplyFlag);
            }
            else
            {
                supplyFlag.Flag = (byte)SupplyFlagType.FLAG_SELL;
                SQLiteHandler.Update(supplyFlag);
            }

            Character.Inventory[inventory.Pos].SupplyFlag = (byte)SupplyFlagType.FLAG_SELL;
        }
    }

    private void ClearFlagButton_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in ItemListDataGridView.SelectedRows)
        {
            var inventory = (Inventory)row.DataBoundItem;

            if (inventory.ItemID == 0)
                continue;

            var supplyFlag = SQLiteHandler.Table<SupplyFlag>().SingleOrDefault(x => x.ItemId == inventory.ItemID);

            if (supplyFlag != null)
                supplyFlag.Id = SQLiteHandler.Delete(supplyFlag);

            Character.Inventory[inventory.Pos].SupplyFlag = (byte)SupplyFlagType.FLAG_NONE;
        }
    }
}
