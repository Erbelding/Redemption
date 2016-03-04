using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using RedemptionEngine.Items.StatusEffects;

namespace RedemptionEngine.ObjectClasses
{
    class LightningBolt : Projectile
    {
        //Attributes

        public int damage = 0;
        private const int BASE_SPEED = 350;
        private double crit;

        public LightningBolt(Character owner, Vector2 direction, int speed, int damageMod)
            :base(owner, BASE_SPEED + speed, direction)
        {
            texture = owner.Content.Load<Texture2D>("Projectiles//bolt sheet");

            animations.Add("Airborn", new Animation(new Point(0, 0), new Point(32, 64), 4, 100));

            CurrentAnimationKey = "Airborn";

            center = owner.Center;
            origin = center;
            position = new Vector2(owner.Center.X - CurrentAnimation.SourceRect.Width / 2, owner.Center.Y - CurrentAnimation.SourceRect.Height / 2);

            //If direction is up
            if (direction == new Vector2(0, -1))
            {
                //Set bounds offsets accordingly
                boundsXOffset = 14;
                boundsYOffset = 0;
                boundsWidthOffset = -28;
                boundsHeightOffset = 0;
            }
            //If direction is down
            else if (direction == new Vector2(0, 1))
            {
                //Set bounds offsets accordingly
                boundsXOffset = 14;
                boundsYOffset = 0;
                boundsWidthOffset = -28;
                boundsHeightOffset = 0;
            }
            //If direction is left
            else if (direction == new Vector2(-1, 0))
            {
                boundsXOffset = -16;
                boundsYOffset = 30;
                boundsWidthOffset = 16;
                boundsHeightOffset = -56;
            }
            //iF direction is right
            else if (direction == new Vector2(1, 0))
            {
                boundsXOffset = -16;
                boundsYOffset = 30;
                boundsWidthOffset =  32;
                boundsHeightOffset = -56;
            }

            crit = (1 + DropItems.Rand.NextDouble() * DropItems.Rand.NextDouble());
            this.damage = (int)(damageMod * owner.Magic.Value * crit);
        }


        public override void HandleRemoval()
        {
            if (character is Player)
            {
                foreach (NPC npc in GameManager.NPCS)
                {
                    if (npc.IsOnScreen())
                    {
                        if (CollidesWith(npc))
                        {
                            //animate
                            gameManager.RemoveEntity(this);
                            //Do damage to that npc
                            if (crit > 1.5f) npc.TakeDamage(character, damage, false, Color.Red);
                            else npc.TakeDamage(character, damage, false);
                        }
                    }
                }
            }
            if (character is NPC)
            {
                if (CollidesWith(gameManager.Player))
                {
                    //animate
                    gameManager.RemoveEntity(this);
                    //Do damage to that npc
                    if (crit > 1.5f) gameManager.Player.TakeDamage(character, damage, false, Color.Red);
                    else gameManager.Player.TakeDamage(character, damage, false);
                }
            }

            if ((!IsOnScreen() || IsCollidingWithMap(character.GameManager.CurrentMap, Bounds)))
            {
                gameManager.RemoveEntity(this);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //Draw collision box
            //spriteBatch.Draw(colTex, Bounds, Color.White);
        }
    }
}
