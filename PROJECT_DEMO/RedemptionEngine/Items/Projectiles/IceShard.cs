using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.Items.Projectiles
{
    class IceShard : Projectile
    {
        public int damage = 0;
        private const int BASE_SPEED = 300;
        private double crit;


        public IceShard(Character owner, Vector2 direction, int speed, int damageMod)
            : base(owner, BASE_SPEED + speed, direction)
        {
            texture = owner.Content.Load<Texture2D>("Projectiles//Icicle");


            center = owner.Center;
            origin = center;
            position = new Vector2(owner.Center.X - texture.Width / 2, owner.Center.Y - texture.Height / 2);

            //If direction is up
            if (owner.Dir == Direction.UP)
            {
                //Set bounds offsets accordingly
                boundsXOffset = 12;
                boundsYOffset = 0;
                boundsWidthOffset = -26;
                boundsHeightOffset = 0;
            }
            //If direction is down
            else if (owner.Dir == Direction.DOWN)
            {
                //Set bounds offsets accordingly
                boundsXOffset = 15;
                boundsYOffset = 0;
                boundsWidthOffset = -26;
                boundsHeightOffset = 0;
            }
            //If direction is left
            else if (owner.Dir == Direction.LEFT)
            {
                boundsXOffset = 0;
                boundsYOffset = 15;
                boundsWidthOffset = 0;
                boundsHeightOffset = -26;
            }
            //iF direction is right
            else if (owner.Dir == Direction.RIGHT)
            {
                boundsXOffset = 0;
                boundsYOffset = 12;
                boundsWidthOffset = 0;
                boundsHeightOffset = -26;
            }

            crit = (1 + DropItems.Rand.NextDouble() * DropItems.Rand.NextDouble());
            this.damage = (int)(damageMod * owner.Magic.Value * crit);


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
                        //npc.AddEffect(new Poison("Poison",npc, character, 1, 2000, 20000));
                        GameManager.RemoveEntity(this);
                    }
                }
            }
            if (character is NPC)
            {
                if (CollidesWith(gameManager.Player))
                {
                    //Do damage to that npc
                    if (crit > 1.5f) gameManager.Player.TakeDamage(character, damage, false, Color.Red);
                    else gameManager.Player.TakeDamage(character, damage, false);
                    //npc.AddEffect(new Poison("Poison",npc, character, 1, 2000, 20000));
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
