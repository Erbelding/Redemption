using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.TileEngine
{
    public class TileSheet
    {
        //Attributes
        private Texture2D texture;
        private int tilesWide;
        private int tilesHigh;

        //Properties
        public Texture2D Texture { get { return texture; } }
        public int TilesWide { get { return tilesWide; } }
        public int TilesHigh { get { return tilesHigh; } }

        //Constructor
        public TileSheet(Texture2D tex)
        {
            texture = tex;
            tilesWide = texture.Width / Tile.TILE_WIDTH;
            tilesHigh = texture.Height / Tile.TILE_HEIGHT;
        }
    }
}
