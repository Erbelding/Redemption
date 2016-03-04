/************************************************************************/
/* Matt Guerrette
 * Date: 3/15/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Editor;

namespace MapEditor.Engine
{
    //Represents a terrain layer of terrain tiles
    public class TerrainLayer
    {
        //Attributes
        private TerrainTile[,] tiles;
        private Map owner;

        //Gets the layer data
        public TerrainTile[,] Tiles { get { return tiles; } }

        //Constructor
        public TerrainLayer(Map owner)
        {
            this.owner = owner;
            tiles = new TerrainTile[owner.MapHeight, owner.MapWidth];

            for (int y = 0; y < owner.MapHeight; y++)
            {
                for (int x = 0; x < owner.MapWidth; x++)
                {
                    tiles[y, x] = new TerrainTile(0, new Vector2(x * owner.TileWidth, y * owner.TileHeight), 
                        owner.TileWidth, owner.TileHeight);
                }
            }
        }

        //Updates the layer
        public void Update(Camera camera)
        {
            float camPosX = camera.Position.X; //- (owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Width/2);
            float camPosY = camera.Position.Y;//- (owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Height/2);

            int leftTile = (int)Math.Floor((float)camPosX / 32);
            int topTile = (int)Math.Floor((float)camPosY / 32);
            int bottomTile = (int)Math.Ceiling((float)(camPosY + 600) / 32) - 1;
            int rightTile = (int)Math.Ceiling((float)(camPosX + 750) / 32) - 1;

            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x <= rightTile; ++x)
                {
                    if (x < 0 || y < 0 || x > owner.MapWidth-1 || y > owner.MapHeight-1)
                        continue;

                    TerrainTile curTile = tiles[y, x];

                    curTile.Update(camera);
                }
            }
        }

        //Draws the layer
        public void Draw(Camera camera, SpriteBatch spriteBatch)
        {

            float camPosX = camera.Position.X; //- (owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Width/2);
            float camPosY = camera.Position.Y;//- (owner.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Height/2);

            int leftTile = (int)Math.Floor((float)camPosX / 32);
            int topTile = (int)Math.Floor((float)camPosY / 32);
            int bottomTile = (int)Math.Ceiling((float)(camPosY + 600) / 32) - 1;
            int rightTile = (int)Math.Ceiling((float)(camPosX + 750) / 32) - 1;

            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x <= rightTile; ++x)
                {
                    if (x < 0 || y < 0 || x > owner.MapWidth-1 || y > owner.MapHeight-1)
                        continue;

                    TerrainTile curTile = tiles[y, x];

                    //if (curTile.Pos.X < camera.Position.X - 32 || curTile.Pos.X > (camera.Position.X + 782))
                    //    continue;
                    //if (curTile.Pos.Y < camera.Position.Y - 32 || curTile.Pos.Y > (camera.Position.Y + 632))
                    //    continue;

                    TileSheet tileSheet = owner.Owner.Owner.TilePalette.TileSheet;

                    int id = curTile.ID;
                    int xPos = id % tileSheet.TilesWide;
                    int yPos = (id - xPos) / tileSheet.TilesWide;

                    Rectangle sourceRect = new Rectangle(xPos * owner.TileWidth, yPos * owner.TileHeight,
                        owner.TileWidth, owner.TileHeight);

                    spriteBatch.Draw(tileSheet.Texture, curTile.Pos - new Vector2(camera.Position.X,
                        camera.Position.Y), sourceRect, Color.White);
                }
            }

            //for (int y = 0; y < owner.MapHeight; y++)
            //{
            //    for (int x = 0; x < owner.MapWidth; x++)
            //    {
            //        TerrainTile curTile = tiles[y, x];

            //        if (curTile.Pos.X < camera.Position.X - 32 || curTile.Pos.X > (camera.Position.X + 782))
            //            continue;
            //        if (curTile.Pos.Y < camera.Position.Y - 32 || curTile.Pos.Y > (camera.Position.Y + 632))
            //            continue;

            //        TileSheet tileSheet = owner.Owner.Owner.TilePalette.TileSheet;

            //        int id = curTile.ID;
            //        int xPos = id % tileSheet.TilesWide;
            //        int yPos = (id - xPos) / tileSheet.TilesWide;

            //        Rectangle sourceRect = new Rectangle(xPos * owner.TileWidth, yPos * owner.TileHeight,
            //            owner.TileWidth, owner.TileHeight);

            //        spriteBatch.Draw(tileSheet.Texture, curTile.Pos - new Vector2(camera.Position.X,
            //            camera.Position.Y), sourceRect, Color.White);
            //    }
            //}
        }

        //writes a new tile over the specified position
        public void WriteTile(int index, int x, int y)
        {
            tiles[y, x] = new TerrainTile(index, new Vector2(x*owner.TileWidth, y*owner.TileHeight), 
                owner.TileWidth, owner.TileHeight);
        }
    }
}
