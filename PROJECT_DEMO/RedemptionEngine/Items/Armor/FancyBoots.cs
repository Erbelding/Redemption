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
    public class FancyBoots: Item
    {
        public FancyBoots(ContentManager content)
            : base(content)
        {
            name = "Fancy Boots";
            type = ItemType.ARMOR_FEET;
            
            texture = content.Load<Texture2D>("Items//Armor//FancyBoots");

            AddModifier("Defense + 20", "Defense", 20, false);
            AddModifier("Strength + 15", "Strength", 15, false);
            AddModifier("Magic + 15", "Magic", 15, false);
        }
    }
}
