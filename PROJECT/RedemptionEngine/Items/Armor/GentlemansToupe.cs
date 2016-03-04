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
    public class GentlemansToupe: Item
    {
        public GentlemansToupe(ContentManager content)
            : base(content)
        {
            name = "Gentleman's Toupe";
            type = ItemType.ARMOR_HEAD;
            texture = content.Load<Texture2D>("Items//Armor//Afro");

            AddModifier("Defense + 5", "Defense", 5, false);
            AddModifier("Magic + 15", "Magic", 15, false);
            AddModifier("Max Mana + 50", "Mana", 50, true);
        }
    }
}
