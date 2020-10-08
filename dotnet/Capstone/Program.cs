using System;
using System.Runtime.ConstrainedExecution;

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
            while(true) //rn this is an infinite loop. You'll need a 'finished' option and then you'll break after that option is selected
            {
                String selection = (string)ui.PromptForSelection(MAIN_MENU_OPTIONS);
                if (selection==MAIN_MENU_OPTION_DISPLAY_ITEMS)
                {
                    //display the vending machine items (probably should call a method to do this)
                }
                else if (selection==MAIN_MENU_OPTION_PURCHASE)
                {
                    //do the purchase (probably should call a method to do this too)
                }

            }
        }


    }
}
