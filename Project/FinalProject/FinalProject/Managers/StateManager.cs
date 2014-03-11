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

        private static StateManager instance;

        private StateManager()
        {
            this.gamePlayState = new GamePlayState();
            this.mainMenuState = new MainMenuState();
            this.curState = this.gamePlayState;
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
    }
}
