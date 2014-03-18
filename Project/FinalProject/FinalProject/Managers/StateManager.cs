using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class StateManager
    {
        private GameState gamePlayState;
        private GameState mainMenuState;
        private GameState gameWonState;
        private GameState gameOverState;
        private GameState curState = null;
        private GameState nextState = null;

        private static StateManager instance;
        public enum States { GameState, MainMenuState, GameOverState, GameWonState};

        private StateManager()
        {
            this.gamePlayState = new GamePlayState();
            this.mainMenuState = new MainMenuState();
            this.gameOverState = new GameOverState();
            this.gameWonState = new GameWonState();

            this.curState = this.mainMenuState;
            this.nextState = this.curState;
        }

        public static StateManager GetInstance()
        {
            if (instance == null)
            {
                instance = new StateManager();
            }
            return instance;
        }

        public void InitStates(Game1 game, ContentManager gameContentManager, GraphicsDevice gd)
        {
            gamePlayState.Init(game, gameContentManager, gd);
            mainMenuState.Init(game, gameContentManager, gd);
            gameOverState.Init(game, gameContentManager, gd);
            gameWonState.Init(game, gameContentManager, gd);
        }

        public GameState CurrentState
        {
            get { return this.curState; }
        }

        public GameState getState(States stateRequest)
        {
            switch (stateRequest)
            {
                case States.GameState:
                    return this.gamePlayState;
                case States.MainMenuState:
                    return this.mainMenuState;
                case States.GameOverState:
                    return this.gameOverState;
                case States.GameWonState:
                    return this.gameWonState;
                default:
                    return this.curState;
            }
        }

        public GameState NextState
        {
            set { this.nextState = value; }
        }

        public void MoveToNextState()
        {
            this.curState = this.nextState;
        }
    }
}
