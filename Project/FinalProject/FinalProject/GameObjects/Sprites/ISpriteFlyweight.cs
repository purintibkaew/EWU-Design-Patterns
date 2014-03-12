using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public interface ISpriteFlyweight
    {
        Texture2D GetSprite(string spriteFileName);
    }
}
