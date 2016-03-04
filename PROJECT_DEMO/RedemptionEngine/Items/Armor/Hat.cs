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
    public class Hat: Item
    {
        public Hat(ContentManager content)
            : base(content)
        {
            name = "The Hat";
            type = ItemType.ARMOR_HEAD;
            
            texture = content.Load<Texture2D>("Items//Armor//the hat");

            AddModifier("Max Health + 100", "Health", 100, true);
            AddModifier("Max Mana + 100", "Mana", 100, true);
        }
    }
}
