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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MapEditor.Engine;
using System.Windows.Forms;
using MapEditor.Editor;

namespace MapEditor.Controls
{
    //MapDisplay control which displays a map for editing
    public class MapDisplay : WinFormsGraphicsDevice.GraphicsDeviceControl
    {
        //Attributes
        private SpriteBatch spriteBatch;
        private ContentManager content;
        private EditorForm owner;
        private Map map;
        private bool mouseFocus;
        private Camera camera;

        public EditorForm Owner { get { return owner; } set { owner = value; } }
        public Camera Cam { get { return camera; } }
        public Map Map { get { return map; } set { map = value; } }
        public ContentManager Content { get { return content; } }
        public bool MouseFocus { get { return mouseFocus; } set { mouseFocus = value; } }

        //Initialize method called when control is created
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            content = new ContentManager(Services, "Content");

            camera = new Camera(this);

            owner.CamNumerator.Value = (int)camera.Speed;

            Application.Idle += delegate { Invalidate(); };
        }

        //Draws to the control
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (map != null)
            {
                if (mouseFocus)
                {
                    if (Mouse.GetState().RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        //Show right click menu
                        owner.RightClickMenu.Show(this, new System.Drawing.Point(Mouse.GetState().X, Mouse.GetState().Y));
                    }
                    camera.Update(map);
                    map.Update(camera);
                }

                map.Draw(camera, spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
