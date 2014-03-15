﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class PlayerManager
    {
        public enum Character {BMO, NEPTR};
        private static PlayerManager instance;

        public static PlayerManager GetInstance()
        {
            if (instance == null)
                instance = new PlayerManager();
            return instance;
        }

        private Player[] players;

        private PlayerManager()
        {
            players = new Player[4];

            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player(PlayerIndex.One);
                players[i].IsActive = false;
            }
        }

        public List<Player> GetActivePlayers()
        {
            List<Player> playersToReturn = new List<Player>();

            foreach(Player p in players)
            {
                if (p.IsActive)
                    playersToReturn.Add(p);
            }

            return playersToReturn;
        }

        //get the average position of all active players (for determining camera position)
        public Vector2 GetPlayerAveragePosition()
        {
            Vector2 toReturn = new Vector2();
            int numActivePlayers = 0;

            foreach (Player p in players)
            {
                if (p.IsActive)
                {
                    numActivePlayers++;
                    toReturn += p.Position + p.PlayerCenter;
                }
            }

            if (numActivePlayers != 0)
                toReturn /= numActivePlayers;

            return toReturn;
        }

        //return a the minimum rectangle that contains all players
        public Rectangle GetPlayerDistanceRectangle()
        {
            bool dimensionsDefined = false;

            int minX = -1, maxX = -1, minY = -1, maxY = -1;

            foreach(Player p in players)
            {
                if(p.IsActive)
                {
                    if(!dimensionsDefined)
                    {
                        maxX = minX = (int)p.Position.X;
                        maxY = minY = (int)p.Position.Y;

                        dimensionsDefined = true;
                    }
                    else
                    {
                        if((int)p.Position.X < minX)
                            minX = (int)p.Position.X; 
                        if((int)p.Position.X > maxX)
                            maxX = (int)p.Position.X;
                        if((int)p.Position.Y < minY)
                            minY = (int)p.Position.Y; 
                        if((int)p.Position.Y > maxY)
                            maxY = (int)p.Position.Y;
                    }
                }

            }

            return new Rectangle(minX, minY, maxX - minX, maxY - minY);
        }


        public void SetPlayer(int playerIndex, Character c)
        {
            GamePlayDrawManager drawManager = GamePlayDrawManager.GetInstance();
            GamePlayInputManager inputManager = GamePlayInputManager.GetInstance();
            GamePlayLogicManager logicManager = GamePlayLogicManager.GetInstance();
            GamePlayMapManager mapManager = GamePlayMapManager.GetInstance();
            Random rand = new Random();

            Texture2D playerSprite;
            switch (c)
            {
                case Character.BMO:
                    playerSprite = SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Characters/BMOStanding");
                    break;
                case Character.NEPTR:
                    playerSprite = SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Characters/NeptrStanding");
                    break;
                default:
                    playerSprite = SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Characters/BMOStanding");
                    break;

            }

            PlayerIndex pi;
            switch (playerIndex)
            {
                case 1:
                    pi = PlayerIndex.One;
                    break;
                case 2:
                    pi = PlayerIndex.Two;
                    break;
                default:
                    pi = PlayerIndex.One;
                    break;
            }

            Player player = new Player(playerSprite, mapManager.Map.MapData.Spawn + new Vector2(rand.Next(50) - 25, rand.Next(50) - 25), pi, new BaseStats(10, 10, 5));
            SetPlayer(playerIndex - 1, player);



            //Setting up player specific input
            if (playerIndex == 1)
            {
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
            }
            else
            {
                drawManager.Add(player, GamePlayDrawManager.DRAW_LIST_LEVEL.ENTITY);
                inputManager.Add(player);
                logicManager.AddMovable(player);
                logicManager.AddCollidable(player);

                //really hacky temporary way of setting player bindings
                player.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_ATTACK] = new PlayerInputBinding(Keys.RightControl, Buttons.A);
                player.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_MOVE_UP] = new PlayerInputBinding(Keys.Up, Buttons.DPadUp);
                player.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_MOVE_DOWN] = new PlayerInputBinding(Keys.Down, Buttons.DPadDown);
                player.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_MOVE_LEFT] = new PlayerInputBinding(Keys.Left, Buttons.DPadLeft);
                player.PlayerBindings[(int)Player.PlayerKeyBind.PLAYER_MOVE_RIGHT] = new PlayerInputBinding(Keys.Right, Buttons.DPadRight);
            }

        }

        public void SetPlayer(int playerIndex, Player p)
        {
            players[playerIndex] = p;
            p.IsActive = true;
        }


        public void setupDefaultPlayers()
        {
            GamePlayDrawManager drawManager = GamePlayDrawManager.GetInstance();
            GamePlayInputManager inputManager = GamePlayInputManager.GetInstance();
            GamePlayLogicManager logicManager = GamePlayLogicManager.GetInstance();
            GamePlayMapManager mapManager = GamePlayMapManager.GetInstance();


            //temporary hacky player loading, hard coded
            Player player = new Player(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Characters/BMOStanding"), mapManager.Map.MapData.Spawn, PlayerIndex.One, new BaseStats(10, 10, 5));

            //we're going to be doing these calls a lot - consider factory or facade or similar
            this.SetPlayer(0, player);
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

            Player player2 = new Player(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Characters/NeptrStanding"), mapManager.Map.MapData.Spawn + new Vector2(50, 50), PlayerIndex.Two, new BaseStats(10, 10, 5));

            this.SetPlayer(1, player2);
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
        }
    }
}
