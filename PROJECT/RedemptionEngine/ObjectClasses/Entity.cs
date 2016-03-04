/**
 * Matt Guerrette
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using RedemptionEngine.TileEngine;

namespace RedemptionEngine.ObjectClasses
{
    public class Entity : GameObject
    {
        public enum Direction { UP, DOWN, LEFT, RIGHT };
        protected Direction dir;

        protected Texture2D colTex;
        protected Texture2D overlay;

        //Attributes
        protected GameManager gameManager;
        
        protected Dictionary<string, Animation> animations; //Collection of animations
        private string currentAnimation; //String containing the key of the current animation

        //Offsets for collision bounds
        protected int boundsXOffset;
        protected int boundsYOffset;
        protected int boundsWidthOffset;
        protected int boundsHeightOffset;


        //Properties

        public GameManager GameManager
        {
            get { return gameManager; }
            set { gameManager = value; }
        }
        public Direction Dir
        {
            get { return dir; }
        }

        public Animation GetAnimation(string name)
        {
            if(animations.ContainsKey(name))
            {
                return animations[name];
            }
            else return null;
        }

        public Texture2D Overlay { set { overlay = value; } }

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

        public override Rectangle Bounds
        {
            get
            {
                Rectangle r = new Rectangle();
                //If the entity has an animation
                if (animations.Count > 0)
                {
                    //We set the bounds of the object based on the pos and frame size
                    bounds = new Rectangle((int)Position.X, (int)Position.Y, CurrentAnimation.SourceRect.Width, CurrentAnimation.SourceRect.Height);

                    
                    
                }
                //Otherwise if the entity doesn't have animations
                else
                {
                    //We set the bounds based on pos and texture dimensions
                    bounds = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
                }

                r = new Rectangle(bounds.X + boundsXOffset,
                                     bounds.Y + boundsYOffset,
                                     bounds.Width + boundsWidthOffset,
                                     bounds.Height + boundsHeightOffset);

                //Return the bounds
                return r;
            }
        }

        public override Vector2 Origin
        {
            get
            {
                //If we have an animated object
                if (animations.Count > 0)
                {
                    //Set the origin based on frame size
                    origin = new Vector2(CurrentAnimation.SourceRect.Width / 2, CurrentAnimation.SourceRect.Height / 2);
                }
                //Otherwise, we just set it based on texture dimensions
                else
                {
                    origin = new Vector2(texture.Width / 2, texture.Height / 2);
                }

                //Return origin
                return base.Origin;
            }
        }

        public override Vector2 Center
        {
            get
            {
                //Set the origin based on frame size
                center = new Vector2(Position.X + Origin.X, Position.Y + Origin.Y);
                
                return base.Center;
            }
        }
        
        //Constructor
        public Entity(ContentManager content)
        {
            tint = Color.White;
            scale = 1.0f;
            visible = true;
            animations = new Dictionary<string, Animation>();
            alpha = 1.0f;
            colTex = content.Load<Texture2D>("Debug//collisionSquare");
            boundsXOffset = 0;
            boundsYOffset = 0;
            boundsWidthOffset = 0;
            boundsHeightOffset = 0;
            
            
        }

        public Entity()
        {

        }

        #region Methods

        public virtual void HandleCollision(Map map, Rectangle oldRect)
        {
            if(IsCollidingWithMap(map, Bounds))
            {
                position.X = oldRect.X;
                position.Y = oldRect.Y;
                bounds = oldRect;
            }
        }

        public bool IsCollidingWithMap(Map map, Rectangle rect)
        {
            int leftTile = (int)Math.Floor((float)rect.Left / Tile.TILE_WIDTH);
            int topTile = (int)Math.Floor((float)rect.Top / Tile.TILE_HEIGHT);
            int bottomTile = (int)Math.Ceiling((float)rect.Bottom / Tile.TILE_HEIGHT) - 1;
            int rightTile = (int)Math.Ceiling((float)rect.Right / Tile.TILE_WIDTH) - 1;

            for (int y = topTile; y <= bottomTile; ++y)
            {
                for (int x = leftTile; x <= rightTile; ++x)
                {
                    if (map != null)
                    {
                        if (map.IsCollideableTile(x, y))
                        {
                            Rectangle r1 = map.Collision.Tiles[y, x].Bounds[0];
                            Rectangle r2 = map.Collision.Tiles[y, x].Bounds[1];

                            Rectangle d1 = Rectangle.Intersect(rect, r1);
                            Rectangle d2 = Rectangle.Intersect(rect, r2);

                            if (d1 != Rectangle.Empty || d2 != Rectangle.Empty)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void AddAnimation(string name, Animation animation)
        {
            //Check to see if the animation isn't already in our collection
            if (!animations.ContainsKey(name))
            {
                //Add that new animation
                animations.Add(name, animation);
            }
        }
        
        public override bool CollidesWith(GameObject obj)
        {
            //Collision Code here
            if (this.Bounds.Intersects(obj.Bounds))
                return true;

            //empty return
            return false;
        }

        public bool IsOnScreen()
        {
            if (gameManager != null)
            {
                float camPosX = gameManager.camera.cameraPos.X;
                float camPosY = gameManager.camera.cameraPos.Y;

                int leftTile = (int)camPosX - 50;
                int topTile = (int)camPosY - 50;
                int bottomTile = (int)camPosY + gameManager.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Height + 50;
                int rightTile = (int)camPosX + gameManager.gamePlayScreen.ScreenManager.Game.GraphicsDevice.Viewport.Width + 50;

                if (Center.X > leftTile && Center.X < rightTile && Center.Y < bottomTile && Center.Y > topTile) return true;
                else return false;
            }
            else return true;
        }

        public override void Move(float dt)
        {
            //Move code here

        }

        public override void Update(GameTime gameTime)
        {
            if (velocity == new Vector2(0, 1))
                dir = Direction.DOWN;
            else if (velocity == new Vector2(0, -1))
                dir = Direction.UP;
            else if (velocity == new Vector2(1, 0))
                dir = Direction.RIGHT;
            else if (velocity == new Vector2(-1, 0))
                dir = Direction.LEFT;

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

            //Update the current animation if not null
            if(CurrentAnimation != null)
                CurrentAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //If the entity is visible we draw it
            if (visible)
            {
                //If it is animated we draw using the current animation source rectangle
                if(animations.Count > 0)
                    spriteBatch.Draw(Texture, Center, CurrentAnimation.SourceRect,
                        Tint * Alpha, Rotation, Origin, Scale, SpriteEffects.None, 0.0f);
                //Otherwise, it is a static object so source rect will be null
                else
                    spriteBatch.Draw(Texture, Center, null,
                        Tint * Alpha, Rotation, Origin, Scale, SpriteEffects.None, 0.0f);
            }

            if(overlay != null) DrawOverlay(spriteBatch);
            overlay = null;
        }

        public virtual void DrawOverlay(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(overlay, Center, null, Tint * Alpha, -Rotation, Origin, Scale, SpriteEffects.None, 0.0f);
        }

        #endregion
    }
}
