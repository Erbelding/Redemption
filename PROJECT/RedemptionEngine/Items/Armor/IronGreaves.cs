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
    public class IronGreaves: Item
    {
        public IronGreaves(ContentManager content)
            : base(content)
        {
            name = "Iron Greaves";
            type = ItemType.ARMOR_LEGS;
            
            texture = content.Load<Texture2D>("Items//Armor//iron greaves");

            AddModifier("Defense + 15", "Defense", 15, false);
            AddModifier("Strength + 5", "Strength", 5, false);
        }
    }
}
