using KOF.Core.Handlers;
using KOF.Core.Models;
using KOF.Core;
using System.ComponentModel;
using System.Reflection;
using KOF.Data;
using KOF.Core.Enums;
using System.Diagnostics;

namespace KOF.UI.Forms
{
    public partial class ShopWindow : Form
    {
        public Client Client { get; set; } = default!;
        private Character Character { get { return Client.Character; } }
        private CharacterHandler CharacterHandler { get { return Client.CharacterHandler; } }
        private BindingList<ShopItem> ShopItemList { get; set; } = new();
        private BindingList<Inventory> InventoryItemList { get; set; } = new();

        public ShopWindow(Client client)
        {
            Client = client;

            InitializeComponent();

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.SetProperty, null,
            ShopItemListDataGridView, new object[] { true });

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.SetProperty, null,
            InventoryItemListDataGridView, new object[] { true });
        }

        private void UITimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (CharacterHandler.GetGameState() != GameState.GAME_STATE_INGAME) return;

                ShopItemListDataGridView.Refresh();
                InventoryItemListDataGridView.Refresh();
            
                ShopNoahTextLabel.Text = $"Noah : {Character.Gold.ToString("N0")}";
                ShopWeightLabel.Text = $"Weight : {Character.Weight.ToString("F1")} / {Character.MaxWeight.ToString("F1")}";

                InventoryNoahTextLabel.Text = ShopNoahTextLabel.Text;
                InventoryWeightLabel.Text = ShopWeightLabel.Text;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void ShopWindow_Load(object sender, EventArgs e)
        {
            InventoryItemList = new BindingList<Inventory>(Character.Inventory);
            InventoryItemListDataGridView.DataSource = InventoryItemList;

            var shopItemList = TableHandler.GetItemSellList().FindAll(x => x.SellingGroup == Character.NpcEventGroup);

            if (shopItemList == null) return;
            
            byte itemPosition = 0;
            
            shopItemList.ForEach(x =>
            {
                x.ItemList.ForEach(y =>
                {
                    var shopItem = new ShopItem()
                    {
                        Pos = itemPosition,
                        ItemID = y,
                    };

                    var itemTable = TableHandler.GetItemById(y);

                    if (itemTable != null)
                    {
                        shopItem.Name = itemTable.Name;
                        shopItem.Price = itemTable.SellPrice; //TODO: itemTable.SellPrice * Taxrate
                    }

                    ShopItemList.Add(shopItem);

                    itemPosition++;
                });
            });

            ShopItemListDataGridView.DataSource = ShopItemList;
        }

        private void BuyItemButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ShopItemListDataGridView.SelectedRows)
            {
                var shopItem = (ShopItem)row.DataBoundItem;

                if (shopItem == null) continue;

                CharacterHandler.ItemBuy(Character.NpcEventId, Character.NpcEventGroup, shopItem.ItemID, shopItem.Count);
            }

            CharacterHandler.StoreClose();
        }

        private void SellItemButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in InventoryItemListDataGridView.SelectedRows)
            {
                var inventoryItem = (Inventory)row.DataBoundItem;

                if (inventoryItem == null) continue;

                CharacterHandler.ItemSell(Character.NpcEventId, Character.NpcEventGroup, (int)inventoryItem.ItemID, (short)inventoryItem.Count);
            }

            CharacterHandler.StoreClose();
        }
    }
}


