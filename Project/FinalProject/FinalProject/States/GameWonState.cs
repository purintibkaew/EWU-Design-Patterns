using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    class GameWonState : GameState
    {
        private SpriteFont font;

        GraphicsDevice gd;

        public GameWonState()
        {

        }

        public void Init(Game1 game, ContentManager gameContentManager, GraphicsDevice gd)
        {
            font = gameContentManager.Load<SpriteFont>("Fonts/ArialFont");
            this.gd = gd;
        }

        public void HandleInput()
        {

        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "You've escaped!", new Vector2(gd.Viewport.Width / 2, gd.Viewport.Height / 2), Color.Black);

            spriteBatch.End();
        }

        public void NextState()
        {

        }
    }
}
