using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.TileEngine
{
    public class CollisionTile : Tile
    {
        //Defines the type of collision a tile has
        public enum CollisionType
        {
            NONE,
            IMPASSABLE
        }

        //Defines what area of collision a tile has
        public enum CollisionArea
        {
            NONE,
            FULL,
            TOP,
            RIGHT,
            BOTTOM,
            LEFT,
            TOP_LEFT_DUAL,
            TOP_RIGHT_DUAL,
            BOTTOM_LEFT_DUAL,
            BOTTOM_RIGHT_DUAL,
            BOTTOM_RIGHT,
            BOTTOM_LEFT,
            TOP_RIGHT,
            TOP_LEFT
        }


        //Attributes
        private CollisionType colType = CollisionType.IMPASSABLE;
        private CollisionArea colArea;

        public CollisionType ColType { get { return colType; } }


        //Constructor
        public CollisionTile(int id, Vector2 pos)
            : base(id, pos)
        {
            SetBounds(id);
        }

        private void SetBounds(int id)
        {
            //Switch on id
            switch (id)
            {
                case (int)CollisionArea.NONE:
                    colArea = CollisionArea.NONE;
                    colType = CollisionType.NONE;
                    break;

                case (int)CollisionArea.FULL:
                    colArea = CollisionArea.FULL;
                    bounds = new Rectangle((int)tilePos.X, (int)tilePos.Y,
                                          TILE_WIDTH, TILE_HEIGHT);
                    break;

                case (int)CollisionArea.TOP:
                    colArea = CollisionArea.TOP;
                    bounds = new Rectangle((int)tilePos.X, (int)tilePos.Y,
                                          TILE_WIDTH, TILE_HEIGHT / 2);
                    break;

                case (int)CollisionArea.BOTTOM:
                    colArea = CollisionArea.BOTTOM;
                    bounds = new Rectangle((int)tilePos.X, (int)(tilePos.Y + TILE_HEIGHT / 2),
                                          TILE_WIDTH, TILE_HEIGHT / 2);
                    break;

                case (int)CollisionArea.LEFT:
                    colArea = CollisionArea.LEFT;
                    bounds = new Rectangle((int)tilePos.X, (int)tilePos.Y,
                                          TILE_WIDTH / 2, TILE_HEIGHT);
                    break;
                    
                case (int)CollisionArea.RIGHT:
                    colArea = CollisionArea.RIGHT;
                    bounds = new Rectangle((int)(tilePos.X + TILE_WIDTH / 2), (int)tilePos.Y,
                                          TILE_WIDTH / 2, TILE_HEIGHT);
                    break;

                case (int)CollisionArea.BOTTOM_LEFT:
                    colArea = CollisionArea.BOTTOM_LEFT;
                    bounds = new Rectangle((int)tilePos.X, (int)(tilePos.Y + TILE_HEIGHT / 2),
                                         TILE_WIDTH / 2, TILE_HEIGHT / 2);
                    break;

                case (int)CollisionArea.BOTTOM_RIGHT:
                    colArea = CollisionArea.BOTTOM_RIGHT;
                    bounds = new Rectangle((int)(tilePos.X + TILE_WIDTH / 2), (int)(tilePos.Y + TILE_HEIGHT / 2),
                                            TILE_WIDTH / 2, TILE_HEIGHT / 2);
                    break;

                case (int)CollisionArea.TOP_LEFT:
                    colArea = CollisionArea.TOP_LEFT;
                    bounds = new Rectangle((int)tilePos.X, (int)tilePos.Y, TILE_WIDTH / 2, TILE_HEIGHT / 2);
                    break;

                case (int)CollisionArea.TOP_RIGHT:
                    colArea = CollisionArea.TOP_RIGHT;
                    bounds = new Rectangle((int)(tilePos.X + TILE_WIDTH / 2), (int)tilePos.Y,
                                          TILE_WIDTH / 2, TILE_HEIGHT / 2);
                    break;
            }
        }
    }
}
