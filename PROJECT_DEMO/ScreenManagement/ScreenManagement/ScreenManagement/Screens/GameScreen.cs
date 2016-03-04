using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ScreenManagement
{
    public abstract class GameScreen
    {
        //possible states
        public enum ScreenState { Off, Active, Paused, TransitioningOn, TransitioningOff };
        protected ScreenState screenState;

        protected ScreenManager screenManager;  //every screen has to be able to access the screen manager
        protected GameScreen previousScreen;    //screens know what screen was before them in order to go back
        protected float transitionTime;         //float between 0 and 1.     0 is off, 1 is all the way on
        protected Texture2D background;

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

        //constructor
        public GameScreen(ScreenManager screenManager)
        {
            transitionTime = 0;
            this.screenManager = screenManager;
        }

        //Draw and Update Basically just call the correct methods in each class based on what state it's in

        #region Update

        public void Update()
        {
            switch (screenState)
            {
                case ScreenState.Active:
                    UpdateActive();
                    break;
                case ScreenState.TransitioningOn:
                    UpdateTransitionOn();
                    break;
                case ScreenState.TransitioningOff:
                    UpdateTransitionOff();
                    break;
            }
        }

        public abstract void UpdateActive();
        public abstract void UpdateTransitionOn();
        public abstract void UpdateTransitionOff();

        #endregion

        #region Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (screenState)
            {
                case ScreenState.Paused:
                    DrawPaused(spriteBatch);
                    break;
                case ScreenState.Active:
                    DrawActive(spriteBatch);
                    break;
                case ScreenState.TransitioningOn:
                    DrawTransitionOn(spriteBatch);
                    break;
                case ScreenState.TransitioningOff:
                    DrawTransitionOff(spriteBatch);
                    break;
            }
        }

        public abstract void DrawPaused(SpriteBatch spriteBatch);
        public abstract void DrawActive(SpriteBatch spriteBatch);
        public abstract void DrawTransitionOn(SpriteBatch spriteBatch);
        public abstract void DrawTransitionOff(SpriteBatch spriteBatch);

        #endregion


    }
}
