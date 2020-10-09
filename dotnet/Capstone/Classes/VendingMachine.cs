using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
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
        public bool HasBalance()
        {
            if (CustomerBalance > 0)
            {
                return true;
            }
            return false;   
        }

        public string DispenseSound(string selectedItem)
        {
            string sound = "";
            foreach (Item i in Inventory)
            {
                if (i.LocationSlot == selectedItem)
                {
                    if (i.Category == "Chip")
                    {
                        sound = "Crunch Crunch, Yum";
                    }
                    else if (i.Category == "Candy")
                    {
                        sound =  "Munch Munch, Yum";
                    }
                    else if (i.Category == "Drink")
                    {
                        sound =  "Glug Glug, Yum";
                    }
                    else if (i.Category == "Gum")
                    {
                        sound = "Chew Chew, Yum";
                    }
                }
                
           }
            return sound;
        }

        public void FillInventory()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string inventoryFile = "Inventory.txt";
            string fullInventoryPath = Path.Combine(currentDirectory, @"..\..\..\..\..\Example Files", inventoryFile);


            using (StreamReader sr = new StreamReader(fullInventoryPath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] lineArray = line.Split("|");
                    Item item = new Item();
                    item.Category = lineArray[3];
                    item.Price = Double.Parse(lineArray[2]);
                    item.LocationSlot = lineArray[0];
                    item.Name = lineArray[1];
                    Inventory.Add(item);

                }
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
            if(CustomerBalance > item.Price)
            {
                CustomerBalance -= item.Price;
            }
            else
            {
                Console.WriteLine("Not enough money");
            }
            
        }

        public double SelectItem(string item)
        {
            double price = 0;
            foreach(Item i in Inventory)
            {
                if (i.LocationSlot == item)
                {
                    if(i.Quantity > 0)
                    {
                        price = i.Price;
                        
                    }
                    else
                    {
                        Console.WriteLine("Out of Stock");
                    }
                }

            }return price;
            //Create unit test to verify that this method returns item balance if item exists!


        }

        public void RemoveItemQuantity(string item)
        {
            foreach (Item i in Inventory)
            {
                if (i.LocationSlot == item)
                {
                    i.Quantity--;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
