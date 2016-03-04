using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.ObjectClasses;
using RedemptionEngine.Items.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RedemptionEngine.Screens;

namespace RedemptionEngine.Items.Weapons
{
    public class LightBolt : Weapon
    {
        

        public LightBolt(ContentManager content)
            : base(content)
        {
            POWER = 17;
            name = "Light Bolt";
            type = ItemType.WEAPON;
            weapontype = Weapon.WeaponType.RANGED;
            texture = content.Load<Texture2D>("Items//Weapons//lightbolt spellbook");

            AddModifier("Strength + 5", "Strength", 5, false);
        }

        public override void Attack(Character owner)
        {
            LightningBolt lightningBolt = null;
            int power = POWER;

            owner.Mana.Value -= 5;

            if (owner.Dir == Direction.LEFT)
            {
                lightningBolt = new LightningBolt(owner, new Vector2(-1, 0), owner.Magic.Value, power);
                owner.GameManager.AddEntity(lightningBolt);
            }
            else if (owner.Dir == Direction.RIGHT)
            {
                lightningBolt = new LightningBolt(owner, new Vector2(1, 0), owner.Magic.Value, power);
                owner.GameManager.AddEntity(lightningBolt);
            }
            else if (owner.Dir == Direction.DOWN)
            {
                lightningBolt = new LightningBolt(owner, new Vector2(0, 1), owner.Magic.Value, power);
                owner.GameManager.AddEntity(lightningBolt);
            }
            else
            {
                lightningBolt = new LightningBolt(owner, new Vector2(0, -1), owner.Magic.Value, power);
                owner.GameManager.AddEntity(lightningBolt);
            }
            
        }


        public override void Update(GameTime gameTime, Character owner, Keys key)
        {
            base.Update(gameTime);
            if (Controller.KeyIsDown(key))
            {
                active = true;
                visible = false;
            }

            if (Controller.OnKeyReleased(key))
            {
                if (owner.Mana.Value > 0)
                {
                    Attack(owner);
                    active = false;
                    visible = true;
                    rotation = 0;
                }
            }
        }
    }
}
