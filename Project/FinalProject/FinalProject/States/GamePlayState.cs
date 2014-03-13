using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace FinalProject
{
    class GamePlayState : GameState
    {
        private Game1 game;
        private GamePlayManager gameManager; 

        public GamePlayState()
        {
            gameManager = GamePlayManager.GetInstance();
        }

        public void Init(Game1 game, ContentManager gameContentManager, GraphicsDevice gd)
        {
            gameManager.Init(gameContentManager, gd);
            this.game = game;
        }

        public void HandleInput()
        {
            gameManager.HandleInput();
        }

        public void Update()
        {
            gameManager.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            gameManager.Draw(spriteBatch);
        }

        public void NextState()
        {
            StateManager m = StateManager.GetInstance();
            m.NextState = this;
        }
    }
}
