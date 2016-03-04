/**
 * David Erbelding
 * Matt Guerrette
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.Screens;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace RedemptionEngine.Items
{
    public class InventorySlot
    {
        //constants
        private const int STACK_CAP = 20;

        //delegates
        public delegate void OnSelect(object sender, EventArgs e);
        public delegate void OnUse(object sender, EventArgs e);
        public delegate void OnDrop(object sender, EventArgs e);

        //attributes
        public enum SlotType { WEAPON, ARMOR_HEAD, ARMOR_BODY, ARMOR_LEGS, ARMOR_FEET, ANY }

        private SlotType holds;

        private Item item;
        private Texture2D slotTex;
        private Vector2 position;
        private int stack;
        private int stackCap;
        private event OnSelect Clicked;
        private event OnUse Used;
        private event OnDrop Dropped;
        private bool selected;
        private bool hovered;
        private Color tint;
        private float scale;
        public bool oldSelectedState;

        //properties
        public Item Item
        {
            get { return item; }
            set { item = value; }
        }

        public int StackCap { get { return stackCap; } set { stackCap = value; } }

        public int Stack 
        { 
            get { return stack; }
            set { stack = value; } 
        }

        public Vector2 Position { get { return position; } set { position = value; } }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)(slotTex.Width * scale),
                    (int)(slotTex.Height * scale));
            }
        }

        public bool Hovered { get { return hovered; } set { hovered = false; } }

        public bool Selected { get { return selected; } set { selected = false; } }

        //constructor
        public InventorySlot(ContentManager content, SlotType type)
        {
            this.holds = type;
            this.stack = 0;
            this.stackCap = STACK_CAP;
            
            this.slotTex = content.Load<Texture2D>("GUI//InventoryTile");
            this.Clicked += Slot_Selected;
            this.Used += Slot_Used;
            this.Dropped += Slot_Dropped;
            scale = 1.5f;
        }

        public void Update()
        {
            //Set old selected state
            oldSelectedState = selected;

            //Check if slot is hovered by mouse
            if (Bounds.Contains(Controller.mouseState.X, Controller.mouseState.Y))
            {
                hovered = true;
                //If user clicks left mouse
                if (Controller.IsMouseButtonClicked(true))
                {
                    //Set selected
                    Clicked(this, new EventArgs());
                }

               
            }
            else hovered = false;

            if (selected)
            {
                if (Controller.SingleKeyPress(Keys.U))
                {
                    Used(this, new EventArgs());
                }
                if (Controller.SingleKeyPress(Keys.D))
                {
                    Dropped(this, new EventArgs());
                }
            }
        }

        

        //check if you can put this item here
        public bool CanInsertItem(object item)
        {
            Item itm = (item as Item);
            switch (holds)
            {
                case SlotType.WEAPON:
                    if(itm.Type != Item.ItemType.WEAPON) return false;
                    else return true;
                case SlotType.ARMOR_HEAD:
                    if(itm.Type != Item.ItemType.ARMOR_HEAD) return false;
                    else return true;
                case SlotType.ARMOR_BODY:
                    if (itm.Type != Item.ItemType.ARMOR_BODY) return false;
                    else return true;
                case SlotType.ARMOR_LEGS:
                    if (itm.Type != Item.ItemType.ARMOR_LEGS) return false;
                    else return true;
                case SlotType.ARMOR_FEET:
                    if (itm.Type != Item.ItemType.ARMOR_FEET) return false;
                    else return true;
                case SlotType.ANY:
                    return true;
            }
            return true;
        }


        public bool IsFull()
        {
            if (stack >= STACK_CAP) return true;
            else return false;
        }

        private void Slot_Selected(object sender, EventArgs e)
        {
            //Set selected
            selected = !selected;
        }

        private void Slot_Dropped(object sender, EventArgs e)
        {
            //If the slot is currently selected
            if (selected)
            {
                //If there is an item
                if (item != null)
                {
                    //Drop the item
                    item.Drop();
                    stack--;
                    if (stack <= 0)
                    {
                        item = null;
                        selected = false;
                    }
                }
            }
        }
    

        private void Slot_Used(object sender, EventArgs e)
        {
            //If the slot is currently selected
            if (selected)
            {
                if (item != null)
                {
                    //If the item is a consumable
                    if (item.Type == Item.ItemType.CONSUMABLE)
                    {
                        item.Use();
                        stack--;
                        if (stack <= 0)
                        {
                            item = null;
                            selected = false;
                        }
                    }
                }
            }
        }

        public void InsertItem(object item)
        {
            //Increment stack
            //stack++;
            //Set item
            //Need to convert from generic object to item type
            this.item = (item as Item);
        }

        public void Draw(SpriteBatch spriteBatch, float dt, SpriteFont font)
        {
            if (selected)
                tint = Color.Yellow;
            else if(!selected)
                tint = Color.White;

            

            //Draw inventory slot
            spriteBatch.Draw(slotTex, position, null, tint * dt, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.0f);

            //Draw its item if it has one
            if (item != null)
            {
                item.Alpha = dt;

                item.Position = new Vector2(this.Position.X + this.Bounds.Width / 2 - item.Origin.X,
                    this.Position.Y + this.Bounds.Height / 2 - item.Origin.Y);

                if (item.GetAnimation("Inventory") != null)
                    spriteBatch.Draw(item.Texture, item.Position, item.GetAnimation("Inventory").SourceRect, Color.White * item.Alpha);

                else spriteBatch.Draw(item.Texture, item.Position, Color.White * item.Alpha);
                //draw item
                
            }

            //Print stack number in bottom right corner of slot
            Vector2 fontDim = font.MeasureString(stack.ToString());
            Vector2 stackPos = new Vector2(this.Position.X + (Bounds.Width - fontDim.X),
                this.Position.Y + (Bounds.Height - fontDim.Y));

            if(stack > 1) spriteBatch.DrawString(font, stack.ToString(), stackPos, Color.White * dt);

            if (selected && item != null)
            {
                spriteBatch.DrawString(font, item.ToString(), new Vector2(415, 90), Color.White * dt);
                if (item.Type == Item.ItemType.CONSUMABLE)
                {
                    spriteBatch.DrawString(font, "Drop: D", new Vector2(415, 115), (Color.White * dt));
                    spriteBatch.DrawString(font, "Use: U", new Vector2(415, 140), (Color.White * dt));
                }
                else
                {
                    spriteBatch.DrawString(font, "Drop: D", new Vector2(415, 115), (Color.White * dt));
                }
                DrawData(spriteBatch, font);
            }
        }
        public void DrawData(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (item.Type == Item.ItemType.WEAPON)
            {
                spriteBatch.DrawString(font, "Power: " + (item as Weapon).GETPOWER, new Vector2(415, 140), (Color.White));
            }
            if (item.Type == Item.ItemType.WEAPON || item.Type == Item.ItemType.ARMOR_BODY || item.Type == Item.ItemType.ARMOR_LEGS
                || item.Type == Item.ItemType.ARMOR_HEAD || item.Type == Item.ItemType.ARMOR_FEET)
            {
                int yOffset = 0;
                foreach (Modifier mod in item.Modifiers)
                {
                    if(mod.Value > 0)
                        spriteBatch.DrawString(font, mod.Name, new Vector2(720 - font.MeasureString(mod.Name).X, 115 + yOffset), Color.Green);
                    else
                        spriteBatch.DrawString(font, mod.Name, new Vector2(720 - font.MeasureString(mod.Name).X, 115 + yOffset), Color.Red);
                    yOffset += 25;
                }
            }
        }
    }
}

