using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.Items
{
    public class SuperHealthPotion : Item
    {
        public SuperHealthPotion(ContentManager content)
            : base(content)
        {
            name = "Super Health Potion";
            type = Item.ItemType.CONSUMABLE;
            texture = content.Load<Texture2D>("Items//Other//Super Health Potion");
        }

        public override void Use()
        {
            GameManager.Player.Health.Value += 100;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
