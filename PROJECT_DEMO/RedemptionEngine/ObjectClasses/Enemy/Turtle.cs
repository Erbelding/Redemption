using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RedemptionEngine.Items.Projectiles;

namespace RedemptionEngine.ObjectClasses.Enemy
{
    public class Turtle: Hostile
    {

        public Turtle(ContentManager content)
            : base(content)
        {
            texture = content.Load<Texture2D>("Characters//Tortoise");
            speed = 40f;
            scale = 1.0f;

            animations.Add("Idle_Down",
                new Animation(new Point(0, 0), new Point(32, 32), 1, 100));
            animations.Add("Idle_Right",
                new Animation(new Point(0, 32), new Point(32, 32), 1, 100));
            animations.Add("Idle_Up",
                new Animation(new Point(0, 64), new Point(32, 32), 1, 100));
            animations.Add("Idle_Left",
                new Animation(new Point(0, 96), new Point(32, 32), 1, 100));

            animations.Add("Move_Down",
                new Animation(new Point(32, 0), new Point(32, 32), 4, 100));
            animations.Add("Move_Right",
                new Animation(new Point(32, 32), new Point(32, 32), 4, 100));
            animations.Add("Move_Up",
                new Animation(new Point(32, 64), new Point(32, 32), 4, 100));
            animations.Add("Move_Left",
                new Animation(new Point(32, 96), new Point(32, 32), 4, 100));

            animations.Add("Hide_Down",
                new Animation(new Point(160, 0), new Point(32, 32), 2, 100));
            animations.Add("Hide_Right",
                new Animation(new Point(160, 32), new Point(32, 32), 2, 100));
            animations.Add("Hide_Up",
                new Animation(new Point(160, 64), new Point(32, 32), 2, 100));
            animations.Add("Hide_Left",
                new Animation(new Point(160, 96), new Point(32, 32), 2, 100));

            animations.Add("Hidden_Down",
                new Animation(new Point(192, 0), new Point(32, 32), 1, 100));
            animations.Add("Hidden_Right",
                new Animation(new Point(192, 32), new Point(32, 32), 1, 100));
            animations.Add("Hidden_Up",
                new Animation(new Point(192, 64), new Point(32, 32), 1, 100));
            animations.Add("Hidden_Left",
                new Animation(new Point(192, 96), new Point(32, 32), 1, 100));

            animations.Add("UnHide_Down",
                new Animation(new Point(192, 0), new Point(32, 32), 2, 100));
            animations.Add("UnHide_Right",
                new Animation(new Point(192, 32), new Point(32, 32), 2, 100));
            animations.Add("UnHide_Up",
                new Animation(new Point(192, 64), new Point(32, 32), 2, 100));
            animations.Add("UnHide_Left",
                new Animation(new Point(192, 96), new Point(32, 32), 2, 100));

            boundsXOffset = 2;
            boundsYOffset = 1;
            boundsWidthOffset = -4;
            boundsHeightOffset = -1;

            CurrentAnimationKey = "Idle_Down";

            dropPool.AddItem(DropItems.Items["Sword"], 8);
            dropPool.AddItem(DropItems.Items["SlowPoke"], 8);
            dropPool.AddItem(DropItems.Items["VikingShield"], 6);
            dropPool.AddItem(DropItems.Items["ThrowingKnife"], 6);
            dropPool.AddItem(DropItems.Items["IronChest"], 4);
            dropPool.AddItem(DropItems.Items["Rainbow"], 1);
            dropPool.AddItem(DropItems.Items["Potion"], 80);
            dropPool.AddItem(DropItems.Items["ManaPotion"], 30);
            dropPool.AddItem(DropItems.Items["Blowgun"], 4);
        }

        public override CharacterAttribute Level
        {
            set
            {
                int x = value.Value;
                level.Value = value.Value;
                baseHealth = new CharacterAttribute("Health", 50 + x * 2, 50 + x * 2);
                baseDefense = new CharacterAttribute("Defense", x * 3, 200);
                baseStrength = new CharacterAttribute("Strength", (int)(x * 1.5), 150);
                baseMagic = new CharacterAttribute("Magic", (int)(x * 1.8), 180);

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
                if (CurrentAnimationKey == "Move_Down") CurrentAnimationKey = "Idle_Down";
                if (CurrentAnimationKey == "Move_Right") CurrentAnimationKey = "Idle_Right";
                if (CurrentAnimationKey == "Move_Up") CurrentAnimationKey = "Idle_Up";
                if (CurrentAnimationKey == "Move_Left") CurrentAnimationKey = "Idle_Left";
            }
        }

        public override void Die(Character attacker)
        {
            int level = Level.Value;
            int expAmount = (int)(2.3 * Math.Pow(1.15, level));
            attacker.AddExperience(expAmount);
            GameManager.RemoveEntity(this);
            DropItem(.6f);
        }

        int attackDelay = 0;
        
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

                if (d <= 400)
                {
                    state = State.CHASING;
                }
            }

            if (state == State.CHASING)
            {

                if (d <= 175)
                {
                    state = State.ATTACKING;
                    Stop();
                }

                if (d > 400)
                {
                    state = State.IDLE;
                    Stop();
                }


                MoveTowardsTarget();

            }

            if (state == State.ATTACKING)
            {
                if (d > 175)
                {
                    state = State.CHASING;
                }
                if (d <= 100 || DropItems.Rand.Next(100) > 98)
                {
                    state = State.HIDING;
                    baseDefense.Value = baseDefense.Value * 2;
                    baseDefense.MaxValue = baseDefense.MaxValue * 2;
                }

                
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

                //set animation to open turtle shell
                if (CurrentAnimationKey == "Hidden_Down")
                {
                    CurrentAnimationKey = "UnHide_Down";
                }
                if (CurrentAnimationKey == "Hidden_Right")
                {
                    CurrentAnimationKey = "UnHide_Right";
                }
                if (CurrentAnimationKey == "Hidden_Up")
                {
                    CurrentAnimationKey = "UnHide_Up";
                }
                if (CurrentAnimationKey == "Hidden_Left")
                {
                    CurrentAnimationKey = "UnHide_Left";
                }

                //completely out of shell
                if (CurrentAnimationKey == "UnHide_Down" && CurrentAnimation.PlayCount == 1)
                {
                    CurrentAnimationKey = "Idle_Down";
                }
                if (CurrentAnimationKey == "UnHide_Right" && CurrentAnimation.PlayCount == 1)
                {
                    CurrentAnimationKey = "Idle_Right";
                }
                if (CurrentAnimationKey == "UnHide_Up" && CurrentAnimation.PlayCount == 1)
                {
                    CurrentAnimationKey = "Idle_Up";
                }
                if (CurrentAnimationKey == "UnHide_Left" && CurrentAnimation.PlayCount == 1)
                {
                    CurrentAnimationKey = "Idle_Left";
                }

            }
            if (state == State.HIDING)
            {
                
                if (d > 100 && DropItems.Rand.Next(100) > 97)
                {
                    baseDefense.Value = baseDefense.Value / 2;
                    baseDefense.MaxValue = baseDefense.MaxValue / 2;
                    state = State.ATTACKING;
                }

                //attack stuff
                attackDelay += DropItems.Rand.Next(8);
                if (attackDelay > 320)
                {
                    attackDelay = 0;
                    Attack();
                }





                //begin hiding animation

                if (CurrentAnimationKey == "Idle_Down" || CurrentAnimationKey == "Move_Down")
                {
                    Stop();
                    CurrentAnimationKey = "Hide_Down";
                }
                if (CurrentAnimationKey == "Idle_Right" || CurrentAnimationKey == "Move_Right")
                {
                    Stop();
                    CurrentAnimationKey = "Hide_Right";
                }
                if (CurrentAnimationKey == "Idle_Up" || CurrentAnimationKey == "Move_Up")
                {
                    Stop();
                    CurrentAnimationKey = "Hide_Up";
                }
                if (CurrentAnimationKey == "Idle_Left" || CurrentAnimationKey == "Move_Left")
                {
                    Stop();
                    CurrentAnimationKey = "Hide_Left";
                }






                if (CurrentAnimationKey == "Hide_Down" && CurrentAnimation.PlayCount == 1)
                {
                    CurrentAnimationKey = "Hidden_Down";
                }
                if (CurrentAnimationKey == "Hide_Right" && CurrentAnimation.PlayCount == 1)
                {
                    CurrentAnimationKey = "Hidden_Right";
                }
                if (CurrentAnimationKey == "Hide_Up" && CurrentAnimation.PlayCount == 1)
                {
                    CurrentAnimationKey = "Hidden_Up";
                }
                if (CurrentAnimationKey == "Hide_Left" && CurrentAnimation.PlayCount == 1)
                {
                    CurrentAnimationKey = "Hidden_Left";
                }
 
            }
        }



        public void Attack()
        {
            gameManager.AddEntity(new NeedleShot(this, new Vector2(1, 0), Level.Value));
            gameManager.AddEntity(new NeedleShot(this, Vector2.Normalize(new Vector2(1,1)), Level.Value));
            gameManager.AddEntity(new NeedleShot(this, Vector2.Normalize(new Vector2(1,-1)), Level.Value));

            gameManager.AddEntity(new NeedleShot(this, new Vector2(-1, 0), Level.Value));
            gameManager.AddEntity(new NeedleShot(this, Vector2.Normalize(new Vector2(-1, 1)), Level.Value));
            gameManager.AddEntity(new NeedleShot(this, Vector2.Normalize(new Vector2(-1, -1)), Level.Value));

            gameManager.AddEntity(new NeedleShot(this, new Vector2(0, 1), Level.Value));
            gameManager.AddEntity(new NeedleShot(this, new Vector2(0, -1), Level.Value));


        }
    }
}
