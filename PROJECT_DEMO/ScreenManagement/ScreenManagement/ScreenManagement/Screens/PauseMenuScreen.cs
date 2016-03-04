using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ScreenManagement
{
    public class PauseMenuScreen: GameScreen
    {
        enum MenuStates { Resume, Save, Options, Quit };
        private MenuStates menuState;

        private SpriteFont font;

        public PauseMenuScreen(ScreenManager screenManager):base(screenManager)
        {
            font = screenManager.Content.Load<SpriteFont>("Font");
        }


        #region Update

        public override void UpdateActive()
        {
            if (Controller.SingleKeyPress(Keys.Escape)) screenManager.OpenPreviousScreen(this);

            switch (menuState)
            {
                case MenuStates.Resume:
                    if (Controller.SingleKeyPress(Keys.W)) menuState = MenuStates.Quit;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter)) screenManager.ClosePopupScreen(this);
                    break;
                case MenuStates.Save:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter)) { } //Do Nothing
                    break;
                case MenuStates.Options:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter)) screenManager.OpenPopupScreen(this, screenManager.OptionsScreen);
                    break;
                case MenuStates.Quit:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState = MenuStates.Resume;
                    if (Controller.SingleKeyPress(Keys.Enter))
                    {
                        screenManager.ClosePopupScreen(this);
                        screenManager.OpenNextScreen(this.previousScreen, screenManager.MainMenuScreen);
                        screenManager.MainMenuScreen.PreviousScreen = screenManager.TitleScreen;
                    }
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
            //You can't pause this screen!
        }

        public override void DrawActive(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Game Paused", new Vector2(40f, 40f), Color.White * transitionTime);

            switch (menuState)
            {
                case MenuStates.Resume:
                    {
                        spriteBatch.DrawString(font, "Resume", new Vector2(500f, 300f), Color.LimeGreen);
                        spriteBatch.DrawString(font, "Save", new Vector2(500f, 320f), Color.White);
                        spriteBatch.DrawString(font, "Options", new Vector2(500f, 340f), Color.White);
                        spriteBatch.DrawString(font, "Quit", new Vector2(500f, 360f), Color.White);
                    }
                    break;
                case MenuStates.Save:
                    {
                        spriteBatch.DrawString(font, "Resume", new Vector2(500f, 300f), Color.White);
                        spriteBatch.DrawString(font, "Save", new Vector2(500f, 320f), Color.LimeGreen);
                        spriteBatch.DrawString(font, "Options", new Vector2(500f, 340f), Color.White);
                        spriteBatch.DrawString(font, "Quit", new Vector2(500f, 360f), Color.White);
                    }
                    break;
                case MenuStates.Options:
                    {
                        spriteBatch.DrawString(font, "Resume", new Vector2(500f, 300f), Color.White);
                        spriteBatch.DrawString(font, "Save", new Vector2(500f, 320f), Color.White);
                        spriteBatch.DrawString(font, "Options", new Vector2(500f, 340f), Color.LimeGreen);
                        spriteBatch.DrawString(font, "Quit", new Vector2(500f, 360f), Color.White);
                    }
                    break;
                case MenuStates.Quit:
                    {
                        spriteBatch.DrawString(font, "Resume", new Vector2(500f, 300f), Color.White);
                        spriteBatch.DrawString(font, "Save", new Vector2(500f, 320f), Color.White);
                        spriteBatch.DrawString(font, "Options", new Vector2(500f, 340f), Color.White);
                        spriteBatch.DrawString(font, "Quit", new Vector2(500f, 360f), Color.LimeGreen);
                    }
                    break;
            }
        }

        public override void DrawTransitionOn(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Game Paused", new Vector2(40f, 40f), Color.White * transitionTime);

            spriteBatch.DrawString(font, "Resume", new Vector2(500f, 300f), Color.White * transitionTime);
            spriteBatch.DrawString(font, "Save", new Vector2(500f, 320f), Color.White * transitionTime);
            spriteBatch.DrawString(font, "Options", new Vector2(500f, 340f), Color.White * transitionTime);
            spriteBatch.DrawString(font, "Quit", new Vector2(500f, 360f), Color.White * transitionTime);
        }

        public override void DrawTransitionOff(SpriteBatch spriteBatch)
        {
            DrawTransitionOn(spriteBatch);
        }

        #endregion
    }
}
