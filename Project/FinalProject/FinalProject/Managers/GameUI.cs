using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class GameUI
    {
        private List<GameUIElement> elementsAbsolute;
        private List<GameUIElement> elementsRelative;

        private SpriteFont font;

        public GameUI()
        {
            elementsAbsolute = new List<GameUIElement>();
            elementsRelative = new List<GameUIElement>();
        }

        public SpriteFont Font
        {
            set
            {
                this.font = value;
            }
        }

        public void AddElementA(GameUIElement e)
        {
            if (!elementsAbsolute.Contains(e))
                elementsAbsolute.Add(e);
        }

        public void AddElementR(GameUIElement e)
        {
            if (!elementsRelative.Contains(e))
                elementsRelative.Add(e);
        }

        public void RemoveElementA(GameUIElement e)
        {
            if (elementsAbsolute.Contains(e))
                elementsAbsolute.Remove(e);
        }

        public void RemoveElementR(GameUIElement e)
        {
            if (elementsRelative.Contains(e))
                elementsRelative.Remove(e);
        }

        public void RemoveElement(GameUIElement e)
        {
            RemoveElementA(e);
            RemoveElementR(e);
        }


        public void DrawAbsolute(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < elementsAbsolute.Count; i++)
            {
                elementsAbsolute[i].Draw(spriteBatch, font);
                elementsAbsolute[i].Update();
            }
        }

        public void DrawRelative(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < elementsRelative.Count; i++)
            {
                elementsRelative[i].Draw(spriteBatch, font);
                elementsRelative[i].Update();
            }
        }
    }
}
