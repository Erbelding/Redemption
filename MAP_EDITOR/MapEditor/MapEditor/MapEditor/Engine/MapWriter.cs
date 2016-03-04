/************************************************************************/
/* Matt Guerrette
 * Date: 3/15/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MapEditor.Engine
{
    //Class that writes out map info to text file
    public class MapWriter
    {
        //Attributes
        private StreamWriter writer;
        private Stream fileStream;
        private Map map;

        //Constructor
        public MapWriter(Stream stream, Map map)
        {
            this.fileStream = stream;
            this.map = map;
        }

        //Writes map data to text file
        public void Write()
        {
            try
            {
                writer = new StreamWriter(fileStream);

                

                writer.WriteLine("[TILESET]");
                if(map.TileSheet != null)
                    writer.WriteLine(map.TileSheet.FileName);
                writer.WriteLine();

                writer.WriteLine("[TERRAIN]");

                for (int y = 0; y < map.MapHeight; y++)
                {
                    for (int x = 0; x < map.MapWidth; x++)
                    {
                        int id = map.Layer.Tiles[y, x].ID;

                        writer.Write(id + " ");
                    }
                    writer.WriteLine();
                }

                writer.WriteLine();

                writer.WriteLine("[COLLISION]");

                for (int y = 0; y < map.MapHeight; y++)
                {
                    for (int x = 0; x < map.MapWidth; x++)
                    {
                        int id = map.ColLayer.Tiles[y, x].ID;

                        writer.Write(id + " ");
                    }
                    writer.WriteLine();
                }


                writer.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
