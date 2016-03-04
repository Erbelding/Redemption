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
    public class LeatherBoots: Item
    {
        public LeatherBoots(ContentManager content)
            : base(content)
        {
            name = "Leather Boots";
            type = ItemType.ARMOR_FEET;
            
            texture = content.Load<Texture2D>("Items//Armor//leather boots");

            AddModifier("Defense + 5", "Defense", 5, false);
        }
    }
}
