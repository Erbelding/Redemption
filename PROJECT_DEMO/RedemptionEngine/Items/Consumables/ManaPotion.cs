using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.Items
{
    public class ManaPotion : Item
    {
        public ManaPotion(ContentManager content)
            : base(content)
        {
            name = "Mana Potion";
            type = Item.ItemType.CONSUMABLE;
            texture = content.Load<Texture2D>("Items//Other//ManaPotion");
        }

        public override void Use()
        {
            GameManager.Player.Mana.Value += 50;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
