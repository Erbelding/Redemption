using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using RedemptionEngine.ObjectClasses;
using RedemptionEngine.Screens;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace RedemptionEngine.TileEngine
{
    public class Level
    {
        //attributes
        GamePlayScreen gamePlayScreen;

        Map map;
        Player player;


        public Map CurrentMap { get { return map; } }

        public Level(GamePlayScreen screen)
        {
            gamePlayScreen = screen;
        }

        public void CreateNewGame(string characterFile)
        {
            //This will create a new save file with the specified name that is a copy of the empty save file
            StreamWriter writer = new StreamWriter("SaveFiles\\" + characterFile + ".txt");
            StreamReader reader = new StreamReader("SaveFiles\\New Player.txt");
            writer.Write(reader.ReadToEnd());
            writer.Close();
            reader.Close();

            //Then Load the Game
            LoadGame(characterFile);
        }

        public void LoadGame(string characterFile)
        {
            //Loads the Character File. The Character File will load the Map File
            Thread loadCharacterThread = new Thread(LoadCharacter);
            loadCharacterThread.Start(characterFile);
        }


        

        public void LoadCharacter(object characterFile)
        {
            gamePlayScreen.Loading = true;
            //cast back to a string
            string file = "SaveFiles\\" + (string)characterFile + ".txt";
            //create player
            player = Player.FromFile(this, file);
            
            

            gamePlayScreen.Loading = false;
        }


        public void LoadMap(string mapFile)
        {
            //This method will load in the map.
            //Called when the player is loaded

            map = Map.FromFile(gamePlayScreen.ScreenManager.Content, mapFile);
            map.Collision.Tex = gamePlayScreen.ScreenManager.Content.Load<Texture2D>("collisionSquare");
        }

        public void Update()
        {





        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw the map here
            map.Draw(spriteBatch);
            
            //need to draw all game objects here


            //draw all foreground objects here
        }


    }
}
