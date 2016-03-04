using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using RedemptionEngine.ObjectClasses;
using RedemptionEngine.Screens;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Reflection;
using RedemptionEngine.Items;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

namespace RedemptionEngine.TileEngine
{
    //Represents a game level
    public class GameManager
    {
        public static string GAME;

        //attributes
        public GamePlayScreen gamePlayScreen;
        private Map curMap;
        private Player player;
        private List<Entity> entities;
        private List<Entity> impassableObjects;
        private List<Entity> npcs;
        private List<Entity> projectiles;
        private List<Entity> items;
        private List<Entity> toAdd; //put stuff in here when you want to add it
        private List<Entity> garbage; //put stuff in here when you want to remove it

        public Map CurrentMap { get { return curMap; } }
        public Player Player { get { return player; } }
        public List<Entity> NPCS { get { return npcs; } }
        public List<Entity> Projectiles { get { return projectiles; } }
        public List<Entity> Items { get { return items; } }
        public EquipmentBar EquipBar { get { return equipmentBar; } }

        public Camera camera;
        private HealthBar healthBar;
        private ManaBar manaBar;
        private EquipmentBar equipmentBar;
        
        //Constructor
        public GameManager(GamePlayScreen screen)
        {
            gamePlayScreen = screen;
            camera = new Camera();
            entities = new List<Entity>();
            impassableObjects = new List<Entity>();
            npcs = new List<Entity>();
            projectiles = new List<Entity>();
            items = new List<Entity>();
            toAdd = new List<Entity>();
            garbage = new List<Entity>();
        }

        public void AddEntity(Entity entity)
        {
            toAdd.Add(entity);
        }

        //adds entities correctly to the map
        private void PlaceEntity(Entity entity)
        {
            entities.Add(entity);
            if (entity is NPC)
            {
                (entity as NPC).path = new Path(curMap.NodeMap);
                impassableObjects.Add(entity);
                npcs.Add(entity);
            }
            if(entity is Item)
            {
                items.Add(entity);
            }
            if (entity is Projectile)
            {
                projectiles.Add(entity);
            }
        }




        public void RemoveEntity(Entity e)
        {
            garbage.Add(e);
        }


        //Creates a new game
        public void CreateNewGame(string characterFile)
        {
            //This will create a new save file with the specified name that is a copy of the empty save file
            File.WriteAllText("SaveFiles\\" + characterFile + ".txt", String.Empty);
            StreamWriter writer = new StreamWriter("SaveFiles\\" + characterFile + ".txt");
            StreamReader reader = new StreamReader("SaveFiles\\New Player.txt");
            writer.Write(reader.ReadToEnd());
            writer.Close();
            reader.Close();

            //Then Load the Game
            LoadGame(characterFile);
        }



        #region SaveGame


        //saves game data including location data
        public void SaveGame(string map, Vector2 location)
        {
            //clear the text file
            File.WriteAllText("SaveFiles\\" + GAME + ".txt", String.Empty);
            StreamWriter writer = new StreamWriter("SaveFiles\\" + GAME + ".txt");
            //start writing
            writer.WriteLine("[LOCATION]");
            writer.WriteLine( map + " " + location.X + " " + location.Y);
            writer.WriteLine();

            writer.Close();
            //saves the rest of the game the normal way
            SaveGame();
        }

        //saves the game ignoring location data
        public void SaveGame()
        {
            //we don't want to write over the location so we just copy it over into the next file
            StreamReader reader = new StreamReader("SaveFiles\\" + GAME + ".txt");
            List<string> tempLines = new List<string>();
            string line = "";
            //read until you reach location
            while (!tempLines.Contains("[LOCATION]"))
            {
                line = reader.ReadLine();
                tempLines.Add(line);
            }
            //read two extra lines
            line = reader.ReadLine();
            tempLines.Add(line);
            line = reader.ReadLine();
            tempLines.Add(line);

            reader.Close(); //done reading

            //clear text file
            File.WriteAllText("SaveFiles\\" + GAME + ".txt", String.Empty);

            //start writing
            StreamWriter writer = new StreamWriter("SaveFiles\\" + GAME + ".txt");

            foreach(string insert in tempLines)
            {
                writer.WriteLine(insert);
            }

            writer.WriteLine("[ATTRIBUTES]");
            writer.WriteLine("Level" + " " + Player.Level.Value + " " + Player.Level.MaxValue);
            writer.WriteLine("Experience" + " " + Player.Experience.Value + " " + Player.Experience.MaxValue);
            writer.WriteLine("Health" + " " + Player.Health.Value + " " + Player.BaseHealth.MaxValue);
            writer.WriteLine("Mana" + " " + Player.Mana.Value + " " + Player.BaseMana.MaxValue);
            writer.WriteLine("Stamina" + " " + Player.Stamina.Value + " " + Player.BaseStamina.MaxValue);
            writer.WriteLine("Defense" + " " + Player.BaseDefense.Value + " " + Player.BaseDefense.MaxValue);
            writer.WriteLine("Strength" + " " + Player.BaseStrength.Value + " " + Player.BaseStrength.MaxValue);
            writer.WriteLine("Magic" + " " + Player.BaseMagic.Value + " " + Player.BaseMagic.MaxValue);

            writer.WriteLine();
            writer.WriteLine("[EQUIPMENT]");
            if (player.Inventory.EquipmentLeft.Item != null) writer.Write(player.Inventory.EquipmentLeft.Item.GetType().Name + " ");
            else writer.Write("NONE ");

            if (player.Inventory.EquipmentRight.Item != null) writer.Write(player.Inventory.EquipmentRight.Item.GetType().Name + " ");
            else writer.Write("NONE ");

            if (player.Inventory.EquipmentHead.Item != null) writer.Write(player.Inventory.EquipmentHead.Item.GetType().Name + " ");
            else writer.Write("NONE ");

            if (player.Inventory.EquipmentBody.Item != null) writer.Write(player.Inventory.EquipmentBody.Item.GetType().Name + " ");
            else writer.Write("NONE ");

            if (player.Inventory.EquipmentLegs.Item != null) writer.Write(player.Inventory.EquipmentLegs.Item.GetType().Name + " ");
            else writer.Write("NONE ");

            if (player.Inventory.EquipmentFeet.Item != null) writer.Write(player.Inventory.EquipmentFeet.Item.GetType().Name + " ");
            else writer.Write("NONE ");

            writer.WriteLine();

            writer.WriteLine("[INVENTORY]");
            foreach (InventorySlot slot in player.Inventory.ItemList)
            {
                if (slot.Item != null)
                {
                    int count = slot.Stack;

                    for(int i = 0; i < count; i++)
                        writer.Write(slot.Item.GetType().Name + " ");
                }
            }
            writer.WriteLine();

            writer.WriteLine("[QUESTS]");


            writer.Close();
        }

        #endregion



        //Loads a game from text file
        public void LoadGame(string characterFile)
        {
            //Loads the Character File. The Character File will load the Map File
            Thread loadCharacterThread = new Thread(LoadCharacter);
            GAME = characterFile;
            loadCharacterThread.Start(characterFile);
        }
        
        //Loads a character from text file
        public void LoadCharacter(object characterFile)
        {
            gamePlayScreen.Loading = true;

            Thread.Sleep(1500);
            //cast back to a string
            string file = "SaveFiles\\" + (string)characterFile + ".txt";
            //create player
            player = Player.FromFile(gamePlayScreen.ScreenManager.Content, this, file);
            healthBar = new HealthBar(gamePlayScreen, player, 20, 20);
            manaBar = new ManaBar(gamePlayScreen, player, 20, 40);
            equipmentBar = new EquipmentBar(gamePlayScreen, player);
            
            (gamePlayScreen.ScreenManager.InventoryScreen as InventoryScreen).DisplayInventory = player.Inventory;
            gamePlayScreen.Loading = false;
        }


        private enum Reading { Tilesheet, Terrain, Collision, NPCS, Warps, Songs, NONE }
        //Loads map from text file
        public void LoadMap(string mapFile)
        {
            //This method will load in the map.
            //Called when the player is loaded


            #region MapReadingCode

            entities = new List<Entity>();

            
           
            //Create stream reader object
            StreamReader reader = null;

            try
            {
                //Try and load text file into reader
                reader = new StreamReader(TitleContainer.OpenStream(mapFile));

                List<List<int>> tempTerrainLayout = new List<List<int>>();
                List<List<int>> tempCollisionLayout = new List<List<int>>();
                TileSheet sheet = null;
                List<Entity> tempEntities = new List<Entity>();


                Reading reading = Reading.NONE;

                //While reader hasn't reached end of file
                while (!reader.EndOfStream)
                {
                    //We want to read in all data to create a map object

                    //Get a line from file
                    string line = reader.ReadLine().Trim();

                    //If the line is null or empty we skip reading it
                    if (string.IsNullOrEmpty(line)) continue;

                    if (line.Contains("[TILESET]")) //We are reading tileSheet
                    {
                        reading = Reading.Tilesheet;
                    }
                    else if (line.Contains("[NPCS]"))
                    {
                        reading = Reading.NPCS;
                    }
                    else if (line.Contains("[WARPS]"))
                    {
                        reading = Reading.Warps;

                    }
                    else if (line.Contains("[SONGS]"))
                    {
                        reading = Reading.Songs;
                    }
                    else if (line.Contains("[TERRAIN]")) //We are reading terrain
                    {
                        reading = Reading.Terrain;
                    }
                    else if (line.Contains("[COLLISION]")) //We are reading collision
                    {
                        reading = Reading.Collision;
                    }
                    else if (reading == Reading.Tilesheet)
                    {
                        //Create and store tileSheet
                        sheet = new TileSheet(line, gamePlayScreen.ScreenManager.Content.Load<Texture2D>("TileSheets//" + line));
                    }
                    else if (reading == Reading.NPCS)
                    {
                        //Read in npcs from file
                        if (!string.IsNullOrEmpty(line))
                        {
                            //Split line by spaces to get npc data
                            string[] NPCData = line.Split(' ');

                            Assembly assembly = typeof(ObjectClasses.Enemy.Hostile).Assembly;
                            Type[] types = assembly.GetTypes();
                            Type t = null;

                            foreach (Type type in types)
                            {
                                if (NPCData[0].Equals(type.Name))
                                {
                                    //Create an object of that type
                                    t = type;
                                    ConstructorInfo cInfo = t.GetConstructor(new[] { typeof(ContentManager) });
                                    object obj = cInfo.Invoke(new object[] { gamePlayScreen.ScreenManager.Content });
                                    NPC j = (obj as NPC);
                                    j.Owner = this;
                                    j.Level = new CharacterAttribute("Level", int.Parse(NPCData[1]), int.MaxValue);
                                    j.Position = new Vector2(int.Parse(NPCData[2]), int.Parse(NPCData[3]));
                                    tempEntities.Add(j);
                                    break;
                                }
                            }

                        }
                    }
                    else if (reading == Reading.Warps)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            string[] warpData = line.Split(' ');

                            int warpX = int.Parse(warpData[0]);
                            int warpY = int.Parse(warpData[1]);
                            int warpWidth = int.Parse(warpData[2]);
                            int warpHeight = int.Parse(warpData[3]);
                            string nextMap = warpData[4];
                            Vector2 pSpawn = new Vector2(int.Parse(warpData[5]), int.Parse(warpData[6]));
                            WarpGate wg = new WarpGate(gamePlayScreen.ScreenManager.Content,
                                warpX, warpY, warpWidth, warpHeight, nextMap, pSpawn);
                            wg.Owner = this;
                            tempEntities.Add(wg);
                            Console.WriteLine("WarpGate added");
                        }
                    }
                    else if (reading == Reading.Songs)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            MediaPlayer.Stop();
                            GameAudio.PlaySong(line, true);
                            
                        }
                    }
                    else if (reading == Reading.Terrain)
                    {
                        //Read and store terrain data

                        if (!string.IsNullOrEmpty(line))
                        {
                            //Split line by spaces to retrieve cell #'s
                            string[] cells = line.Split(' ');

                            //Temp list to hold row of cells
                            List<int> row = new List<int>();

                            foreach (string c in cells)
                            {
                                if (!string.IsNullOrEmpty(c))
                                {
                                    row.Add(int.Parse(c));
                                }
                            }

                            //Add each row to temp layout
                            tempTerrainLayout.Add(row);

                        }

                    }
                    else if (reading == Reading.Collision)
                    {
                        //Read and store collision data

                        if (!string.IsNullOrEmpty(line))
                        {
                            //Split line by spaces to retrieve cell #'s
                            string[] cells = line.Split(' ');

                            //Temp list to hold row of cells
                            List<int> row = new List<int>();

                            foreach (string c in cells)
                            {
                                if (!string.IsNullOrEmpty(c))
                                {
                                    row.Add(int.Parse(c));
                                }
                            }

                            //Add each row to temp layout
                            tempCollisionLayout.Add(row);

                        }
                    }
                }

                Console.WriteLine("Map Created");

                //Get width and height of map
                int width = tempTerrainLayout[0].Count;
                int height = tempTerrainLayout.Count;

                //Create map
                curMap = new Map(width, height);
                curMap.Owner = this;
                curMap.Terrain.TileSheet = sheet;


                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                       
                        //Initialize both terrain and collision layers
                        curMap.Terrain.Tiles[y, x] = new Tile(tempTerrainLayout[y][x],
                            new Vector2(x * Tile.TILE_WIDTH, y * Tile.TILE_HEIGHT));
                        curMap.Collision.Tiles[y, x] = new CollisionTile(tempCollisionLayout[y][x],
                            new Vector2(x * Tile.TILE_WIDTH, y * Tile.TILE_HEIGHT));
                    }
                }

                Console.WriteLine("Layers Created");

                curMap.NodeMap.FillMap(curMap);

                foreach(Entity e in tempEntities)
                {
                    AddEntity(e);
                }

                curMap.Collision.Tex = gamePlayScreen.ScreenManager.Content.Load<Texture2D>("Debug//collisionSquare");


                //Close reader
                reader.Close();

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.TargetSite);

            }

            #endregion
        }

        //updates the entities in the map
        public void Update(GameTime gameTime)
        {
            if (gamePlayScreen.Loading)
                return;


            player.Update(gameTime);

            

            foreach (Entity e in entities)
            {
                e.Update(gameTime);
            }

            foreach (Entity e in toAdd)
            {
                PlaceEntity(e);
            }
            toAdd.Clear();

            //foreach (Item item in items)
            //{
            //    if (item.CollidesWith(player))
            //    {
                    
            //        if (Controller.SingleKeyPress(Keys.E))
            //        {
            //            SplashText itemText = new SplashText(this, player.Center, item.GetType().Name, Color.White, 1.5f);
            //            AddEntity(itemText);
            //            player.Inventory.AddItem(item);
            //            RemoveEntity(item);
            //        }
            //    }
            //}

            foreach (Entity e in garbage)
            {
                entities.Remove(e);
                impassableObjects.Remove(e);
                npcs.Remove(e);
                projectiles.Remove(e);
                items.Remove(e);
            }
            garbage.Clear();

        }

        

        //Draws the map
        public void Draw(SpriteBatch spriteBatch)
        {
            if (gamePlayScreen.Loading)
                return;

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp,
                null, null, null, camera.Transform);
            //Draw the map here
            curMap.Draw(spriteBatch);

            //need to draw all game objects here
            foreach (Entity e in entities)
            {
                e.Draw(spriteBatch);
            }

            player.Draw(spriteBatch);

            spriteBatch.End();
            spriteBatch.Begin();

            //draw all foreground objects here
            healthBar.Draw(spriteBatch);
            manaBar.Draw(spriteBatch);
            equipmentBar.Draw(spriteBatch);
        }  
    }
}
