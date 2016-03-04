using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.Screens;
using RedemptionEngine.Items.Projectiles;

namespace RedemptionEngine.Items.Weapons
{
    public class SnowShot : Weapon
    {

        public SnowShot(ContentManager content)
            : base(content)
        {
            POWER = 5;
            name = "Snowball";
            type = ItemType.WEAPON;
            weapontype = Weapon.WeaponType.RANGED;
            texture = content.Load<Texture2D>("Items//Weapons//snowball spellbook");

            AddModifier("Magic + 10", "Magic", 10, false);
        }

        public override void Attack(Character owner)
        {
            Snowball snowball = null;

            owner.Mana.Value--;

            if (owner.Dir == Direction.LEFT)
            {
                snowball = new Snowball(owner, new Vector2(-1, 0), owner.Magic.Value, POWER);
                snowball.Rotation = -MathHelper.PiOver2;
            }
            else if (owner.Dir == Direction.RIGHT)
            {
                snowball = new Snowball(owner, new Vector2(1, 0), owner.Magic.Value, POWER);
                snowball.Rotation = MathHelper.PiOver2;
            }
            else if (owner.Dir == Direction.DOWN)
            {
                snowball = new Snowball(owner, new Vector2(0, 1), owner.Magic.Value, POWER);
                snowball.Rotation = MathHelper.Pi;
            }
            else snowball = new Snowball(owner, new Vector2(0, -1), owner.Magic.Value, POWER);

            owner.GameManager.AddEntity(snowball);
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
