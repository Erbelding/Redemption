using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.Items;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.Screens;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.TileEngine
{
    public class EquipmentBar
    {
        private Texture2D tile;
        private Texture2D backpack;
        private SpriteFont font;
        private Player player;
        private Item pickupItem;

        public Item PickupItem { set { pickupItem = value; } }

        public EquipmentBar(GamePlayScreen screen, Player p)
        {
            player = p;
            tile = screen.ScreenManager.Content.Load<Texture2D>("GUI//InventoryTile");
            backpack = screen.ScreenManager.Content.Load<Texture2D>("GUI//Backpack");
            font = screen.ScreenManager.Content.Load<SpriteFont>("Fonts//Font");
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tile, new Rectangle(355, 20, 48, 48), Color.White);
            spriteBatch.Draw(tile, new Rectangle(405, 20, 48, 48), Color.White);
            //draw left equipment
            if (player.Inventory.EquipmentLeft.Item != null)
            {
                if (player.Inventory.EquipmentLeft.Item.GetAnimation("Inventory") != null)
                    spriteBatch.Draw(player.Inventory.EquipmentLeft.Item.Texture,
                        new Vector2(355 + 24 - player.Inventory.EquipmentLeft.Item.Origin.X,
                            20 + 24 - player.Inventory.EquipmentLeft.Item.Origin.Y),
                        player.Inventory.EquipmentLeft.Item.GetAnimation("Inventory").SourceRect, Color.White);

                else spriteBatch.Draw(player.Inventory.EquipmentLeft.Item.Texture,
                    new Vector2(355 + 24 - player.Inventory.EquipmentLeft.Item.Origin.Y,
                            20 + 24 - player.Inventory.EquipmentLeft.Item.Origin.Y), Color.White);
            }
            //draw right equipment
            if (player.Inventory.EquipmentRight.Item != null)
            {
                if (player.Inventory.EquipmentRight.Item.GetAnimation("Inventory") != null)
                    spriteBatch.Draw(player.Inventory.EquipmentRight.Item.Texture,
                        new Vector2(405 + 24 - player.Inventory.EquipmentRight.Item.Origin.X,
                            20 + 24 - player.Inventory.EquipmentRight.Item.Origin.Y),
                        player.Inventory.EquipmentRight.Item.GetAnimation("Inventory").SourceRect, Color.White);

                else spriteBatch.Draw(player.Inventory.EquipmentRight.Item.Texture,
                    new Vector2(405 + 24 - player.Inventory.EquipmentRight.Item.Origin.X,
                            20 + 24 - player.Inventory.EquipmentRight.Item.Origin.Y), Color.White);
            }

            spriteBatch.DrawString(font, "J", new Vector2(390, 32), Color.White);
            spriteBatch.DrawString(font, "K", new Vector2(440, 32), Color.White);

            if (pickupItem != null)
            {
                spriteBatch.Draw(tile, new Rectangle(500, 20, 48, 48),  Color.White);
                spriteBatch.DrawString(font, "E", new Vector2(535, 32), Color.White);

                if (pickupItem.GetAnimation("Inventory") != null)
                    spriteBatch.Draw(pickupItem.Texture,
                        new Vector2(500 + 24 - pickupItem.Origin.X,
                            20 + 24 - pickupItem.Origin.Y),
                        pickupItem.GetAnimation("Inventory").SourceRect, Color.White);
                else spriteBatch.Draw(pickupItem.Texture,
                    new Vector2(500 + 24 - pickupItem.Origin.X,
                            20 + 24 - pickupItem.Origin.Y), Color.White);
            }

            spriteBatch.Draw(backpack, new Rectangle(732, 20, 48, 48), Color.White);
            spriteBatch.DrawString(font, "I", new Vector2(767, 32), Color.White);
        }
    }
}
