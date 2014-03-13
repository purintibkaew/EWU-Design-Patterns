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
    class MainMenuState : GameState
    {
        private Game1 game;
        private GamePlayManager gameManager;
        private Menu mainMenu;

        public MainMenuState()
        {
            gameManager = GamePlayManager.GetInstance();

        }

        public void Init(Game1 game, ContentManager gameContentManager, GraphicsDevice gd)
        {
            gameManager.Init(gameContentManager, gd);
            this.game = game;
            this.mainMenu = new MainMenu(gameContentManager);
        }

        public void HandleInput()
        {
            if (mainMenu.HandleInput())
            {
                this.NextState();
            }
        }

        public void Update()
        {
            mainMenu.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            mainMenu.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void NextState()
        {
            StateManager m = StateManager.GetInstance();
            m.NextState = m.getState(StateManager.States.GameState);
        }
    }
}
