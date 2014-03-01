using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    abstract class GameObject : Collidable
    {
        protected Vector2 position;
        protected Rectangle boundingBox;


        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return this.boundingBox;
            }
        }

        public GameObject(Vector2 position)
        {
            this.position = position;
        }

        public abstract void Logic();

        public abstract void Hit(int amount, int type);
    }
}
