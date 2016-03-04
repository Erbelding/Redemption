using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace RedemptionEngine.ObjectClasses.Enemy
{
    public class Bat : Hostile
    {

        public Bat(ContentManager content)
            : base(content)
        {
            MELEE_DAMAGE = 4;

            texture = content.Load<Texture2D>("Characters//Bat");
            speed = 180f;
            scale = 1.0f;

            
            animations.Add("Idle", new Animation(new Point(0, 0), new Point(34, 17), 4, 100));
            animations.Add("Moving", new Animation(new Point(0, 0), new Point(34, 17), 4, 60));

            boundsXOffset = 3;
            boundsYOffset = 3;
            boundsWidthOffset = -6;
            boundsHeightOffset = -6;

            CurrentAnimationKey = "Idle";


            dropPool.AddItem(DropItems.Items["BlackSword"], 1);
            dropPool.AddItem(DropItems.Items["WingedBoots"], 1);
            dropPool.AddItem(DropItems.Items["WizardHat"], 6);
            dropPool.AddItem(DropItems.Items["DecoratedSword"], 6);
            dropPool.AddItem(DropItems.Items["Nunchuck"], 6);
            dropPool.AddItem(DropItems.Items["SeatlessTrousers"], 1);
            dropPool.AddItem(DropItems.Items["LeatherHat"], 4);
            dropPool.AddItem(DropItems.Items["Potion"], 80);
            dropPool.AddItem(DropItems.Items["ManaPotion"], 30);
        }

        public override CharacterAttribute Level
        {
            set
            {
                int x = value.Value;
                level.Value = value.Value;
                baseHealth = new CharacterAttribute("Health", (int)(20 + x * 2.5), (int)(20 + x * 2.5));
                baseDefense = new CharacterAttribute("Defense", -10 + (int)(x * 1.5), 140);
                baseStrength = new CharacterAttribute("Strength", (int)(x * 1.8), 180);
                InitializeAttributes();
            }
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);

            AI();

        }

        public override void Die(Character attacker)
        {
            int level = Level.Value;
            int expAmount = (int)(1.2 * Math.Pow(1.15, level));
            attacker.AddExperience(expAmount);
            GameManager.RemoveEntity(this);
            DropItem(.2f);
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
                    CurrentAnimationKey = "Moving";
                    state = State.CHASING;
                }
            }

            if (state == State.CHASING)
            {

                if (d <= 64)
                {
                    state = State.ATTACKING;
                    Stop();
                    return;
                }

                if (d > 300)
                {
                    CurrentAnimationKey = "Idle";
                    state = State.IDLE;
                    Stop();
                    return;
                }


                MoveTowardsTarget();


            }

            if (state == State.ATTACKING)
            {
                if (d > 64)
                {
                    state = State.CHASING;
                }

                if (DropItems.Rand.Next(100) > 60)
                {
                    if (DropItems.Rand.Next(100) > 98)
                    {
                        int direction = DropItems.Rand.Next(5);
                        switch (direction)
                        {
                            case 0: MoveDown(); break;
                            case 1: MoveRight(); break;
                            case 2: MoveUp(); break;
                            case 3: MoveLeft(); break;
                            case 4: Stop(); break;
                        }
                    }
                }
                else MoveTowardsTarget();

                

                

            }


        }
    }
}
