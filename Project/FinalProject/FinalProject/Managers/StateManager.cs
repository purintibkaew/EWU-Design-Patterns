using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class StateManager
    {
        private GameState gamePlayState;
        private GameState mainMenuState;
        private GameState curState = null;
        private GameState nextState = null;

        private static StateManager instance;
        public enum States { GameState, MainMenuState };

        private StateManager()
        {
            this.gamePlayState = new GamePlayState();
            this.mainMenuState = new MainMenuState();
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
