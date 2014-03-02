using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class GameMapForest : GameMap
    {
        private float[][] mapPoints;
        private readonly static int BORDER_WIDTH = 5;

        public GameMapForest(int height, int width)
        {
            this.Height = height;
            this.Width = width;

            this.mapPoints = new float[this.Height / MapEntity.MAP_ENTITY_BASE_SIZE][];

            for (int i = 0; i < this.mapPoints.Length; i++)
                this.mapPoints[i] = new float[this.Width / MapEntity.MAP_ENTITY_BASE_SIZE];

            SetMapPointsToEmpty();

            this.mapData = new MapData(CreateSpawnPoint(), CreateEndPoint());
            DebugText.GetInstance().WriteLinePerm("Spawn: " + this.mapData.Spawn + " End: " + this.mapData.End);
        }
        
        private void SetMapPointsToEmpty()
        {
            for (int i = 0; i < mapPoints.Length; i++)
            {
                for (int j = 0; j < mapPoints[i].Length; j++)
                    mapPoints[i][j] = -1;
            }
        }

        public override void LoadContent()
        {
            CreateMapBorder();
            CreatePathFromStartToEnd();
            RandomlyGenerateTerrain();
            PopulateMap();
        }

        private Vector2 CreateSpawnPoint()
        {
            Random rand = new Random();
            int min = BORDER_WIDTH * BORDER_WIDTH * MapEntity.MAP_ENTITY_BASE_SIZE;

            return new Vector2(rand.Next(min, Width - min) / 2, rand.Next(min, Height - min) / 2);
        }

        private Vector2 CreateEndPoint()
        {
            Random rand = new Random();
            int min = BORDER_WIDTH * BORDER_WIDTH * MapEntity.MAP_ENTITY_BASE_SIZE;

            return new Vector2(rand.Next(min, Width - min) * 1.5f, rand.Next(min, Height - min) * 1.5f);            
        }

        private void CreatePathFromStartToEnd()
        {
            Random rand = new Random();
            int[] curr = new int[] { (int) this.mapData.Spawn.X / MapEntity.MAP_ENTITY_BASE_SIZE, (int) this.mapData.Spawn.Y / MapEntity.MAP_ENTITY_BASE_SIZE},
                    end = new int[] { (int) this.mapData.End.X / MapEntity.MAP_ENTITY_BASE_SIZE, (int) this.mapData.End.Y / MapEntity.MAP_ENTITY_BASE_SIZE};

            int val = 0;

            ClearAnOpenPath(curr);
            ClearAnOpenPath(end);

            //while(!(Math.Abs(curr[0] - end[0]) < 2 && Math.Abs(curr[1] - end[1]) < 2))
            //{
                
            //}
        }

        private void ClearAnOpenPath(int[] point)
        {
            int x = point[0];
            int y = point[1];
            float spawnValue = 0f;
            int clearLength = 5;

            for(int i = x - clearLength / 2; i < x + clearLength; i++)
            {
                for(int j = y - clearLength / 2; j < y + clearLength; j++)
                {
                    this.mapPoints[i][j] = spawnValue;
                }
            }
        }

        private void CreateMapBorder()
        {
            FillEntireMapWithBorder();
            CutOutMiddleMapWithoutBorder();
        }

        private void FillEntireMapWithBorder()
        {
            Random rand = new Random();

            for (int i = 0; i < mapPoints.Length; i += rand.Next(0, 2))
            {
                for (int j = 0; j < mapPoints.Length; j += rand.Next(0, 4))
                {
                    if (rand.Next(0, 100) < 10)
                        mapPoints[i][j] = 1f;
                    else if (rand.Next(0, 10) <= 5)
                        mapPoints[i][j] = 0.94f;
                    else
                        mapPoints[i][j] = 0.96f;
                }
            }
        }

        private void CutOutMiddleMapWithoutBorder()
        {
            for (int i = BORDER_WIDTH; i < mapPoints.Length - BORDER_WIDTH; i++)
            {
                for (int j = BORDER_WIDTH; j < mapPoints.Length - BORDER_WIDTH; j++)
                {
                    mapPoints[i][j] = -1;
                }
            }
        }

        private CollidableMapEntity CreateTreeOrBush(float value, Vector2 position)
        {
            MapEntityFactory factory = MapEntityFactory.GetInstance();
            CollidableMapEntity treeOrBush = null;

            if (value < .95f)
                treeOrBush = factory.CreateCollidableMapEntity(MapEntityFactory.MAP_ENTITY.TREE_LARGE, position);
            else if (value < .97f)
                treeOrBush = factory.CreateCollidableMapEntity(MapEntityFactory.MAP_ENTITY.TREE_SMALL, position);
            else
                treeOrBush = factory.CreateCollidableMapEntity(MapEntityFactory.MAP_ENTITY.BUSH, position);

            return treeOrBush;
        }

        private void RandomlyGenerateTerrain()
        {
            IGenerator valueGenerator = new GeneratorSimplexNoise();

            for (int i = 0; i < this.mapPoints.Length; i++)
            {
                for (int j = 0; j < this.mapPoints[i].Length; j++)
                {
                    if (this.mapPoints[i][j] == -1) // empty spot, fill it with something more or less randomly
                        this.mapPoints[i][j] = valueGenerator.GetValue(i * MapEntity.MAP_ENTITY_BASE_SIZE, j * MapEntity.MAP_ENTITY_BASE_SIZE, new Random().Next(100000));
                }
            }
        }

        private void PopulateMap()
        {
            MapEntityFactory factory = MapEntityFactory.GetInstance();
            float value = 0.0f;

            for (int i = 0; i < this.mapPoints.Length; i++)
            {
                for (int j = 0; j < this.mapPoints[i].Length; j++)
                {
                    value = this.mapPoints[i][j];
                    Vector2 position = new Vector2(i * MapEntity.MAP_ENTITY_BASE_SIZE, j * MapEntity.MAP_ENTITY_BASE_SIZE);

                    if (value < 0.1f)
                    {
                        AddToDrawList(factory.CreateMapEntity(MapEntityFactory.MAP_ENTITY.DIRT, position), GamePlayDrawManager.DRAW_LIST_LEVEL.MAP_BACKGROUND);

                        if(value <= 0.07)
                            AddToDrawList(factory.CreateMapEntity(MapEntityFactory.MAP_ENTITY.GRASS_BLADES, position), GamePlayDrawManager.DRAW_LIST_LEVEL.MAP_BACKGROUND);
                    }
                    else if (value <= .9f)
                    {
                        AddToDrawList(factory.CreateMapEntity(MapEntityFactory.MAP_ENTITY.GRASS, position), GamePlayDrawManager.DRAW_LIST_LEVEL.MAP_BACKGROUND);
                    }
                    else if (value <= 1f)
                    {
                        // this makes sure something is spawned in the upper left corner of the tree or bush because otherwise the background will bleed through and become visible
                        AddToDrawList(factory.CreateMapEntity(MapEntityFactory.MAP_ENTITY.GRASS, position), GamePlayDrawManager.DRAW_LIST_LEVEL.MAP_BACKGROUND);
                        CollidableMapEntity tree = CreateTreeOrBush(value, position);
                        AddToDrawList(tree, GamePlayDrawManager.DRAW_LIST_LEVEL.MAP_FOREGROUND);
                        AddToCollideList(tree);
                    }
                }
            }
        }
    }
}
