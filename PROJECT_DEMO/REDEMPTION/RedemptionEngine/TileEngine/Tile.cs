using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.TileEngine
{
    public class Tile
    {
        //Constants
        public const int TILE_WIDTH = 32;
        public const int TILE_HEIGHT = 32;

        //Attributes
        protected int tileID;
        protected Vector2 tilePos;
        protected Rectangle bounds;

        //Properties

        public int ID { get { return tileID; } }
        public Vector2 Pos { get { return tilePos; } }

        public Rectangle Bounds
        {
            get { return bounds; }
        }

        //Constructor
        public Tile(int id, Vector2 pos)
        {
            tileID = id;
            tilePos = pos;
            //bounds = new Rectangle((int)pos.X, (int)pos.Y, TILE_WIDTH, TILE_HEIGHT);
        }

    }
}
