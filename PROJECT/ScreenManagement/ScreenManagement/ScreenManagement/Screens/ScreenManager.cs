using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ScreenManagement
{
    public class ScreenManager
    {

        //attributes
        private List<GameScreen> gameScreens;  //List of all gamescreens to be iterated through for all menus etc.
        private List<GameScreen> gameScreens2; //Need this list to be able to alter the list while iterating through it

        private ContentManager content;
        private ScreenManagerTest screenManagerTest;

        public ContentManager Content
        { get { return content; } }

        #region Screen Attributes/Properties

        //all screens
        private TitleScreen titleScreen;
        private MainMenuScreen mainMenuScreen;
        private OptionsScreen optionsScreen;
        private LoadingScreen loadingScreen;
        private InventoryScreen inventoryScreen;
        private PauseMenuScreen pauseMenuScreen;
        private GamePlayScreen gamePlayScreen;

        //properties for all screens
        public GameScreen TitleScreen { get { return titleScreen; } }
        public GameScreen MainMenuScreen { get { return mainMenuScreen; } }
        public GameScreen OptionsScreen { get { return optionsScreen; } }
        public GameScreen LoadingScreen { get { return loadingScreen; } }
        public GameScreen InventoryScreen { get { return inventoryScreen; } }
        public GameScreen PauseMenuScreen { get { return pauseMenuScreen; } }
        public GameScreen GamePlayScreen { get { return gamePlayScreen; } }
        public ScreenManagerTest ScreenManagerTest { get { return screenManagerTest;  } }

        #endregion


        //Screen Manager Constructor

        public ScreenManager(ContentManager content,ScreenManagerTest screenManagerTest)
        {
            //parent
            this.screenManagerTest = screenManagerTest;
            this.content = content;

            //initialize list and add all screens to it
            gameScreens = new List<GameScreen>();
            gameScreens2 = new List<GameScreen>();
            gameScreens.Add(titleScreen = new TitleScreen(this));
            gameScreens.Add(mainMenuScreen = new MainMenuScreen(this));
            gameScreens.Add(optionsScreen = new OptionsScreen(this));
            gameScreens.Add(loadingScreen = new LoadingScreen(this));
            gameScreens.Add(inventoryScreen = new InventoryScreen(this));
            gameScreens.Add(pauseMenuScreen = new PauseMenuScreen(this));
            gameScreens.Add(gamePlayScreen = new GamePlayScreen(this));

            //cue title screen
            titleScreen.State = GameScreen.ScreenState.TransitioningOn;
            
        }

        #region Draw And Update

        public void Update()
        {

            Controller.Update();

            //save the current screenlist
            gameScreens2 = gameScreens.ToList<GameScreen>();

            //iterate through all screens
            foreach (GameScreen gamescreen in gameScreens)
            {
                switch (gamescreen.State)
                {
                    case GameScreen.ScreenState.Off:
                        //Do Nothing
                        break;
                    case GameScreen.ScreenState.Active:
                        gamescreen.Update();
                        break;
                    case GameScreen.ScreenState.Paused:
                        //Do Nothing
                        break;
                    case GameScreen.ScreenState.TransitioningOn:
                        gamescreen.Update();
                        break;
                    case GameScreen.ScreenState.TransitioningOff:
                        gamescreen.Update();
                        break;
                }
            }

            //apply any changes made to the screenlist
            gameScreens = gameScreens2.ToList<GameScreen>();
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            //save the current screenlist
            gameScreens2 = gameScreens.ToList<GameScreen>();

            foreach (GameScreen gamescreen in gameScreens)
            {
                switch (gamescreen.State)
                {
                    case GameScreen.ScreenState.Off:
                        //Do Nothing
                        break;
                    case GameScreen.ScreenState.Active:
                        gamescreen.Draw(spriteBatch);
                        break;
                    case GameScreen.ScreenState.Paused:
                        gamescreen.Draw(spriteBatch);
                        break;
                    case GameScreen.ScreenState.TransitioningOn:
                        gamescreen.Draw(spriteBatch);
                        break;
                    case GameScreen.ScreenState.TransitioningOff:
                        gamescreen.Draw(spriteBatch);
                        break;
                }
            }

            //apply any changes made to the screenlist
            gameScreens = gameScreens2.ToList<GameScreen>();
        }

        #endregion

        #region ScreenMethods

        //opens up the next screen
        public void OpenNextScreen(GameScreen current, GameScreen next)
        {
            current.State = GameScreen.ScreenState.TransitioningOff;
            next.State = GameScreen.ScreenState.TransitioningOn;
            next.PreviousScreen = current;
        }

        //Sends you to the previous screen
        public void OpenPreviousScreen(GameScreen current)
        {
            current.State = GameScreen.ScreenState.TransitioningOff;
            current.PreviousScreen.State = GameScreen.ScreenState.TransitioningOn;
        }

        //keeps the current screen in the background and opens up the second screen as a popup
        public void OpenPopupScreen(GameScreen current, GameScreen popup)
        {
            current.State = GameScreen.ScreenState.Paused;
            popup.State = GameScreen.ScreenState.TransitioningOn;
            popup.PreviousScreen = current;
            BringScreenToFront(popup);
        }

        //closes popup and returns to previous screen
        public void ClosePopupScreen(GameScreen popup)
        {
            popup.State = GameScreen.ScreenState.TransitioningOff;
            popup.PreviousScreen.State = GameScreen.ScreenState.Active;
            popup.PreviousScreen.TransitionTime = 1;
        }

        //brings specified gamescreen to the end of the list so it's draw method will be called last
        public void BringScreenToFront(GameScreen gameScreen)
        {
            gameScreens2.Remove(gameScreen);
            gameScreens2.Add(gameScreen);
        }

        #endregion

    }
}
