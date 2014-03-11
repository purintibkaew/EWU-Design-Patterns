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
        private PlayerManager playerManager;

        private GamePlayManager()
        {
            drawManager = GamePlayDrawManager.GetInstance();
            inputManager = GamePlayInputManager.GetInstance();
            logicManager = GamePlayLogicManager.GetInstance();
            contentManager = GamePlayContentManager.GetInstance();
            playerManager = PlayerManager.GetInstance();
        }

        public void Init(ContentManager gameContentManager, GraphicsDevice gd)
        {
            contentManager.AddContent(gameContentManager);
            drawManager.ManagerGraphicsDevice = gd; //consider putting together an init for each manager before loading any content
        }

        public void HandleInput()
        {
            inputManager.HandleInput();
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
