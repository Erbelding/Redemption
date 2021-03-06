﻿/**
 * Matt Guerrette
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.Items
{
    public class Potion : Item
    {
        public Potion(ContentManager content)
            : base(content)
        {
            name = "Health Potion";
            type = Item.ItemType.CONSUMABLE;
            texture = content.Load<Texture2D>("Items//Other//HealthPotion");
        }

        public override void Use()
        {
            GameManager.Player.Health.Value += 50;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
