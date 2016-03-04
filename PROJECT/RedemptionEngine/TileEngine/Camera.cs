/************************************************************************/
/* Matt Guerrette
 * Date: 3/22/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.TileEngine
{
    public class Camera
    {
        //Attributes
        private Matrix transform;  //transform matrix
        public Vector2 cameraPos;  //position

        //Properties
        public Matrix Transform
        {
            get
            {
                //returns a translation matrix from the cameras position
                //used with spriteBatch.Begin() to shift the draw position of the map
                //achieves the effect of an orthographic 2D camera
                transform = Matrix.CreateTranslation(new Vector3(-cameraPos.X, -cameraPos.Y, 0));
                return transform;
            }
        }
    }
}
