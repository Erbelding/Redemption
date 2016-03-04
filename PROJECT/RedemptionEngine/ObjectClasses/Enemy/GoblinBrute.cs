using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.Items;
using RedemptionEngine.Items.Weapons;
using RedemptionEngine.Items.Weapons.Special;

namespace RedemptionEngine.ObjectClasses.Enemy
{
    public class GoblinBrute : Hostile
    {
        public GoblinBrute(ContentManager content)
            : base(content)
        {
            name = "Goblin Brute";
            speed = 80.0f;
            texture = content.Load<Texture2D>("Characters//GoblinBrute");

            //Add animations
            animations.Add("Walking_Down",
                new Animation(new Point(48, 0), new Point(48, 48), 4, 200));
            animations.Add("Walking_Up",
                new Animation(new Point(48, 48), new Point(48, 48), 4, 200));
            animations.Add("Walking_Right",
                new Animation(new Point(48, 96), new Point(48, 48), 4, 200));
            animations.Add("Walking_Left",
                new Animation(new Point(48, 144), new Point(48, 48), 4, 200));
            animations.Add("Idle_Down",
                new Animation(new Point(0, 0), new Point(48, 48), 2, 200));
            animations.Add("Idle_Up",
                new Animation(new Point(0, 48), new Point(48, 48), 2, 200));
            animations.Add("Idle_Right",
                new Animation(new Point(0, 96), new Point(48, 48), 2, 200));
            animations.Add("Idle_Left",
                new Animation(new Point(0, 144), new Point(48, 48), 2, 200));

            CurrentAnimationKey = "Walking_Down";

            inventory = new Inventory(this);

            boundsXOffset = 5;
            boundsYOffset = 4;
            boundsWidthOffset = -10;
            boundsHeightOffset = -4;


            int r = DropItems.Rand.Next(10);
            if (r >= 0) inventory.EquipmentRight.InsertItem(new Flail(content));
            if (r > 4) inventory.EquipmentRight.InsertItem(new SteelSword(content));
            if (r > 7) inventory.EquipmentRight.InsertItem(new ChainSword(content));
            if (r > 8) inventory.EquipmentRight.InsertItem(new Club(content));

            dropPool.AddItem(DropItems.Items["ChainSword"], 8);
            dropPool.AddItem(DropItems.Items["Flail"], 8);
            dropPool.AddItem(DropItems.Items["SteelSword"], 6);
            dropPool.AddItem(DropItems.Items["VikingShield"], 6);
            dropPool.AddItem(DropItems.Items["Club"], 4);
            dropPool.AddItem(DropItems.Items["LeatherBoots"], 4);
            dropPool.AddItem(DropItems.Items["IronGreaves"], 4);
            dropPool.AddItem(DropItems.Items["Potion"], 80);
            dropPool.AddItem(DropItems.Items["ManaPotion"], 30);
        }

        public override CharacterAttribute Level
        {
            set
            {
                int x = value.Value;
                level.Value = value.Value;
                baseHealth = new CharacterAttribute("Health", 160 + x * 7, 160 + x * 7);
                baseDefense = new CharacterAttribute("Defense", 20 + x * 2, 220);
                baseStrength = new CharacterAttribute("Strength", (int)(x * 4), 400);

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
            target = pTarget.Center;

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

                if (d <= 32)
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
                Stop();

                //attack stuff
                attackDelay += DropItems.Rand.Next(8);
                if (attackDelay >= 150)
                {
                    attackDelay = 0;
                    Attack();
                }

                if (inventory.EquipmentRight.Item is Bow)
                {
                    if (d > 120)
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
            (inventory.EquipmentRight.Item as Weapon).Attack(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //spriteBatch.Draw(colTex, Bounds, Color.White);
        }
    }
}
