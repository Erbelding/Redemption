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
    public class FancyHelmet: Item
    {
        public FancyHelmet(ContentManager content)
            : base(content)
        {
            name = "Fancy Helmet";
            type = ItemType.ARMOR_HEAD;
            
            texture = content.Load<Texture2D>("Items//Armor//FancyHelmet");

            AddModifier("Defense + 20", "Defense", 20, false);
            AddModifier("Strength + 15", "Strength", 15, false);
            AddModifier("Magic + 15", "Magic", 15, false);
        }
    }
}
