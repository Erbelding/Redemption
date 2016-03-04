using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RedemptionEngine.ObjectClasses;

namespace RedemptionEngine.Items.StatusEffects
{
    public class Burn : StatusEffect
    {
        protected override void ApplyEffect()
        {
            this.owner.TakeDamage(attacker, value, true, Color.OrangeRed);
        }

        public Burn(string name, Character owner, Character attacker, int value, int coolDown, int duration)
            : base(name, owner, attacker, value, coolDown, duration)
        {
        }
    }
}
