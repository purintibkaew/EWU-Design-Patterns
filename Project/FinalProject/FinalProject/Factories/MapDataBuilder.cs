using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class MapDataBuilder
    {
        private Vector2 spawnPoint;
        private int spawnHeight;
        private int spawnWidth;
        private Vector2 endPoint;
        private List<MapEntity[][]> mapContents;


        public MapDataBuilder()
        {
            spawnPoint = Vector2.Zero;
            endPoint = Vector2.Zero;
            spawnHeight = 0;
            spawnWidth = 0;
            mapContents = new List<MapEntity[][]>();
        }

        public MapData GetResult()
        {
            return new MapData(mapContents, spawnPoint, spawnHeight, spawnWidth, endPoint);
        }

        public void SetSpawn(int x, int y)
        {
            spawnPoint = new Vector2(x, y);
        }

        public void SetEnd(int x, int y)
        {
            endPoint = new Vector2(x, y);
        }

        public void SetSpawn(Vector2 spawn)
        {
            spawnPoint = spawn;
        }

        public void SetSpawnHeight(int height)
        {
            spawnHeight = height;
        }

        public void SetSpawnWidth(int width)
        {
            spawnWidth = width;
        }

        public void SetEnd(Vector2 end)
        {
            endPoint = end;
        }

        public void SetMapContents(List<MapEntity[][]> contents)
        {
            mapContents = contents;
        }
    }
}