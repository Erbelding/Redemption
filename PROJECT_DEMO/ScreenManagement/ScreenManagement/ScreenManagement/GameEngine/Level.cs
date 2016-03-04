using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScreenManagement.GameEngine
{
    public class Level
    {
        //attributes
        GamePlayScreen gamePlayScreen;

        public Level(GamePlayScreen screen, string mapFile, string characterFile)
        {
            gamePlayScreen = screen;
            LoadContent(mapFile, characterFile);

        }

        public void LoadContent(string mapFile, string characterFile)
        {
            Thread loadMapThread = new Thread(LoadMap);
            loadMapThread.Start(mapFile);

            Thread loadCharacterThread = new Thread(LoadCharacter);
            loadCharacterThread.Start(characterFile);

        }

        public void LoadMap(object mapFile)
        {
            gamePlayScreen.Loading = true;
            Console.WriteLine("Waiting");
            Thread.Sleep(1000);
            Console.WriteLine("Done Waiting");
            gamePlayScreen.Loading = false;
        }

        public void LoadCharacter(object characterFile)
        {
            Thread.Sleep(1000);
        }


    }
}
