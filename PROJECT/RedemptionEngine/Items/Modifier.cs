/**
 * David Erbelding
 * Matt Guerrette
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.Items
{
    public class Modifier
    {
        private string modname;
        private string attribute;
        private int value;
        private bool modmax;

        public Modifier(string name, string attribute, int value, bool modmax)
        {
            this.modname = name;
            this.attribute = attribute;
            this.value = value;
            this.modmax = modmax;
        }

        public string Name { get { return modname; } }
        public int Value
        {
            get { return value; }
            set { }
        }

        double regenCooldown = 0;
        public virtual void Modify(Character c, GameTime gameTime)
        {
            if (attribute == "Health")
            {
                if (modmax) c.Health.MaxValue += value;
                else c.Health.Value += value;
            }
            else if (attribute == "Stamina")
            {
                if (modmax) c.Stamina.MaxValue += value;
                else c.Stamina.Value += value;
            }
            else if (attribute == "Mana")
            {
                if (modmax) c.Mana.MaxValue += value;
                else c.Mana.Value += value;
            }
            else if (attribute == "Defense")
            {
                if (modmax) c.Defense.MaxValue += value;
                else c.Defense.Value += value;
            }
            else if (attribute == "Strength")
            {
                if (modmax) c.Strength.MaxValue += value;
                else c.Strength.Value += value;
            }
            else if (attribute == "Magic")
            {
                if (modmax) c.Magic.MaxValue += value;
                else c.Magic.Value += value;
                MathHelper.Clamp(c.Magic.Value, 1, c.Magic.MaxValue);
            }

            if (gameTime == null) return;

            if (attribute == "Health Regen")
            {
                regenCooldown += gameTime.ElapsedGameTime.TotalSeconds;
                if (regenCooldown >= 1)
                {
                    regenCooldown = 0;
                    c.Health.Value += value;
                }
            }

            if (attribute == "Mana Regen")
            {
                regenCooldown += gameTime.ElapsedGameTime.TotalSeconds;
                if (regenCooldown >= 1)
                {
                    regenCooldown = 0;
                    c.Mana.Value += value;
                }
            }

            if (attribute == "Move")
            {
                c.Speed = (int)(c.Speed * 1.2);
            }
        }
    }
}
