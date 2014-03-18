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

            // Call all factories here so that they can load in their sprites
            MapEntityFactory.LoadSprites(cm);


            //temporary hacky map loading, hard coded
            int mapWidth = 5000, mapHeight = 5000;
            logicManager.CreateCollisionTree(new Rectangle(0, 0, mapWidth, mapHeight));
            GameMap map = MapFactory.GetInstance().GetMap(MapFactory.MAPS.FOREST_PATH, mapWidth, mapHeight);
            GamePlayMapManager.GetInstance().Map = map;

            //playerManager.setupDefaultPlayers();
            /*
            TestMonster monster = new TestMonster(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Characters/Gunterstanding"), map.MapData.End, new BaseStats(30, 10, 5));

            drawManager.Add(monster, GamePlayDrawManager.DRAW_LIST_LEVEL.ENTITY);
            logicManager.AddCollidable(monster);
            logicManager.AddMovable(monster);
            logicManager.AddUpdatable(monster);
            */

            /*
            GameEndTrigger endTrigger = new GameEndTrigger(new Rectangle((int)map.MapData.End.X, (int)map.MapData.End.Y, 100, 100));
            logicManager.AddUpdatable(endTrigger);
            */

            List<Item> chestItems = new List<Item>();
            chestItems.Add(new Weapon(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("TempSprites/tempsword"), "Test sword", new BaseStats(0, 10000, 5)));

            MapEntityData chestData = new MapEntityData();
            chestData.Sprite = SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("TempSprites/SimpleChest");

            ChestMapEntity chest = new ChestMapEntity(chestData, map.MapData.End, chestItems);
            drawManager.Add(chest, GamePlayDrawManager.DRAW_LIST_LEVEL.ENTITY);
            logicManager.AddCollidable(chest);

            List<Item> chestItems2 = new List<Item>();
            chestItems2.Add(new Weapon(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("TempSprites/tempsword"), "Test sword 2", new BaseStats(0, -10, -4)));

            ChestMapEntity chest2 = new ChestMapEntity(chestData, map.MapData.End + new Vector2(100, 100), chestItems2);
            drawManager.Add(chest2, GamePlayDrawManager.DRAW_LIST_LEVEL.ENTITY);
            logicManager.AddCollidable(chest2);
        }
    }
}
