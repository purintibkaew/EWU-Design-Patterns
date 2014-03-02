using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class MapData
    {
        private Vector2 spawnPoint;
        private Vector2 endPoint;

        public Vector2 Spawn { get { return spawnPoint; } set { spawnPoint = value; } }
        public Vector2 End { get { return endPoint; } set { endPoint = value; } }


        public MapData(Vector2 spawnPoint, Vector2 endPoint)
        {
            this.spawnPoint = spawnPoint;
            this.endPoint = endPoint;
        }
    }
}
