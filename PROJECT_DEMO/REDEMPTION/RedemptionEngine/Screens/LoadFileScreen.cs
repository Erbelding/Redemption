using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.Screens
{
    class LoadFileScreen : GameScreen
    {
        enum MenuStates { File1, File2, File3, Back };
        private MenuStates menuState;
        private SpriteFont font;

        public LoadFileScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            font = screenManager.Content.Load<SpriteFont>("Font");
            background = screenManager.Content.Load<Texture2D>("MenuScreen");
        }


        #region Update

        public override void UpdateActive()
        {
            if (Controller.SingleKeyPress(Keys.Escape)) screenManager.OpenPreviousScreen(this);

            switch (menuState)
            {
                case MenuStates.File1:
                    if (Controller.SingleKeyPress(Keys.W)) menuState = MenuStates.Back;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter))
                    {
                        screenManager.OpenNextScreen(this, screenManager.GamePlayScreen);
                        screenManager.OpenNextScreen(screenManager.GamePlayScreen, screenManager.LoadingScreen);
                        GamePlayScreen gameScreen = (GamePlayScreen)screenManager.GamePlayScreen;
                        gameScreen.LoadGame("Game1");
                    }
                    break;
                case MenuStates.File2:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter))
                    {
                        screenManager.OpenNextScreen(this, screenManager.GamePlayScreen);
                        screenManager.OpenNextScreen(screenManager.GamePlayScreen, screenManager.LoadingScreen);
                        GamePlayScreen gameScreen = (GamePlayScreen)screenManager.GamePlayScreen;
                        gameScreen.LoadGame("Game2");
                    }
                    break;
                case MenuStates.File3:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter))
                    {
                        screenManager.OpenNextScreen(this, screenManager.GamePlayScreen);
                        screenManager.OpenNextScreen(screenManager.GamePlayScreen, screenManager.LoadingScreen);
                        GamePlayScreen gameScreen = (GamePlayScreen)screenManager.GamePlayScreen;
                        gameScreen.LoadGame("Game3");
                    }
                    break;
                case MenuStates.Back:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState = MenuStates.File1;
                    if (Controller.SingleKeyPress(Keys.Enter)) screenManager.OpenPreviousScreen(this);
                    break;
            }




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
            spriteBatch.Draw(background, screenManager.Game.GraphicsDevice.Viewport.Bounds, Color.White * .5f);
            spriteBatch.DrawString(font, "Select a File to Play", new Vector2(40f, 40f), Color.Black * .5f);

            spriteBatch.DrawString(font, "File 1", new Vector2(500f, 300f), Color.Black * .5f);
            spriteBatch.DrawString(font, "File 2", new Vector2(500f, 320f), Color.Black * .5f);
            spriteBatch.DrawString(font, "File 3", new Vector2(500f, 340f), Color.Black * .5f);
            spriteBatch.DrawString(font, "Back", new Vector2(500f, 360f), Color.Black * .5f);
        }

        public override void DrawActive(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, screenManager.Game.GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.DrawString(font, "Select a File to Play", new Vector2(40f, 40f), Color.Black * transitionTime);

            switch (menuState)
            {
                case MenuStates.File1:
                    {
                        spriteBatch.DrawString(font, "Game 1", new Vector2(480f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "Game 2", new Vector2(500f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "Game 3", new Vector2(500f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "Back", new Vector2(500f, 360f), Color.Black);
                    }
                    break;
                case MenuStates.File2:
                    {
                        spriteBatch.DrawString(font, "Game 1", new Vector2(500f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "Game 2", new Vector2(480f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "Game 3", new Vector2(500f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "Back", new Vector2(500f, 360f), Color.Black);
                    }
                    break;
                case MenuStates.File3:
                    {
                        spriteBatch.DrawString(font, "Game 1", new Vector2(500f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "Game 2", new Vector2(500f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "Game 3", new Vector2(480f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "Back", new Vector2(500f, 360f), Color.Black);
                    }
                    break;
                case MenuStates.Back:
                    {
                        spriteBatch.DrawString(font, "Game 1", new Vector2(500f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "Game 2", new Vector2(500f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "Game 3", new Vector2(500f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "Back", new Vector2(480f, 360f), Color.Black);
                    }
                    break;
            }

        }

        public override void DrawTransitionOn(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, screenManager.Game.GraphicsDevice.Viewport.Bounds, Color.White * transitionTime);
            spriteBatch.DrawString(font, "Select a File to Play", new Vector2(40f * transitionTime, 40f), Color.Black * transitionTime);

            spriteBatch.DrawString(font, "Game 1", new Vector2(500f, 300f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Game 2", new Vector2(500f, 320f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Game 3", new Vector2(500f, 340f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Back", new Vector2(500f, 360f), Color.Black * transitionTime);
        }

        public override void DrawTransitionOff(SpriteBatch spriteBatch)
        {
            DrawTransitionOn(spriteBatch);
        }

        #endregion

    }
}
