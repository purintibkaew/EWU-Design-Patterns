using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class GamePlayLogicManager
    {
        private static GamePlayLogicManager instance;

        public static GamePlayLogicManager GetInstance()
        {
            if (instance == null)
                instance = new GamePlayLogicManager();
            return instance;
        }

        private List<Movable> updateList;
        private QuadTree<Collidable> collidableEntities;

        public QuadTree<Collidable> CollisionTree
        {
            get
            {
                return this.collidableEntities;
            }
        }

        private GamePlayLogicManager()
        {
            //CHANGE THIS WHEN WE HAVE AN ACTUAL MAP OBJECT WITH CONCRETE DIMENSIONS
            Rectangle screenRect = new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            updateList = new List<Movable>();
            collidableEntities = new QuadTree<Collidable>((x => x.BoundingBox), screenRect);
        }

        public void Update()
        {
            foreach(Movable m in updateList)
            {
                m.Logic();
            }

            foreach (Movable m in updateList)
            {
                m.Move();
            }
        }

        public void AddMovable(Movable m)
        {
            updateList.Add(m);
        }

        public void AddCollidable(Collidable c)
        {
            collidableEntities.Add(c);
        }

        public void Remove(Movable m)
        {
            if(updateList.Contains(m))
                updateList.Remove(m);
        }
    }
}
