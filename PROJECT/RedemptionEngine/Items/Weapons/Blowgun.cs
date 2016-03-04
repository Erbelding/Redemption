using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework;
using RedemptionEngine.Items.Projectiles;
using RedemptionEngine.Screens;
using Microsoft.Xna.Framework.Input;

namespace RedemptionEngine.Items.Weapons.Special
{
    public class Blowgun: Weapon
    {
        public Blowgun(ContentManager content)
            : base(content)
        {
            POWER = 3;
            name = "Blowgun";
            type = ItemType.WEAPON;
            weapontype = Weapon.WeaponType.RANGED;
            texture = content.Load<Texture2D>("Items//Weapons//Blowgun");
            

            AddAnimation("Inventory",
                new Animation(new Point(0, 0), new Point(32, 32), 1, 200));
            AddAnimation("Wield",
                new Animation(new Point(0, 0), new Point(32, 32), 1, 200));
            CurrentAnimationKey = "Inventory";
        }

        public override void Attack(Character owner)
        {

            Dart dart = null;

            if (owner.Dir == Direction.LEFT)
            {
                dart = new Dart(owner, new Vector2(-1, 0), 50, POWER);
                dart.Rotation = -MathHelper.PiOver2;
            }
            else if (owner.Dir == Direction.RIGHT)
            {
                dart = new Dart(owner, new Vector2(1, 0), 50, POWER);
                dart.Rotation = MathHelper.PiOver2;
            }
            else if (owner.Dir == Direction.DOWN)
            {
                dart = new Dart(owner, new Vector2(0, 1), 50, POWER);
                dart.Rotation = MathHelper.Pi;
            }
            else dart = new Dart(owner, new Vector2(0, -1), 50, POWER);


            dart.SetEffect((int)owner.Magic.Value/5, 1000, 10000);

            owner.GameManager.AddEntity(dart);
        }



        public override void Update(GameTime gameTime, Character owner, Keys key)
        {
            base.Update(gameTime);
            if (Controller.KeyIsDown(key))
            { 
                active = true;
                CurrentAnimationKey = "Wield";

                if (owner.Dir == Direction.LEFT)
                {
                    rotation = -MathHelper.PiOver2;
                    position.X = owner.Position.X - 10;
                    position.Y = owner.Position.Y;
                    alpha = 1;
                }

                if (owner.Dir == Direction.UP)
                {
                    rotation = 0;
                    position.X = owner.Position.X;
                    position.Y = owner.Position.Y - 10;
                    alpha = 1;
                }

                if (owner.Dir == Direction.RIGHT)
                {
                    rotation = MathHelper.PiOver2;
                    position.X = owner.Position.X + 10;
                    position.Y = owner.Position.Y;
                    alpha = 1;
                }

                if (owner.Dir == Direction.DOWN)
                {
                    rotation = MathHelper.Pi;
                    position.X = owner.Position.X;
                    position.Y = owner.Position.Y + 10;
                    alpha = 1;
                }
            }

            if (Controller.OnKeyReleased(key))
            {
                CurrentAnimationKey = "Inventory";
                Attack(owner);
                active = false;
                rotation = 0;
            }
        }
    }
}
