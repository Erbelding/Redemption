using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using RedemptionEngine.ObjectClasses;
using RedemptionEngine.Screens;

namespace RedemptionEngine
{
    public class GUIBar
    {
        //Attributes
        protected Texture2D foreground;
        protected Texture2D texture;
        protected int x;
        protected int y;
        protected Player p;


        public GUIBar(GamePlayScreen screen, Player p, int x, int y, Color c)
        {
            this.p = p;
            this.x = x;
            this.y = y;

            foreground = screen.ScreenManager.Content.Load<Texture2D>("GUI//barForeground");
            
        }

        

    }
}
