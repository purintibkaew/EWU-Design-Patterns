using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        private GamePlayLogicManager()
        {
            updateList = new List<Movable>();
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

        public void Add(Movable m)
        {
            updateList.Add(m);
        }

        public void Remove(Movable m)
        {
            if(updateList.Contains(m))
                updateList.Remove(m);
        }
    }
}
