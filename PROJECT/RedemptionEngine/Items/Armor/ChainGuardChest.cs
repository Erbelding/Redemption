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
    public class ChainGuardChest: Item
    {
        public ChainGuardChest(ContentManager content)
            : base(content)
        {
            name = "Chainguard Vest";
            type = ItemType.ARMOR_BODY;
            texture = content.Load<Texture2D>("Items//Armor//ChainGuardChest");

            AddModifier("Defense + 25", "Defense", 25, false);
            AddModifier("Magic + 30", "Magic", 30, false);
        }
    }
}
