﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace FinalProject
{
    class GamePlayState : GameState
    {
        private GamePlayManager gameManager; 

        public GamePlayState()
        {
            gameManager = GamePlayManager.GetInstance();
        }

        public void Init(ContentManager gameContentManager)
        {
            gameManager.Init(gameContentManager);
        }

        public void HandleInput(KeyboardState kb, GamePadState gp)
        {
            gameManager.HandleInput(kb, gp);
        }

        public void Update()
        {
            gameManager.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            gameManager.Draw(spriteBatch);
        }
    }
}
