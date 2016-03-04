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
    public class VikingShield : Weapon
    {
        public VikingShield(ContentManager content)
            : base(content)
        {
            name = "Viking Shield";
            type = ItemType.WEAPON;
            animations.Add("Inventory", new Animation(new Point(0, 0), new Point(32, 32), 1, 1000));
            animations.Add("Use", new Animation(new Point(32, 0), new Point(32, 32), 1, 1000));
            CurrentAnimationKey = "Inventory";
            texture = content.Load<Texture2D>("Items//Weapons//Viking Shield");

            AddModifier("Defense + 7", "Defense", 7, false);
            AddModifier("Strength - 4", "Strength", -4, false);
        }

        public override void Update(GameTime gameTime, Character owner, Keys key)
        {
            base.Update(gameTime);
            if (Controller.KeyIsDown(key))
            {
                active = true;
                CurrentAnimationKey = "Use";

                if (owner.Dir == Direction.LEFT)
                {
                    rotation = -MathHelper.PiOver2;
                    position.X = owner.Center.X - Bounds.Width - owner.Origin.X;
                    position.Y = owner.Center.Y - origin.Y;
                    alpha = 1;
                    boundsYOffset = 0;
                    boundsHeightOffset = 0;
                    boundsXOffset = (int)origin.Y;
                    boundsWidthOffset = -(int)origin.Y / 2;
                }

                if (owner.Dir == Direction.UP)
                {
                    rotation = 0;
                    position.X = owner.Center.X - origin.X;
                    position.Y = owner.Center.Y - Bounds.Height - owner.Origin.Y;
                    alpha = 1;
                    boundsXOffset = 0;
                    boundsWidthOffset = 0;
                    boundsYOffset = (int)Origin.Y;
                    boundsHeightOffset = -(int)Origin.Y / 2;
                }

                if (owner.Dir == Direction.RIGHT)
                {
                    rotation = MathHelper.PiOver2;
                    position.X = owner.Center.X + owner.Origin.X;
                    position.Y = owner.Center.Y - Origin.Y;
                    alpha = 1;
                    boundsYOffset = 0;
                    boundsHeightOffset = 0;
                    boundsXOffset = -(int)Origin.Y/2;
                    boundsWidthOffset = -(int)Origin.Y/2;
                }

                if (owner.Dir == Direction.DOWN)
                {
                    rotation = MathHelper.Pi;
                    position.X = owner.Center.X - Origin.X;
                    position.Y = owner.Center.Y + owner.Origin.Y;
                    alpha = 1;
                    boundsXOffset = 0;
                    boundsWidthOffset = 0;
                    boundsYOffset = -(int)Origin.Y / 2;
                    boundsHeightOffset = -(int)Origin.Y / 2;
                }

                Attack(owner);
            }

            if (Controller.OnKeyReleased(key))
            {
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
