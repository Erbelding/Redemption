using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.ObjectClasses
{
    public class Entity : GameObject
    {
        //Attributes

        private Dictionary<string, Animation> animations; //Collection of animations
        private string currentAnimation; //String containing the key of the current animation

        //Properties

        //Gets the current animation
        public Animation CurrentAnimation
        {
            get
            {
                //If we have a current animation
                if (!string.IsNullOrEmpty(currentAnimation))
                {
                    //return that animation
                    return animations[currentAnimation];
                }
                //Otherwise we just return null
                else
                    return null;
            }
        }

        //Gets and Sets the current animation string key
        public string CurrentAnimationKey
        {
            get { return currentAnimation; }
            set
            {
                //If our animations collection contains the key
                if (animations.ContainsKey(value))
                {
                    //Set the current animation key to that value
                    currentAnimation = value;
                    animations[currentAnimation].CurrentFrameIndex = 0;
                }
            }
        }
        
        //Constructor
        public Entity(Texture2D tex)
        {
            texture = tex;
            position = Vector2.Zero;
            animations = new Dictionary<string, Animation>();
        }

        public Entity()
        { }

        public void AddAnimation(string name, Animation animation)
        {
            //Check to see if the animation isn't already in our collection
            if (!animations.ContainsKey(name))
            {
                //Add that new animation
                animations.Add(name, animation);
            }
        }


        #region Methods

        public override bool CollidesWith(GameObject obj)
        {
            //Collision Code here

            //empty return
            return false;
        }

        public override void Move(float dt)
        {
            //Move code here

        }

        public override void Update(GameTime gameTime)
        {
            //If our current animation key is null
            if (CurrentAnimationKey == null)
            {
                //Check to see if we have any animations
                if (animations.Count > 0)
                {
                    //Copy keys to an array and set our current animation key
                    //to the first animation
                    string[] keys = new string[animations.Count];
                    animations.Keys.CopyTo(keys, 0);
                    CurrentAnimationKey = keys[0];
                }
                else
                    return; //if there is no animation to play
            }

            //Update the current animation
            CurrentAnimation.Update(gameTime);


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, CurrentAnimation.SourceRect,
                Color.White, 0.0f, Vector2.Zero,
                1.5f, SpriteEffects.None, 0.0f);
        }

        #endregion
    }
}
