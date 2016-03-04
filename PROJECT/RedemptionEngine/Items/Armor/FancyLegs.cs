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
    public class FancyLegs: Item
    {
        public FancyLegs(ContentManager content)
            : base(content)
        {
            name = "Fancy Legs";
            type = ItemType.ARMOR_LEGS;
            
            texture = content.Load<Texture2D>("Items//Armor//FancyLegs");

            AddModifier("Defense + 25", "Defense", 25, false);
            AddModifier("Strength + 20", "Strength", 20, false);
            AddModifier("Magic + 20", "Magic", 20, false);
        }
    }
}
