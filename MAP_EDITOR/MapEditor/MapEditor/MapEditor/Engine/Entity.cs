/************************************************************************/
/* Matt Guerrette
 * Date: 3/20/13
/************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Engine
{
    //Represents an entity object
    public class Entity
    {
        //Entity type
        public enum Type
        {
            SCENERY,
            SPAWN_POINT,
        }

        //Attributes
        private Texture2D entityTexture; //texture
        private string entityName; //entity name
        private Vector2 position; //position
        private Type entityType; //type

        //Properties

        //Gets the position
        public Vector2 Pos { get { return position; } }
        //Gets the type
        public Type EntityType { get { return entityType; } }

        //Constructor
        public Entity(Type type, Texture2D tex, Vector2 pos)
        {
            this.entityType = type;
            this.entityTexture = tex;
            this.position = pos;
        }

        //Draws entity
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draws entity at location
            spriteBatch.Draw(entityTexture, position, Color.White);
        }
    }
}
