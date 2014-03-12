using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class Animation : IAnimate
    {
        private Texture2D[] sprites;

        private int elapsedTicks;
        private int tickInterval;
        
        private int frameTime;
        private int currentFrameIndex;


        public Texture2D CurrentFrame { get{ return sprites[currentFrameIndex]; } }

        public Boolean IsLooping { get; set; }


        public Animation(Texture2D[] sprites, int frameTime, bool isLooping)
        {
            this.sprites = sprites;
            this.frameTime = frameTime;
            IsLooping = isLooping;
            elapsedTicks = 0;
            currentFrameIndex = 0;
        }

        public void Update(int interval)
        {
            elapsedTicks += interval;

            if (elapsedTicks > frameTime)
            {
                currentFrameIndex++;

                if (currentFrameIndex > sprites.Length - 1 && !IsLooping)
                {
                    currentFrameIndex = sprites.Length - 1;
                }
                else
                {
                    currentFrameIndex = currentFrameIndex % sprites.Length;
                    elapsedTicks = 0;
                }
            }
        }
    }
}
