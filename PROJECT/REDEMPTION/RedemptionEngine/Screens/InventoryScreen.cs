using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.Screens
{
    public class InventoryScreen: GameScreen
    {
        private SpriteFont font;

        public InventoryScreen(ScreenManager screenManager):base(screenManager)
        {
            font = screenManager.Content.Load<SpriteFont>("Font");
        }


        #region Update

        public override void UpdateActive()
        {
            if (Controller.SingleKeyPress(Keys.Escape)) screenManager.ClosePopupScreen(this);
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

        }

        public override void DrawActive(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Inventory Screen", new Vector2(40f, 40f), Color.White * transitionTime);
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