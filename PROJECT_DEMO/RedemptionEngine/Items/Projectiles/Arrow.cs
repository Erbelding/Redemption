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
    class Arrow: Projectile
    {
        private Dictionary<String, Texture2D> arrowTextures;

        public int damage = 0;
        private const int BASE_SPEED = 200;
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

        public void SetArrowType(string name)
        {
            if(arrowTextures.ContainsKey(name)) texture = arrowTextures[name];
        }

        public Arrow(Character owner, Vector2 direction, int speed, int damageMod)
            :base(owner, BASE_SPEED + speed, direction)
        {
            arrowTextures = new Dictionary<string, Texture2D>();

            arrowTextures.Add("Arrow", owner.Content.Load<Texture2D>("Projectiles//arrow"));
            arrowTextures.Add("PoisonArrow", owner.Content.Load<Texture2D>("Projectiles//PoisonArrow"));

            texture = arrowTextures["Arrow"];

            center = owner.Center;
            origin = center;
            position = new Vector2(owner.Center.X - texture.Width / 2, owner.Center.Y - texture.Height / 2);

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
                        if (texture == arrowTextures["PoisonArrow"])
                        {
                            npc.AddEffect(new Poison("Poison", npc, character, effectDamage, effectFrequency, effectDuration));
                        }
                        GameManager.RemoveEntity(this);
                    }
                }
            }
            if (character is NPC)
            {
                if (CollidesWith(gameManager.Player))
                {
                    //Do damage to the player
                    if (crit > 1.5f) gameManager.Player.TakeDamage(character, damage, false, Color.Red);
                    else gameManager.Player.TakeDamage(character, damage, false);
                    if (texture == arrowTextures["PoisonArrow"])
                    {
                        gameManager.Player.AddEffect(new Poison("Poison", gameManager.Player, character, effectDamage, effectFrequency, effectDuration));
                    }
                    GameManager.RemoveEntity(this);
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
