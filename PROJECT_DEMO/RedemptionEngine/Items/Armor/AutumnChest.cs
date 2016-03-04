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
    public class AutumnChest: Item
    {
        public AutumnChest(ContentManager content)
            : base(content)
        {
            name = "Chestplate of Autumn";
            type = ItemType.ARMOR_BODY;
            texture = content.Load<Texture2D>("Items//Armor//AutumnChestPlate");


            AddModifier("Defense + 10", "Defense", 10, false);
            AddModifier("Strength + 5", "Strength", 5, false);
            AddModifier("Max Health + 50", "Health", 50, true);
        }
    }
}
