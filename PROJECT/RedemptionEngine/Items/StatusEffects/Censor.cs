using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RedemptionEngine.ObjectClasses;
using Microsoft.Xna.Framework.Graphics;

namespace RedemptionEngine.Items.StatusEffects
{
    public class Censor: StatusEffect
    {
        private static Texture2D texture;
        private Dictionary<string, Animation> animations; //Collection of animations
        private string currentAnimation; //String containing the key of the current animation


        public Animation GetAnimation(string name)
        {
            if (animations.ContainsKey(name))
            {
                return animations[name];
            }
            else return null;
        }
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
                else return null;
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            CurrentAnimation.Update(gameTime);

            ApplyEffect();

        }

        protected override void ApplyEffect()
        {

            Texture2D cropTexture = new Texture2D(owner.GameManager.gamePlayScreen.ScreenManager.Game.GraphicsDevice, 32, 32);
            Color[] data = new Color[32 * 32];
            texture.GetData(0, CurrentAnimation.SourceRect, data, 0, data.Length);
            cropTexture.SetData(data);

            owner.Overlay = cropTexture;
            if (duration <= 0) owner.Overlay = null;
        }

        public Censor(string name, Character owner, Character attacker, int duration)
            :base(name, owner, attacker, 0, 100, duration)
        {
            if (texture == null)
            {
                texture = owner.Content.Load<Texture2D>("GUI//Blur");
            }
            animations = new Dictionary<string, Animation>();
            animations.Add("Blur", new Animation(new Point(0,0),new Point(32, 32),8, 6000));
            currentAnimation = "Blur";
        }
    }
}
