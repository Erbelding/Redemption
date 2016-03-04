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
    public class HealthBar : GUIBar
    {
        public HealthBar(GamePlayScreen screen, Player p, int x, int y)
            : base(screen, p, x, y, Color.Red)
        {
            texture = screen.ScreenManager.Content.Load<Texture2D>("GUI//HealthBar");
            foreground = screen.ScreenManager.Content.Load<Texture2D>("GUI//HealthBarForeground");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(x, y, (int)(foreground.Width/2 * ((float)p.Health.Value / p.Health.MaxValue)), foreground.Height/2), Color.Red);
            spriteBatch.Draw(foreground, new Rectangle(x, y, foreground.Width/2, foreground.Height/2), Color.White);
        }
    }
}
