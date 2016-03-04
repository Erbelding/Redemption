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
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework.Media;

namespace RedemptionEngine.Screens
{
    public class LoadingScreen: GameScreen
    {
        private SpriteFont font;
        private InfinityAnimation animation;

        //load content
        public LoadingScreen(ScreenManager screenManager):base(screenManager)
        {
            font = screenManager.Content.Load<SpriteFont>("Fonts//Font");
            background = screenManager.Content.Load<Texture2D>("GUI//Menu");

            animation = new InfinityAnimation(ScreenManager.Content);
            animation.Position = new Vector2(700, 500);
        }


        #region Update

        public override void UpdateActive(GameTime gameTime)
        {
            //close the loading screen once the gameplay screen has loaded
            GamePlayScreen gScreen = (GamePlayScreen)screenManager.GamePlayScreen;
            if (!gScreen.Loading)
            {
                

                screenManager.ClosePopupScreen(this);
                screenManager.BringScreenToFront(this);
            }
        }

        public override void UpdateTransitionOn()
        {
            transitionTime += .02f;
            MediaPlayer.Volume = (1 - transitionTime) * GameAudio.MusicVolume;

            if (transitionTime >= 1)
            {
                
                screenState = ScreenState.Active;
            }
        }

        public override void UpdateTransitionOff()
        {
            transitionTime -= .02f;
            MediaPlayer.Volume = (1 - transitionTime) * GameAudio.MusicVolume;
            if (transitionTime <= 0)
            {
                screenState = ScreenState.Off;
            }
        }

        #endregion

        #region Draw

        public override void DrawPaused(SpriteBatch spriteBatch)
        {
            //This Screen Should Never Be Paused
        }

        public override void DrawActive(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animation.Update(gameTime);

            animation.Alpha = transitionTime;

            //draw something for us to look at
            spriteBatch.Draw(background, screenManager.Game.GraphicsDevice.Viewport.Bounds, Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Loading", new Vector2(40f, 40f), Color.White * transitionTime);
            animation.Draw(spriteBatch);
        }

        //draw transitions just do the same thing as the active one
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
