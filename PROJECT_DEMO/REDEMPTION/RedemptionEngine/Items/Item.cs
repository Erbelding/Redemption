using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.ObjectClasses;

namespace RedemptionEngine.Items
{
    public class Item : Entity
    {
        


        protected string name;
        protected string description;
        protected Character owner;

        public enum ItemType { CONSUMABLE, WEAPON, QUEST, ARMOR }
        protected ItemType type;
        protected List<Modifier> modifiers;
        protected int price;

        public List<Modifier> Modifiers { get { return modifiers; } }

        public ItemType Type
        {
            get { return type; }
        }

        #region constructor

        
        #endregion


        public void AddModifier(string name, CharacterAttribute attribute, int value, bool modmax)
        {
            modifiers.Add(new Modifier(name, attribute, value, modmax));
        }


    }
}
