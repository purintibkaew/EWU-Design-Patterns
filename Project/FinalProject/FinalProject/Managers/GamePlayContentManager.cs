using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class GamePlayContentManager
    {
        private static GamePlayContentManager instance;

        public static GamePlayContentManager GetInstance()
        {
            if (instance == null)
                instance = new GamePlayContentManager();
            return instance;
        }

        private GamePlayDrawManager drawManager;
        private GamePlayInputManager inputManager;
        private GamePlayLogicManager logicManager;

        private ContentManager gameContentManager;

        private GamePlayContentManager()
        {
            drawManager = GamePlayDrawManager.GetInstance();
            inputManager = GamePlayInputManager.GetInstance();
            logicManager = GamePlayLogicManager.GetInstance();
        }

        public void AddContent(ContentManager cm)
        {
            gameContentManager = cm;

            //temporary hacky player loading, hard coded
            Player player = new Player(cm.Load<Texture2D>("ship"), PlayerIndex.One);
            //we're going to be doing these calls a lot - consider factory or facade or similar
            drawManager.Add(player);
            inputManager.Add(player);
            logicManager.AddMovable(player);
            logicManager.AddCollidable(player);
        }
    }
}
