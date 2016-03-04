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
    public class WizardsHat: Item
    {
        public WizardsHat(ContentManager content)
            : base(content)
        {
            name = "Wizards Hat";
            type = ItemType.ARMOR_HEAD;
            
            texture = content.Load<Texture2D>("Items//Armor//WizardHat");

            AddModifier("Defense + 5", "Defense", 5, false);
            AddModifier("Magic + 15", "Magic", 15, false);
            AddModifier("Max Mana + 25", "Mana", 25, true);
        }
    }
}
