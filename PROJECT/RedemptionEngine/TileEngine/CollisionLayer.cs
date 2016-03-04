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
    //Represents a collision layer
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

        //Draws the layer
        public void Draw(GameManager owner, SpriteBatch spriteBatch)
        {
            float camPosX = owner.camera.cameraPos.X; //- (owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Width/2);
            float camPosY = owner.camera.cameraPos.Y;//- (owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Height/2);

            int leftTile = (int)Math.Floor((float)camPosX / Tile.TILE_WIDTH);
            int topTile = (int)Math.Floor((float)camPosY / Tile.TILE_HEIGHT);
            int bottomTile = (int)Math.Ceiling((float)(camPosY + owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Height) / Tile.TILE_HEIGHT) - 1;
            int rightTile = (int)Math.Ceiling((float)(camPosX + owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Width) / Tile.TILE_WIDTH) - 1;

            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x <= rightTile; ++x)
                {
                    if (owner.CurrentMap != null)
                    {
                        if (x < 0 || y < 0 || x > owner.CurrentMap.MapWidth - 1 || y > owner.CurrentMap.MapHeight - 1)
                            continue;
                    }

                    CollisionTile tile = tiles[y, x];

                    spriteBatch.Draw(Tex, tile.Bounds[0], Color.Red);
                    spriteBatch.Draw(Tex, tile.Bounds[1], Color.Red);
                }
            }
        }
    }
}
