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
    public class LeatherHat: Item
    {
        public LeatherHat(ContentManager content)
            : base(content)
        {
            name = "Leather Hat";
            type = ItemType.ARMOR_HEAD;
            
            texture = content.Load<Texture2D>("Items//Armor//leatherhat");

            AddModifier("Defense + 10", "Defense", 10, false);
        }
    }
}
