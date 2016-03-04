using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedemptionEngine.ObjectClasses;

namespace RedemptionEngine.Items
{
    public class Modifier
    {
        private string modname;
        private CharacterAttribute attribute;
        private int value;
        private bool modmax;

        public Modifier(string name, CharacterAttribute attribute, int value, bool modmax)
        {
            this.modname = name;
            this.attribute = attribute;
            this.value = value;
            this.modmax = modmax;
        }

        public string Name { get { return modname; } }
        public int Value { get { return value; } }

        public void Modify()
        {
            if (modmax) attribute.MaxValue += value;
            else attribute.Value += value;
        }
        public void UnModify()
        {
            if (modmax) attribute.MaxValue -= value;
            else attribute.Value -= value;
        }
    }
}
