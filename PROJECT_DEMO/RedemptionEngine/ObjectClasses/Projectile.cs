using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using RedemptionEngine.TileEngine;
using RedemptionEngine.Items.Projectiles;

namespace RedemptionEngine.ObjectClasses
{
    class Projectile: Entity
    {
        //attributes
        protected Character character;

        public Character Character { get { return character; } }

        public Projectile(Character owner, float speed, Vector2 direction)
            :base(owner.Content)
        {
            rotation = (float)Math.Atan2(direction.X, -direction.Y);
            this.gameManager = owner.GameManager;
            this.character = owner;
            this.speed = speed;
            velocity = direction * speed;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //move
            position.X += velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            HandleRemoval();
        }

        public virtual void HandleRemoval()
        {
            if (!IsOnScreen() || IsCollidingWithMap(character.GameManager.CurrentMap, Bounds))
            {
                gameManager.RemoveEntity(this);
            }
        }

    }
}
