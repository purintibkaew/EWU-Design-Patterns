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

        public enum MAPS { SIMPLE = 0, RANDOM_FOREST = 1, FOREST_PATH = 2 };

        public GameMap GetMap(MAPS map_type)
        {
            switch(map_type)
            {
                case MAPS.SIMPLE:
                    return new GameMapSimple();
                case MAPS.RANDOM_FOREST:
                    return new GameMapSimplexNoiseGenerated(1024, 1024);
                case MAPS.FOREST_PATH:
                    return new GameMapForest(1024, 1024);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
