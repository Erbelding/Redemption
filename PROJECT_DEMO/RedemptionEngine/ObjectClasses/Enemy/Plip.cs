using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace RedemptionEngine.ObjectClasses.Enemy
{
    public class Plip : Hostile
    {

        public Plip(ContentManager content)
            : base(content)
        {
            MELEE_DAMAGE = 3;

            texture = content.Load<Texture2D>("Characters//plip");
            speed = 100f;
            scale = 1.0f;
            animations.Add("Move_Down",
                new Animation(new Point(0, 0), new Point(32, 32), 5, 100));
            animations.Add("Move_Up",
                new Animation(new Point(0, 32), new Point(32, 32), 5, 100));
            animations.Add("Move_Right",
                new Animation(new Point(0, 64), new Point(32, 32), 4, 100));
            animations.Add("Move_Left",
                new Animation(new Point(0, 96), new Point(32, 32), 4, 100));
            animations.Add("Idle",
                new Animation(new Point(0, 128), new Point(32, 32), 4, 100));

            boundsXOffset = 10;
            boundsYOffset = 12;
            boundsWidthOffset = -20;
            boundsHeightOffset = -12;

            CurrentAnimationKey = "Idle";

            dropPool.AddItem(DropItems.Items["FancyHelmet"], 1);
            dropPool.AddItem(DropItems.Items["FancyBoots"], 1);
            dropPool.AddItem(DropItems.Items["FancyChest"], 1);
            dropPool.AddItem(DropItems.Items["FancyLegs"], 1);
            dropPool.AddItem(DropItems.Items["GentlemansToupe"], 1);
            dropPool.AddItem(DropItems.Items["Hat"], 1);
            dropPool.AddItem(DropItems.Items["Potion"], 160);
            dropPool.AddItem(DropItems.Items["ManaPotion"], 60);

        }

        public override CharacterAttribute Level
        {
            set
            {
                int x = value.Value;
                level.Value = value.Value;
                baseHealth = new CharacterAttribute("Health", 80 + x * 3, 80 + x * 3);
                baseDefense = new CharacterAttribute("Defense", (int)(5 + x * 1.3), 135);
                baseStrength = new CharacterAttribute("Strength", (int)(5 + x * 1.5), 155);
                InitializeAttributes();
            }
        }

        public override void Update(GameTime gameTime)
        {
            


            base.Update(gameTime);

            AI();


            if (velocity.X > 0)
            {
                if (CurrentAnimationKey != "Move_Right")
                {
                    CurrentAnimationKey = "Move_Right";
                }
            }
            if (velocity.X < 0)
            {
                if (CurrentAnimationKey != "Move_Left")
                {
                    CurrentAnimationKey = "Move_Left";
                }
            }
            if (velocity.Y < 0)
            {
                if (CurrentAnimationKey != "Move_Up")
                {
                    CurrentAnimationKey = "Move_Up";
                }
            }
            if (velocity.Y > 0)
            {
                if (CurrentAnimationKey != "Move_Down")
                {
                    CurrentAnimationKey = "Move_Down";
                }
            }
            if (velocity == Vector2.Zero)
            {
                if (CurrentAnimationKey != "Idle")
                {
                    CurrentAnimationKey = "Idle";
                }
            }
        }

        public override void Die(Character attacker)
        {
            int level = Level.Value;
            int expAmount = (int)(1.2 * Math.Pow(1.15, level));
            attacker.AddExperience(expAmount);
            GameManager.RemoveEntity(this);
            DropItem(.6f);
        }

        protected virtual void AI()
        {
            if (pTarget == null)
            {
                pTarget = gameManager.Player;
            }
            target = pTarget.Center;

            float deltaX = MathHelper.Distance(target.X, Center.X);
            float deltaY = MathHelper.Distance(target.Y, Center.Y);

            float d = (float)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));

            if (state == State.IDLE)
            {
                velocity = Vector2.Zero;

                if (d <= 300)
                {
                    state = State.CHASING;
                }
            }

            if (state == State.CHASING)
            {

                if (d <= 32)
                {
                    state = State.ATTACKING;
                    Stop();
                    return;
                }

                if (d > 300)
                {
                    state = State.IDLE;
                    Stop();
                    return;
                }


                MoveTowardsTarget();


            }

            if (state == State.ATTACKING)
            {
                if (d > 32)
                {
                    state = State.CHASING;
                }

                MoveTowardsTarget();

                

                

            }


        }
    }
}
