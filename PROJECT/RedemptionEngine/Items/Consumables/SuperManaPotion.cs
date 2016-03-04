using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.Items
{
    public class SuperManaPotion : Item
    {
        public SuperManaPotion(ContentManager content)
            : base(content)
        {
            name = "Super Mana Potion";
            type = Item.ItemType.CONSUMABLE;
            texture = content.Load<Texture2D>("Items//Other//Super Mana Potion");
        }

        public override void Use()
        {
            GameManager.Player.Mana.Value += 100;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
