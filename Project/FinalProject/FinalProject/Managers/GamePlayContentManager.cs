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

            // Call all factories here so that they can load in their sprites
            MapEntityFactory.LoadSprites(cm);


            //temporary hacky map loading, hard coded
            int mapWidth = 5000, mapHeight = 5000;
            logicManager.CreateCollisionTree(new Rectangle(0, 0, mapWidth, mapHeight));
            GameMap map = MapFactory.GetInstance().GetMap(MapFactory.MAPS.FOREST_PATH, mapWidth, mapHeight);
            GamePlayMapManager.GetInstance().Map = map;

            //temporary hacky player loading, hard coded
            Player player = new Player(cm.Load<Texture2D>("Entities/Characters/BMOStanding"), map.MapData.Spawn, PlayerIndex.One, new BaseStats(10, 10, 5));

            //we're going to be doing these calls a lot - consider factory or facade or similar
            playerManager.SetPlayer(0, player);
            drawManager.Add(player, GamePlayDrawManager.DRAW_LIST_LEVEL.ENTITY);
            inputManager.Add(player);
            logicManager.AddMovable(player);
            logicManager.AddCollidable(player);
        }
    }
}
