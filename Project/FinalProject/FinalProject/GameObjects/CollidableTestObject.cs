//This is just a test object (ostensibly a temporary one), set up, at the moment, to just sit there and do nothing but be run into by mobiles.

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
    class CollidableTestObject : Collidable, Drawable
    {
        private Texture2D sprite;
        private Vector2 position;

        public Rectangle SpriteRectangle    //May or may not remove this, need to see whether quad tree is worth it for draw manager
        {
            get
            {
                return sprite.Bounds;
            }
        }

        public CollidableTestObject(Texture2D sprite, Vector2 position)
        {
            this.sprite = sprite;
            this.position = position;
            this.boundingBox = sprite.Bounds;
            this.boundingBox.Location = new Point((int)position.X, (int)position.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }



    }
}
