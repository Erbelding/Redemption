using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.TileEngine;

namespace RedemptionEngine
{
    public class PathNode
    {
        public static Texture2D NodeTex;

        public const int INIT_WEIGHT = 999999;

        //Attributes

        private int weight;
        private bool walkable;
        private bool isPath;
        private PathNode previous;
        private Vector2 pos;

        public PathNode()
        {
            weight = INIT_WEIGHT;
            walkable = true;
            isPath = false;
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public bool IsWalkable
        {
            get { return walkable; }
            set { walkable = value; }
        }

        public PathNode Previous
        {
            get { return previous; }
            set { previous = value; }
        }

        public bool IsPath
        {
            get { return isPath; }
            set { isPath = value; }
        }

        public void Clear()
        {
            IsPath = false;
            Previous = null;
            Weight = INIT_WEIGHT;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(NodeTex, new Vector2(pos.X - 16, pos.Y - 16), Color.White);
        }

        public Queue<PathNode> Path(Queue<PathNode> path)
        {
            path.Enqueue(this);
            if(previous != null)
            {
                path = previous.Path(path);
            }
            return path;
        }

     }
}
