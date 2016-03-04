using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.ObjectClasses;
using RedemptionEngine.TileEngine;
using Microsoft.Xna.Framework;

namespace RedemptionEngine
{
    public class Path
    {
        //Attributes
        private NodeMap nodeMap;

        //Valid move directions
        private Point[] Movements =
        { 
            new Point(0, -1),
            new Point(1, 0),
            new Point(0, 1),
            new Point(-1, 0)
        };

        //Constructor
        public Path(NodeMap nodeMap)
        {
            this.nodeMap = nodeMap;
        }

        public void ClearLogic()
        {
            for (int y = 0; y < nodeMap.Height; y++)
            {
                for (int x = 0; x < nodeMap.Width; x++)
                {
                    nodeMap.Nodes[y, x].Clear();
                }
            }
        }

        //Methods


        //finds next node in a path
        public Queue<PathNode> FindPath(Character start, Character end, Map map)
        {
            int curX = (int)((end.Bounds.X + end.Bounds.Width / 2) / 16);
            int curY = (int)((end.Bounds.Y + end.Bounds.Height / 2) / 16);

            int targetX = (int)((start.Bounds.X + start.Bounds.Width / 2) / 16);
            int targetY = (int)((start.Bounds.Y + start.Bounds.Height / 2) / 16);

            Rectangle size = start.Bounds;
            int curWeight = 0;
            List<PathNode> curLevel = new List<PathNode>();
            PathNode curNode = nodeMap.Nodes[curY, curX];
            curNode.Weight = curWeight;
            curLevel.Add(curNode);

            while (true)
            {
                bool foundTarget = false;
                curWeight++;
                List<PathNode> nextLevel = new List<PathNode>();


                foreach (PathNode node in curLevel)
                {
                    //add the adjacent nodes that haven't been added yet to
                    //the next level and set their weights
                    
                    foreach (PathNode adjNode in GetAdjacentNodes(node))
                    {
                        Rectangle rectToCheck = new Rectangle((int)adjNode.Pos.X - (int)start.Bounds.Width/2, (int)adjNode.Pos.Y - (int)start.Bounds.Height/2, start.Bounds.Width, start.Bounds.Height);
                        if (adjNode.Weight > curWeight && adjNode.IsWalkable && !start.IsCollidingWithMap(map, rectToCheck))
                        {
                            adjNode.Previous = node;
                            adjNode.IsPath = true;
                            adjNode.Weight = curWeight;
                            nextLevel.Add(adjNode);

                            //If adjacent node is equal to target node
                            if (adjNode == nodeMap.Nodes[targetY, targetX])
                            {
                                //We have found a path
                                foundTarget = true;
                            }
                        }
                    }
                }

                curLevel = nextLevel;

                if (foundTarget) break;

                if (curLevel.Count == 0 || curWeight >= 32) break; //unreachable target

            }


            Queue<PathNode> path = new Queue<PathNode>();
            //now we have our path
            foreach (PathNode node in GetAdjacentNodes(nodeMap.Nodes[targetY, targetX]))
            {
                if (node.Weight < nodeMap.Nodes[targetY, targetX].Weight)
                {
                    return node.Path(path);
                }
            }

            return path;
        }

        private List<PathNode> GetAdjacentNodes(PathNode node)
        {
            List<PathNode> nodes = new List<PathNode>();

            if (ValidCoordinates((int)node.Pos.X / 16, (int)node.Pos.Y / 16 - 1))
            {
                nodes.Add(nodeMap.Nodes[(int)node.Pos.Y / 16 - 1, (int)node.Pos.X / 16]);
            }
            if (ValidCoordinates((int)node.Pos.X / 16, (int)node.Pos.Y / 16 + 1))
            {
                nodes.Add(nodeMap.Nodes[(int)node.Pos.Y / 16 + 1, (int)node.Pos.X / 16]);
            }
            if (ValidCoordinates((int)node.Pos.X / 16 - 1, (int)node.Pos.Y / 16))
            {
                nodes.Add(nodeMap.Nodes[(int)node.Pos.Y / 16, (int)node.Pos.X / 16 - 1]);
            }
            if (ValidCoordinates((int)node.Pos.X / 16 + 1, (int)node.Pos.Y / 16))
            {
                nodes.Add(nodeMap.Nodes[(int)node.Pos.Y / 16, (int)node.Pos.X / 16 + 1]);
            }

            return nodes;
        }

        public void HighlightPath(Character start, Character end)
        {
            
        }

        private bool ValidCoordinates(int x, int y)
        {
            //Make sure the coordinates are within node map range
            if (x < 0)
            {
                return false;
            }
            if (y < 0)
            {
                return false;
            }
            if (x >= nodeMap.Width)
            {
                return false;
            }
            if (y >= nodeMap.Height)
            {
                return false;
            }
            return true;
        }

        private IEnumerable<Point> ValidNodes(int x, int y)
        {
            //Returns each valid node
            foreach (Point p in Movements)
            {
                int newX = x + p.X;
                int newY = y + p.Y;

                if (ValidCoordinates(newX, newY) &&
                    nodeMap.Nodes[newY,newX].IsWalkable)
                {
                    yield return new Point(newX, newY);
                }
            }
        }
    }
}
