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
        private PlayerManager playerManager;

        private ContentManager gameContentManager;

        public ContentManager GameContentManager { get { return gameContentManager; } }


        private GamePlayContentManager()
        {
            drawManager = GamePlayDrawManager.GetInstance();
            inputManager = GamePlayInputManager.GetInstance();
            logicManager = GamePlayLogicManager.GetInstance();
            playerManager = PlayerManager.GetInstance();
        }

        public void AddContent(ContentManager cm)
        {
            gameContentManager = cm;

            drawManager.UI.Font = gameContentManager.Load<SpriteFont>("Fonts/ArialFont");


            //temporary hacky map loading, hard coded
            int mapWidth = 5000, mapHeight = 5000;
            logicManager.CreateCollisionTree(new Rectangle(0, 0, mapWidth, mapHeight));
            GameMap map = MapFactory.GetInstance().GetMap(MapFactory.MAPS.FOREST_PATH, mapWidth, mapHeight);
            GamePlayMapManager.GetInstance().Map = map;

            
            GameEndTrigger endTrigger = new GameEndTrigger(new Rectangle((int)map.MapData.End.X, (int)map.MapData.End.Y, 100, 100));
            logicManager.AddUpdatable(endTrigger);
        }
    }
}
