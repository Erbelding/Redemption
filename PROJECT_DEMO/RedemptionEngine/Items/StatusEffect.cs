using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.Items
{
    public abstract class StatusEffect
    {
        protected string name;

        protected Character owner;
        protected Character attacker;

        protected int value;
        protected double coolDown;
        protected double duration;
        protected double timer;

        public StatusEffect(string name, Character owner, Character attacker, int value, int coolDown, int duration)
        {
            this.name = name;
            this.owner = owner;
            this.attacker = attacker;
            this.value = value;
            this.coolDown = coolDown;
            this.timer = coolDown;
            this.duration = duration;
        }

        public virtual void Update(GameTime gameTime)
        {
            timer -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer <= 0)
            {
                timer = coolDown;
                duration -= coolDown;
                ApplyEffect();
            }
            if(duration <= 0) Remove();
        }

        protected abstract void ApplyEffect();

        protected void Remove()
        {
            owner.RemoveEffect(this);
        }
    }
}
