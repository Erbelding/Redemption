using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.TileEngine
{
    public class CollisionLayer
    {
        //Attributes
        private CollisionTile[,] tiles;

        public Texture2D Tex;

        //Properties
        public CollisionTile[,] Tiles
        {
            get { return tiles; }
        }

        //Constructor
        public CollisionLayer(int width, int height)
        {
            //Init tiles
            tiles = new CollisionTile[height, width];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < tiles.GetLength(0); y++)
            {
                for (int x = 0; x < tiles.GetLength(1); x++)
                {
                    spriteBatch.Draw(Tex, tiles[y,x].Bounds, Color.Red);
                }
            }
        }
    }
}
