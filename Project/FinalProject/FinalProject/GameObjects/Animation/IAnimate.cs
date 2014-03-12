using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public interface IAnimate
    {
        Texture2D CurrentFrame { get; }
        void Update(int interval);
    }
}
