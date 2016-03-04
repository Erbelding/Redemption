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
using Microsoft.Xna.Framework.Content;
using MapEditor.Editor;

namespace MapEditor.Engine
{
    //Repesents the grid layer that is displayed in map display
    public class GridLayer
    {
        //attributes
        private Map owner;          //map that owns this grid layer
        private Texture2D image;    //texture
       

        //Constructor
        public GridLayer(Map owner, ContentManager content)
        {
            this.owner = owner;
            this.image = content.Load<Texture2D>("GridTile");
        }

     
        //Draws the grid layer
        public void Draw(Camera camera, SpriteBatch spritebatch)
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
                    if (x < 0 || y < 0 || x > owner.MapWidth - 1 || y > owner.MapHeight - 1)
                        continue;

                    spritebatch.Draw(image, new Vector2(x * owner.TileWidth - camera.Position.X,
                        y * owner.TileHeight - camera.Position.Y), Color.White);
                }
            }
        }
        
    }
}
