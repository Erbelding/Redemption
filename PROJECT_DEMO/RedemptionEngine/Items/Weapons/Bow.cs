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
    public class Bow: Weapon
    {
        public Bow(ContentManager content)
            : base(content)
        {
            POWER = 5;
            name = "Bow";
            type = ItemType.WEAPON;
            weapontype = Weapon.WeaponType.RANGED;
            texture = content.Load<Texture2D>("Items//Weapons//Bow");
            

            AddAnimation("Inventory",
                new Animation(new Point(0, 0), new Point(32, 32), 1, 200));
            CurrentAnimationKey = "Inventory";
        }

        public override void Attack(Character owner)
        {

            Arrow arrow = null;
            int power = POWER + (timer / 600) * POWER;

            if (owner.Dir == Direction.LEFT)
            {
                arrow = new Arrow(owner, new Vector2(-1, 0), timer, power);
                arrow.Rotation = -MathHelper.PiOver2;
            }
            else if (owner.Dir == Direction.RIGHT)
            {
                arrow = new Arrow(owner, new Vector2(1, 0), timer, power);
                arrow.Rotation = MathHelper.PiOver2;
            }
            else if (owner.Dir == Direction.DOWN)
            {
                arrow = new Arrow(owner, new Vector2(0, 1), timer, power);
                arrow.Rotation = MathHelper.Pi;
            }
            else arrow = new Arrow(owner, new Vector2(0, -1), timer, power);

            //arrow.SetArrowType("PoisonArrow");
            //arrow.SetEffect(1, 1000, 10000);

            owner.GameManager.AddEntity(arrow);
        }


        private int timer = 0;

        public override void Update(GameTime gameTime, Character owner, Keys key)
        {
            base.Update(gameTime);
            if (Controller.KeyIsDown(key))
            { 
                if (timer < 600) timer += (int)(gameTime.ElapsedGameTime.TotalSeconds * 450); //charge bow
                active = true;

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
                Attack(owner);
                timer = 0;
                active = false;
                rotation = 0;
            }
        }
    }
}
