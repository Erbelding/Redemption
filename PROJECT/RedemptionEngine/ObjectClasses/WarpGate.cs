using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using RedemptionEngine.TileEngine;
namespace RedemptionEngine.ObjectClasses
{
    public class WarpGate : Entity
    {
        public static Texture2D WarpGateTex;

        //Attributes
        private string nextMap;
        private Vector2 playerSpawn;
        private GameManager owner;

        //Properties
        public override Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        public GameManager Owner { get { return owner; } set { owner = value; } }

        //Constructor
        public WarpGate(ContentManager content, int x, int y, int width, int height, string nextMap, Vector2 pSpawn)
            :base(content)
        {
            position = new Vector2(x, y);
            bounds = new Rectangle(x, y, width, height);
            this.nextMap = nextMap;
            playerSpawn = pSpawn;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (CollidesWith(owner.Player))
            {
                Console.WriteLine(GameManager.GAME);
                owner.SaveGame(nextMap, playerSpawn);
                owner.gamePlayScreen.LoadGame(GameManager.GAME);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(WarpGateTex, Bounds, Color.Cyan);
        }
    }
}
