/**
 * David Erbelding
 * Matt Guerrette
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.ObjectClasses
{
    public class CharacterAttribute
    {
        private string name; //The attribute/stat name
        private int attributeValue; //the current value of the attribute/stat
        private int maxValue; //the maximum value of the attribute/stat

        public CharacterAttribute(string nm, int val, int max)
        {
            name = nm;
            attributeValue = val;
            maxValue = max;
        }

        public string Name //returns name of the attribute
        {
            get { return name; }
        }

        public int Value //get/set the value of your attribute (can be negative to infinity)
        {
            get { return attributeValue; }
            set
            {
                if (name == "Strength" || name == "Defense" ||  name == "Magic")      
                    this.attributeValue = (int)MathHelper.Clamp(value, 1, maxValue);
                else this.attributeValue = (int)MathHelper.Clamp(value, 0, maxValue);
            }
        }
        public int MaxValue //maximum value 
        {
            get { return maxValue; }
            set { this.maxValue = value; }
        }
    }
}

