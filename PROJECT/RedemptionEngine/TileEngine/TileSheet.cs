/************************************************************************/
/* Matt Guerrette
 * Date: 3/22/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.TileEngine
{
    //Represents a tile sheet
    public class TileSheet
    {
        //Attributes
        private Texture2D texture;
        private int tilesWide;
        private int tilesHigh;
        private string name;

        //Properties
        public Texture2D Texture { get { return texture; } }
        public int TilesWide { get { return tilesWide; } }
        public int TilesHigh { get { return tilesHigh; } }
        public string Name { get { return name; } }

        //Constructor
        public TileSheet(string name, Texture2D tex)
        {
            this.name = name;
            texture = tex;
            tilesWide = texture.Width / Tile.TILE_WIDTH;
            tilesHigh = texture.Height / Tile.TILE_HEIGHT;
        }
    }
}
