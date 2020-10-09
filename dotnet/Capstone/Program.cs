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
            ourMachine.FillInventory();

            ourMachine.IsOn = true;
            bool isPurchasing = true;
            bool enoughFunds = true;
            double amountEntered = 0;

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
                    while (enoughFunds==false && isPurchasing)
                    {
                        Console.WriteLine("Please insert money.");
                        amountEntered = double.Parse(Console.ReadLine()) + amountEntered;
                        while (amountEntered <= 0)
                        {
                            if (amountEntered > 0)
                            {

                                break;
                            }
                            else
                            {
                                Console.WriteLine("Please insert money.");
                                amountEntered = double.Parse(Console.ReadLine());
                            }

                        }
                        Console.WriteLine("Balance: " + amountEntered);
                        while (isPurchasing && enoughFunds)
                        {
                            Console.WriteLine("Please select an item");
                            string selectedItem = Console.ReadLine();
                            if (amountEntered > ourMachine.SelectItem(selectedItem))
                            {

                            }
                            else
                            {
                                enoughFunds = false;
                                break;
                            }
                            amountEntered -= ourMachine.SelectItem(selectedItem);
                            Console.WriteLine("Balance: " + amountEntered);
                       


                            Console.Write("Do you want to continue purchasing?(Y/N)");
                            string purchasingAns = Console.ReadLine();
                            if (purchasingAns == "Y" || purchasingAns == "y")
                            {
                                ourMachine.RemoveItemQuantity(selectedItem);
                                Console.WriteLine(ourMachine.DispenseSound(selectedItem));
                            }
                            else if (purchasingAns == "N" || purchasingAns == "n")
                            {
                                ourMachine.RemoveItemQuantity(selectedItem);
                                Console.WriteLine(ourMachine.DispenseSound(selectedItem));
                                isPurchasing = false;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid response, purchasing ended.");
                                ourMachine.RemoveItemQuantity(selectedItem);
                                Console.WriteLine(ourMachine.DispenseSound(selectedItem));
                                isPurchasing = false;
                                break;
                            }


                        }
                    }
                    

                    
                    //Change to no longer allow a negative balance to occur
                    //Change to when invalid input for location slot sends back to purchasing screen

                    

                    
                }

                }
        }


    }
}
