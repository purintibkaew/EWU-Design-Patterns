using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class GamePlayMapManager
    {
        private static GamePlayMapManager instance = new GamePlayMapManager();


        private GamePlayMapManager() { }

        public static GamePlayMapManager GetInstance()
        {
            return instance;
        }

        private GameMap gameMap;

        public GameMap Map { get { return gameMap; } set { gameMap = value; } }

        public List<List<Drawable>> GetDrawablesInArea(int x, int y, int w, int h)
        {
            List<List<Drawable>> entities = new List<List<Drawable>>(GameMap.MAX_LAYERS);

            for (int i = 0; i < GameMap.MAX_LAYERS; i++)
            {
                entities.Add(new List<Drawable>());
            }

            x /= MapEntity.MAP_ENTITY_BASE_SIZE;
            y /= MapEntity.MAP_ENTITY_BASE_SIZE;
            w /= MapEntity.MAP_ENTITY_BASE_SIZE;
            h /= MapEntity.MAP_ENTITY_BASE_SIZE;

            if (x < 0) x = 0;
            if (y < 0) y = 0;

            DebugText dt = DebugText.GetInstance();

            for (int k = 0; k < GameMap.MAX_LAYERS; k++)
            {
                for (int i = x; i < x + w && i < gameMap.MapData.Contents[0].Length - 1; i++)
                {
                    for (int j = y; j < y + h && j < gameMap.MapData.Contents[0][0].Length - 1; j++)
                    {
                        if (gameMap.MapData.Contents[k][i][j] != null)
                            entities[k].Add(gameMap.MapData.Contents[k][i][j]);
                    }
                }
            }

            return entities;
        }
    }
}
