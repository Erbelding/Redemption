/**
 * David Erbelding
 * Matt Guerrette
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.ObjectClasses;
using RedemptionEngine.Screens;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RedemptionEngine.Items
{
    public class Inventory
    {
        //constant
        private const int MIN_ITEMS_SLOTS = 24;
        

        #region Attributes/Properties

        //attributes
        private InventorySlot equipmentLeft;
        private InventorySlot equipmentRight;

        private InventorySlot equipmentHead;
        private InventorySlot equipmentBody;
        private InventorySlot equipmentLegs;
        private InventorySlot equipmentFeet;

        private List<InventorySlot> itemSlots;

        private Character character;
        //private InventoryScreen inventoryScreen;


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

        public List<InventorySlot> ItemList
        {
            get { return itemSlots; }
        }

        public Character Character { get { return character; } }

        InventorySlot[] slotsSelected = new InventorySlot[2];


        public List<InventorySlot> SlotsToCheck
        {
            get
            {
                List<InventorySlot> slotsToCheck = new List<InventorySlot>();
                slotsToCheck.Add(EquipmentLeft);
                slotsToCheck.Add(EquipmentRight);
                slotsToCheck.Add(EquipmentHead);
                slotsToCheck.Add(EquipmentBody);
                slotsToCheck.Add(EquipmentLegs);
                slotsToCheck.Add(EquipmentFeet);

                for (int i = 0; i < itemSlots.Count; i++)
                {
                    slotsToCheck.Add(itemSlots[i]);
                }
                return slotsToCheck;
            }
        }
        #endregion

        #region Constructor

        //constructor
        public Inventory(Character character)
        {
            this.character = character;
            //this.inventoryScreen = inventoryScreen;

            itemSlots = new List<InventorySlot>();

            //Create inventory slots
            equipmentLeft = new InventorySlot(character.Content, InventorySlot.SlotType.WEAPON);
            equipmentLeft.StackCap = 1;
            equipmentRight = new InventorySlot(character.Content, InventorySlot.SlotType.WEAPON);
            equipmentRight.StackCap = 1;

            equipmentHead = new InventorySlot(character.Content, InventorySlot.SlotType.ARMOR_HEAD);
            equipmentHead.StackCap = 1;
            equipmentBody = new InventorySlot(character.Content, InventorySlot.SlotType.ARMOR_BODY);
            equipmentBody.StackCap = 1;
            equipmentLegs = new InventorySlot(character.Content, InventorySlot.SlotType.ARMOR_LEGS);
            equipmentLegs.StackCap = 1;
            equipmentFeet = new InventorySlot(character.Content, InventorySlot.SlotType.ARMOR_FEET);
            equipmentFeet.StackCap = 1;

            for (int i = 0; i < MIN_ITEMS_SLOTS; i++)
            {
                itemSlots.Add(new InventorySlot(character.Content, InventorySlot.SlotType.ANY));
            }
        }
        #endregion

        #region Methods

        public void UnselectAll()
        {
            equipmentLeft.Selected = false;
            equipmentRight.Selected = false;
            equipmentHead.Selected = false;
            equipmentBody.Selected = false;
            equipmentLegs.Selected = false;
            equipmentFeet.Selected = false;

            foreach (InventorySlot itemSlot in itemSlots)
            {
                itemSlot.Selected = false;
            }
        }

        public void AddItem(object item)
        {
            //Loops through each slot in itemsSlots in inventory
            foreach (InventorySlot slot in itemSlots)
            {
                //If we find that this slot already contains this type of item. We just increment the stack
                if (slot.Item != null && item.GetType().Name.Equals(slot.Item.GetType().Name) && slot.CanInsertItem(item) && !slot.IsFull())
                {
                    slot.Stack++;
                    return;
                }
            }


            //go through the list
            foreach (InventorySlot slot in itemSlots)
            {
                if (slot.Item == null && slot.CanInsertItem(item))
                {
                    //if the item can be added add it and stop going through
                    slot.Item = (item as Item);
                    slot.Item.GameManager = this.Character.GameManager;
                    slot.Stack++;
                    return;
                }
            }
        }

        public void SwapItem(InventorySlot firstSlot, InventorySlot secondSlot)
        {
            //deselect the two slots
            firstSlot.Selected = false;
            secondSlot.Selected = false;

            //don't do anything if both slots are empty
            if (firstSlot.Item == null && secondSlot.Item == null) return;

            //In the case that we have the first selected slot as null, and the second
            //selected slot is not null, we must swap them.
            if (firstSlot.Item == null && secondSlot.Item != null)
            {
                if (firstSlot.CanInsertItem(secondSlot.Item))
                {
                    
                    firstSlot.Item = secondSlot.Item;
                    firstSlot.Stack = secondSlot.Stack;
                    secondSlot.Item = null;
                    secondSlot.Stack = 0;
                }
            }
            // the opposite of the above case
            else if (secondSlot.Item == null && firstSlot.Item != null)
            {
                SwapItem(secondSlot, firstSlot);
            }


            

            //In the case that we have two items we want to either swap or stack
            if (firstSlot.Item != null && secondSlot.Item != null)
            {
                //If the first slot holds an item that is the same as the second slot
                //instead of swapping their slot location we must stack them
                if (firstSlot.Item.GetType().Name.Equals(secondSlot.Item.GetType().Name))
                {
                    secondSlot.Stack += firstSlot.Stack;
                    firstSlot.Stack = 0;
                    firstSlot.Item = null;
                }
                //if both of the slots have something in them and also different items, swap where the items are
                else if (firstSlot.CanInsertItem(secondSlot.Item) && secondSlot.CanInsertItem(firstSlot.Item))
                {
                    Item extraItem = firstSlot.Item;
                    int tempStack = firstSlot.Stack;
                    firstSlot.Item = secondSlot.Item;
                    firstSlot.Stack = secondSlot.Stack;
                    secondSlot.Item = extraItem;
                    secondSlot.Stack = tempStack;
                }
            }


            character.ApplyAllModifiers(null);
        }
    

        public Item RemoveItem(Item item, InventorySlot slot)
        {
            Item itemToReturn = slot.Item;
            slot.Stack--;
            slot.Item = null;
            return itemToReturn;
            
        }

        public void Update(GameTime gameTime)
        {
            
            int index = 0;
            foreach (InventorySlot i in SlotsToCheck)
            {
                if (i.Item != null)
                {
                    if (i.Item.GameManager == null)
                    {
                        i.Item.GameManager = Character.GameManager;
                    }
                }
                
                if (i.Selected == true)
                {
                    slotsSelected[index] = i;
                    index++;
                }

                if (index > 1)
                {
                    if (slotsSelected[0].oldSelectedState == true)
                        SwapItem(slotsSelected[0], slotsSelected[1]);
                    else
                        SwapItem(slotsSelected[1], slotsSelected[0]);
                    break;
                }

            }

            //Check if two slots are selected, if so we swap the items contained in them
            for (int i = 0; i < SlotsToCheck.Count; i++)
            {
                
                SlotsToCheck[i].Update();

                if (SlotsToCheck[i].Item != null)
                    SlotsToCheck[i].Item.Update(gameTime);
                
            }

            
            


            foreach (InventorySlot slot in SlotsToCheck)
            {
                while (slot.Stack > slot.StackCap)
                {
                    AddItem(slot.Item);
                    slot.Stack--;
                }


            }

            
        }

        //Draws all inventory slots and character information
        public void Draw(SpriteBatch spriteBatch, float dt, SpriteFont font)
        {
            #region Draw Equipment Slots

            float equipYOffset = 0;
            Vector2 equipStart = new Vector2(175, 275);

            equipmentHead.Position = new Vector2(equipStart.X, equipStart.Y + equipYOffset);
            equipmentHead.Draw(spriteBatch, dt, font);

            equipYOffset += 64;

            equipmentBody.Position = new Vector2(equipStart.X, equipStart.Y + equipYOffset);
            equipmentBody.Draw(spriteBatch, dt, font);

            equipmentLeft.Position = new Vector2(100, equipStart.Y + equipYOffset);
            equipmentLeft.Draw(spriteBatch, dt, font);

            equipmentRight.Position = new Vector2(250, equipStart.Y + equipYOffset);
            equipmentRight.Draw(spriteBatch, dt, font);


            equipYOffset += 64;

            equipmentLegs.Position = new Vector2(equipStart.X, equipStart.Y + equipYOffset);
            equipmentLegs.Draw(spriteBatch, dt, font);

            equipYOffset += 64;

            equipmentFeet.Position = new Vector2(equipStart.X, equipStart.Y + equipYOffset);
            equipmentFeet.Draw(spriteBatch, dt, font);

            


            #endregion

            #region Draw Inventory Slots
            //Starting position for all inventory slots
            Vector2 start = new Vector2(325, 275);
            float xOffset = 0;
            float yOffset = 0;
            int index = 0;

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    InventorySlot i = itemSlots[index];
                    i.Position = new Vector2(start.X + xOffset, start.Y + yOffset);
                    
                    i.Draw(spriteBatch, dt, font);
                    index++;
                    xOffset += 64;
                }
                xOffset = 0;
                yOffset += 64;
            }
            #endregion

            
            foreach (InventorySlot slot in SlotsToCheck)
            {
                if (slot.Hovered)
                {
                    if (slot.Item != null)
                    {
                        Vector2 fontDimensions = font.MeasureString(slot.Item.ToString());
                        spriteBatch.DrawString(font, slot.Item.Name, new Vector2(Mouse.GetState().X -font.MeasureString(slot.Item.Name).X/2, Mouse.GetState().Y + 10), Color.White * dt);
                    }
                }
            }
        }

        public override string ToString()
        {
            string s = "";

            if(equipmentLeft.Item != null)
                s += "Equipment Left: " + equipmentLeft.Item.ToString() + "\n";
            if (equipmentRight.Item != null)
                s += "Equipment Right: " + equipmentRight.Item.ToString() + "\n";

            if (equipmentHead.Item != null)
                s += "Equipment Head: " + equipmentHead.Item.ToString() + "\n";
            if (equipmentBody.Item != null)
                s += "Equipment Body: " + equipmentBody.Item.ToString() + "\n";
            if (equipmentLegs.Item != null)
                s += "Equipment Legs: " + equipmentLegs.Item.ToString() + "\n";
            if (equipmentFeet.Item != null)
                s += "Equipment Feet: " + equipmentFeet.Item.ToString() + "\n";

            for (int i = 0; i < itemSlots.Count; i++)
            {
                if(itemSlots[i].Item != null)
                    s += "Item Slot " + (i + 1) + ": " + itemSlots[i].Item.ToString() + " : " + itemSlots[i].Stack + "\n";
            }

            return s;
        }


        #endregion
    }
}
