/************************************************************************/
/* Matt Guerrette
 * Date: 3/20/13
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor.Engine
{
    //Represents an object layer that contains entity objects
    public class ObjectLayer
    {
        //Attributes
        private List<Entity> objects;

        //Constructor
        public ObjectLayer()
        {
            objects = new List<Entity>();
        }

        //Sorts objects by their Y position
        //to achieve correct occlusion when drawn
        public void Sort()
        {
            //Sort list if we have objects in it
            if (objects.Count > 1)
            {
                objects.Sort(delegate(Entity obj1, Entity obj2)
                {
                    return obj1.Pos.Y.CompareTo(obj2.Pos.Y);
                });
            }
        }
    }
}
