using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class GameMapSimplexNoiseGenerated : GameMap
    {
        private static readonly int MAX_PRIORITY_LEVEL = 10;
        private float[][] mapPoints;

        /*
         * Max priority is 0, Min is at MAX_PRIORITY_LEVEL-1
         */
        private List<List<Drawable>> drawablesPriorityList;


        public GameMapSimplexNoiseGenerated(int height, int width)
        {
            this.Height = height;
            this.Width = width;
            this.mapPoints = new float[this.Height / MapEntity.MAP_ENTITY_BASE_SIZE][];
            
            for(int i = 0; i < this.mapPoints.Length; i++)
                this.mapPoints[i] = new float[this.Width / MapEntity.MAP_ENTITY_BASE_SIZE];

            drawablesPriorityList = new List<List<Drawable>>(MAX_PRIORITY_LEVEL);

            for (int i = 0; i < MAX_PRIORITY_LEVEL; i++)
            {
                drawablesPriorityList.Add(new List<Drawable>());
            }
        }

        public override void LoadContent()
        {
            Generator valueGenerator = new GeneratorSimplexNoise();

            for (int i = 0; i < this.mapPoints.Length; i++)
            {
                for (int j = 0; j < this.mapPoints[i].Length; j++)
                {
                    this.mapPoints[i][j] = valueGenerator.GetValue(i * MapEntity.MAP_ENTITY_BASE_SIZE, j * MapEntity.MAP_ENTITY_BASE_SIZE, new Random().Next(100000));
                }
            }

            PopulatePriorityLists();
            AddContent();
        }

        private void PopulatePriorityLists()
        {
            MapEntityFactory factory = MapEntityFactory.GetInstance();
            float value = 0.0f;

            for (int i = 0; i < this.mapPoints.Length; i++)
            {
                for (int j = 0; j < this.mapPoints[i].Length; j++)
                {
                    value = this.mapPoints[i][j];
                    Vector2 position = new Vector2(i * MapEntity.MAP_ENTITY_BASE_SIZE, j * MapEntity.MAP_ENTITY_BASE_SIZE);

                    if (value < 0.7)
                    {
                        AddToPriorityList(MAX_PRIORITY_LEVEL-1, factory.CreateDirtMapEntity(position));
                    }
                    else if (value < .9)
                    {
                        AddToPriorityList(MAX_PRIORITY_LEVEL-2, factory.CreateGrassMapEntity(position));
                    }
                    else if (value < 1)
                    {
                        CollidableMapEntity tree = factory.CreateTreeMapEntity(position);
                        AddToPriorityList(MAX_PRIORITY_LEVEL-3, tree);
                        AddToCollideList(tree);
                    }
                }
            }
        }

        private void AddToPriorityList(int priority, Drawable drawableToAdd)
        {
            drawablesPriorityList[priority].Add(drawableToAdd);
        }

        private void AddContent()
        {
            for (int i = drawablesPriorityList.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < drawablesPriorityList[i].Count; j++)
                {
                    AddToDrawList(drawablesPriorityList[i][j]);
                }
            }
        }
    }
}
