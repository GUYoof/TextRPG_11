using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXT11;

namespace TXT11
{
    public class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public ItemType Type { get; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public bool IsSold { get; set; } = false;
        public bool IsEquipped = false;

        public Item(string name, string description, int price, ItemType type, int attact = 0, int defense = 0)
        {
            Name = name;
            Price = price;
            Description = description;
            Type = type;
            Attack = attact;
            Defense = defense;
        }
    }
}
