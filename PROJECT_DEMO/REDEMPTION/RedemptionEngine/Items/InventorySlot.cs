using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedemptionEngine.Items
{
    public class InventorySlot
    {
        //attributes
        public enum SlotType { WEAPON, ARMOR, ANY }
        private SlotType holds;

        private Item item;
        private int stack;
        public int stackCap;


        //properties
        public Item Item
        {
            get { return item; }
            set { item = value; }
        }

        //constructor
        public InventorySlot(SlotType type, int stack, int stackCap)
        {
            this.holds = type;
            this.stack = stack;
            this.stackCap = stackCap;
        }


        //check if you can put this item here
        public bool CanInsertItem(Item item)
        {
            switch (holds)
            {
                case SlotType.WEAPON:
                    if(item.Type != Item.ItemType.WEAPON) return false;
                    else return true;
                case SlotType.ARMOR:
                    if(item.Type != Item.ItemType.ARMOR) return false;
                    else return true;
                case SlotType.ANY:
                    return true;
            }
            return true;
        }

        public void InsertItem(Item item)
        {
            this.item = item;
        }
    }
}

