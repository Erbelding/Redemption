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
    public class Crossbow: Weapon
    {
        public Crossbow(ContentManager content)
            : base(content)
        {
            POWER = 7;
            name = "Crossbow";
            type = ItemType.WEAPON;
            weapontype = Weapon.WeaponType.RANGED;
            texture = content.Load<Texture2D>("Items//Weapons//Crossbow");
            

            AddAnimation("Inventory",
                new Animation(new Point(0, 0), new Point(32, 32), 1, 200));
            AddAnimation("Wield",
                new Animation(new Point(32, 0), new Point(32, 32), 1, 200));
            CurrentAnimationKey = "Inventory";
        }

        public override void Attack(Character owner)
        {

            Arrow arrow = null;

            if (owner.Dir == Direction.LEFT)
            {
                arrow = new Arrow(owner, new Vector2(-1, 0), 50, POWER);
                arrow.Rotation = -MathHelper.PiOver2;
            }
            else if (owner.Dir == Direction.RIGHT)
            {
                arrow = new Arrow(owner, new Vector2(1, 0), 50, POWER);
                arrow.Rotation = MathHelper.PiOver2;
            }
            else if (owner.Dir == Direction.DOWN)
            {
                arrow = new Arrow(owner, new Vector2(0, 1), 50, POWER);
                arrow.Rotation = MathHelper.Pi;
            }
            else arrow = new Arrow(owner, new Vector2(0, -1), 50, POWER);

            //arrow.SetArrowType("PoisonArrow");
            //arrow.SetEffect(1, 1000, 10000);

            owner.GameManager.AddEntity(arrow);
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
                    rotation = 0;
                    position.X = owner.Position.X - 10;
                    position.Y = owner.Position.Y;
                    alpha = 1;
                }

                if (owner.Dir == Direction.UP)
                {
                    rotation = MathHelper.PiOver2;
                    position.X = owner.Position.X;
                    position.Y = owner.Position.Y - 10;
                    alpha = 1;
                }

                if (owner.Dir == Direction.RIGHT)
                {
                    rotation = MathHelper.Pi;
                    position.X = owner.Position.X + 10;
                    position.Y = owner.Position.Y;
                    alpha = 1;
                }

                if (owner.Dir == Direction.DOWN)
                {
                    rotation = -MathHelper.PiOver2;
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
