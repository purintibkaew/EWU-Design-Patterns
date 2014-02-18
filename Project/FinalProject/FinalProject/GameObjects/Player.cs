using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace FinalProject
{
    class Player : Collidable, Drawable, Movable
    {
        private Texture2D sprite;
        private Vector2 position, velocity;
        private int speed;

        private PlayerIndex playerNum;

        private QuadTree<Collidable> collisionTree; //reference to the collision tree for easy access

        public PlayerIndex PlayerNum        //return player number for gamepad state checks, look into whether this is necessary if we're getting gamepad state in this object
        {
            get
            {
                return playerNum;
            }
        }

        public Rectangle SpriteRectangle    //May or may not remove this, need to see whether quad tree is worth it for draw manager
        {
            get
            {
                return sprite.Bounds;
            }
        }

        public Player(Texture2D sprite, PlayerIndex playerNum)
        {
            this.sprite = sprite;
            this.boundingBox = sprite.Bounds; //bounding box simply defined by sprite rectangle for now, might want to change in the future
            this.playerNum = playerNum;
            this.position = new Vector2(50, 50);
            speed = 5;

            this.collisionTree = GamePlayLogicManager.GetInstance().CollisionTree;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }

        public void Logic()
        {
            DebugText dt = DebugText.GetInstance();
            dt.WriteLine("Initial Velocity: " + velocity);
            dt.WriteLine("Initial Position: " + position);
            
            if (position.X - velocity.X < 0)
                position.X = 0;
            else if (position.X + velocity.X + sprite.Width >= GraphicsDeviceManager.DefaultBackBufferWidth)
                position.X = GraphicsDeviceManager.DefaultBackBufferWidth - sprite.Width;
            if (position.Y - velocity.Y < 0)
                position.Y = 0;
            else if (position.Y + velocity.Y + sprite.Height >= GraphicsDeviceManager.DefaultBackBufferHeight)
                position.Y = GraphicsDeviceManager.DefaultBackBufferHeight - sprite.Height;

            if(velocity != new Vector2(0, 0))   //small optimization - don't bother checking for collisions if the entity isn't moving
                HandleCollisions();

            collisionTree.UpdatePosition(this); //We'll need to do this for every moving collidable entity, consider putting in MobileEntity abstract class or something

            dt.WriteLine("Adjusted Velocity: " + velocity);
        }

        public void Move()
        {
            position += velocity;
        }

        public void HandleInput()
        {
            KeyboardState kb = Keyboard.GetState();         //these state checks might be moved, but they should work just fine here
            GamePadState gp = GamePad.GetState(playerNum);

            velocity.X = velocity.Y = 0;

            if (kb.IsKeyDown(Keys.W))
                velocity.Y -= speed;
            if (kb.IsKeyDown(Keys.S))
                velocity.Y += speed;
            if (kb.IsKeyDown(Keys.D))
                velocity.X += speed;
            if (kb.IsKeyDown(Keys.A))
                velocity.X -= speed;
        }

        public void HandleCollisions() //all of this will probably go to MobileEntity or something, every enemy/player/whatever will probably be handled the same, consider making protected or private
        {
            //NOTE: At the moment, I'm doing lots of int casting - we may want to move from vec2s for position to points, which use ints instead.

            boundingBox.Location = new Point((int)position.X, (int)position.Y); //KLUDGY - sprite bounding box is at 0, 0, ostensibly
            Rectangle potentialBoundingBox = boundingBox;

            potentialBoundingBox.Inflate(Math.Abs((int)(velocity.X)),Math.Abs((int)(velocity.Y))); //Grow the bounding box by the absolute value of the velocity
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

                while(hasCollided && counter > 0)
                {
                    hasCollided = false;

                    Vector2 vecX = new Vector2(velocity.X, 0);
                    Vector2 vecY = new Vector2(0, velocity.Y);
                    if(this.Collide(c, vecX))
                    {
                        dt.WriteLine("Colliding in X");

                        hasCollided = true;
                        
                        velocity.X /= 2;
                        velocity.X = (int)velocity.X;
                    }
                    if(this.Collide(c, vecY))
                    {
                        dt.WriteLine("Colliding in Y");

                        hasCollided = true;
                        
                        velocity.Y /= 2;
                        velocity.Y = (int)velocity.Y;
                    }
                    if(this.Collide(c, velocity) && !hasCollided)
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
    }
}
