using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace RedemptionEngine.ObjectClasses.Enemy
{
    public class RollingThing : Hostile
    {

        public RollingThing(ContentManager content)
            : base(content)
        {
            MELEE_DAMAGE = 5;

            texture = content.Load<Texture2D>("Characters//RollingThing");
            speed = 100f;
            scale = 1.0f;

            animations.Add("Move_Down",
                new Animation(new Point(0, 0), new Point(32, 32), 2, 100));
            animations.Add("Move_Up",
                new Animation(new Point(0, 64), new Point(32, 32), 2, 100));
            animations.Add("Move_Right",
                new Animation(new Point(0, 32), new Point(32, 32), 3, 100));
            animations.Add("Move_Left",
                new Animation(new Point(0, 96), new Point(32, 32), 3, 100));
            animations.Add("Idle",
                new Animation(new Point(0, 32), new Point(32, 32), 1, 100));

            boundsXOffset = 3;
            boundsYOffset = 3;
            boundsWidthOffset = -6;
            boundsHeightOffset = -6;

            CurrentAnimationKey = "Idle";

            dropPool.AddItem(DropItems.Items["ChainSword"], 8);
            dropPool.AddItem(DropItems.Items["AutumnChest"], 8);
            dropPool.AddItem(DropItems.Items["FireSword"], 8);
            dropPool.AddItem(DropItems.Items["Club"], 6);
            dropPool.AddItem(DropItems.Items["Flail"], 6);
            dropPool.AddItem(DropItems.Items["Geradon"], 2);
            dropPool.AddItem(DropItems.Items["Potion"], 80);
            dropPool.AddItem(DropItems.Items["ManaPotion"], 30);

        }

        public override CharacterAttribute Level
        {
            set
            {
                int x = value.Value;
                level.Value = value.Value;
                baseHealth = new CharacterAttribute("Health", 80 + x * 4, 20 + x * 4);
                baseDefense = new CharacterAttribute("Defense", 5 + (int)(x * 1.8), 185);
                baseStrength = new CharacterAttribute("Strength", 25 + x * 3, 325);
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
            int expAmount = (int)(2 * Math.Pow(1.15, level));
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
