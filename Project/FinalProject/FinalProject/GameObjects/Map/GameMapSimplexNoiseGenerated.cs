using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class GameMapSimplexNoiseGenerated : GameMap
    {        
        private float[][] mapPoints; 

        public GameMapSimplexNoiseGenerated(int height, int width)
        {
            this.Height = height;
            this.Width = width;
            this.mapPoints = new float[this.Height / MapEntity.MAP_ENTITY_BASE_SIZE][];
            
            for(int i = 0; i < this.mapPoints.Length; i++)
                this.mapPoints[i] = new float[this.Width / MapEntity.MAP_ENTITY_BASE_SIZE];
        }

        public override void LoadContent()
        {
            GeneratorSimplexNoise valueGenerator = new GeneratorSimplexNoise();

            for (int i = 0; i < this.mapPoints.Length; i++)
            {
                for (int j = 0; j < this.mapPoints[i].Length; j++)
                {
                    this.mapPoints[i][j] = valueGenerator.GetValue(i * MapEntity.MAP_ENTITY_BASE_SIZE, j * MapEntity.MAP_ENTITY_BASE_SIZE,0/* new Random().Next(100000)*/);
                }
            }

            AddContent();
        }

        private void AddContent()
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
                        AddToDrawList(factory.CreateDirtMapEntity(position));
                    }
                    else if (value < .9)
                    {
                        AddToDrawList(factory.CreateGrassMapEntity(position));
                    }
                    else if (value < 1)
                    {
                        AddToDrawList(factory.CreateTreeMapEntity(position));
                    }
                }
            }
        }

        private void AddToDrawList(Drawable d)
        {
            GamePlayDrawManager.GetInstance().Add(d);
        }
    }
}
