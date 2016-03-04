using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.ObjectClasses
{
    public abstract class GameObject
    {
        //Attributes
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 origin;
        protected Rectangle bounds;
        protected Color tint;
        protected float speed;
        protected float rotation;
        protected float scale;
        protected float alpha;
        protected bool visible;

        //Properties
        public virtual Texture2D Texture { get { return texture; } }
        public virtual Vector2 Position { get { return position; } }
        public virtual Vector2 Velocity { get { return velocity; } }
        public virtual Vector2 Origin { get { return origin; } }
        public virtual Rectangle Bounds { get { return bounds; } }
        public virtual Color Tint { get { return tint; } set { tint = value; } }
        public virtual float Speed { get { return speed; } set { speed = value; } }
        public virtual float Rotation { get { return rotation; } set { rotation = value; } }
        public virtual float Scale { get { return scale; } set { scale = value; } }
        public virtual float Alpha { get { return alpha; } set { alpha = MathHelper.Clamp(value, 0.0f, 1.0f); } }
        public virtual bool Visible { get { return visible; } set { visible = value; } }

        //Methods
        public virtual bool CollidesWith(GameObject obj) { return false; }
        public virtual void Move(float dt) { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }


    }
}
