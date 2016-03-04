using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ScreenManagement
{
    public class OptionsScreen : GameScreen
    {
        enum MenuStates { Option1, Option2, Option3, Option4, Back };
        private SpriteFont font;
        private SpriteFont titleFont;
        private MenuStates menuState;

        public OptionsScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            background = screenManager.Content.Load<Texture2D>("Book");
            font = screenManager.Content.Load<SpriteFont>("Font");
            titleFont = screenManager.Content.Load<SpriteFont>("TitleFont");
        }


        #region Update

        public override void UpdateActive()
        {
            if (Controller.SingleKeyPress(Keys.Escape)) screenManager.OpenPreviousScreen(this);

            switch (menuState)
            {
                case MenuStates.Option1:
                    if (Controller.SingleKeyPress(Keys.W)) menuState = MenuStates.Back;
                    if (Controller.SingleKeyPress(Keys.S)) menuState ++;
                    if (Controller.SingleKeyPress(Keys.Enter)) { } //Do Nothing
                    break;
                case MenuStates.Option2:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter)) { } //Do Nothing
                    break;
                case MenuStates.Option3:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter)) { } //Do Nothing
                    break;
                case MenuStates.Option4:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState++;
                    if (Controller.SingleKeyPress(Keys.Enter)) { } //Do Nothing
                    break;
                case MenuStates.Back:
                    if (Controller.SingleKeyPress(Keys.W)) menuState--;
                    if (Controller.SingleKeyPress(Keys.S)) menuState = MenuStates.Option1;
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
            //This Screen Should Never Be Paused
        }

        public override void DrawActive(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(50,50,700,400), Color.White * transitionTime);
            spriteBatch.DrawString(titleFont, "Options", new Vector2(100f, 100f), Color.Black * transitionTime);

            switch (menuState)
            {
                case MenuStates.Option1:
                    {
                        spriteBatch.DrawString(font, "Option1", new Vector2(500f, 300f), Color.LimeGreen);
                        spriteBatch.DrawString(font, "Option2", new Vector2(500f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "Option3", new Vector2(500f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "Option4", new Vector2(500f, 360f), Color.Black);
                        spriteBatch.DrawString(font, "Back", new Vector2(500f, 380f), Color.Black);
                        break;
                    }
                case MenuStates.Option2:
                    {
                        spriteBatch.DrawString(font, "Option1", new Vector2(500f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "Option2", new Vector2(500f, 320f), Color.LimeGreen);
                        spriteBatch.DrawString(font, "Option3", new Vector2(500f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "Option4", new Vector2(500f, 360f), Color.Black);
                        spriteBatch.DrawString(font, "Back", new Vector2(500f, 380f), Color.Black);
                    }
                    break;
                case MenuStates.Option3:
                    {
                        spriteBatch.DrawString(font, "Option1", new Vector2(500f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "Option2", new Vector2(500f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "Option3", new Vector2(500f, 340f), Color.LimeGreen);
                        spriteBatch.DrawString(font, "Option4", new Vector2(500f, 360f), Color.Black);
                        spriteBatch.DrawString(font, "Back", new Vector2(500f, 380f), Color.Black);
                    }
                    break;
                case MenuStates.Option4:
                    {
                        spriteBatch.DrawString(font, "Option1", new Vector2(500f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "Option2", new Vector2(500f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "Option3", new Vector2(500f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "Option4", new Vector2(500f, 360f), Color.LimeGreen);
                        spriteBatch.DrawString(font, "Back", new Vector2(500f, 380f), Color.Black);
                    }
                    break;
                case MenuStates.Back:
                    {
                        spriteBatch.DrawString(font, "Option1", new Vector2(500f, 300f), Color.Black);
                        spriteBatch.DrawString(font, "Option2", new Vector2(500f, 320f), Color.Black);
                        spriteBatch.DrawString(font, "Option3", new Vector2(500f, 340f), Color.Black);
                        spriteBatch.DrawString(font, "Option4", new Vector2(500f, 360f), Color.Black);
                        spriteBatch.DrawString(font, "Back", new Vector2(500f, 380f), Color.LimeGreen);
                    }
                    break;
            }

        }

        public override void DrawTransitionOn(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(50, 50, 700, 400), Color.White * transitionTime);
            spriteBatch.DrawString(titleFont, "Options", new Vector2(100f, 100f), Color.Black * transitionTime);

            spriteBatch.DrawString(font, "Option1", new Vector2(500f, 300f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Option2", new Vector2(500f, 320f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Option3", new Vector2(500f, 340f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Option4", new Vector2(500f, 360f), Color.Black * transitionTime);
            spriteBatch.DrawString(font, "Back", new Vector2(500f, 380f), Color.Black * transitionTime);
        }

        public override void DrawTransitionOff(SpriteBatch spriteBatch)
        {
            DrawTransitionOn(spriteBatch);
        }

        #endregion


    }
}