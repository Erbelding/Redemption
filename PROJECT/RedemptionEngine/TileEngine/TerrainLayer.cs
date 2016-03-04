/************************************************************************/
/* Matt Guerrette
 * Date: 3/22/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.TileEngine
{
    //Represents a terrain layer
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

        public TileSheet TileSheet { get { return tileSheet; } set { tileSheet = value; } }

        //Constructor
        public TerrainLayer(int width, int height)
        {
            //Init tiles array
            tiles = new Tile[height, width];
        }

        //Draw layer
        public void Draw(GameManager owner, SpriteBatch spriteBatch)
        {
            float camPosX = owner.camera.cameraPos.X;
            float camPosY = owner.camera.cameraPos.Y;

            int leftTile = (int)Math.Floor((float)camPosX / Tile.TILE_WIDTH);
            int topTile = (int)Math.Floor((float)camPosY / Tile.TILE_HEIGHT);
            int bottomTile = (int)Math.Ceiling((float)(camPosY + owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Height) / Tile.TILE_HEIGHT) - 1;
            int rightTile = (int)Math.Ceiling((float)(camPosX + owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Width) / Tile.TILE_WIDTH) - 1;

            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x <= rightTile; ++x)
                {
                    if(owner.CurrentMap != null)
                        if (x < 0 || y < 0 || x > owner.CurrentMap.MapWidth  -1 || y > owner.CurrentMap.MapHeight - 1)
                            continue;

                    Tile curTile = tiles[y, x];

                    int id = curTile.ID;
                    int xPos = id % tileSheet.TilesWide;
                    int yPos = (id - xPos) / tileSheet.TilesWide;

                    Rectangle sourceRect = new Rectangle(xPos * Tile.TILE_WIDTH, yPos * Tile.TILE_HEIGHT,
                        Tile.TILE_WIDTH, Tile.TILE_HEIGHT);

                    spriteBatch.Draw(tileSheet.Texture, curTile.Pos, sourceRect, Color.White);
                }
            }


            //for (int y = 0; y < tiles.GetLength(0); y++)
            //{
            //    for (int x = 0; x < tiles.GetLength(1); x++)
            //    {
            //        Tile curTile = tiles[y, x];

            //        //float camPosX = owner.camera.cameraPos.X - (owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Width / 2);
            //        //float camPosY = owner.camera.cameraPos.Y - (owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Height / 2);


            //        //if (curTile.Pos.X < camPosX - 32 || curTile.Pos.X > (camPosX + owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Width + 32))
            //        //    continue;
            //        //if (curTile.Pos.Y < camPosY - 32 || curTile.Pos.Y > (camPosY + owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Height+32))
            //        //    continue;

            //        int id = curTile.ID;
            //        int xPos = id % tileSheet.TilesWide;
            //        int yPos = (id - xPos) / tileSheet.TilesWide;

            //        Rectangle sourceRect = new Rectangle(xPos * Tile.TILE_WIDTH, yPos * Tile.TILE_HEIGHT,
            //            Tile.TILE_WIDTH, Tile.TILE_HEIGHT);

            //        spriteBatch.Draw(tileSheet.Texture, curTile.Pos, sourceRect, Color.White);
            //    }
            //}
        }
    }
}
