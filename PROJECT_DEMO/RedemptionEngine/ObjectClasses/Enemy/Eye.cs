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
    public class Eye : Hostile
    {
        public Eye(ContentManager content)
            : base(content)
        {
            name = "Eye";
            speed = 150.0f;
            texture = content.Load<Texture2D>("Characters//eye");

            //Add animations
            animations.Add("Walking_Down",
                new Animation(new Point(0, 0), new Point(32, 32), 2, 200));
            animations.Add("Walking_Up",
                new Animation(new Point(128, 0), new Point(32, 32), 2, 200));
            animations.Add("Walking_Right",
                new Animation(new Point(64, 0), new Point(32, 32), 2, 200));
            animations.Add("Walking_Left",
                new Animation(new Point(192, 0), new Point(32, 32), 2, 200));

            CurrentAnimationKey = "Walking_Down";

            inventory = new Inventory(this);

            boundsXOffset = 5;
            boundsYOffset = 4;
            boundsWidthOffset = -10;
            boundsHeightOffset = -4;


            if(DropItems.Rand.Next(4) > 0) inventory.EquipmentRight.InsertItem(new FireballShot(content));
            else inventory.EquipmentRight.InsertItem(new LightBolt(content));


            dropPool.AddItem(DropItems.Items["FireballShot"], 5);
            dropPool.AddItem(DropItems.Items["Potion"], 60);
            dropPool.AddItem(DropItems.Items["SuperHealthPotion"], 10);
            dropPool.AddItem(DropItems.Items["ManaPotion"], 60);
            dropPool.AddItem(DropItems.Items["SuperManaPotion"], 10);
            dropPool.AddItem(DropItems.Items["LightBolt"], 5);
            dropPool.AddItem(DropItems.Items["MarkerOfCensorship"], 1);
            dropPool.AddItem(DropItems.Items["ChainGuardChest"], 1);
            dropPool.AddItem(DropItems.Items["RaySword"], 3);
        }

        public override CharacterAttribute Level
        {
            set
            {
                int x = value.Value;
                level.Value = value.Value;
                baseHealth = new CharacterAttribute("Health", 120 + x * 3, 120 + x * 3);
                baseDefense = new CharacterAttribute("Defense", 10 + x * 2, 210);
                baseStrength = new CharacterAttribute("Strength", (int)(x * 3), 300);
                baseMagic = new CharacterAttribute("Magic", (int)(x * 4), 400);
                InitializeAttributes();
            }
        }

        public override void Die(Character attacker)
        {
            int level = Level.Value;
            int expAmount = (int)( 2.3 *Math.Pow(1.15, level));
            attacker.AddExperience(expAmount);
            GameManager.RemoveEntity(this);
            DropItem(.6f);
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

                if (d <= 120)
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
                LineUpWithTarget();

                //attack stuff
                attackDelay += DropItems.Rand.Next(8);
                if (attackDelay >= 150)
                {
                    attackDelay = 0;
                    Attack();
                }

                if (d > 120)
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
