using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class MapFactory
    {
        private static MapFactory instance = new MapFactory();

        private MapFactory() { }

        public static MapFactory GetInstance()
        {
            return instance;
        }

        public enum MAPS { FOREST_PATH = 0 };

        public GameMap GetMap(MAPS map_type, int width, int height)
        {
            switch(map_type)
            {
                case MAPS.FOREST_PATH:
                    return new GameMapForest(width, height);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
