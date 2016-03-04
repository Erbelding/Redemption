using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.Items;

namespace RedemptionEngine
{
    public static class DropItems
    {
        //Attributes
        public static Dictionary<string, Item> Items = new Dictionary<string, Item>();
        public static Random Rand = new Random();
        //Methods
        public static void AddItem(string name, Item item)
        {
            //If the item doesn't already exist
            if (!Items.ContainsKey(name))
            {
                //Add the item
                Items.Add(name, item);
            }
        }

    }
}
