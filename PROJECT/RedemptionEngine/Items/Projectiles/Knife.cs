using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.Items.StatusEffects;

namespace RedemptionEngine.Items.Projectiles
{
    class Knife: Projectile
    {

        public int damage = 0;
        private const int BASE_SPEED = 150;
        private double crit;

        private int effectDamage;
        private int effectFrequency;
        private int effectDuration;

        public void SetEffect(int dmg, int freq, int time)
        {
            effectDamage = dmg;
            effectFrequency = freq;
            effectDuration = time;
        }


        public Knife(Character owner, Vector2 direction, int speed, int damageMod)
            :base(owner, BASE_SPEED + speed, direction)
        {

            texture = owner.Content.Load<Texture2D>("Items//Weapons//Throwing Knife");

            AddAnimation("Spin", new Animation(new Point(32, 0), new Point(32, 32), 4, 200));
            CurrentAnimationKey = "Spin";


            center = owner.Center;
            origin = center;
            position = owner.Position;

            //If direction is up
            if (direction == new Vector2(0, -1))
            {
                //Set bounds offsets accordingly
                boundsXOffset = 8;
                boundsYOffset = 8;
                boundsWidthOffset = -16;
                boundsHeightOffset = -16;
            }
            //If direction is down
            else if (direction == new Vector2(0, 1))
            {
                //Set bounds offsets accordingly
                boundsXOffset = 8;
                boundsYOffset = 8;
                boundsWidthOffset = -16;
                boundsHeightOffset = -16;
            }
            //If direction is left
            else if (direction == new Vector2(-1, 0))
            {
                boundsXOffset = 8;
                boundsYOffset = 8;
                boundsWidthOffset = -16;
                boundsHeightOffset = -16;
            }
            //iF direction is right
            else if (direction == new Vector2(1, 0))
            {
                boundsXOffset = 8;
                boundsYOffset = 8;
                boundsWidthOffset = -16;
                boundsHeightOffset = -16;
            }
            
            crit = (1 + DropItems.Rand.NextDouble() * DropItems.Rand.NextDouble());
            this.damage = (int)(damageMod * owner.Strength.Value * crit);

            
        }


        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (character is Player)
            {
                foreach (NPC npc in GameManager.NPCS)
                {
                    if (CollidesWith(npc))
                    {
                        //Do damage to that npc
                        if (crit > 1.5f) npc.TakeDamage(character, damage, false, Color.Red);
                        else npc.TakeDamage(character, damage, false);
                        GameManager.RemoveEntity(this);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //Draw collision box
            //spriteBatch.Draw(colTex, Bounds, Color.White);
        }
    }
}
