using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.Screens
{
    class TitleScreen: GameScreen
    {
        private SpriteFont titleFont;

        public TitleScreen(ScreenManager screenManager):base(screenManager)
        {
            titleFont = screenManager.Content.Load<SpriteFont>("TitleFont");
            background = screenManager.Content.Load<Texture2D>("MenuScreen");
        }

        //all screens must implement update and draw

        #region Update

        public override void UpdateActive()
        {
            if (Controller.SingleKeyPress(Keys.Enter)) screenManager.OpenNextScreen(this, screenManager.MainMenuScreen);
            if (Controller.SingleKeyPress(Keys.Escape)) screenManager.Game.Exit();
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
            spriteBatch.Draw(background, screenManager.Game.GraphicsDevice.Viewport.Bounds , Color.White * transitionTime);
            spriteBatch.DrawString(titleFont, "REDEMPTION", new Vector2(screenManager.Game.GraphicsDevice.Viewport.Width/3, 20f * transitionTime), Color.Black * transitionTime);
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
