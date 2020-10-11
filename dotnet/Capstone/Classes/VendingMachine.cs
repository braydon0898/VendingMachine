using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace Capstone.Classes
{
    class VendingMachine
    {
        public string Owner { get; set; }
        public string Model { get; set; }
        private List<VendingMachineItem> CurrentInventory { get; set; } = new List<VendingMachineItem>();
        private List<VendingMachineItem> ItemsSoldToday { get; set; } = new List<VendingMachineItem>();
        public bool IsOn { get; set; }
        private decimal DailySales { get; set; }

        //METHODS
        public void TurnVendingMachineOn()
        {
            IsOn = true;
        }

        public void TurnVendingMachineOff()
        {
            IsOn = false;
        }

        public void FillInventory()
        {
            // read txt file
            // set object properties from read-in data
            try
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
                        VendingMachineItem vmi = new VendingMachineItem
                        {
                            Category = lineArray[3],
                            Price = Decimal.Parse(lineArray[2]),
                            SlotLocation = lineArray[0],
                            Name = lineArray[1]
                        };
                        CurrentInventory.Add(vmi);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("File IO error.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Application error.");
            }
        }

        public void SetInventoryQuantities()
        {
            foreach (VendingMachineItem vmi in CurrentInventory)
            {
                vmi.Quantity = 5;
            }
        }

        public void PrintCurrentInventory()
        {
            // logic to print formatted inventory
            // item displays as sold out if qty less than 1
            if (CurrentInventory.Count > 0)
            {
                Console.WriteLine("*******************************");
                Console.WriteLine("***VENDING MACHINE INVENTORY***");
                Console.WriteLine("*******************************");
                //Console.WriteLine("0----5---10---15---20---25---30---35---40---45---50---55---60");
                Console.Write(String.Format("{0, -35}", "Item Name"));
                Console.Write(String.Format("{0, 10}", "Item Slot"));
                Console.Write(String.Format("{0, 30}", "Item Price\n"));

                foreach (VendingMachineItem vmi in CurrentInventory)
                {
                    if (vmi.Quantity < 1)
                    {
                        Console.Write(String.Format("{0, -35}", vmi.Name + " *SOLD OUT*"));
                    }
                    else
                    {
                        Console.Write(String.Format("{0, -35}", vmi.Name));
                    }

                    Console.Write(String.Format("{0, 10}", vmi.SlotLocation));
                    Console.Write(String.Format("{0, 30:C}", vmi.Price));
                    Console.Write("\n");
                }
            }
            else
            {
                Console.WriteLine("***VENDING MACHINE EMPTY***");
            }

        }

        public void PurchaseItem(string slotSelected, VendingMachineCustomer myCustomer)
        {
            string message = "";
            foreach (VendingMachineItem vmi in CurrentInventory)
            {
                if (vmi.SlotLocation == slotSelected)
                {
                    if (vmi.Quantity > 0)
                    {
                        if (vmi.Price <= myCustomer.Balance)
                        {
                            myCustomer.Balance -= vmi.Price;    // deduct from customer balance
                            vmi.Quantity--;                     // deduct from inventory qty
                            DailySales += vmi.Price;            // add to machine daily sales
                            ItemsSoldToday.Add(vmi);
                            // logs in audit file when an item has been purchased
                            // logs date, time, item name, item slot, item price, customer balance
                            PrintToAuditFile(DateTime.Now.ToString() + " " + vmi.Name + " " + vmi.SlotLocation + " $" + vmi.Price + " $" + myCustomer.Balance);

                            Console.WriteLine(VendItem(vmi));
                            message = "Item sold.";
                            break;
                        }
                        else
                        {
                            message = "***Insufficient funds.***";
                            break;
                        }
                    }
                    else
                    {
                        message = "***Item sold out.***";
                        break;
                    }
                }
                else
                {
                    message = "***NOT A VALID SELECTION***";
                }
            }
            Console.WriteLine(message);
        }

        public void PrintSalesReport()
        {
            // returns the list of items sold today and the daily sales $
            // logic to print formatted sales items

            if (DailySales > 0)
            {
                string currentDirectory = Environment.CurrentDirectory;
                string salesFile = "SalesReport_" + DateTime.Today.ToString("M-dd-yyyy") + ".txt";
                string fullSalesFilePath = Path.Combine(currentDirectory, @"..\..\..\..\..\Example Files", salesFile);

                try
                {
                    using (StreamWriter sw = new StreamWriter(fullSalesFilePath, true))
                    {
                        foreach (VendingMachineItem vmi in ItemsSoldToday)
                        {
                            sw.WriteLine(vmi.Name + "|1");
                        }
                        sw.WriteLine("\n**TOTAL SALES** $" + DailySales);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("File IO error.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Application error.");
                }
            }
            else
            {
                Console.WriteLine("***NO SALES TODAY***");
            }
        }

        private string VendItem(VendingMachineItem vmi)
        {
            // makes the sound
            // after a purchase set isPurchasing = true

            string sound = "";
            switch (vmi.Category)
            {
                case "Chip":
                    sound = "Crunch Crunch, Yum"; ;
                    break;
                case "Candy":
                    sound = "Munch Munch, Yum";
                    break;
                case "Drink":
                    sound = "Glug Glug, Yum";
                    break;
                case "Gum":
                    sound = "Chew Chew, Yum";
                    break;
                default:
                    sound = "***Unknown item category***";
                    break;
            }
            return sound;
        }

        public string DispenseChange(VendingMachineCustomer vmi)
        {
            Console.WriteLine("Balance: " + vmi.Balance);
            string giveChange = "Your change is ";   //6.35

            if (vmi.Balance > 0)
            {
                int numQuarters = (int)(vmi.Balance / 0.25M);   //25
                vmi.Balance -= (decimal)(numQuarters * 0.25M);
                int numDimes = (int)(vmi.Balance / 0.10M);
                vmi.Balance -= (decimal)(numDimes * 0.10M);
                int numNickels = (int)(vmi.Balance / 0.05M);
                vmi.Balance -= (decimal)(numNickels * 0.05M);

                if (numQuarters != 0)
                {
                    giveChange += numQuarters + " quarter(s) ";
                }
                if (numDimes != 0)
                {
                    giveChange += numDimes + " dime(s) ";
                }
                if (numNickels != 0)
                {
                    giveChange += numNickels + " nickel(s)";
                }
                // log to audit file when a customer finishes a transaction
                // logs date, time, amount of change, customer balance (should be zero)
                PrintToAuditFile(DateTime.Now + " GIVE CHANGE: $" + ((numQuarters * 0.25M) + (numDimes * 0.10M) + (numNickels * 0.05M)) + " $" +vmi.Balance);
            }
            else
            {
                giveChange += "0";
            }

            return giveChange;
        }

        public void PrintToAuditFile(string auditLine)
        {
            // if audit report file doesn't exist
            // creates a file with date in the name to append to
            // otherwise open the existing audit file to append to

            string currentDirectory = Environment.CurrentDirectory;
            string auditFile = "Log_" + DateTime.Today.ToString("M-dd-yyyy") + ".txt";
            string fullAuditFilePath = Path.Combine(currentDirectory, @"..\..\..\..\..\Example Files", auditFile);

            try
            {
                using (StreamWriter sw = new StreamWriter(fullAuditFilePath, true))
                {
                    sw.WriteLine(auditLine);
                }
            }

            catch (IOException e)
            {
                Console.WriteLine("File IO error.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Application error.");
            }
        }
    }
}
