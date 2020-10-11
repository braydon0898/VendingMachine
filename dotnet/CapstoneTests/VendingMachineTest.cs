using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTest
    {
        [TestMethod]
        public void TestSelectItem()
        {
            VendingMachine newMachine = new VendingMachine();


            Item item1 = new Item("Fritos", 0.65, "A4", "Chip");
            Item item2 = new Item("M&Ms", 0.95, "B4", "Candy");
            Item item3 = new Item("Mountain Dew", 1.24, "C4", "Drink");

            newMachine.Inventory.Add(item1);
            newMachine.Inventory.Add(item2);
            newMachine.Inventory.Add(item3);

            double itemPrice = newMachine.SelectItem("C4");
            Assert.AreEqual(1.24, itemPrice);
        }
    }
}
