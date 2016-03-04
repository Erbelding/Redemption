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
    class Dart: Projectile
    {

        public int damage = 0;
        private const int BASE_SPEED = 250;
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


        public Dart(Character owner, Vector2 direction, int speed, int damageMod)
            :base(owner, BASE_SPEED + speed, direction)
        {

            texture = owner.Content.Load<Texture2D>("Projectiles//Dart");

            center = owner.Center;
            origin = center;
            position = new Vector2(owner.Center.X - texture.Width / 2, owner.Center.Y - texture.Height / 2);


            //Set bounds offsets accordingly
            boundsXOffset = 12;
            boundsYOffset = 12;
            boundsWidthOffset = -26;
            boundsHeightOffset = -26;
            

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
                        npc.AddEffect(new Poison("Poison", npc, character, effectDamage, effectFrequency, effectDuration));

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
                    gameManager.Player.AddEffect(new Poison("Poison", gameManager.Player, character, effectDamage, effectFrequency, effectDuration));

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
