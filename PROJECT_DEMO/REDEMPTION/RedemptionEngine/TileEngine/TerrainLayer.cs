using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.TileEngine
{
    public class TerrainLayer
    {
        //Attributes
        private Tile[,] tiles;
        private TileSheet tileSheet;
        
        //Properties
        public Tile[,] Tiles
        {
            get { return tiles; }
        }

        public TileSheet TileSheet { set { tileSheet = value; } }

        //Constructor
        public TerrainLayer(int width, int height)
        {
            //Init tiles array
            tiles = new Tile[height, width];
        }

        //Draw layer
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < tiles.GetLength(0); y++)
            {
                for (int x = 0; x < tiles.GetLength(1); x++)
                {
                    Tile curTile = tiles[y, x];

                    int id = curTile.ID;
                    int xPos = id % tileSheet.TilesWide;
                    int yPos = (id - xPos) / tileSheet.TilesWide;

                    Rectangle sourceRect = new Rectangle(xPos * Tile.TILE_WIDTH, yPos * Tile.TILE_HEIGHT,
                        Tile.TILE_WIDTH, Tile.TILE_HEIGHT);

                    //spriteBatch.Draw(tileSheet.Texture, curTile.Pos - new Vector2(EditorForm.HScrollValue,
                    //    EditorForm.VScrollValue), sourceRect, Color.White);

                    spriteBatch.Draw(tileSheet.Texture, curTile.Pos, sourceRect, Color.White);
                }
            }
        }
    }
}
