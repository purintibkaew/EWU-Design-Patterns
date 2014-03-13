using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace FinalProject
{
    public interface GameState
    {
        void Init(Game1 game, ContentManager gameContentManager, GraphicsDevice gd);
        void HandleInput();
        void Update();
        void Draw(SpriteBatch spriteBatch);
        void NextState();
    }
}
