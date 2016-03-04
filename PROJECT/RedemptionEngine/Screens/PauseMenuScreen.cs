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
    public class PauseMenuScreen: GameScreen
    {
        enum MenuStates { Resume, Save, Options, Quit };
        private MenuStates menuState;

        private SpriteFont font;

        //initialized pause menu stuff
        public PauseMenuScreen(ScreenManager screenManager):base(screenManager)
        {
            font = screenManager.Content.Load<SpriteFont>("Fonts//Font");
        }


        #region Update

        public override void UpdateActive(GameTime gameTime)
        {
            if (Controller.SingleKeyPress(Keys.Escape)) screenManager.OpenPreviousScreen(this);

            switch (menuState)
            {
                case MenuStates.Resume:
                    //move the selection up and down
                    if (Controller.SingleKeyPress(Keys.W)) menuState = MenuStates.Quit;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    //resume closes the pause menu
                    if (Controller.SingleKeyPress(Keys.Enter)) screenManager.ClosePopupScreen(this);
                    break;
                case MenuStates.Save:
                    //move selector up and down
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter))
                    {
                        (ScreenManager.GamePlayScreen as GamePlayScreen).Level.SaveGame();
                    }
                    break;
                case MenuStates.Options:
                    // move selector
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    //open the options screen over the pause screen over the gameplay screen
                    if (Controller.SingleKeyPress(Keys.Enter)) screenManager.OpenPopupScreen(this, screenManager.OptionsScreen);
                    break;
                case MenuStates.Quit:
                    //move selector
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState = MenuStates.Resume;
                    //close the gameplay screen and pause screen
                    if (Controller.SingleKeyPress(Keys.Enter))
                    {
                        MediaPlayer.Stop();

                        GameAudio.PlaySong("MenuTheme", true);

                        //close pause screen
                        screenManager.ClosePopupScreen(this);
                        //open the main menu screen from the gameplay screen
                        screenManager.OpenNextScreen(this.previousScreen, screenManager.MainMenuScreen);
                        //tell the main menu screen that it's previous screen is the title screen
                        screenManager.MainMenuScreen.PreviousScreen = screenManager.TitleScreen;
                    }
                    break;
            }
        }

        //what to do when transitioning on
        public override void UpdateTransitionOn()
        {
            transitionTime += .02f;
            if (transitionTime >= 1) screenState = ScreenState.Active;
        }

        //what to do when transitioning off
        public override void UpdateTransitionOff()
        {
            transitionTime -= .02f;
            if (transitionTime <= 0) screenState = ScreenState.Off;
        }

        #endregion

        #region Draw

        public override void DrawPaused(SpriteBatch spriteBatch)
        {
            //This screen should never be paused
        }


        public override void DrawActive(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //title across the top
            spriteBatch.DrawString(font, "Game Paused", new Vector2(40f, 40f), Color.White * transitionTime);

            //things that it draws based on which option is selected
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

        //draws the text fading in and out based on transition time
        public override void DrawTransitionOn(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Game Paused", new Vector2(40f, 40f), Color.White * transitionTime);

            spriteBatch.DrawString(font, "Resume", new Vector2(500f, 300f), Color.White * transitionTime);
            spriteBatch.DrawString(font, "Save", new Vector2(500f, 320f), Color.White * transitionTime);
            spriteBatch.DrawString(font, "Options", new Vector2(500f, 340f), Color.White * transitionTime);
            spriteBatch.DrawString(font, "Quit", new Vector2(500f, 360f), Color.White * transitionTime);
        }

        //does the same thing as transition on but transition time is going down this time
        public override void DrawTransitionOff(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawTransitionOn(gameTime, spriteBatch);
        }

        #endregion
    }
}
