using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class Sticker : Drawable
    {
        private Texture2D sprite;

        public Vector2 Position { get; set; }

        public Sticker(Texture2D sprite, Vector2 position)
        {
            this.sprite = sprite;
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position, Color.White);
        }
    }
}
