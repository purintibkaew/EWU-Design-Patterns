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

        public Texture2D Sprite { get; set; }
        public Rectangle Bounds { get; set; }
        public string Text { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.sprite, this.bounds, Color.White);
        }
        
    }
}
