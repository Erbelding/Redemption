/**
 * David Erbelding
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.ObjectClasses;
using RedemptionEngine.Items;

namespace RedemptionEngine.Screens
{
    public class InventoryScreen: GameScreen
    {
        private SpriteFont font;
        private SpriteFont attrFont;
        private Inventory inventory;
        private Texture2D backTex;
        private Texture2D attributePanel;

        //inventory needs to get access to the player's inventory
        public Inventory DisplayInventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        //load images to use
        public InventoryScreen(ScreenManager screenManager):base(screenManager)
        {
            backTex = screenManager.Content.Load<Texture2D>("GUI//InventoryBackground");
            font = screenManager.Content.Load<SpriteFont>("Fonts//Font");
            attrFont = screenManager.Content.Load<SpriteFont>("Fonts//attributefont");
            attributePanel = screenManager.Content.Load<Texture2D>("GUI//AttributePanel");
        }


        #region Update

        public override void UpdateActive(GameTime gameTime)
        {
            //closes the inventory screen
            if (Controller.SingleKeyPress(Keys.Escape) || Controller.SingleKeyPress(Keys.I))
            {
                screenManager.Game.IsMouseVisible = false;
                screenManager.ClosePopupScreen(this);
            }
        }

        public override void UpdateTransitionOn()
        {
            transitionTime += .02f;
            if (inventory.EquipmentLeft.Item != null)
                inventory.EquipmentLeft.Item.Rotation = 0;
            if (inventory.EquipmentRight.Item != null)
                inventory.EquipmentRight.Item.Rotation = 0;
            if (transitionTime >= 1)
            {
                
                //set the screen to active and show the mouse
                screenManager.Game.IsMouseVisible = true;
                screenState = ScreenState.Active;
            }
        }

        public override void UpdateTransitionOff()
        {
            transitionTime -= .02f;
            
            if (transitionTime <= 0)
            {
                if (inventory.EquipmentLeft.Item != null)
                    inventory.EquipmentLeft.Item.Rotation = 0;
                if (inventory.EquipmentRight.Item != null)
                    inventory.EquipmentRight.Item.Rotation = 0;
                inventory.UnselectAll();
                screenState = ScreenState.Off;
            }
        }

        #endregion

        #region Draw

        public override void DrawPaused(SpriteBatch spriteBatch)
        {

        }

        public override void DrawActive(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //title
            //spriteBatch.DrawString(font, "Inventory Screen", new Vector2(40f, 40f), Color.White * transitionTime);

            //if (Controller.SingleKeyPress(Keys.P))
            //{
            //    inventory.AddItem(new Potion(screenManager.Content));
            //}

            //Update
            inventory.Update(gameTime);

            spriteBatch.Draw(backTex, new Vector2(50, 70), (Color.White * .8f) * transitionTime);
            spriteBatch.Draw(attributePanel, new Vector2(65, 85), (Color.White * .8f) * transitionTime);

            //Draw the inventory;
            inventory.Draw(spriteBatch, transitionTime, font);

            //Draw character stats
            Character c = inventory.Character;


            //draws all the player's stats
            string s = "Level: " + c.Level.Value + "\n" + "Exp: " + c.Experience.Value + "/" + c.Experience.MaxValue + "\n" + 
                "Health: " + c.Health.Value + "/" + c.Health.MaxValue +
                  "\n" + "Mana: " + c.Mana.Value + "/" + c.Mana.MaxValue + "\n" +
                "Strength: " + c.Strength.Value + "\n" + "Defense: " + c.Defense.Value + "\n" +
                "Magic: " + c.Magic.Value;
            
            

            Vector2 textStart = new Vector2(75, 90);
            
            spriteBatch.DrawString(attrFont, s, textStart, Color.White * transitionTime,
                0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

        }

        //draws the same thing it would if it was active
        public override void DrawTransitionOn(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawActive(gameTime, spriteBatch);
        }

        public override void DrawTransitionOff(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawActive(gameTime, spriteBatch);
        }

        #endregion
    }
}