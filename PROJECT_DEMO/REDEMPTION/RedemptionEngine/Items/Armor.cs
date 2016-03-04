using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedemptionEngine.ObjectClasses;

namespace RedemptionEngine.Items
{
    public class Armor : Item
    {
        #region constructor

        
        #endregion

        #region Attributes
        protected enum ArmorSlot { HEAD, TORSO, LEGS, FEET }
        protected ArmorSlot slot;

        #endregion
    }
}
