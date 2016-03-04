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
    public class WingedBoots: Item
    {
        public WingedBoots(ContentManager content)
            : base(content)
        {
            name = "Ye Olde Winged Boots";
            type = ItemType.ARMOR_FEET;
            
            texture = content.Load<Texture2D>("Items//Armor//wingedshoes");

            AddModifier("Defense + 10", "Defense", 10, false);
            AddModifier("Strength + 5", "Strength", 5, false);
            AddModifier("+20% Movement Speed", "Move", 1, false);


        }
    }
}
