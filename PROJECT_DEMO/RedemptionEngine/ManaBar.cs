using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.Screens;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine
{
    public class ManaBar : GUIBar
    {
        public ManaBar(GamePlayScreen screen, Player p, int x, int y)
            : base(screen, p, x, y, Color.Red)
        {
            texture = screen.ScreenManager.Content.Load<Texture2D>("GUI//ManaBar");
            foreground = screen.ScreenManager.Content.Load<Texture2D>("GUI//ManaBarForeground");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(x, y, (int)(foreground.Width/2 * ((float)p.Mana.Value / p.Mana.MaxValue)), foreground.Height/2), Color.Blue);
            spriteBatch.Draw(foreground, new Rectangle(x, y, foreground.Width/2, foreground.Height/2), Color.White);
        }
    }
}
