/************************************************************************/
/* Matt Guerrette
 * Date: 3/22/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.TileEngine
{
    //Represents a tile object
    public class Tile
    {
        //Constants
        public const int TILE_WIDTH = 32;
        public const int TILE_HEIGHT = 32;

        //Attributes
        protected int tileID;
        protected Vector2 tilePos;
        

        //Properties

        public int ID { get { return tileID; } }
        public Vector2 Pos { get { return tilePos; } }

        

        //Constructor
        public Tile(int id, Vector2 pos)
        {
            tileID = id;
            tilePos = pos;
        }

    }
}
