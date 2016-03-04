/************************************************************************/
/* Matt Guerrette
 * DATE: 3/16/2013
 * Purpose: TilePalette class for selecting image to draw to map
/************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MapEditor.Engine;
using MapEditor.Editor;
using System.Windows.Forms;

namespace MapEditor.Controls
{
    public class TilePalette : WinFormsGraphicsDevice.GraphicsDeviceControl
    {
        public static int ID;

        //Attributes
        private SpriteBatch spriteBatch;        //SpriteBatch object used for drawing
        private ContentManager content;         //ContentManager used for loading objects
        private EditorForm owner;               //EditorForm owner so that tilePalette knows about mapDisplay
        private TileSheet currentTileSheet;      //The current tile sheet the user is drawing from
        private Texture2D tileSheetTex;         
        private Texture2D selectorTile;
        private bool mouseFocus;
        private Vector2 selectTilePos;

        //Properties
        public EditorForm Owner { set { owner = value; } }
        public ContentManager Content { get { return content; } }
        public bool MouseFocus { set { mouseFocus = value; } }
        public Texture2D TileSheetTex { set { tileSheetTex = value; } }
        public TileSheet TileSheet { get { return currentTileSheet; } set { currentTileSheet = value; } }

        
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            content = new ContentManager(Services, "Content");

            selectorTile = content.Load<Texture2D>("SelectorTile");

            Application.Idle += delegate { Invalidate(); };
        }

        private void UpdatePalette()
        {
            if (owner.MapPanel.Map != null)
            {

                //If tile palette actually has a loaded tilesheet
                if (currentTileSheet != null)
                {
                    int tilesWide = currentTileSheet.TilesWide;
                    int tilesHigh = currentTileSheet.TilesHigh;

                    int mX = Mouse.GetState().X;
                    int mY = Mouse.GetState().Y;

                    int tileX = mX / owner.MapPanel.Map.TileWidth;
                    selectTilePos.X = tileX * owner.MapPanel.Map.TileWidth;
                    int tileY = mY / owner.MapPanel.Map.TileHeight;
                    selectTilePos.Y = tileY * owner.MapPanel.Map.TileWidth;

                    int id = (tileY * tilesWide) + tileX;

                    id = (int)MathHelper.Clamp(id, 0, tilesWide * tilesHigh-1);

                    if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        ID = id;
                    }

                }
            }
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (owner != null)
            {
                if (owner.MapPanel.Map != null)
                {
                    if (mouseFocus)
                        UpdatePalette();

                    spriteBatch.Begin();

                    if (currentTileSheet != null)
                        spriteBatch.Draw(currentTileSheet.Texture, Vector2.Zero, Color.White);

                    if (mouseFocus)
                        spriteBatch.Draw(selectorTile, selectTilePos, Color.White);

                    spriteBatch.End();
                }
            } 
        }
    }
}
