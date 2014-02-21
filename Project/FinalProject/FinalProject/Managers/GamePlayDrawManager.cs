using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class GamePlayDrawManager
    {
        private static GamePlayDrawManager instance;

        public static GamePlayDrawManager GetInstance()
        {
            if (instance == null)
                instance = new GamePlayDrawManager();
            return instance;
        }

        private List<Drawable> drawList;
        private Rectangle screenRect;


        private GamePlayDrawManager()
        {
            screenRect = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            drawList = new List<Drawable>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Drawable d in drawList)
            {
                d.Draw(spriteBatch);
            }
        }

        public void Add(Drawable d)
        {
            drawList.Add(d);
        }

        public void Remove(Drawable d)
        {
            //TODO: Look into whether this is safe or not
            drawList.Remove(d);
        }
    }
}
