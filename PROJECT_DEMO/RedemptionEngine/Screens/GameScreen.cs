/**
 * David Erbelding
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.Screens
{   
    public abstract class GameScreen
    {

        #region Attributes/Properties

        //possible states
        public enum ScreenState { Off, Active, Paused, TransitioningOn, TransitioningOff };
        protected ScreenState screenState;

        protected ScreenManager screenManager;  //every screen has to be able to access the screen manager
        protected GameScreen previousScreen;    //screens know what screen was before them in order to go back
        protected float transitionTime;         //float between 0 and 1.     0 is off, 1 is all the way on
        protected Texture2D background;         //Image that the screen draws underneath everything (optional)

        public ScreenState State
        {
            get { return screenState; }
            set { screenState = value; }
        }

        public GameScreen PreviousScreen
        {
            get { return previousScreen; }
            set { previousScreen = value; }
        }

        public float TransitionTime
        {
            get { return transitionTime;  }
            set { transitionTime = value; }
        }

        public ScreenManager ScreenManager
        {
            get { return screenManager; }
        }

        #endregion

        #region Constructor

        //constructor
        public GameScreen(ScreenManager screenManager)
        {
            transitionTime = 0;
            this.screenManager = screenManager;
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            //Every screen seperates it's states into different method calls.
            switch (screenState)
            {
                case ScreenState.Active:
                    UpdateActive(gameTime);
                    break;
                case ScreenState.TransitioningOn:
                    UpdateTransitionOn();
                    break;
                case ScreenState.TransitioningOff:
                    UpdateTransitionOff();
                    break;
            }
        }

        //Every screen will have to implement these
        //Some of them might be empty
        public abstract void UpdateActive(GameTime gameTime);
        public abstract void UpdateTransitionOn();
        public abstract void UpdateTransitionOff();

        #endregion

        #region Draw

        //Gamescreen calls different draw method in different states
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (screenState)
            {
                case ScreenState.Paused:
                    DrawPaused(spriteBatch);
                    break;
                case ScreenState.Active:
                    DrawActive(gameTime, spriteBatch);
                    break;
                case ScreenState.TransitioningOn:
                    DrawTransitionOn(gameTime, spriteBatch);
                    break;
                case ScreenState.TransitioningOff:
                    DrawTransitionOff(gameTime, spriteBatch);
                    break;
            }
        }

        //draw methods that need to be implemented in every game screen
        public abstract void DrawPaused(SpriteBatch spriteBatch);
        public abstract void DrawActive(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void DrawTransitionOn(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void DrawTransitionOff(GameTime gameTime, SpriteBatch spriteBatch);

        #endregion

    }
}
