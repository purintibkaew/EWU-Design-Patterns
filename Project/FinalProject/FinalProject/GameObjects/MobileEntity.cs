using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    abstract class MobileEntity: Drawable, Movable, Collidable
    {
        protected Texture2D sprite;
        protected Vector2 velocity;
        protected QuadTree<Collidable> collisionTree; //reference to the collision tree for easy access

        protected bool entityIsActive;

        protected int speed; //this is temporary, remove when we actually get a stats object here
        protected Rectangle boundingBox;
        protected Vector2 position;

        public Vector2 Position { set { position = value; } get { return position; } }

        public Rectangle BoundingBox
        {
            get { return this.boundingBox; }
        }

        public bool IsActive
        {
            get
            {
                return entityIsActive;
            }
            set
            {
                entityIsActive = value;
            }
        }

        protected MobileEntity(Texture2D sprite, Vector2 position)
        {
            this.sprite = sprite;
            this.position = position;

            if (sprite != null)
            {
                this.boundingBox = sprite.Bounds;               //hacky and temporary
                this.boundingBox.Location = new Point((int)position.X, (int)position.Y);
            }
            else
            {
                this.boundingBox = new Rectangle();
            }

            this.collisionTree = GamePlayLogicManager.GetInstance().CollisionTree;

            this.entityIsActive = true;
        }

        public abstract void CheckStatus();

        public void Move()
        {
            position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.IsActive)
            {
                spriteBatch.Draw(sprite, position, Color.White);
            }
        }

        protected bool Collide(Collidable toTest, Vector2 positionOffset)
        {
            Rectangle shiftedBoundingBox = boundingBox;
            shiftedBoundingBox.Offset((int)positionOffset.X, (int)positionOffset.Y);

            if (shiftedBoundingBox.Intersects(toTest.BoundingBox))
                return true;
            return false;
        }

        public void HandleCollisions() //all of this will probably go to MobileEntity or something, every enemy/player/whatever will probably be handled the same, consider making protected or private
        {
            //NOTE: At the moment, I'm doing lots of int casting - we may want to move from vec2s for position to points, which use ints instead.

            boundingBox.Location = new Point((int)position.X, (int)position.Y); //KLUDGY - sprite bounding box is at 0, 0, ostensibly
            Rectangle potentialBoundingBox = boundingBox;

            potentialBoundingBox.Inflate(Math.Abs((int)(velocity.X)), Math.Abs((int)(velocity.Y))); //Grow the bounding box by the absolute value of the velocity
            //Abs to make sure the values won't shrink the rectangle

            Collidable[] toTest = collisionTree.GetItems(potentialBoundingBox);


            DebugText dt = DebugText.GetInstance();


            dt.WriteLine("Entity bounding box being tested at " + potentialBoundingBox.Location);
            dt.WriteLine("Potential bounding box: L: " + potentialBoundingBox.Left + ", R: " + potentialBoundingBox.Right + ", T: " + potentialBoundingBox.Top + ", B: " + potentialBoundingBox.Bottom);

            foreach (Collidable c in toTest)
            {
                if (c.Equals(this)) //SUPER KLUDGY, FIGURE OUT A FIX
                    continue;

                dt.WriteLine("Testing collision with entity at " + c.BoundingBox.Location);

                int counter = this.speed;
                bool hasCollided = true;

                while (hasCollided && counter > 0)
                {
                    hasCollided = false;

                    Vector2 vecX = new Vector2(velocity.X, 0);
                    Vector2 vecY = new Vector2(0, velocity.Y);
                    if (this.Collide(c, vecX))
                    {
                        dt.WriteLine("Colliding in X");

                        hasCollided = true;

                        velocity.X /= 2;
                        velocity.X = (int)velocity.X;
                    }
                    if (this.Collide(c, vecY))
                    {
                        dt.WriteLine("Colliding in Y");

                        hasCollided = true;

                        velocity.Y /= 2;
                        velocity.Y = (int)velocity.Y;
                    }
                    if (this.Collide(c, velocity) && !hasCollided)
                    {
                        dt.WriteLine("Colliding in X and Y");

                        hasCollided = true;

                        velocity /= 2;
                        velocity.X = (int)velocity.X;
                        velocity.Y = (int)velocity.Y;
                    }

                    counter--;
                }
                if (counter <= 0)
                {
                    velocity = new Vector2(0, 0);
                }
            }
        }

        public abstract void Logic();

        public abstract void Hit(int amount, int type);
    }
}
