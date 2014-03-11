using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    abstract class GameMap
    {
        protected MapData mapData;

        public int BASE_SIZE { get { return 128; } }
        public MapData MapData { get { return mapData; } }


        public readonly static int MAX_LAYERS = 4;

        public enum LAYERS { GROUND = 0, ABOVE_GROUND = 1, TREES = 2, SKY = 3 }

        public int Height 
        {
            get
            {
                return height;
            }

            set
            {
                if(value >= BASE_SIZE && value % BASE_SIZE == 0)
                    height = value;
                else
                    height = (value / BASE_SIZE) * BASE_SIZE; // set it to the lowest nearest divisible of BASE_SIZE
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                if (value >= BASE_SIZE && value % BASE_SIZE == 0)
                    width = value;
                else
                    width = (value / BASE_SIZE) * BASE_SIZE; // set it to the lowest nearest divisible of BASE_SIZE
            }
        }

        protected int height;
        protected int width;
        protected List<MapEntity[][]> contentLayers;
        protected List<Collidable> collidablesToAdd;

        public abstract void LoadContent();

        protected void AddToDrawList(Drawable d)
        {
            GamePlayDrawManager.GetInstance().Add(d);
        }

        protected void AddToCollideList(Collidable c)
        {
            collidablesToAdd.Add(c);
        }

        public void PopulateCollisionTree()
        {
            foreach(Collidable c in collidablesToAdd)
                if(c != null)
                    GamePlayLogicManager.GetInstance().AddCollidable(c);
        }
    }
}
