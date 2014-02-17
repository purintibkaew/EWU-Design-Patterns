using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class MapEntityFactory
    {
        private static MapEntityFactory instance = new MapEntityFactory();

        private MapEntityFactory() { }

        public static MapEntityFactory GetInstance()
        {
            return instance;
        }

        public Drawable CreateGrassMapEntity(Vector2 position)
        {
            Texture2D texture = LoadTexture("SimpleGrass");
            MapEntityData data = GetMapEntityData(texture);
            MapEntity grass = new MapEntity(data, position);
            return grass;
        }

        public Drawable CreateDirtMapEntity(Vector2 position)
        {
            Texture2D texture = LoadTexture("SimpleDirt");
            MapEntityData data = GetMapEntityData(texture);
            MapEntity dirt = new MapEntity(data, position);
            return dirt;
        }

        public Drawable CreateTreeMapEntity(Vector2 position)
        {
            Texture2D texture = LoadTexture("SimpleTree");
            MapEntityData data = GetMapEntityData(texture);
            CollidableMapEntity tree = new CollidableMapEntity(data, position);
            return tree;
        }

        private Texture2D LoadTexture(string s)
        {
            return (Texture2D) GamePlayContentManager.GetInstance().Load(s);
        }

        private MapEntityData GetMapEntityData(Texture2D texture)
        {
            MapEntityData data = new MapEntityData();
            data.Sprite = texture;
            return data;
        }
    }
}
