/**
 * Matt Guerrette
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RedemptionEngine.Screens;

namespace RedemptionEngine.Items.Weapons
{
    public class SlowPoke : Weapon
    {
        public SlowPoke(ContentManager content)
            : base(content)
        {
            name = "The Slow Poke";
            type = ItemType.WEAPON;
            animations.Add("Inventory", new Animation(new Point(0, 0), new Point(32, 32), 1, 1000));
            animations.Add("Use", new Animation(new Point(0, 0), new Point(32, 32), 1, 1000));
            CurrentAnimationKey = "Inventory";
            texture = content.Load<Texture2D>("Items//Weapons//The Slow Poke");
            
            AddModifier("Defense + 15", "Defense", 15, false);
        }


        public override void Update(GameTime gameTime, Character owner, Keys key)
        {
            base.Update(gameTime);

            if (Controller.KeyIsDown(key))
            {
                owner.Speed = 0;

                active = true;
                CurrentAnimationKey = "Use";
                position = owner.Position;

                owner.Overlay = this.Texture;

                boundsXOffset = -5;
                boundsYOffset = -5;
                boundsWidthOffset = 10;
                boundsHeightOffset = 10;

                Attack(owner);
            }

            if (Controller.OnKeyReleased(key))
            {
                owner.Overlay = null;
                CurrentAnimationKey = "Inventory";
                active = false;
                rotation = 0;
            }

        }

        public override void Attack(Character owner)
        {
            if (gameManager == null)
            {
                gameManager = owner.GameManager;
            }


            foreach (Projectile projectile in gameManager.Projectiles)
            {
                if (owner is Player && projectile.Character is NPC)
                {
                    if (this.CollidesWith(projectile)) gameManager.RemoveEntity(projectile);
                }
                if (owner is NPC && projectile.Character is Player)
                {
                    if (this.CollidesWith(projectile)) gameManager.RemoveEntity(projectile);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //spriteBatch.Draw(colTex, Bounds, Color.White);
        }
    }
}
