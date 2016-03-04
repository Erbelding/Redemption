/**
 * Matt Guerrette
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RedemptionEngine.ObjectClasses
{
    //Defines a single string of animations
    //that create a single animation of a game object
    public class Animation
    {
        //Attributes
        private int curFrameIndex;  //The current index of this frame animation
        private int numFrames = 1; //The total number of frames that make up this animation
        private Point frameSize; //The size of each frame in this animation (This must be uniform)
        private int timeSinceLastFrame; //The seconds since last frame was drawn
        private int secondsPerFrame; //The seconds to wait between frame changes
        private Point curFrame; //The X,Y of current frame
        private int playCount;

        //Properties

        //Gets and Sets the SecsPerFrame
        public int SecondsPerFrame
        {
            get { return secondsPerFrame; }
            set { secondsPerFrame = value; }
        }

        //Gets and Sets the current frame index
        public int CurrentFrameIndex
        {
            get { return curFrameIndex; }
            set { curFrameIndex = (int)MathHelper.Clamp(value, 0, numFrames - 1); playCount = 0; }
        }

        public int PlayCount { get { return playCount; } }

        //Gets a source rectangle based on this animation
        public Rectangle SourceRect
        {
            get
            {
                return new Rectangle(curFrame.X + (frameSize.X * curFrameIndex), curFrame.Y,
                                     frameSize.X, frameSize.Y);
            }
        }

        //Constructor
        public Animation(Point startingFrame, Point frameSize, int numFrames, int millisecPerFrame)
        {
            //Set starting frame
            this.curFrameIndex = 0;
            this.curFrame = startingFrame;
            this.frameSize = frameSize;
            this.numFrames = numFrames;
            this.secondsPerFrame = millisecPerFrame;
        }

        //Methods

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > secondsPerFrame) //it is time to change frame
            {
                timeSinceLastFrame = 0;

                curFrameIndex = (curFrameIndex + 1) % numFrames;


                if (curFrameIndex == numFrames-1)
                {
                    playCount++;
                }

                //set upper left corner of frame
                //curFrame.X = frameSize.X * curFrameIndex;
            }
        }
    }
}
