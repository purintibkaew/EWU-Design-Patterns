using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class GamePlayManager
    {
        private static GamePlayManager instance;

        public static GamePlayManager GetInstance()
        {
            if (instance == null)
                instance = new GamePlayManager();
            return instance;
        }

        private GamePlayDrawManager drawManager;
        private GamePlayInputManager inputManager;
        private GamePlayLogicManager logicManager;
        private GamePlayContentManager contentManager;

        private GamePlayManager()
        {
            drawManager = GamePlayDrawManager.GetInstance();
            inputManager = GamePlayInputManager.GetInstance();
            logicManager = GamePlayLogicManager.GetInstance();
            contentManager = GamePlayContentManager.GetInstance();
        }

        public void Init(ContentManager gameContentManager)
        {
            contentManager.AddContent(gameContentManager);
        }

        public void HandleInput(KeyboardState kb, GamePadState gp)
        {
            inputManager.HandleInput(kb, gp);
        }

        public void Update()
        {
            logicManager.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            drawManager.Draw(spriteBatch);
        }
    }
}
