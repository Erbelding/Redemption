using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.Items;
using RedemptionEngine.Items.Weapons;

namespace RedemptionEngine.ObjectClasses.Enemy
{
    public class SnowMan : Hostile
    {
        public SnowMan(ContentManager content)
            : base(content)
        {
            name = "Snowman";
            speed = 100.0f;
            texture = content.Load<Texture2D>("Characters//SnowMan");

            //Add animations
            animations.Add("Walking_Down",
                new Animation(new Point(0, 0), new Point(32, 32), 2, 200));
            animations.Add("Walking_Up",
                new Animation(new Point(0, 64), new Point(32, 32), 2, 200));
            animations.Add("Walking_Right",
                new Animation(new Point(0,32), new Point(32, 32), 2, 200));
            animations.Add("Walking_Left",
                new Animation(new Point(0, 0), new Point(32, 32), 2, 200));
            animations.Add("Idle_Down",
                new Animation(new Point(0, 0), new Point(32, 32), 1, 200));
            animations.Add("Idle_Up",
                new Animation(new Point(0, 64), new Point(32, 32), 1, 200));
            animations.Add("Idle_Right",
                new Animation(new Point(0, 32), new Point(32, 32), 1, 200));
            animations.Add("Idle_Left",
                new Animation(new Point(0, 0), new Point(32, 32), 1, 200));

            CurrentAnimationKey = "Walking_Down";

            inventory = new Inventory(this);

            boundsXOffset = 5;
            boundsYOffset = 4;
            boundsWidthOffset = -10;
            boundsHeightOffset = -4;

            int r = DropItems.Rand.Next(10);
            if(r >= 0) inventory.EquipmentRight.InsertItem(new SnowShot(content));
            if (r > 4) inventory.EquipmentRight.InsertItem(new IceSickle(content));
            if (r > 7) inventory.EquipmentRight.InsertItem(new IcicleShot(content));
            if (r > 8) inventory.EquipmentRight.InsertItem(new Popsickle(content));

            
            dropPool.AddItem(DropItems.Items["Potion"], 60);
            dropPool.AddItem(DropItems.Items["ManaPotion"], 20);
            dropPool.AddItem(DropItems.Items["SnowShot"], 8);
            dropPool.AddItem(DropItems.Items["IceSickle"], 6);
            dropPool.AddItem(DropItems.Items["IcicleShot"], 4);
            dropPool.AddItem(DropItems.Items["FootWarmers"], 2);
            dropPool.AddItem(DropItems.Items["Popsickle"], 2);
        }

        public override CharacterAttribute Level
        {
            set
            {
                int x = value.Value;
                level.Value = value.Value;
                baseHealth = new CharacterAttribute("Health", (int)(150 + x * 6.2), (int)(150 + x * 3.2));
                baseDefense = new CharacterAttribute("Defense", (int)(20 + x * 1.8), 200);
                baseStrength = new CharacterAttribute("Strength", (int)(25 + x * 1.35), 160);
                baseStrength = new CharacterAttribute("Magic", (int)(20 + x * 2), 220);
                baseStrength = new CharacterAttribute("Mana", (int)(20 + x * 5), 520);

                InitializeAttributes();
            }
        }

        public override void Die(Character attacker)
        {
            int level = Level.Value;
            int expAmount = (int)(2.2 * Math.Pow(1.15, level));
            attacker.AddExperience(expAmount);
            GameManager.RemoveEntity(this);
            DropItem(.2f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            

            AI();



            if (velocity.X > 0)
            {
                if (CurrentAnimationKey != "Walking_Right")
                {
                    CurrentAnimationKey = "Walking_Right";
                }
            }
            if (velocity.X < 0)
            {
                if (CurrentAnimationKey != "Walking_Left")
                {
                    CurrentAnimationKey = "Walking_Left";
                }
            }
            if (velocity.Y < 0)
            {
                if (CurrentAnimationKey != "Walking_Up")
                {
                    CurrentAnimationKey = "Walking_Up";
                }
            }
            if (velocity.Y > 0)
            {
                if (CurrentAnimationKey != "Walking_Down")
                {
                    CurrentAnimationKey = "Walking_Down";
                }
            }
            if (velocity == Vector2.Zero)
            {
                if (CurrentAnimationKey == "Walking_Down") CurrentAnimationKey = "Idle_Down";
                if (CurrentAnimationKey == "Walking_Right") CurrentAnimationKey = "Idle_Right";
                if (CurrentAnimationKey == "Walking_Up") CurrentAnimationKey = "Idle_Up";
                if (CurrentAnimationKey == "Walking_Left") CurrentAnimationKey = "Idle_Left";
            }
        }


        int attackDelay = 0;

        protected virtual void AI()
        {

            if (pTarget == null)
            {
                pTarget = gameManager.Player;
            }
            target = new Vector2(pTarget.Bounds.X + pTarget.Bounds.Width / 2, pTarget.Bounds.Y + pTarget.Bounds.Height / 2);

            float deltaX = MathHelper.Distance(target.X, Bounds.X + Bounds.Width / 2);
            float deltaY = MathHelper.Distance(target.Y, Bounds.Y + Bounds.Height / 2);

            float d = (float)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));



            if (state == State.IDLE)
            {
                velocity = Vector2.Zero;

                if (d <= 400)
                {
                    state = State.CHASING;
                    return;
                }
            }

            if (state == State.CHASING)
            {
                MoveTowardsTarget();

                if (d <= 120 && (inventory.EquipmentRight.Item is SnowShot || inventory.EquipmentRight.Item is IcicleShot))
                {
                    state = State.ATTACKING;
                    Stop();
                    return;
                }
                else if (d <= 32)
                {
                    state = State.ATTACKING;
                    Stop();
                    return;
                }

                if (d > 400)
                {
                    state = State.IDLE;
                    Stop();
                    return;
                }
            }


            if (state == State.ATTACKING)
            {
                if(inventory.EquipmentRight.Item is Popsickle || inventory.EquipmentRight.Item is IceSickle)
                    Stop();
                else
                    LineUpWithTarget();

                //attack stuff
                attackDelay += DropItems.Rand.Next(8);
                if (attackDelay >= 150)
                {
                    attackDelay = 0;
                    Attack();
                }

                if (inventory.EquipmentRight.Item is SnowShot || inventory.EquipmentRight.Item is IcicleShot)
                {
                    if(d > 120)
                    {
                        state = State.CHASING;
                        return;
                    }
                }
                else if (d > 32)
                {
                    state = State.CHASING;
                    return;
                }



            }
        }
        public void Attack()
        {
            Mana.Value = Mana.MaxValue;
            (inventory.EquipmentRight.Item as Weapon).Attack(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //spriteBatch.Draw(colTex, Bounds, Color.White);
        }
    }
}
