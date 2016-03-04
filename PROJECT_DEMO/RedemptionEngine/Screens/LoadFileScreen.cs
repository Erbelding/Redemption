﻿/**
 * David Erbelding
 * */

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
            font = screenManager.Content.Load<SpriteFont>("Fonts//Font");
            background = screenManager.Content.Load<Texture2D>("GUI//Menu");
        }


        #region Update

        public override void UpdateActive(GameTime gameTime)
        {
            //escape closes the menu screen
            if (Controller.SingleKeyPress(Keys.Escape)) screenManager.OpenPreviousScreen(this);

            switch (menuState)
            {
                case MenuStates.File1:
                    if (Controller.SingleKeyPress(Keys.W)) menuState = MenuStates.Back;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter))
                    {
                        //open gameplay screen
                        screenManager.OpenNextScreen(this, screenManager.GamePlayScreen);
                        
                        //load the game in the gamescreen
                        (screenManager.GamePlayScreen as GamePlayScreen).LoadGame("Game1");
                    }
                    break;
                case MenuStates.File2:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter))
                    {
                        //same as above
                        screenManager.OpenNextScreen(this, screenManager.GamePlayScreen);

                        (screenManager.GamePlayScreen as GamePlayScreen).LoadGame("Game2");
                    }
                    break;
                case MenuStates.File3:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter))
                    {
                        //same as above
                        screenManager.OpenNextScreen(this, screenManager.GamePlayScreen);

                        (screenManager.GamePlayScreen as GamePlayScreen).LoadGame("Game3");
                    }
                    break;
                case MenuStates.Back:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState = MenuStates.File1;
                    //go back to main menu
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
            spriteBatch.Draw(background, new Rectangle(150, -50, 500, 700), Color.White * .5f);
            spriteBatch.DrawString(font, "Select a File to Play", new Vector2(190f, 40f), Color.Black * .5f);

            spriteBatch.DrawString(font, "File 1", new Vector2(500f, 300f), Color.Black * .5f);
            spriteBatch.DrawString(font, "File 2", new Vector2(500f, 320f), Color.Black * .5f);
            spriteBatch.DrawString(font, "File 3", new Vector2(500f, 340f), Color.Black * .5f);
            spriteBatch.DrawString(font, "Back", new Vector2(500f, 360f), Color.Black * .5f);
        }

        public override void DrawActive(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //title and background
            spriteBatch.Draw(background, new Rectangle(150, -50, 500, 700), Color.White);
            spriteBatch.DrawString(font, "Select a File to Play", new Vector2(190f, 40f), Color.Black * transitionTime);

            //highlight selected option
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

        //fade in and out
        public override void DrawTransitionOn(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(150, -50, 500, 700), Color.White * transitionTime);
            spriteBatch.DrawString(font, "Select a File to Play", new Vector2(190f * transitionTime, 40f), Color.Black * transitionTime);

            spriteBatch.DrawString(font, "Game 1", new Vector2(500f, 300f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Game 2", new Vector2(500f, 320f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Game 3", new Vector2(500f, 340f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Back", new Vector2(500f, 360f), Color.Black * transitionTime);
        }

        //same as above
        public override void DrawTransitionOff(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawTransitionOn(gameTime, spriteBatch);
        }

        #endregion

    }
}