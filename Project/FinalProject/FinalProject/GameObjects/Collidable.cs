using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    abstract class Collidable
    {
        private Rectangle boundingBox;

        private bool collide(Collidable toTest, Vector2 positionOffset)
        {
            return false;
        }

        public void checkCollisions()
        {
        }
    }
}
