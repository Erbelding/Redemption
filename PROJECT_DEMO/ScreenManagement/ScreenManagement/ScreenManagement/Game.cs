using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ScreenManagement
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ScreenManagerTest : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;

        public ScreenManagerTest()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }



        protected override void Initialize()
        {
            screenManager = new ScreenManager(Content, this);


            base.Initialize();
        }



        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            screenManager.Update();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            spriteBatch.Begin();

            screenManager.Draw(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
