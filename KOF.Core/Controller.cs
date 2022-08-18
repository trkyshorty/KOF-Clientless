using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KOF.Database;
using KOF.Database.Models;
using KOF.Core.Handlers;
using System.Text.Json;
using KOF.Core.Models;

namespace KOF.Core
{
    public class Controller
    {
        private Account? Account { get; set; }
        private List<Control> ControlList { get; set; } = new List<Control>();

        public Controller(Account? account = null)
        {
            Account = account;

            if (account != null)
            {
                ControlList = SQLiteHandler.Table<Control>().Where(x => x.Form == account.Character).ToList();

                var supplyList = JsonSerializer.Deserialize<List<Supply>>(GetControl("SupplyList", "[]"))!;

                if (supplyList.Count == 0)
                {
                    supplyList = new List<Supply>
                    {
                        { new Supply(false, 253000, 389010000, "Holy water", 25) },
                        { new Supply(false, 253000, 389011000, "Water of life", 25) },
                        { new Supply(false, 253000, 389012000, "Water of love", 25) },
                        { new Supply(false, 253000, 389013000, "Water of grace", 25) },
                        { new Supply(false, 253000, 389014000, "Water of favors", 25) },
                        { new Supply(false, 253000, 389016000, "Potion of Spirit", 50) },
                        { new Supply(false, 253000, 389017000, "Potion of Intelligence", 50) },
                        { new Supply(false, 253000, 389018000, "Potion of Sagacity", 50) },
                        { new Supply(false, 253000, 389019000, "Potion of Wisdom", 50) },
                        { new Supply(false, 253000, 389020000, "Potion of Soul", 50) },
                        { new Supply(false, 255000, 391010000, "Arrow", 9998) },
                        { new Supply(false, 255000, 370004000, "Blood Of Wolf", 25) },
                        { new Supply(false, 255000, 379091000, "Transformation Gem", 50) },
                        { new Supply(false, 255000, 389026000, "Prayer of god's power", 25) },
                    };

                    SetControl("SupplyList", JsonSerializer.Serialize(supplyList));
                }
            }
            else
                ControlList = SQLiteHandler.Table<Control>().Where(x => x.Form == "KOF").ToList();
        }

        public List<Control> GetControlList()
        {
            return ControlList;
        }

        public string GetControl(string name, string defaultValue = "")
        {
            var control = ControlList.Where(x => x.Name == name)?.FirstOrDefault();

            if (control == null)
            {
                if (defaultValue != "")
                    SetControl(name, defaultValue);

                return defaultValue;
            }

            return control.Value;
        }

        public bool GetControl(string name, bool defaultValue = false)
        {
            return Convert.ToBoolean(GetControl(name, defaultValue.ToString()));
        }

        public decimal GetControl(string name, decimal defaultValue = 0)
        {
            return Convert.ToDecimal(GetControl(name, defaultValue.ToString()));
        }

        public void SetControl(string name, bool value)
        {
            SetControl(name, value.ToString());
        }

        public void SetControl(string name, decimal value)
        {
            SetControl(name, value.ToString());
        }

        public void SetControl(string name, string value)
        {
            var control = ControlList.FirstOrDefault(x => x.Name == name);

            if (control == null)
            {
                control = new Control();

                if (Account != null)
                {
                    control.Form = Account.Character;
                    control.Server = Account.Server;
                }
                else
                    control.Form = "KOF";

                control.Name = name;
                control.Value = value;

                control.Id = (int)SQLiteHandler.Insert(control);

                ControlList.Add(control);
            }
            else
            {
                control.Value = value;
                SQLiteHandler.Update(control);
            }
        }
    }
}
