using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    abstract class Collidable
    {
        protected Rectangle boundingBox;

        public Rectangle BoundingBox    //the actual bounding box
        {
            get
            {
                return this.boundingBox;
            }
        }

        protected bool Collide(Collidable toTest, Vector2 positionOffset)
        {
            Rectangle shiftedBoundingBox = boundingBox;
            shiftedBoundingBox.Offset((int)positionOffset.X, (int)positionOffset.Y);

            if(shiftedBoundingBox.Intersects(toTest.BoundingBox))
                return true;
            return false;
        }

        //public abstract void HandleCollisions();  //commented out for now, this may or may not go here (do we really need all objects that can be collided with to handle their own collisions?)
    }
}
