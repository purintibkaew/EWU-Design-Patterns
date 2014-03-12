using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameObjects
{
    public interface IAnimationSequence : IAnimate
    {
        int CurrentAnimationIndex { get; set; }
    }
}
