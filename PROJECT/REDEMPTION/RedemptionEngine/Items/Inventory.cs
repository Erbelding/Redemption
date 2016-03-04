using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.ObjectClasses;
using RedemptionEngine.Screens;

namespace RedemptionEngine.Items
{
    public class Inventory
    {
        #region Attributes/Properties

        //attributes
        private InventorySlot equipmentLeft;
        private InventorySlot equipmentRight;

        private InventorySlot equipmentHead;
        private InventorySlot equipmentBody;
        private InventorySlot equipmentLegs;
        private InventorySlot equipmentFeet;

        private List<InventorySlot> inventory;

        private Player player;
        private InventoryScreen inventoryScreen;


        //properties

        public InventorySlot EquipmentLeft
        {
            get { return equipmentLeft;  }
            set { equipmentLeft = value; }
        }
        public InventorySlot EquipmentRight
        {
            get { return equipmentRight; }
            set { equipmentRight = value; }
        }
        public InventorySlot EquipmentHead
        {
            get { return equipmentHead; }
            set { equipmentHead = value; }
        }
        public InventorySlot EquipmentBody
        {
            get { return equipmentBody; }
            set { equipmentBody = value; }
        }
        public InventorySlot EquipmentLegs
        {
            get { return equipmentLegs; }
            set { equipmentLegs = value; }
        }
        public InventorySlot EquipmentFeet
        {
            get { return equipmentFeet; }
            set { equipmentFeet = value; }
        }

        #endregion

        #region Constructor
        //constructor
        public Inventory(Player player, InventoryScreen inventoryScreen)
        {
            this.player = player;
            this.inventoryScreen = inventoryScreen;
        }
        #endregion

        #region Methods

        public void SwapItem(InventorySlot firstSlot, InventorySlot secondSlot)
        {
            //don't do anything
            if (firstSlot.Item == null && secondSlot.Item == null) return;
            //only need to move one way
            if (firstSlot.Item == null)
            {
                if (firstSlot.CanInsertItem(secondSlot.Item)) firstSlot.InsertItem(secondSlot.Item);
                return;
            }
            //only need to move one way
            if (secondSlot.Item == null)
            {
                if (secondSlot.CanInsertItem(firstSlot.Item)) secondSlot.InsertItem(firstSlot.Item);
                return;
            }
            //we have items in both slots that need to be moved so just move them around
            if (firstSlot.CanInsertItem(secondSlot.Item) && secondSlot.CanInsertItem(firstSlot.Item))
            {
                Item extraItem = firstSlot.Item;
                firstSlot.Item = secondSlot.Item;
                secondSlot.Item = extraItem;
            }
        }

        

        public void DropItem(int item)
        {
 
        }



        #endregion
    }
}
