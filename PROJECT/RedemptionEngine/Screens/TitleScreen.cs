/**
 * David Erbelding
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace RedemptionEngine.Screens
{
    class TitleScreen: GameScreen
    {
        private SpriteFont titleFont;
        private float time;

        //the constructor just loads the images the screen has
        public TitleScreen(ScreenManager screenManager):base(screenManager)
        {
            time = 0;
            titleFont = screenManager.Content.Load<SpriteFont>("Fonts//TitleFont");
            background = screenManager.Content.Load<Texture2D>("GUI//Title");
        }

        //all screens must implement update and draw

        #region Update

        public override void UpdateActive(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time > 3)
            {
                time = 0;
                screenManager.OpenNextScreen(this, screenManager.MainMenuScreen);
            }
            if (Controller.SingleKeyPress(Keys.Enter))
            {
                time = 0;
                screenManager.OpenNextScreen(this, screenManager.MainMenuScreen);
            }
            if (Controller.SingleKeyPress(Keys.Escape)) screenManager.Game.Exit();
        }

        //what the screen does when it's transitioning on
        public override void UpdateTransitionOn()
        {
            transitionTime += .02f;
            if (transitionTime >= 1)
            {
                GameAudio.PlaySong("MenuTheme", true);
                MediaPlayer.Volume = GameAudio.MusicVolume;
                screenState = ScreenState.Active;
            }
        }

        //what the screen does when it's transitioning off
        public override void UpdateTransitionOff()
        {
            transitionTime -= .02f;
            if (transitionTime <= 0) screenState = ScreenState.Off;
        }

        #endregion

        #region Draw

        //required to be implemented from gamescreen
        public override void DrawPaused(SpriteBatch spriteBatch)
        {
            //This Screen Should Never Be Paused
        }

        //when the screen is active it just displays an image and the background
        public override void DrawActive(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, screenManager.Game.GraphicsDevice.Viewport.Bounds , Color.White * transitionTime);
            //spriteBatch.DrawString(titleFont, "REDEMPTION", new Vector2(screenManager.Game.GraphicsDevice.Viewport.Width/3, 20f * transitionTime), Color.Black * transitionTime);
        }

        //the draw for transitioning on and off does the same thing as when it's active
        public override void DrawTransitionOn(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawActive(gameTime, spriteBatch);
        }

        public override void DrawTransitionOff(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawActive(gameTime, spriteBatch);
        }

        #endregion

    }
}
