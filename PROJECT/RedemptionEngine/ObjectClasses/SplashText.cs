using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.TileEngine;

namespace RedemptionEngine.ObjectClasses
{
    class SplashText: Entity
    {
        private static SpriteFont font;
        private string text;
        private float timer;
        private float initTime;
        private float xOffset;
        #region constructors
        public SplashText(GameManager gameManager, Vector2 position, string text, Color color, float time)
        {
            if (font == null)
            {
                font = gameManager.gamePlayScreen.ScreenManager.Content.Load<SpriteFont>("Fonts//SplashText");
            }
            this.gameManager = gameManager;
            xOffset = (float)(new Random().NextDouble() * 6) - 3;
            this.position = position;
            this.position.X -= font.MeasureString(text).X/2;
            this.text = text;
            tint = color;
            timer = time;
            initTime = time;
            
            
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            timer -= (float)(gameTime.ElapsedGameTime.TotalSeconds);
            alpha = 5 * timer / initTime;
            position.Y -= (float)Math.Pow(timer / initTime, 3) * 3;
            position.X += (float)Math.Pow(timer / initTime, 3) * xOffset;
            if (timer <= 0)
            {
                gameManager.RemoveEntity(this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, tint * alpha);
        }
    }
}
