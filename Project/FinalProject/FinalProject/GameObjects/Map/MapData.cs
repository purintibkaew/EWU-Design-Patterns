using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class MapData : IMapData
    {
        private List<MapEntity[][]> mapContents;
        private Vector2 spawnPoint;
        private int spawnHeight;
        private int spawnWidth;
        private Vector2 endPoint;

        public List<MapEntity[][]> Contents { get { return mapContents; } }
        public Vector2 Spawn { get { return spawnPoint; } }
        public int SpawnHeight { get { return spawnHeight; } }
        public int SpawnWidth { get { return spawnWidth; } }
        public Vector2 End { get { return endPoint; } }


        public MapData(List<MapEntity[][]> mapContents, Vector2 spawnPoint, int spawnHeight, int spawnWidth, Vector2 endPoint)
        {
            this.mapContents = mapContents;
            this.spawnPoint = spawnPoint;
            this.spawnHeight = spawnHeight;
            this.spawnWidth = spawnWidth;
            this.endPoint = endPoint;
        }
    }
}
