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

namespace RedemptionEngine.Items.Weapons
{
    public class FireballShot : Weapon
    {

        public FireballShot(ContentManager content)
            : base(content)
        {
            POWER = 15;
            name = "Fireball";
            type = ItemType.WEAPON;
            weapontype = Weapon.WeaponType.RANGED;
            texture = content.Load<Texture2D>("Items//Weapons//fireball spellbook");

            AddModifier("Magic + 5", "Magic", 5, false);
        }

        public override void Attack(Character owner)
        {
            Fireball Fireball = null;
            int power = POWER;

            owner.Mana.Value -= 3;

            if (owner.Dir == Direction.LEFT)
            {
                Fireball = new Fireball(owner, new Vector2(-1, 0), owner.Magic.Value/2, power);
                Fireball.Rotation = -MathHelper.PiOver2;
            }
            else if (owner.Dir == Direction.RIGHT)
            {
                Fireball = new Fireball(owner, new Vector2(1, 0), owner.Magic.Value / 2, power);
                Fireball.Rotation = MathHelper.PiOver2;
            }
            else if (owner.Dir == Direction.DOWN)
            {
                Fireball = new Fireball(owner, new Vector2(0, 1), owner.Magic.Value / 2, power);
                Fireball.Rotation = MathHelper.Pi;
            }
            else Fireball = new Fireball(owner, new Vector2(0, -1), owner.Magic.Value / 2, power);

            owner.GameManager.AddEntity(Fireball);
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
