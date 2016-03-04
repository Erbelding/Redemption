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
    public class SeatlessTrousers : Item
    {
        public SeatlessTrousers(ContentManager content)
            : base(content)
        {
            name = "Seatless Trousers";
            type = ItemType.ARMOR_LEGS;

            texture = content.Load<Texture2D>("Items//Armor//SeatlessTrousers");

            AddModifier("Defense - 25", "Defense", -25, false);
            AddModifier("Strength + 50", "Strength", 50, false);
            AddModifier("Max Mana + 50", "Mana", 50, true);
            AddModifier("Health Regen: 1HP/s", "Health Regen", 1, false);
        }
    }
}
