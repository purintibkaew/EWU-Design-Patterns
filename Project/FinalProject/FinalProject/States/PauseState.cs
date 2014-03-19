using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    class PauseState : GameState
    {
        private Game1 game;
        private GamePlayManager gameManager;
        private Menu pauseMenu;

        public PauseState()
        {
            gameManager = GamePlayManager.GetInstance();
        }

        public void Init(Game1 game, ContentManager gameContentManager, GraphicsDevice gd)
        {
            //gameManager.Init(gameContentManager, gd);
            this.game = game;
            this.pauseMenu = new PauseMenu(gameContentManager);
        }

        public void HandleInput()
        {
            if (pauseMenu.HandleInput())
            {
                this.NextState();
            }
        }

        public void Update()
        {
            pauseMenu.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            pauseMenu.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void NextState()
        {
            StateManager m = StateManager.GetInstance();
            m.NextState = m.getState(StateManager.States.GameState);
        }
    }
}
