using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.ObjectClasses
{
    public class InfinityAnimation : Entity
    {
        public InfinityAnimation(ContentManager content)
            : base(content)
        {
            texture = content.Load<Texture2D>("GUI//LoadingSprite");

            animations.Add("Loop",
                new Animation(new Point(0, 0), new Point(32, 32), 13, 40));

            scale = 1.5f;
            rotation = MathHelper.PiOver2;

            CurrentAnimationKey = "Loop";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //rotation += 1 * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

    }
}
