using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class CollidableNullBox : Collidable
    {
        private Rectangle boundingBox;

        public Rectangle BoundingBox
        {
            get { return boundingBox; }
        }

        public CollidableNullBox(Rectangle boundingBox)
        {
            this.boundingBox = boundingBox;
        }

        public void Hit(int amount, int type)
        {
            
        }
    }
}
