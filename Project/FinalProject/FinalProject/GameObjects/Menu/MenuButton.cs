using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    public class MenuButton : Drawable
    {
        private Texture2D sprite;
        private Rectangle bounds;
        private String text;

        public MenuButton(Rectangle bounds, Texture2D sprite, String text)
        {
            this.bounds = bounds;
            this.sprite = sprite;
            this.text = text;
        }

        public Texture2D Sprite { get { return this.sprite; } set { this.sprite = value; } }
        public Rectangle Bounds { get { return this.bounds; } set { this.bounds = value; } }
        public string Text { get { return this.text; } set { this.text = value; } }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.sprite, this.bounds, Color.White);
        }

        public Vector2 Position
        {
            get { return new Vector2(this.bounds.X, this.bounds.Y); }
            set { this.bounds.X = (int) value.X; this.bounds.Y = (int) value.Y; }
        }
    }
}
