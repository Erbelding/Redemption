/************************************************************************/
/* Matt Guerrette
 * Date: 3/15/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using MapEditor.Editor;
using MapEditor.Controls;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace MapEditor.Engine
{
    //Class for reading in maps from text files
    public class MapReader
    {
        //Attributes
        private StreamReader reader;
        private EditorForm owner;
        private string fileName;

        //Constructor
        public MapReader(EditorForm owner, string fileName)
        {
            this.fileName = fileName;
            this.owner = owner;
        }

        //reads, builds, and returns a map from text file
        public Map Read()
        {
            //Create map variable
            Map map = null;

            try
            {
                //Initialize reader using filePath to content project file
                reader = new StreamReader(fileName);

                //Temp variables
                List<List<int>> tempLayout = new List<List<int>>();
                List<List<int>> tempColLayout = new List<List<int>>();
                string tileSheet = null;
                bool readingTileSet = false;
                bool readingTerrain = false;
                bool readingCollision = false;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().Trim();

                    if (string.IsNullOrEmpty(line))
                        continue;

                    if (line.Contains("[TILESET]"))
                    {
                        readingTileSet = true;
                        readingTerrain = false;
                        readingCollision = false;
                    }
                    else if (line.Contains("[TERRAIN]"))
                    {
                        readingTileSet = false;
                        readingCollision = false;
                        readingTerrain = true;
                    }
                    else if (line.Contains("[COLLISION]"))
                    {
                        readingCollision = true;
                        readingTerrain = false;
                        readingTileSet = false;
                    }
                    else if (readingTileSet)
                    {
                        tileSheet = line;
                    }
                    else if (readingTerrain)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            string[] cells = line.Split(' ');

                            List<int> row = new List<int>();

                            foreach (string c in cells)
                            {
                                if (!string.IsNullOrEmpty(c))
                                {
                                    row.Add(int.Parse(c));
                                }
                            }

                            tempLayout.Add(row);
                        }
                    }
                    else if (readingCollision)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            string[] cells = line.Split(' ');

                            List<int> row = new List<int>();

                            foreach (string c in cells)
                            {
                                if (!string.IsNullOrEmpty(c))
                                {
                                    row.Add(int.Parse(c));
                                }
                            }

                            tempColLayout.Add(row);
                        }
                    }
                }

                int width = tempLayout[0].Count;
                int height = tempLayout.Count;

                Bitmap bmp = new Bitmap("TileSets\\" + tileSheet + ".png");
                Texture2D tex = null;

                using (MemoryStream s = new MemoryStream())
                {
                    bmp.Save(s, System.Drawing.Imaging.ImageFormat.Png);
                    s.Seek(0, SeekOrigin.Begin);
                    tex = Texture2D.FromStream(owner.MapPanel.GraphicsDevice, s);
                }

                map = new Map(owner.MapPanel, owner.MapPanel.Content, width, height, 32, 32);
                TileSheet sheet = new TileSheet(map, tex, tileSheet);
                map.TileSheet = sheet;
                owner.TilePalette.TileSheet = sheet;
                

                for (int y = 0; y < map.MapHeight; y++)
                {
                    for (int x = 0; x < map.MapWidth; x++)
                    {
                        map.Layer.Tiles[y, x] = new TerrainTile(tempLayout[y][x],
                            new Vector2(x * 32, y * 32), 32, 32);
                        map.ColLayer.Tiles[y, x] = new CollisionTile(tempColLayout[y][x],
                            new Vector2(x * 32, y * 32), tempColLayout[y][x], 32, 32);
                    }
                }

                reader.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            return map;
        }
    }
}
