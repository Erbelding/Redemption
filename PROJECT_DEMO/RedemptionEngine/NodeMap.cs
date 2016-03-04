using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.TileEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine
{
    public class NodeMap
    {
        //Attributes
        private PathNode[,] nodes;
        private int width;
        private int height;

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public PathNode[,] Nodes { get { return nodes; } set { nodes = value; } }

        //Constructor
        public NodeMap(int mapWidth, int mapHeight)
        {
            width = (mapWidth * 2 + 1);
            height = (mapHeight * 2 + 1);
            nodes = new PathNode[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    nodes[y, x] = new PathNode();
                    nodes[y, x].Pos = new Vector2(x * 16, y * 16);
                }
            }
        }

        public void FillMap(Map map)
        {
            for (int y = 0; y < map.MapHeight; y++)
            {
                for (int x = 0; x < map.MapWidth; x++)
                {

                    CollisionTile.CollisionArea colArea = map.Collision.Tiles[y,x].ColArea;

                    

                    switch (colArea)
                    {

                        case CollisionTile.CollisionArea.BOTTOM:
                            nodes[y * 2 + 1, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 2].IsWalkable = false;
                            break;
                        case CollisionTile.CollisionArea.BOTTOM_LEFT:
                            nodes[y * 2 + 1, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 1].IsWalkable = false;
                            break;
                        case CollisionTile.CollisionArea.BOTTOM_LEFT_DUAL:
                            nodes[y * 2 , x * 2 ].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 2].IsWalkable = false;
                            break;
                        case CollisionTile.CollisionArea.BOTTOM_RIGHT:
                            nodes[y * 2 + 1, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 2].IsWalkable = false;
                            break;
                        case CollisionTile.CollisionArea.BOTTOM_RIGHT_DUAL:
                            nodes[y * 2 , x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 2].IsWalkable = false;
                            break;
                        case CollisionTile.CollisionArea.FULL:
                            nodes[y * 2 , x * 2 ].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 2].IsWalkable = false;
                            break;
                        case CollisionTile.CollisionArea.LEFT:
                            nodes[y * 2 , x * 2 ].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 1].IsWalkable = false;
                            break;
                        case CollisionTile.CollisionArea.NONE:
                            break;
                        case CollisionTile.CollisionArea.RIGHT:
                            nodes[y * 2 , x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 2].IsWalkable = false;
                            break;
                        case CollisionTile.CollisionArea.TOP:
                            nodes[y * 2 , x * 2 ].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 2].IsWalkable = false;
                            break;
                        case CollisionTile.CollisionArea.TOP_LEFT:
                            nodes[y * 2 , x * 2 ].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 1].IsWalkable = false;
                            break;
                        case CollisionTile.CollisionArea.TOP_LEFT_DUAL:
                            nodes[y * 2 , x * 2 ].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 1].IsWalkable = false;
                            break;
                        case CollisionTile.CollisionArea.TOP_RIGHT:
                            nodes[y * 2 , x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 2].IsWalkable = false;
                            break;
                        case CollisionTile.CollisionArea.TOP_RIGHT_DUAL:
                            nodes[y * 2 , x * 2 ].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 , x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 ].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 1, x * 2 + 2].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 1].IsWalkable = false;
                            nodes[y * 2 + 2, x * 2 + 2].IsWalkable = false;
                            break;
                            
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < nodes.GetLength(1); y++)
            {
                for (int x = 0; x < nodes.GetLength(0); x++)
                {
                    if(nodes[y,x].IsPath)
                        nodes[y, x].Draw(spriteBatch);
                }
            }
        }
    }
}
