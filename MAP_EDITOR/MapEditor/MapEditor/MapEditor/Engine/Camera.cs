/************************************************************************/
/* Matt Guerrette
 * Date: 3/15/13
 * Description: Camera class that defines a 2D orthographic camera
 * used to navigate the map display control
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MapEditor.Controls;

namespace MapEditor.Engine
{
    public class Camera
    {
        //Attributes
        private MapDisplay owner; //map display that "owns" this camera
        private Vector2 position; //camera position
        private float speed; //camera speed

        //Gets the camera position
        public Vector2 Position { get { return position; } }
        public float Speed { get { return speed; } set { speed = value; } }

        //Constructor
        public Camera(MapDisplay owner)
        {
            this.owner = owner;
            position = Vector2.Zero;
            speed = 10.0f;
        }

        //Updates the camera
        public void Update(Map currentMap)
        {
            //Moves camera up,down,left, or right
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                position.Y -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                position.Y += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                position.X -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                position.X += speed;

            //Clamps camera on map
            if (position.X > currentMap.MapWidthInPixels - owner.ClientSize.Width)
                position.X = currentMap.MapWidthInPixels - owner.ClientSize.Width;
            if (position.Y > currentMap.MapHeightInPixels - owner.ClientSize.Height)
                position.Y = currentMap.MapHeightInPixels - owner.ClientSize.Height;
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;

            //Console.WriteLine(position);
        }
    }
}
