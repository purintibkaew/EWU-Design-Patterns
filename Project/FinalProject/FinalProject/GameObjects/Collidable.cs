using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    interface Collidable
    {
        Rectangle BoundingBox  
        {
            get;
        }

        void Hit(int amount, int type); //hit the target collidable object with an attack doing an amount of damage with possible types (contained as bits in type int, use bitwise operators to work on)
    }
}
