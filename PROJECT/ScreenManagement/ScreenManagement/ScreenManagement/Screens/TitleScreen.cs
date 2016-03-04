using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ScreenManagement
{
    class TitleScreen: GameScreen
    {
        private SpriteFont font;

        public TitleScreen(ScreenManager screenManager):base(screenManager)
        {
            font = screenManager.Content.Load<SpriteFont>("Font");
            background = screenManager.Content.Load<Texture2D>("MenuScreen");
        }

        //all screens must implement update and draw

        #region Update

        public override void UpdateActive()
        {
            if (Controller.SingleKeyPress(Keys.Enter)) screenManager.OpenNextScreen(this, screenManager.MainMenuScreen);
            if (Controller.SingleKeyPress(Keys.Escape)) screenManager.ScreenManagerTest.Exit();
        }

        public override void UpdateTransitionOn()
        {
            transitionTime += .02f;
            if(transitionTime >= 1) screenState = ScreenState.Active;
        }

        public override void UpdateTransitionOff()
        {
            transitionTime -= .02f;
            if (transitionTime <= 0) screenState = ScreenState.Off;
        }

        #endregion

        #region Draw

        public override void DrawPaused(SpriteBatch spriteBatch)
        {
            //This Screen Should Never Be Paused
        }

        public override void DrawActive(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, screenManager.ScreenManagerTest.GraphicsDevice.Viewport.Bounds , Color.White * transitionTime);
            spriteBatch.DrawString(font, "Title Screen", new Vector2(20f, 20f * transitionTime), Color.White * transitionTime);
        }

        public override void DrawTransitionOn(SpriteBatch spriteBatch)
        {
            DrawActive(spriteBatch);
        }

        public override void DrawTransitionOff(SpriteBatch spriteBatch)
        {
            DrawActive(spriteBatch);
        }

        #endregion

    }
}
