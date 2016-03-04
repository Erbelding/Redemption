using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RedemptionEngine.TileEngine;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine
{
    public class PathFinder
    {
        /*
        public NodeMap nodeMap;

        private Point[] Movements =
        { 
            new Point(0, -1),
            new Point(1, 0),
            new Point(0, 1),
            new Point(-1, 0)
        };

        public PathFinder(NodeMap nodeMap)
        {
            this.nodeMap = nodeMap;
        }

        public void ClearNodes()
        {
            ////Reset every node
            //foreach (Point p in AllNodes())
            //{
            //    nodes[p.Y, p.X] = new PathNode();
            //}
        }

        public void ClearLogic()
        {
            for (int y = 0; y < nodeMap.Height; y++)
            {
                for (int x = 0; x < nodeMap.Width; x++)
                {
                    nodeMap
                }
            }
        }

        static private bool ValidCoordinates(Map map, int x, int y)
        {
            
            if (x < 0)
            {
                return false;
            }
            if (y < 0)
            {
                return false;
            }
            if (x >= map.MapWidth)
            {
                return false;
            }
            if (y >= map.MapHeight)
            {
                return false;
            }
            return true;
        }

        public void FindPath(Map map, Character start, Character target)
        {
            int startX = (int)(start.Position.X / Tile.TILE_WIDTH);
            int startY = (int)(start.Position.Y / Tile.TILE_HEIGHT);

            if (startY == -1 || startX == -1)
            {
                return;
            }

            nodes[startY, startX].Weight = 0;

            while (true)
            {
                bool progress = false;

                //Look at each node in grid
                foreach (Point p in AllNodes())
                {
                    int x = p.X;
                    int y = p.Y;

                    if (!map.IsCollideableTile(x, y))
                    {
                        int pass = nodes[y, x].Weight;

                        foreach (Point newP in ValidNodes(map, x, y))
                        {
                            int newX = newP.X;
                            int newY = newP.Y;
                            int newPass = pass + 1;

                            if (nodes[newY, newX].Weight > newPass)
                            {
                                nodes[newY, newX].Weight = newPass;
                                progress = true;
                            }
                        }

                    }
                }

                if (!progress)
                {
                    break;
                }
            }
        }

        public void HighlightPath(Map map, Character start, Character finish)
        {
            //Mark path from target to start
            int startX = (int)(finish.Position.X / Tile.TILE_WIDTH);
            int startY = (int)(finish.Position.Y / Tile.TILE_HEIGHT);

            if (startY == -1 || startX == -1)
                return;

            while (true)
            {
                Point lowestP = Point.Zero;
                int lowest = 10000;

                foreach (Point p in ValidNodes(map, startX, startY))
                {
                    int count = nodes[p.Y, p.X].Weight;
                    if (count < lowest)
                    {
                        lowest = count;
                        lowestP.X = p.X;
                        lowestP.Y = p.Y;
                    }
                }

                if (lowest != 10000)
                {
                    nodes[lowestP.Y, lowestP.X].IsPath = true;
                    startX = lowestP.X;
                    startY = lowestP.Y;
                }
                else
                {
                    break;
                }

                int sX = (int)(start.Position.X / Tile.TILE_WIDTH);
                int sY = (int)(start.Position.Y / Tile.TILE_HEIGHT);

                if (nodes[startY, startX] == nodes[sY, sX])
                    break;
            }
        }

        private IEnumerable<Point> AllNodes()
        {
            for (int y = 0; y < nodes.GetLength(0); y++)
            {
                for (int x = 0; x < nodes.GetLength(1); x++)
                {
                    yield return new Point(x, y);
                }
            }
        }

        private IEnumerable<Point> ValidNodes(Map map, int x, int y)
        {
            //Returns each valid node
            foreach (Point p in Movements)
            {
                int newX = x + p.X;
                int newY = y + p.Y;

                if (ValidCoordinates(map, newX, newY) &&
                    !map.IsCollideableTile(newX, newY))
                {
                    yield return new Point(newX, newY);
                }
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < nodes.GetLength(1); y++)
            {
                for (int x = 0; x < nodes.GetLength(0); x++)
                {
                    PathNode p = nodes[y, x];

                    if (p.IsPath)
                    {
                        spriteBatch.Draw(PathNode.NodeTex, new Vector2(x * 32, y * 32), Color.White);
                    }
                }
            }
        }
         * */
    }
}
