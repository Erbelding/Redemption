/**
 * Matt Guerrette
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace RedemptionEngine.Items
{
    public abstract class Weapon : Item
    {
        protected enum WeaponType { MELEE, RANGED }
        protected WeaponType weapontype;
        protected bool active;
        protected int POWER;

        public bool Active { get { return active; } }
        public int GETPOWER { get { return POWER; } }
        #region constructor

        public Weapon(ContentManager content)
            : base(content)
        {

        }
        
        #endregion


        public abstract void Attack(Character owner);
        public abstract void Update(GameTime gameTime, Character owner, Keys key);

    }
}
