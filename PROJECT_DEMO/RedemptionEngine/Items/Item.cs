/**
 * David Erbelding
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
using System.Reflection;

namespace RedemptionEngine.Items
{
    public class Item : Entity
    {
        protected string name;
        protected string description;
        protected Character owner;

        public enum ItemType { CONSUMABLE, WEAPON, QUEST, ARMOR_HEAD, ARMOR_BODY, ARMOR_LEGS, ARMOR_FEET }
        protected ItemType type;
        protected List<Modifier> modifiers = new List<Modifier>();
        protected int price;

        public List<Modifier> Modifiers { get { return modifiers; } }

        public string Name { get { return name; } }

        public ItemType Type
        {
            get { return type; }
        }

        #region constructor

        public Item(ContentManager content)
            : base(content)
        {
        }
        
        #endregion


        public void AddModifier(string name, string attributeName, int value, bool modmax)
        {
            modifiers.Add(new Modifier(name, attributeName, value, modmax));
        }

        

        public virtual void Use()
        {
            //Do something if consumable
        }

        public virtual void Drop()
        {
            Type t = this.GetType();
            ConstructorInfo cInfo = t.GetConstructor(new[] { typeof(ContentManager) });
            object obj = cInfo.Invoke(new object[] { GameManager.gamePlayScreen.ScreenManager.Content });
            Item item = (obj as Item);

            item.Position = GameManager.Player.Position;
            

            GameManager.AddEntity(item);
        }


        public override string ToString()
        {
            string s = "";

            s += name;

            return s;
        }

    }
}
