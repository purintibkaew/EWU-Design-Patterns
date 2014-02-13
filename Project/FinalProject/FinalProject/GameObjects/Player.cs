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
            if (position.X - velocity.X < 0)
                position.X = 0;
            else if (position.X + velocity.X + sprite.Width >= GraphicsDeviceManager.DefaultBackBufferWidth)
                position.X = GraphicsDeviceManager.DefaultBackBufferWidth - sprite.Width;
            if (position.Y - velocity.Y < 0)
                position.Y = 0;
            else if (position.Y + velocity.Y + sprite.Height >= GraphicsDeviceManager.DefaultBackBufferHeight)
                position.Y = GraphicsDeviceManager.DefaultBackBufferHeight - sprite.Height;

            HandleCollisions();

            collisionTree.UpdatePosition(this); //We'll need to do this for every moving collidable entity, consider putting in MobileEntity abstract class or something
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
            Rectangle potentialBoundingBox = boundingBox;

            potentialBoundingBox.Inflate(Math.Abs((int)(velocity.X/2)),Math.Abs((int)(velocity.Y/2))); //Grow the bounding box by the absolute value of one half of the velocity
                                                                                               //Abs to make sure the values won't shrink the rectangle, one half because it applies to each side (doubling the input)

            Collidable[] toTest = collisionTree.GetItems(potentialBoundingBox);

            foreach (Collidable c in toTest)
            {
                //not coding this quite yet, but logic will go:

                //while a counter is greater than zero (to limit number of collision checks) and a boolean flag representing whether or not we're finished isn't true
                //if object collides in x and y vels, divide both by two
                //else if object collides in x vel, divide x by two
                //else if object collides in y vel, divide y by two
                //else the object no longer collides (or didn't in the first place), set finished flag to true
                //decrement counter by one
            }
        }
    }
}
