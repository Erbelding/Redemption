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
    public class FootWarmers: Item
    {
        public FootWarmers(ContentManager content)
            : base(content)
        {
            name = "Foot Warmers";
            type = ItemType.ARMOR_FEET;
            
            texture = content.Load<Texture2D>("Items//Armor//TheFootWarmers");

            AddModifier("Defense + 10", "Defense", 10, false);
            AddModifier("Strength + 5", "Strength", 5, false);
            AddModifier("Mana Regen: 1MP/s", "Mana Regen", 1, false);

            AddAnimation("Inventory",
                new Animation(new Point(0, 0), new Point(32, 32), 15, 200));
            CurrentAnimationKey = "Inventory";
        }
    }
}
