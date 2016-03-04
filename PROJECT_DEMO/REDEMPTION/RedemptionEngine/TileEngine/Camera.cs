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
        private Matrix transform;
        public Vector2 cameraPos;

        //Properties
        public Matrix Transform
        {
            get
            {
                transform = Matrix.CreateTranslation(new Vector3(-cameraPos.X, -cameraPos.Y, 0));
                return transform;
            }
        }
    }
}
