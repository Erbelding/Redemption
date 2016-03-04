using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System.Threading;
using ScreenManagement.GameEngine;

namespace ScreenManagement
{
    public class GamePlayScreen: GameScreen
    {
        
        //attributes
        private bool loading;
        private Level level;
        private SpriteFont font;

        //properties
        public bool Loading
        {
            get { return loading; }
            set { loading = value; }
        }

        public GamePlayScreen(ScreenManager screenManager):base(screenManager)
        {
            font = screenManager.Content.Load<SpriteFont>("Font");
            background = screenManager.Content.Load<Texture2D>("GameScreen");
        }
        

        #region Update

        public override void UpdateActive()
        {

            if (Controller.SingleKeyPress(Keys.Escape)) screenManager.OpenPopupScreen(this, screenManager.PauseMenuScreen);
            if (Controller.SingleKeyPress(Keys.I)) screenManager.OpenPopupScreen(this, screenManager.InventoryScreen);
        }

        

        public override void UpdateTransitionOn()
        {
            transitionTime += .02f;
            if (transitionTime >= 1) screenState = ScreenState.Active;
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
            spriteBatch.Draw(background, screenManager.ScreenManagerTest.GraphicsDevice.Viewport.Bounds, Color.White * .5f);
            spriteBatch.DrawString(font, "GamePlayScreen", new Vector2(60f, 60f), Color.LightBlue * .5f );
        }

        public override void DrawActive(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, screenManager.ScreenManagerTest.GraphicsDevice.Viewport.Bounds, Color.White * transitionTime);
            spriteBatch.DrawString(font, "Gameplay Screen", new Vector2(60f, 60f), Color.White * transitionTime);
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

        #region LoadContent

        public void LoadContent(string level)
        {
            this.level = new Level(this, "", "");
        }
        #endregion
    }
}
