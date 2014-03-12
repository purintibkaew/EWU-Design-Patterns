using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinalProject.GameObjects;

namespace FinalProject
{
    public class AnimationSequence : IAnimationSequence
    {
        private IAnimate[] animations;
        
        
        public int CurrentAnimationIndex { get; set; }


        public Texture2D CurrentFrame
        {
            get { return animations[CurrentAnimationIndex].CurrentFrame; }
        }


        public AnimationSequence(Animation[] animations)
        {
            this.animations = animations;
        }

        public void Update(int interval)
        {
            animations[CurrentAnimationIndex].Update(interval);
        }
    }
}