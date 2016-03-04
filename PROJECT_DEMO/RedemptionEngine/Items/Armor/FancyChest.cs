using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using RedemptionEngine.ObjectClasses;

namespace RedemptionEngine.Items.Armor
{
    public class FancyChest: Item
    {
        public FancyChest(ContentManager content)
            : base(content)
        {
            name = "Fancy Chestplate";
            type = ItemType.ARMOR_BODY;
            
            texture = content.Load<Texture2D>("Items//Armor//FancyChestPiece");

            AddModifier("Defense + 35", "Defense", 35, false);
            AddModifier("Strength + 25", "Strength", 25, false);
            AddModifier("Magic + 25", "Magic", 25, false);
        }
    }
}
