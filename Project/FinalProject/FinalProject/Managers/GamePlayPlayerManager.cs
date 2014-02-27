using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    class GamePlayPlayerManager
    {
        private static GamePlayPlayerManager instance;

        public static GamePlayPlayerManager GetInstance()
        {
            if (instance == null)
                instance = new GamePlayPlayerManager();
            return instance;
        }

        private Player[] players;

        private GamePlayPlayerManager()
        {
            players = new Player[4];

            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player(null, new Vector2(), PlayerIndex.One); //init them all to garbage, consider different constructor for this in Player
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
                    toReturn += p.Position;
                }
            }

            if (numActivePlayers != 0)
                toReturn /= numActivePlayers;

            return toReturn;
        }

        public void SetPlayer(int playerIndex, Player p)
        {
            players[playerIndex] = p;
            p.IsActive = true;
        }
    }
}
