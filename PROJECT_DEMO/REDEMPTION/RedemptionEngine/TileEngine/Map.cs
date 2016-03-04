using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.TileEngine
{
    public class Map
    {
        //Attributes
        private TerrainLayer terrainLayer;
        private CollisionLayer collisionLayer;
        private int mapWidth;
        private int mapHeight;
        private int mapWidthInPixels;
        private int mapHeightInPixels;

        //Properties
        public CollisionLayer Collision { get { return collisionLayer; } }

        public int MapWidthInPixels { get { return mapWidthInPixels; } }
        public int MapHeightInPixels { get { return mapHeightInPixels; } }

        //Constructor
        public Map(int width, int height)
        {
            terrainLayer = new TerrainLayer(width, height);
            collisionLayer = new CollisionLayer(width, height);
            mapWidth = width;
            mapHeight = height;
            mapWidthInPixels = mapWidth * Tile.TILE_WIDTH;
            mapHeightInPixels = mapHeight * Tile.TILE_HEIGHT;
        }

        public static Map FromFile(ContentManager content, string fileName)
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
                bool readingTileSheet = false;
                bool readingTerrain = false;
                bool readingCollision = false;

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
                        readingTileSheet = true;
                        readingTerrain = false;
                        readingCollision = false;
                    }
                    else if (line.Contains("[TERRAIN]")) //We are reading terrain
                    {
                        readingTerrain = true;
                        readingTileSheet = false;
                        readingCollision = false;
                    }
                    else if (line.Contains("[COLLISION]")) //We are reading collision
                    {
                        readingCollision = true;
                        readingTerrain = false;
                        readingTileSheet = false;
                    }
                    else if (readingTileSheet)
                    {
                        //Create and store tileSheet
                        sheet = new TileSheet(content.Load<Texture2D>(line));
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

        public bool IsCollideableTile(int x, int y)
        {
            if(x < 0 || x >= mapWidth) return false;
            if (y < 0 || y >= mapHeight) return false;
            return Collision.Tiles[y, x].ColType != CollisionTile.CollisionType.NONE;
        }

        //Draw layers
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw terrain layer
            terrainLayer.Draw(spriteBatch);
            //Draw collision layer
            //collisionLayer.Draw(spriteBatch);
        }

    }
}
