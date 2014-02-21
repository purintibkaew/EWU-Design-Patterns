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

        public GameMap GetSimpleTestMap()
        {
            return new GameMapSimple();
        }

        public GameMap GetSimplexNoiseMap()
        {
            return new GameMapSimplexNoiseGenerated(1024, 1024);
        }
    }
}
