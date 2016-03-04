/************************************************************************/
/* Matt Guerrette
 * Date: 3/15/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MapEditor.Editor;
using Microsoft.Xna.Framework.Input;
using MapEditor.Controls;

namespace MapEditor.Engine
{
    //Represents a tile object
    public class Tile
    {
        //Delegate event handler
        public delegate void OnClick(object sender, EventArgs e);

        //Attributes
        protected int tileID;
        protected int tileWidth;
        protected int tileHeight;
        protected Vector2 tilePos;
        protected Rectangle bounds;
        public event OnClick Clicked;

        //Properties
        public int ID { get { return tileID; } set { tileID = value; } }
        public Vector2 Pos { get { return tilePos; } }
        public Rectangle Bounds { get { return bounds; } }


        //Constructor
        public Tile(int id, Vector2 pos, int tileWidth, int tileHeight)
        {
            tileID = id;
            tilePos = pos;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;

            Clicked += TileClicked;
        }

        //Updates the tile
        public void Update(Camera camera)
        {
            bounds = new Rectangle((int)(tilePos.X - camera.Position.X),
                (int)(tilePos.Y - camera.Position.Y), tileWidth, tileHeight);

            //If mouse is hovering over tile
            if (Bounds.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)))
            {
                //We check to see if mouse left button is clicked
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    Clicked(this, new EventArgs());
                }
            }
        }

        //Event for when tile is clicked
        protected virtual void TileClicked(object sender, EventArgs e)
        {
            //Implement what happens when a tile is clicked here..
            //......
        }


    }
}
