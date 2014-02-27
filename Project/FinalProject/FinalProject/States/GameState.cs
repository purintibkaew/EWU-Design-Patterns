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
    interface GameState
    {
        void Init(ContentManager gameContentManager, GraphicsDevice gd);
        void HandleInput();
        void Update();
        void Draw(SpriteBatch spriteBatch);
        void NextState();
    }
}
