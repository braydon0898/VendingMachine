using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
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
	    private readonly string[] MAIN_MENU_OPTIONS = { MAIN_MENU_OPTION_DISPLAY_ITEMS, MAIN_MENU_OPTION_PURCHASE }; //const has to be known at compile time, the array initializer is not const in C#

        private readonly IBasicUserInterface ui = new MenuDrivenCLI();

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        public void Run()
        {
            VendingMachine ourMachine = new VendingMachine();
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
                    ourMachine.Inventory.Add(item);

                }
            }


                while (ourMachine.IsOn) //rn this is an infinite loop. You'll need a 'finished' option and then you'll break after that option is selected
                {
                    String selection = (string)ui.PromptForSelection(MAIN_MENU_OPTIONS);
                    if (selection == MAIN_MENU_OPTION_DISPLAY_ITEMS)
                    {
                        ourMachine.PrintInventory(); 
                        //display the vending machine items (probably should call a method to do this)
                    }
                    else if (selection == MAIN_MENU_OPTION_PURCHASE)
                    {
                    Console.WriteLine("Please insert money.");
                    double amountEntered = double.Parse(Console.ReadLine());
                    Console.WriteLine("Please select an item");
                    string selectedItem = Console.ReadLine();
                    
                        //do the purchase (probably should call a method to do this too)
                    }

                }
        }


    }
}
