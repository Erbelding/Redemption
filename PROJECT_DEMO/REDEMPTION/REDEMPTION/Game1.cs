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
using RedemptionEngine.TileEngine;
using RedemptionEngine.Screens;
using RedemptionEngine.ObjectClasses;
using RedemptionEngine;
using RedemptionEngine.Items;
using RedemptionEngine.Items.Weapons;
using RedemptionEngine.Items.Weapons.Special;
using RedemptionEngine.Items.Armor;


namespace REDEMPTION
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
       
        ScreenManager manager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            manager = new ScreenManager(Content, this);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            GameAudio.AddSong("BloodSong", Content.Load<Song>("Audio//bloodsong"));
            GameAudio.AddSong("MenuTheme", Content.Load<Song>("Audio//MenuSounds"));
            
            //Add drop items
            DropItems.AddItem("Potion", new Potion(Content));//
            DropItems.AddItem("ManaPotion", new ManaPotion(Content));//
            DropItems.AddItem("Sword", new Sword(Content));//
            DropItems.AddItem("Rainbow", new RainBow(Content));//

            DropItems.AddItem("IceSickle", new IceSickle(Content));//
            DropItems.AddItem("Popsickle", new Popsickle(Content));//
            DropItems.AddItem("SnowShot", new SnowShot(Content));//
            DropItems.AddItem("IcicleShot", new IcicleShot(Content));//

            DropItems.AddItem("Bow", new Bow(Content));//
            DropItems.AddItem("ChainSword", new ChainSword(Content));//


            DropItems.AddItem("DecoratedSword", new DecoratedSword(Content));//
            DropItems.AddItem("FireballShot", new FireballShot(Content));//
            DropItems.AddItem("FireSword", new FireSword(Content));//

            DropItems.AddItem("Shield", new Shield(Content));//
            DropItems.AddItem("SteelSword", new SteelSword(Content));//

            DropItems.AddItem("VikingShield", new VikingShield(Content));//
            DropItems.AddItem("MarkerOfCensorship", new MarkerOfCensorship(Content));//
            DropItems.AddItem("BlackSword", new BlackSword(Content));//

            DropItems.AddItem("AutumnChest", new AutumnChest(Content));//
            DropItems.AddItem("ChainGuardChest", new ChainGuardChest(Content));//
            DropItems.AddItem("FancyBoots", new FancyBoots(Content));//
            DropItems.AddItem("FancyChest", new FancyChest(Content));//
            DropItems.AddItem("FancyLegs", new FancyLegs(Content));//
            DropItems.AddItem("FancyHelmet", new FancyHelmet(Content));//

            DropItems.AddItem("GentlemansToupe", new GentlemansToupe(Content));//
            DropItems.AddItem("SeatlessTrousers", new SeatlessTrousers(Content));//



            DropItems.AddItem("FootWarmers", new FootWarmers(Content));//
            DropItems.AddItem("WizardHat", new WizardsHat(Content));//
            DropItems.AddItem("SuperHealthPotion", new SuperHealthPotion(Content));//
            DropItems.AddItem("SuperManaPotion", new SuperManaPotion(Content));//

            DropItems.AddItem("Club", new Club(Content));//
            DropItems.AddItem("Crossbow", new Crossbow(Content));//
            DropItems.AddItem("Flail", new Flail(Content));//
            DropItems.AddItem("Geradon", new Geradon(Content));//
            DropItems.AddItem("LightBolt", new LightBolt(Content));//

            DropItems.AddItem("Nunchuck", new Nunchuck(Content));//
            DropItems.AddItem("SlowPoke", new SlowPoke(Content));//
            DropItems.AddItem("ThrowingKnife", new ThrowingKnife(Content));//


            DropItems.AddItem("IronChest", new IronChest(Content));//
            DropItems.AddItem("LeatherBoots", new LeatherBoots(Content));//
            DropItems.AddItem("LeatherHat", new LeatherHat(Content));//

            DropItems.AddItem("Blowgun", new Blowgun(Content));//
            DropItems.AddItem("RaySword", new RaySword(Content));//

            DropItems.AddItem("Hat", new Hat(Content));
            DropItems.AddItem("IronGreaves", new IronGreaves(Content));
            DropItems.AddItem("WingedBoots", new WingedBoots(Content));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        protected override void Update(GameTime gameTime)
        {

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Console.WriteLine(dt);

            manager.Update(gameTime);

            
            //    //if (pos.X > GraphicsDevice.Viewport.Width / 2)
            //    //    camera.cameraPos.X += vel.X * dt * 100.0f;
            //    //if (pos.Y > GraphicsDevice.Viewport.Height / 2)
            //    //    camera.cameraPos.Y += vel.Y * dt * 100.0f;
            //}

            // TODO: Add your update logic here

            //if (Keyboard.GetState().IsKeyDown(Keys.W))
            //    camera.cameraPos.Y--;
            //if (Keyboard.GetState().IsKeyDown(Keys.S))
            //    camera.cameraPos.Y++;
            //if (Keyboard.GetState().IsKeyDown(Keys.D))
            //    camera.cameraPos.X++;
            //if (Keyboard.GetState().IsKeyDown(Keys.A))
            //    camera.cameraPos.X--;




            //Player movement

            //Test collision




            //p.Update(gameTime);

            //if (pos.X < 0)
            //    pos.X = 0;
            //if (pos.Y < 0)
            //    pos.Y = 0;
            //if (pos.X > map.MapWidthInPixels - playerBounds.Width)
            //    pos.X = map.MapWidthInPixels - playerBounds.Width;
            //if (pos.Y > map.MapHeightInPixels - playerBounds.Height)
            //    pos.Y = map.MapHeightInPixels - playerBounds.Height;

            //if (camera.cameraPos.X < 0)
            //    camera.cameraPos.X = 0;
            //if (camera.cameraPos.Y < 0)
            //    camera.cameraPos.Y = 0;
            //if (camera.cameraPos.X > map.MapWidthInPixels - GraphicsDevice.Viewport.Width)
            //    camera.cameraPos.X = map.MapWidthInPixels - GraphicsDevice.Viewport.Width;
            //if (camera.cameraPos.Y > map.MapHeightInPixels - GraphicsDevice.Viewport.Height)
            //    camera.cameraPos.Y = map.MapHeightInPixels - GraphicsDevice.Viewport.Height;


            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here


            spriteBatch.Begin();

            manager.Draw(gameTime, spriteBatch);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
