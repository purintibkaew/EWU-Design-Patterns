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

        private QuadTree<Drawable> drawTree;
        private Rectangle screenRect;

        public QuadTree<Drawable> DrawTree
        {
            get
            {
                return this.drawTree;
            }
        }

        private GamePlayDrawManager()
        {
            screenRect = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            drawTree = new QuadTree<Drawable>((x => x.SpriteRectangle), screenRect);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Drawable[] toDraw = drawTree.GetItems(screenRect);

            foreach(Drawable d in toDraw)
            {
                drawTree.UpdatePosition(d);
                d.Draw(spriteBatch);
            }
        }

        public void Add(Drawable d)
        {
            drawTree.Add(d);
        }

        public void Remove(Drawable d)
        {
            //TODO: Look into whether this is safe or not
            drawTree.Remove(d);
        }
    }
}
