using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ScreenManagement
{
    public class MainMenuScreen: GameScreen
    {
        enum MenuStates { NewGame, LoadGame, Options, Exit };
        private MenuStates menuState;
        private SpriteFont font;

        public MainMenuScreen(ScreenManager screenManager):base(screenManager)
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
                case MenuStates.NewGame:
                    if (Controller.SingleKeyPress(Keys.W)) menuState = MenuStates.Exit;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter))
                    {
                        screenManager.OpenNextScreen(this, screenManager.GamePlayScreen);
                        screenManager.OpenPopupScreen(screenManager.GamePlayScreen, screenManager.LoadingScreen);
                        GamePlayScreen gameScreen = (GamePlayScreen)screenManager.GamePlayScreen;
                        gameScreen.LoadContent("");
                    }
                    break;
                case MenuStates.LoadGame:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter)) {} //Do Nothing
                    break;
                case MenuStates.Options:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter)) screenManager.OpenPopupScreen(this, screenManager.OptionsScreen);
                    break;
                case MenuStates.Exit:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState = MenuStates.NewGame;
                    if (Controller.SingleKeyPress(Keys.Enter)) screenManager.ScreenManagerTest.Exit();
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
            spriteBatch.Draw(background, screenManager.ScreenManagerTest.GraphicsDevice.Viewport.Bounds, Color.White * .5f);
            spriteBatch.DrawString(font, "Main Menu Screen", new Vector2(40f, 40f), Color.Black * .5f);

            spriteBatch.DrawString(font, "New Game", new Vector2(500f, 300f), Color.Black * .5f);
            spriteBatch.DrawString(font, "Load Game", new Vector2(500f, 320f), Color.Black * .5f);
            spriteBatch.DrawString(font, "Options", new Vector2(500f, 340f), Color.Black * .5f);
            spriteBatch.DrawString(font, "Exit", new Vector2(500f, 360f), Color.Black * .5f);
        }

        public override void DrawActive(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, screenManager.ScreenManagerTest.GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.DrawString(font, "Main Menu Screen", new Vector2(40f, 40f), Color.Black * transitionTime);

            switch (menuState)
            {
                case MenuStates.NewGame:
                    {
                        spriteBatch.DrawString(font, "New Game", new Vector2(480f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "Load Game", new Vector2(500f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "Options", new Vector2(500f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "Exit", new Vector2(500f, 360f), Color.Black);
                    }
                    break;
                case MenuStates.LoadGame:
                    {
                        spriteBatch.DrawString(font, "New Game", new Vector2(500f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "Load Game", new Vector2(480f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "Options", new Vector2(500f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "Exit", new Vector2(500f, 360f), Color.Black);
                    }
                    break;
                case MenuStates.Options:
                    {
                        spriteBatch.DrawString(font, "New Game", new Vector2(500f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "Load Game", new Vector2(500f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "Options", new Vector2(480f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "Exit", new Vector2(500f, 360f), Color.Black);
                    }
                    break;
                case MenuStates.Exit:
                    {
                        spriteBatch.DrawString(font, "New Game", new Vector2(500f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "Load Game", new Vector2(500f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "Options", new Vector2(500f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "Exit", new Vector2(480f, 360f), Color.Black);
                    }
                    break;
            }
            
        }

        public override void DrawTransitionOn(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, screenManager.ScreenManagerTest.GraphicsDevice.Viewport.Bounds, Color.White * transitionTime);
            spriteBatch.DrawString(font, "Main Menu Screen", new Vector2(40f * transitionTime, 40f), Color.Black * transitionTime);

            spriteBatch.DrawString(font, "New Game", new Vector2(500f, 300f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Load Game", new Vector2(500f, 320f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Options", new Vector2(500f, 340f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Exit", new Vector2(500f, 360f), Color.Black * transitionTime);
        }

        public override void DrawTransitionOff(SpriteBatch spriteBatch)
        {
            DrawTransitionOn(spriteBatch);
        }

        #endregion


    }
}
