using System;
using Capstone.Classes;

namespace Capstone
{
    //Creating a Vending Machine class, Item class, and Balance class
    //Finsish one section of the PDF at a time
    //Inventory updates need to be read in and added from their individual files
    //items read in from the file should be sorted individually as a objects with their assigned properties
    //If item is in the file when the machine starts it is filled with 5 of them
    class Program
    {
        private const string MAIN_MENU_OPTION_DISPLAY_ITEMS = "Display Vending Machine Items";
        private const string MAIN_MENU_OPTION_PURCHASE = "Purchase";
        private const string MAIN_MENU_OPTION_EXIT = "Exit";
        private const string MAIN_MENU_OPTION_SALES_REPORT = "Sales Report";
        private readonly string[] MAIN_MENU_OPTIONS = { MAIN_MENU_OPTION_DISPLAY_ITEMS, MAIN_MENU_OPTION_PURCHASE, MAIN_MENU_OPTION_EXIT, MAIN_MENU_OPTION_SALES_REPORT }; //const has to be known at compile time, the array initializer is not const in C#

        private const string PURCHASE_MENU_OPTION_FEED_MONEY = "Feed Money";
        private const string PURCHASE_MENU_OPTION_SELECT_PRODUCT = "Select product";
        private const string PURCHASE_MENU_OPTION_FINISH_TRANSACTION = "Finish transaction";
        private readonly string[] PURCHASE_MENU_OPTIONS = { PURCHASE_MENU_OPTION_FEED_MONEY, PURCHASE_MENU_OPTION_SELECT_PRODUCT, PURCHASE_MENU_OPTION_FINISH_TRANSACTION };

        private readonly IBasicUserInterface ui = new MenuDrivenCLI();

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        private void Run()
        {

            // create vending machine and vending machine customer objects
            VendingMachine myVendingMachine = new VendingMachine();
            myVendingMachine.Owner = "Umbrella Corp";
            myVendingMachine.Model = "Vendo-Matic 600";

            VendingMachineCustomer myVendingMachineCustomer = new VendingMachineCustomer();

            myVendingMachine.TurnVendingMachineOn();    //turn vending machine on
            myVendingMachine.FillInventory();           //read inventory from txt file
            myVendingMachine.SetInventoryQuantities();  //assign all items in machine qty = 5

            while (myVendingMachine.IsOn)               // only show menus if vending machine is on...
            {
                String mainMenuSelection = (string)ui.PromptForSelection(MAIN_MENU_OPTIONS);  // main menu prompt for selection
                if (mainMenuSelection == MAIN_MENU_OPTION_DISPLAY_ITEMS)
                {
                    myVendingMachine.PrintCurrentInventory();
                }
                if (mainMenuSelection == MAIN_MENU_OPTION_PURCHASE)
                {
                    String purchaseMenuSelection = (string)ui.PromptForSelection(PURCHASE_MENU_OPTIONS);
                    if (purchaseMenuSelection == PURCHASE_MENU_OPTION_FEED_MONEY)
                    {
                        // prompt for money
                        Console.WriteLine("Please enter money in one dollar increments:");
                        string amountDeposited = Console.ReadLine();
                        if (int.Parse(amountDeposited) > 0)
                        {
                            myVendingMachineCustomer.DepositMoney(int.Parse(amountDeposited));
                            // logs in audit file when a customer has deposited money
                            // logs date, time, amount fed, current customer balance
                            myVendingMachine.PrintToAuditFile(DateTime.Now.ToString() + " FEED MONEY: $" + +(decimal)int.Parse(amountDeposited) + " $" + myVendingMachineCustomer.Balance);

                        }
                        else
                        {
                            Console.WriteLine("Negative or zero deposits not allowed.");
                        }
                    }
                    if (purchaseMenuSelection == PURCHASE_MENU_OPTION_SELECT_PRODUCT)
                    {
                        if (myVendingMachineCustomer.HasBalance())
                        {
                            Console.WriteLine("Please select a product by entering the slot number:");
                            string selectedItem = Console.ReadLine();
                            myVendingMachine.PurchaseItem(selectedItem, myVendingMachineCustomer);
                        }
                        else
                        {
                            Console.WriteLine("Must deposit money before making a selection!");
                        }
                    }
                    if (purchaseMenuSelection == PURCHASE_MENU_OPTION_FINISH_TRANSACTION)
                    {
                        //dispense change
                        Console.WriteLine(myVendingMachine.DispenseChange(myVendingMachineCustomer));
                    }
                }
                if (mainMenuSelection == MAIN_MENU_OPTION_EXIT)
                {
                    break;
                }
                if (mainMenuSelection == MAIN_MENU_OPTION_SALES_REPORT)
                {
                    //logic for sales reports goes here...
                    // SALES REPORT FUNCTIONS
                    myVendingMachine.PrintSalesReport();
                    myVendingMachine.TurnVendingMachineOff();
                }
            }
            myVendingMachine.IsOn = false;
        }
    }

}

