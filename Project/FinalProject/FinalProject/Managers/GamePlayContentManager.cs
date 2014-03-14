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

            // Call all factories here so that they can load in their sprites
            MapEntityFactory.LoadSprites(cm);


            //temporary hacky map loading, hard coded
            int mapWidth = 5000, mapHeight = 5000;
            logicManager.CreateCollisionTree(new Rectangle(0, 0, mapWidth, mapHeight));
            GameMap map = MapFactory.GetInstance().GetMap(MapFactory.MAPS.FOREST_PATH, mapWidth, mapHeight);
            GamePlayMapManager.GetInstance().Map = map;

            //temporary hacky player loading, hard coded
            Player player = new Player(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Characters/BMOStanding"), map.MapData.Spawn, PlayerIndex.One, new BaseStats(10, 10, 5));

            //we're going to be doing these calls a lot - consider factory or facade or similar
            playerManager.SetPlayer(0, player);
            drawManager.Add(player, GamePlayDrawManager.DRAW_LIST_LEVEL.ENTITY);
            inputManager.Add(player);
            logicManager.AddMovable(player);
            logicManager.AddCollidable(player);

            //really hacky temporary way of setting player bindings
            player.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_ATTACK] = new PlayerInputBinding(Keys.Space, Buttons.A);
            player.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_MOVE_UP] = new PlayerInputBinding(Keys.W, Buttons.DPadUp);
            player.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_MOVE_DOWN] = new PlayerInputBinding(Keys.S, Buttons.DPadDown);
            player.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_MOVE_LEFT] = new PlayerInputBinding(Keys.A, Buttons.DPadLeft);
            player.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_MOVE_RIGHT] = new PlayerInputBinding(Keys.D, Buttons.DPadRight);

            Player player2 = new Player(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Characters/NeptrStanding"), map.MapData.Spawn + new Vector2(50, 50), PlayerIndex.Two, new BaseStats(10, 10, 5));

            playerManager.SetPlayer(1, player2);
            drawManager.Add(player2, GamePlayDrawManager.DRAW_LIST_LEVEL.ENTITY);
            inputManager.Add(player2);
            logicManager.AddMovable(player2);
            logicManager.AddCollidable(player2);

            //really hacky temporary way of setting player bindings
            player2.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_ATTACK] = new PlayerInputBinding(Keys.RightControl, Buttons.A);
            player2.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_MOVE_UP] = new PlayerInputBinding(Keys.Up, Buttons.DPadUp);
            player2.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_MOVE_DOWN] = new PlayerInputBinding(Keys.Down, Buttons.DPadDown);
            player2.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_MOVE_LEFT] = new PlayerInputBinding(Keys.Left, Buttons.DPadLeft);
            player2.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_MOVE_RIGHT] = new PlayerInputBinding(Keys.Right, Buttons.DPadRight);

            TestMonster monster = new TestMonster(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Characters/Gunterstanding"), map.MapData.End, new BaseStats(30, 10, 5));

            drawManager.Add(monster, GamePlayDrawManager.DRAW_LIST_LEVEL.ENTITY);
            logicManager.AddCollidable(monster);
            logicManager.AddMovable(monster);
            logicManager.AddUpdatable(monster);

            GameEndTrigger endTrigger = new GameEndTrigger(new Rectangle((int)map.MapData.End.X, (int)map.MapData.End.Y, 100, 100));
            logicManager.AddUpdatable(endTrigger);
        }
    }
}
