using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    class VendingMachine
    {
        public List<Item> Inventory { get; set; } = new List<Item>();
        public bool IsOn { get; set; }
        public double CustomerBalance
        {
            get
            {
                return 0;
            }
            set
            {

            }
        }

        public void TurnVendingMachineOn()
        {
            IsOn = true;
        }

        public void TurnVendingMachineOff()
        {
            IsOn = false; ;
        }

        public void PrintInventory()
        {
            foreach(Item item in Inventory)
            {
                Console.WriteLine(item.Name + ", " + item.LocationSlot + ", " +  item.Price);
            }
        }

        public void MakePurchase(Item item)
        {
            CustomerBalance -= item.Price;
        }


    }
}
