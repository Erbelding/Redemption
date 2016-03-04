/************************************************************************/
/* Matt Guerrette
 * Date: 3/22/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Microsoft.Xna.Framework;
using RedemptionEngine.ObjectClasses;
using System.Reflection;

namespace RedemptionEngine.TileEngine
{
    //Represents a map
    public class Map
    {
        //Attributes
        private TerrainLayer terrainLayer;
        private CollisionLayer collisionLayer;
        private int mapWidth;
        private int mapHeight;
        private int mapWidthInPixels;
        private int mapHeightInPixels;
        private List<Entity> entities;
        private GameManager owner;
        private NodeMap nodeMap;

        //Properties
        public CollisionLayer Collision { get { return collisionLayer; } }
        public TerrainLayer Terrain { get { return terrainLayer; } }
        public NodeMap NodeMap { get { return nodeMap; } }
        public GameManager Owner { get { return owner; } set { owner = value; } }

        public int MapWidth { get { return mapWidth; } }
        public int MapHeight { get { return mapHeight; } }
        public int MapWidthInPixels { get { return mapWidthInPixels; } }
        public int MapHeightInPixels { get { return mapHeightInPixels; } }
        public List<Entity> Entities { get { return entities; } }

        //Constructor
        public Map(int width, int height)
        {
            terrainLayer = new TerrainLayer(width, height);
            collisionLayer = new CollisionLayer(width, height);
            nodeMap = new NodeMap(width, height);
            

            mapWidth = width;
            mapHeight = height;
            mapWidthInPixels = mapWidth * Tile.TILE_WIDTH;
            mapHeightInPixels = mapHeight * Tile.TILE_HEIGHT;
            entities = new List<Entity>();
        }

        //Loads map from file
        public static Map FromFile(ContentManager content, GameManager owner, string fileName)
        {
            //Create map object
            Map map = null;
            //Create stream reader object
            StreamReader reader = null;

            try
            {
                //Try and load text file into reader
                reader = new StreamReader(TitleContainer.OpenStream(fileName));

                List<List<int>> tempTerrainLayout = new List<List<int>>();
                List<List<int>> tempCollisionLayout = new List<List<int>>();
                TileSheet sheet = null;
                List<Entity> tempEntities = new List<Entity>();
                bool readingTileSheet = false;
                bool readingTerrain = false;
                bool readingCollision = false;
                bool readingNPCS = false;
                bool readingWarps = false;

                //While reader hasn't reached end of file
                while (!reader.EndOfStream)
                {
                    //We want to read in all data to create a map object

                    //Get a line from file
                    string line = reader.ReadLine().Trim();

                    //If the line is null or empty we skip reading it
                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (line.Contains("[TILESET]")) //We are reading tileSheet
                    {
                        readingWarps = false;
                        readingTileSheet = true;
                        readingTerrain = false;
                        readingCollision = false;
                        readingNPCS = false;
                    }
                    else if (line.Contains("[NPCS]"))
                    {
                        readingWarps = false;
                        readingNPCS = true;
                        readingTileSheet = false;
                        readingTerrain = false;
                        readingCollision = false;
                    }
                    else if (line.Contains("[WARPS]"))
                    {
                        readingWarps = true;
                        readingNPCS = false;
                        readingTileSheet = false;
                        readingTerrain = false;
                        readingCollision = false;
                    }
                    else if (line.Contains("[TERRAIN]")) //We are reading terrain
                    {
                        readingTerrain = true;
                        readingTileSheet = false;
                        readingCollision = false;
                        readingNPCS = false;
                        readingWarps = false;
                    }
                    else if (line.Contains("[COLLISION]")) //We are reading collision
                    {
                        readingCollision = true;
                        readingTerrain = false;
                        readingTileSheet = false;
                        readingNPCS = false;
                        readingWarps = false;
                    }
                    else if (readingTileSheet)
                    {
                        //Create and store tileSheet
                        sheet = new TileSheet(line, content.Load<Texture2D>(line));
                    }
                    else if (readingNPCS)
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
                                    object obj = cInfo.Invoke(new object[] { owner.gamePlayScreen.ScreenManager.Content });
                                    NPC j = (obj as NPC);
                                    j.Owner = owner;
                                    //j.Level.Value = int.Parse(NPCData[1]);
                                    j.Position = new Vector2(int.Parse(NPCData[2]), int.Parse(NPCData[3]));
                                    tempEntities.Add(j);
                                    break;
                                }
                            }

                        }
                    }
                    else if (readingWarps)
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
                            WarpGate wg = new WarpGate(owner.gamePlayScreen.ScreenManager.Content,
                                warpX, warpY, warpWidth, warpHeight, nextMap, pSpawn);
                            wg.Owner = owner;
                            tempEntities.Add(wg);
                            Console.WriteLine("WarpGate added");
                        }
                    }
                    else if (readingTerrain)
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
                    else if (readingCollision)
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

                //Get width and height of map
                int width = tempTerrainLayout[0].Count;
                int height = tempTerrainLayout.Count;

                //Create map
                map = new Map(width, height);
                map.owner = owner;
                map.entities = tempEntities;
                map.terrainLayer.TileSheet = sheet;
               

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        //Initialize both terrain and collision layers
                        map.terrainLayer.Tiles[y, x] = new Tile(tempTerrainLayout[y][x],
                            new Vector2(x * Tile.TILE_WIDTH, y * Tile.TILE_HEIGHT));
                        map.collisionLayer.Tiles[y, x] = new CollisionTile(tempCollisionLayout[y][x],
                            new Vector2(x * Tile.TILE_WIDTH, y * Tile.TILE_HEIGHT));
                    }
                }

                map.nodeMap.FillMap(map);

                foreach (Entity e in map.entities)
                {
                    if(e is NPC)
                    {
                        (e as NPC).path = new Path(map.NodeMap);
                    }
                }

                //Close reader
                reader.Close();
                
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Return the map
            return map;
        }

        //Determines if tile at X,Y is a collideable tile
        public bool IsCollideableTile(int x, int y)
        {
            if (Collision != null)
            {


                if (x < 0 || x >= mapWidth) return false;
                if (y < 0 || y >= mapHeight) return false;
                return Collision.Tiles[y, x].ColType != CollisionTile.CollisionType.NONE;
            }

            return false;
           
        }

        //Draw layers
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw terrain layer
            terrainLayer.Draw(owner, spriteBatch);

            //SortEntities();

            foreach (Entity e in entities)
            {
                e.Draw(spriteBatch);
            }

            //draw nodes
            //nodeMap.Draw(spriteBatch);

            //Draw collision layer
            //collisionLayer.Draw(owner, spriteBatch);
        }

        //Sorts the entity list
        private void SortEntities()
        {
            if (entities.Count > 1)
            {
                entities.Sort(delegate(Entity entity1, Entity entity2)
                {
                    return entity1.Position.Y.CompareTo(entity2.Position.Y);
                });
            }
        }

    }
}
