using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.Items
{
    class WeaponSwing: Projectile
    {

        protected float swingSpeed;
        protected float currentArc = 0;
        protected float arc;
        protected Weapon weapon;
        protected Vector2 offset;

        public WeaponSwing(Weapon weapon, Character owner, Vector2 origin, Vector2 offset, float swingSpeed, Vector2 direction, float arc)
            :base(owner, 0, direction)
        {
            this.offset = offset;
            this.origin.X = origin.X;
            this.origin.Y = origin.Y * 2;
            this.swingSpeed = swingSpeed;
            this.arc = arc;
            this.weapon = weapon;
            if (weapon.CurrentAnimation != null)
            {
                animations.Add("WeaponSwing", weapon.GetAnimation("WeaponSwing"));
                CurrentAnimationKey = "WeaponSwing";
            }
            texture = weapon.Texture;
            

            this.position = new Vector2(character.Position.X + character.Bounds.Width/2, character.Position.Y + character.Bounds.Height/2);
            this.center = this.position + this.offset;


            boundsXOffset -= Bounds.Width/2;
            boundsYOffset -= Bounds.Height/2;

            int height;
            if (CurrentAnimation != null) height = CurrentAnimation.SourceRect.Height;
            else height = texture.Height;

            //Determine bounding box based on character direction
            if (owner.Dir == Entity.Direction.RIGHT)
            {
                boundsXOffset += height * 5 / 4;
                boundsWidthOffset = -height / 2;
                boundsYOffset += -height / 4;
                boundsHeightOffset = height / 2;
            }
            if (owner.Dir == Entity.Direction.LEFT)
            {
                boundsXOffset += -height * 3 / 4;
                boundsWidthOffset = -height / 2;
                boundsYOffset += -height / 4;
                boundsHeightOffset = height / 2;
            }
            if (owner.Dir == Entity.Direction.UP)
            {
                boundsYOffset += -height * 3 / 4;
                boundsHeightOffset = -height / 2;
                boundsXOffset += -height / 4;
                boundsWidthOffset = height / 2;
            }
            if (owner.Dir == Entity.Direction.DOWN)
            {
                boundsYOffset += height * 5 / 4;
                boundsHeightOffset = -height / 2;
                boundsXOffset += -height / 4;
                boundsWidthOffset = height / 2;
            }

        }

        public override void Update(GameTime gameTime)
        {
            //rotate
            this.position = character.Center;
            this.center = this.position + this.offset;
            rotation += swingSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            currentArc += swingSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        public override void HandleRemoval()
        {
            if (arc <= Math.Abs(currentArc))
            {
                gameManager.RemoveEntity(this);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            //If it is animated we draw using the current animation source rectangle
            if (animations.Count > 0)
                spriteBatch.Draw(Texture, center, CurrentAnimation.SourceRect,
                    Tint * Alpha, Rotation, origin, Scale, SpriteEffects.None, 0.0f);
            //Otherwise, it is a static object so source rect will be null
            else
                spriteBatch.Draw(Texture, center, null,
                    Tint * Alpha, Rotation, origin, Scale, SpriteEffects.None, 0.0f);

            //spriteBatch.Draw(colTex, Bounds, Color.White);
        }
    }
}
