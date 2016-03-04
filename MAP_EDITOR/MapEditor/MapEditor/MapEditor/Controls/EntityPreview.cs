using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MapEditor.Controls
{
    public class EntityPreview : WinFormsGraphicsDevice.GraphicsDeviceControl
    {
        //Attributes
        private SpriteBatch spriteBatch;
        private ContentManager content;


        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            content = new ContentManager(Services, "Content");
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
        }
    }
}
