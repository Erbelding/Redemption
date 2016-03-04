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
    class Fireball : Projectile
    {
        //Attributes
        private Dictionary<string, Texture2D> fireballTextures;

        public int damage = 0;
        private const int BASE_SPEED = 150;
        private double crit;

        public Fireball(Character owner, Vector2 direction, int speed, int damageMod)
            :base(owner, BASE_SPEED + speed, direction)
        {
            fireballTextures = new Dictionary<string, Texture2D>();

            fireballTextures.Add("Level1", owner.Content.Load<Texture2D>("Projectiles//fireballLevel1"));
            fireballTextures.Add("Level2", owner.Content.Load<Texture2D>("Projectiles//fireballLevel2"));
            fireballTextures.Add("Level3", owner.Content.Load<Texture2D>("Projectiles//fireballLevel3"));
            fireballTextures.Add("Level4", owner.Content.Load<Texture2D>("Projectiles//fireballLevel4"));

            texture = fireballTextures["Level1"];

            animations.Add("Airborn",
                new Animation(new Point(0, 0), new Point(32, 32), 3, 100));
            animations.Add("Hit",
                new Animation(new Point(96, 0), new Point(32, 32), 2, 100));

            CurrentAnimationKey = "Airborn";

            center = owner.Center;
            origin = center;
            position = new Vector2(owner.Center.X - CurrentAnimation.SourceRect.Width / 2, owner.Center.Y - CurrentAnimation.SourceRect.Height / 2);

            //If direction is up
            if (direction == new Vector2(0, -1))
            {
                //Set bounds offsets accordingly
                boundsXOffset = 12;
                boundsYOffset = 0;
                boundsWidthOffset = -26;
                boundsHeightOffset = 0;
            }
            //If direction is down
            else if (direction == new Vector2(0, 1))
            {
                //Set bounds offsets accordingly
                boundsXOffset = 15;
                boundsYOffset = 0;
                boundsWidthOffset = -26;
                boundsHeightOffset = 0;
            }
            //If direction is left
            else if (direction == new Vector2(-1, 0))
            {
                boundsXOffset = 0;
                boundsYOffset = 15;
                boundsWidthOffset = 0;
                boundsHeightOffset = -26;
            }
            //iF direction is right
            else if (direction == new Vector2(1, 0))
            {
                boundsXOffset = 0;
                boundsYOffset = 12;
                boundsWidthOffset = 0;
                boundsHeightOffset = -26;
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
                        if (CollidesWith(npc) && CurrentAnimationKey != "Hit")
                        {
                            //animate
                            CurrentAnimationKey = "Hit";
                            velocity.Normalize();
                            velocity = velocity * 20;
                            //Do damage to that npc
                            if (crit > 1.5f) npc.TakeDamage(character, damage, false, Color.Red);
                            else npc.TakeDamage(character, damage, false);
                            npc.AddEffect(new Burn("Burn", npc, character, 1, 1000, 20000));
                        }
                    }
                }
            }
            if (character is NPC)
            {
                    if (CollidesWith(gameManager.Player) && CurrentAnimationKey != "Hit")
                    {
                        //animate
                        CurrentAnimationKey = "Hit";
                        velocity.Normalize();
                        velocity = velocity * 20;
                        //Do damage to that npc
                        if (crit > 1.5f) gameManager.Player.TakeDamage(character, damage, false, Color.Red);
                        else gameManager.Player.TakeDamage(character, damage, false);
                        gameManager.Player.AddEffect(new Burn("Burn", gameManager.Player, character, 1, 1000, 20000));
                    }
            }

            if ((!IsOnScreen() || IsCollidingWithMap(character.GameManager.CurrentMap, Bounds)) && CurrentAnimationKey != "Hit")
            {
                CurrentAnimationKey = "Hit";
                velocity.Normalize();
                velocity = velocity * 20;
            }
            if (CurrentAnimationKey == "Hit" && CurrentAnimation.PlayCount == 1 && CurrentAnimation.CurrentFrameIndex == 0) gameManager.RemoveEntity(this);
            //crit colors red
            
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
