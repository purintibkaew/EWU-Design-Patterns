using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class GamePlayInputManager
    {
        private static GamePlayInputManager instance;

        public static GamePlayInputManager GetInstance()
        {
            if (instance == null)
                instance = new GamePlayInputManager();
            return instance;
        }

        private List<Player> players;

        private GamePlayInputManager()
        {
            players = new List<Player>();
        }

        public void HandleInput(KeyboardState kb, GamePadState gp)
        {
            foreach (Player p in players)
            {
                p.HandleInput(kb, gp);
            }
        }

        public void Add(Player p)
        {
            players.Add(p);
        }

        public void Remove(Player p)
        {
            if (players.Contains(p))
                players.Remove(p);
        }
    }
}
