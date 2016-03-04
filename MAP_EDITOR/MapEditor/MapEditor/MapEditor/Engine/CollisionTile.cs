/************************************************************************/
/* Matt Guerrette
 * Date: 3/15/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Controls;

namespace MapEditor.Engine
{
    //Represents a collision tile
    public class CollisionTile : Tile
    {
        //Basic collision types
        public enum CollisionType
        {
            NONE,
            IMPASSABLE
        }

        //Attributes
        private CollisionType collisionType; //collision type

        //Property

        //Gets the collision type
        private CollisionType ColType { get { return collisionType; } }

        //Constructor
        public CollisionTile(int id, Vector2 pos, int colID, int tileWidth, int tileHeight)
            : base(id, pos, tileWidth, tileHeight)
        {
            //Set collision type
            SetCollisionType(colID);
        }

        //Set the collision type based on collision id
        private void SetCollisionType(int colID)
        {
            //Set collision type
            switch (colID)
            {
                case (int)CollisionType.NONE:
                    collisionType = CollisionType.NONE;
                    break;

                case (int)CollisionType.IMPASSABLE:
                    collisionType = CollisionType.IMPASSABLE;
                    break;
            }
        }

        //Converts the collision tile id
        private void SetID(int id)
        {
            //Convert here
            //........
            #region BIG SWITCH
            switch (id)
            {
                case 0:
                    tileID = 6;
                    break;
                case 1:
                    tileID = 2;
                    break;
                case 2:
                    tileID = 7;
                    break;
                case 3:
                    tileID = 10;
                    break;
                case 4:
                    tileID = 4;
                    break;
                case 5:
                    tileID = 11;
                    break;
                case 6:
                    tileID = 5;
                    break;
                case 7:
                    tileID = 0;
                    break;
                case 8:
                    tileID = 3;
                    break;
                case 9:
                    tileID = 3;
                    break;
                case 10:
                    tileID = 1;
                    break;
                case 11:
                    tileID = 5;
                    break;
                case 12:
                    tileID = 8;
                    break;
                case 13:
                    tileID = 4;
                    break;
                case 14:
                    tileID = 9;
                    break;
                case 15:
                    tileID = 12;
                    break;
                case 16:
                    tileID = 2;
                    break;
                case 17:
                    tileID = 13;
                    break;
            }
            #endregion
        }
        
        //Event when tile is clicked
        protected override void TileClicked(object sender, EventArgs e)
        {
            SetID(CollisionPalette.ID);
            //tileID = CollisionPalette.ID;
            SetCollisionType(CollisionPalette.ID);
        }
    }
}
