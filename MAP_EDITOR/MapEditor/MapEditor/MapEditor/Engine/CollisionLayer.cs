/************************************************************************/
/* Matt Guerrette
 * Date: 3/15/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MapEditor.Engine
{
    //Represents a layer of collision tiles
    public class CollisionLayer
    {
        //Attributes
        private CollisionTile[,] tiles; //layer array of collision tiles
        private Map owner; //map that owns this layer

        //Gets the layer data
        public CollisionTile[,] Tiles { get { return tiles; } }

        //Constructor
        public CollisionLayer(Map owner)
        {
            this.owner = owner;

            //Initialize tiles array
            tiles = new CollisionTile[owner.MapHeight, owner.MapWidth];

            for (int y = 0; y < owner.MapHeight; y++)
            {
                for (int x = 0; x < owner.MapWidth; x++)
                {
                    tiles[y, x] = new CollisionTile(0, new Vector2(x * owner.TileWidth, y * owner.TileHeight), 0,
                        owner.TileWidth, owner.TileHeight);
                }
            }
            
        }

        //Update each collision tile
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

                    CollisionTile curTile = tiles[y, x];

                    curTile.Update(camera);
                }
            }
        }

        //Draw each collision tile
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

                    CollisionTile curTile = tiles[y, x];
                    TileSheet tileSheet = owner.Owner.Owner.CollisionPalette.TileSheet;

                    int id = curTile.ID;

                    //converts the tile id
                    //to use the correct source image
                    switch (id)
                    {
                        case 0:
                            id = 7;
                            break;
                        case 1:
                            id = 10;
                            break;
                        case 2:
                            id = 1;
                            break;
                        case 3:
                            id = 9;
                            break;
                        case 4:
                            id = 4;
                            break;
                        case 5:
                            id = 6;
                            break;
                        case 6:
                            id = 0;
                            break;
                        case 7:
                            id = 2;
                            break;
                        case 8:
                            id = 12;
                            break;
                        case 9:
                            id = 14;
                            break;
                        case 10:
                            id = 3;
                            break;
                        case 11:
                            id = 5;
                            break;
                        case 12:
                            id = 15;
                            break;
                        case 13:
                            id = 17;
                            break;
                    }

                    int xPos = id % tileSheet.TilesWide;
                    int yPos = (id - xPos) / tileSheet.TilesWide;

                    Rectangle sourceRect = new Rectangle(xPos * owner.TileWidth, yPos * owner.TileHeight,
                        owner.TileWidth, owner.TileHeight);

                    //spriteBatch.Draw(tileSheet.Texture, curTile.Pos - new Vector2(EditorForm.HScrollValue,
                    //    EditorForm.VScrollValue), sourceRect, Color.White);

                    spriteBatch.Draw(tileSheet.Texture, curTile.Pos - new Vector2(camera.Position.X,
                        camera.Position.Y), sourceRect, Color.White * 0.50f);
                }
            }
        }
    }
}
