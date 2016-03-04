using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RedemptionEngine.Items.StatusEffects;

namespace RedemptionEngine.Items.Projectiles
{
    class Snowball: Projectile
    {
        //Attributes

        public int damage = 0;
        private const int BASE_SPEED = 500;
        private double crit;

        public Snowball(Character owner, Vector2 direction, int speed, int damageMod)
            :base(owner, BASE_SPEED + speed, direction)
        {
            texture = owner.Content.Load<Texture2D>("Projectiles//Snowball");

            animations.Add("Shot", new Animation(new Point(0, 0), new Point(32, 32), 2, 50));
            CurrentAnimationKey = "Shot";

            

            center = owner.Center;
            origin = center;
            position = new Vector2(owner.Center.X - CurrentAnimation.SourceRect.Width / 2, owner.Center.Y - CurrentAnimation.SourceRect.Height / 2);

            boundsXOffset = 12;
            boundsYOffset = 12;
            boundsWidthOffset = -24;
            boundsHeightOffset = -24;

            crit = (1 + DropItems.Rand.NextDouble() * DropItems.Rand.NextDouble());
            this.damage = (int)(owner.Magic.Value * damageMod * crit);
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (character is NPC)
            {
                if (CollidesWith(gameManager.Player))
                {
                    //Do damage to that npc
                    if (crit > 1.5f)
                    {
                        gameManager.Player.TakeDamage(character, damage, false, Color.Red);
                    }
                    else gameManager.Player.TakeDamage(character, damage, false);
                    GameManager.RemoveEntity(this);
                }
            }
            if (character is Player)
            {
                foreach (NPC n in gameManager.NPCS)
                {
                    if (CollidesWith(n))
                    {
                        //Do damage to that npc
                        if (crit > 1.5f)
                        {
                            n.TakeDamage(character, damage, false, Color.Red);
                        }
                        else n.TakeDamage(character, damage, false);
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
