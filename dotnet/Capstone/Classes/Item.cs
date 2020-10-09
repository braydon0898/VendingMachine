using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string LocationSlot {get; set;}
        public string Category { get; set; }
        public int Quantity { get; set; }

        public Item(string name, double price, string locationSlot, string category)
        {
            this.Name = name;
            this.Price = price;
            this.LocationSlot = locationSlot;
            this.Category = category;
            Quantity = 5;
        }

        public Item()
        {
            Quantity = 5;
        }

    }
}
