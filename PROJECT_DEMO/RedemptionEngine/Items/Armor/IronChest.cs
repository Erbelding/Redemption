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
    public class IronChest: Item
    {
        public IronChest(ContentManager content)
            : base(content)
        {
            name = "Iron Chestplate";
            type = ItemType.ARMOR_BODY;
            texture = content.Load<Texture2D>("Items//Armor//iron chestplate");


            AddModifier("Defense + 10", "Defense", 10, false);
        }
    }
}
