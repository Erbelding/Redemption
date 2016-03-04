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

using System.Threading;
using RedemptionEngine.TileEngine;
using RedemptionEngine.ObjectClasses;

namespace RedemptionEngine.Screens
{
    public class GamePlayScreen: GameScreen
    {
        
        //attributes
        private bool loading;
        private GameManager level;
        private SpriteFont font;

        //properties
        public bool Loading
        {
            get { return loading; }
            set { loading = value; }
        }

        public GameManager Level
        {
            get { return level; }
        }

        public SpriteFont Font
        {
            get { return font; }
        }

        //load stuff
        public GamePlayScreen(ScreenManager screenManager):base(screenManager)
        {
            font = screenManager.Content.Load<SpriteFont>("Fonts//Font");
            PathNode.NodeTex = screenManager.Content.Load<Texture2D>("Debug//pathingTile");
            WarpGate.WarpGateTex = screenManager.Content.Load<Texture2D>("Debug//collisionSquare");
        }

        #region Update

        public override void UpdateActive(GameTime gameTime)
        {
            //call update on the level
            level.Update(gameTime);
            //open the pause or inventory menu
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
            if (!loading)
            {
                level.Draw(spriteBatch);
            }
        }

        public override void DrawActive(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!loading)
            {
                level.Draw(spriteBatch);
            }
        }

        public override void DrawTransitionOn(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //DrawActive(spriteBatch);
        }

        public override void DrawTransitionOff(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //DrawActive(spriteBatch);
        }

        #endregion

        #region Game

        //create a new game from the default starting file
        public void NewGame(string characterName)
        {
            //put the loading screen in front of it until it's loaded
            screenManager.OpenNextScreen(screenManager.GamePlayScreen, screenManager.LoadingScreen);
            level = new GameManager(this);
            level.CreateNewGame(characterName);
        }

        //create a game from a save file
        public void LoadGame(string characterFile)
        {
            //open load screen over gameplay screen
            screenManager.OpenNextScreen(screenManager.GamePlayScreen, screenManager.LoadingScreen);
            level = new GameManager(this);
            level.LoadGame(characterFile);
        }

        #endregion
    }
}
