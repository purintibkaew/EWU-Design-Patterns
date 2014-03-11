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
        private Vector2 endPoint;

        public List<MapEntity[][]> Contents { get { return mapContents; } }
        public Vector2 Spawn { get { return spawnPoint; } }
        public Vector2 End { get { return endPoint; } }


        public MapData(List<MapEntity[][]> mapContents, Vector2 spawnPoint, Vector2 endPoint)
        {
            this.mapContents = mapContents;
            this.spawnPoint = spawnPoint;
            this.endPoint = endPoint;
        }
    }
}
