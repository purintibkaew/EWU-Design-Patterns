using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class GameUIElement
    {
        private String text;
        private int duration;

        private Vector2 position;

        GameUI ui;

        public String UIText
        {
            set
            {
                text = value;
            }
        }

        public GameUIElement(GameUI ui, String text, Vector2 position)
            : this(ui, text, position, -1)
        {

        }

        public GameUIElement(GameUI ui, String text, Vector2 position, int duration)
        {
            this.ui = ui;
            this.text = text;
            this.position = position;
            this.duration = duration;
        }

        public void Update()
        {
            if (duration != -1)
            {
                duration--;

                if (duration <= 0)
                {
                    ui.RemoveElement(this);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, text, position, Color.Black);
        }
    }
}
