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
    class Player : Drawable, Movable
    {
        private Texture2D sprite;
        private Vector2 position, velocity;
        private int speed;

        private PlayerIndex playerNum;

        public PlayerIndex PlayerNum
        {
            get
            {
                return playerNum;
            }
        }

        public Rectangle SpriteRectangle
        {
            get
            {
                return sprite.Bounds;
            }
        }

        public Player(Texture2D sprite, PlayerIndex playerNum)
        {
            this.sprite = sprite;
            this.playerNum = playerNum;
            this.position = new Vector2(50, 50);
            speed = 5;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }

        public void Logic()
        {
            if (position.X - velocity.X < 0)
                position.X = 0;
            else if (position.X + velocity.X + sprite.Width >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                position.X = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - sprite.Width;
            if (position.Y - velocity.Y < 0)
                position.Y = 0;
            else if (position.Y + velocity.Y + sprite.Height >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
                position.Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - sprite.Height;
        }

        public void Move()
        {
            position += velocity;
        }

        public void HandleInput(KeyboardState kb, GamePadState gp)
        {
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

    }
}
