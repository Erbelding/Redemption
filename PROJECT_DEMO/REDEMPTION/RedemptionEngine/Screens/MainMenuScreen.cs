using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.Screens
{
    public class MainMenuScreen: GameScreen
    {
        enum MenuStates { NewGame, LoadGame, Options, Exit };
        private MenuStates menuState;
        private SpriteFont font;
        private SpriteFont titleFont;

        public MainMenuScreen(ScreenManager screenManager):base(screenManager)
        {
            font = screenManager.Content.Load<SpriteFont>("Font");
            titleFont = screenManager.Content.Load<SpriteFont>("TitleFont");
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
                        screenManager.OpenNextScreen(this, screenManager.NewFileScreen);
                    }
                    break;
                case MenuStates.LoadGame:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter))
                    {
                        screenManager.OpenNextScreen(this, screenManager.LoadFileScreen);
                    }
                    
                    break;
                case MenuStates.Options:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter)) screenManager.OpenPopupScreen(this, screenManager.OptionsScreen);
                    break;
                case MenuStates.Exit:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState = MenuStates.NewGame;
                    if (Controller.SingleKeyPress(Keys.Enter)) screenManager.Game.Exit();
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
            spriteBatch.DrawString(titleFont, "REDEMPTION", new Vector2(40f, 40f), Color.Black * .5f);

            spriteBatch.DrawString(font, "new Game", new Vector2(500f, 300f), Color.Black * .5f);
            spriteBatch.DrawString(font, "load Game", new Vector2(500f, 320f), Color.Black * .5f);
            spriteBatch.DrawString(font, "options", new Vector2(500f, 340f), Color.Black * .5f);
            spriteBatch.DrawString(font, "exit", new Vector2(500f, 360f), Color.Black * .5f);
        }

        public override void DrawActive(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, screenManager.Game.GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.DrawString(titleFont, "REDEMPTION", new Vector2(40f, 40f), Color.Black * transitionTime);

            switch (menuState)
            {
                case MenuStates.NewGame:
                    {
                        spriteBatch.DrawString(font, "New Game", new Vector2(480f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "load Game", new Vector2(500f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "options", new Vector2(500f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "exit", new Vector2(500f, 360f), Color.Black);
                    }
                    break;
                case MenuStates.LoadGame:
                    {
                        spriteBatch.DrawString(font, "new Game", new Vector2(500f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "Load Game", new Vector2(480f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "options", new Vector2(500f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "exit", new Vector2(500f, 360f), Color.Black);
                    }
                    break;
                case MenuStates.Options:
                    {
                        spriteBatch.DrawString(font, "new Game", new Vector2(500f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "load Game", new Vector2(500f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "Options", new Vector2(480f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "exit", new Vector2(500f, 360f), Color.Black);
                    }
                    break;
                case MenuStates.Exit:
                    {
                        spriteBatch.DrawString(font, "new Game", new Vector2(500f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "load Game", new Vector2(500f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "options", new Vector2(500f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "Exit", new Vector2(480f, 360f), Color.Black);
                    }
                    break;
            }
            
        }

        public override void DrawTransitionOn(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, screenManager.Game.GraphicsDevice.Viewport.Bounds, Color.White * transitionTime);
            spriteBatch.DrawString(titleFont, "REDEMPTION", new Vector2(40f * transitionTime, 40f), Color.Black * transitionTime);

            spriteBatch.DrawString(font, "new Game", new Vector2(500f, 300f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "load Game", new Vector2(500f, 320f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "options", new Vector2(500f, 340f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "exit", new Vector2(500f, 360f), Color.Black * transitionTime);
        }

        public override void DrawTransitionOff(SpriteBatch spriteBatch)
        {
            DrawTransitionOn(spriteBatch);
        }

        #endregion


    }
}
