using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    class VendingMachineCustomer
    {
        public decimal Balance { get; set; }
        public bool IsPurchasing { get; set; }
        public bool HasSelected { get; set; }
        public bool HasBalance()
        {
            if (Balance > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void PrintBalance()
        {
            Console.WriteLine("Your balance is: $" + Balance);
        }

        public void DepositMoney(int amountDeposited)
        {
            // adds to customer balance
            Balance += amountDeposited;
            PrintBalance();
        }
    }
}
