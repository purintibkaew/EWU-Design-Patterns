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
        }

        public override void LoadContent()
        {
            CreateMapBorder();
            RandomlyGenerateTerrain();
            PopulateMap();
        }

        private void CreateMapBorder()
        {
            FillEntireMapWithBorder();
            CutOutMiddleMapWithoutBorder();
        }

        private void FillEntireMapWithBorder()
        {
            for (int i = 0; i < mapPoints.Length; i++)
            {
                for (int j = 0; j < mapPoints.Length; j++)
                {
                    mapPoints[i][j] = 1f;
                }
            }
        }

        private void CutOutMiddleMapWithoutBorder()
        {
            for (int i = BORDER_WIDTH; i < mapPoints.Length - BORDER_WIDTH; i++)
            {
                for (int j = BORDER_WIDTH; j < mapPoints.Length - BORDER_WIDTH; j++)
                {
                    mapPoints[i][j] = 0.0f;
                }
            }
        }

        private CollidableMapEntity CreateRandomTreeOrBush()
        {
            throw new NotImplementedException();
        }

        private void RandomlyGenerateTerrain()
        {
            Generator valueGenerator = new GeneratorSimplexNoise();

            for (int i = 0; i < this.mapPoints.Length; i++)
            {
                for (int j = 0; j < this.mapPoints[i].Length; j++)
                {
                    if (this.mapPoints[i][j] == 0) // empty spot, fill it!
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
                        AddToDrawList(factory.CreateDirtMapEntity(position), GamePlayDrawManager.DRAW_LIST_LEVEL.MAP_BACKGROUND);

                        //if(value <= 0.07)
                        //AddToDrawList(/* Add some grass here on top of dirt */, GamePlayDrawManager.DRAW_LIST_LEVEL.MAP_BACKGROUND);
                    }
                    else if (value <= .9f)
                    {
                        AddToDrawList(factory.CreateGrassMapEntity(position), GamePlayDrawManager.DRAW_LIST_LEVEL.MAP_BACKGROUND);
                    }
                    else if (value <= 1f)
                    {
                        CollidableMapEntity tree = factory.CreateTreeMapEntity(position);
                        AddToDrawList(tree, GamePlayDrawManager.DRAW_LIST_LEVEL.MAP_FOREGROUND);
                        AddToCollideList(tree);
                    }
                }
            }
        }
    }
}
