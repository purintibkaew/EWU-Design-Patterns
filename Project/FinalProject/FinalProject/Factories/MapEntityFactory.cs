using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class MapEntityFactory
    {
        private static Texture2D spriteGrass, spriteDirt, spriteTree;

        private static MapEntityFactory instance = new MapEntityFactory();

        private MapEntityFactory() { }

        public static MapEntityFactory GetInstance()
        {
            return instance;
        }

        public static void LoadSprites(ContentManager cm)
        {
            spriteGrass = cm.Load<Texture2D>("SimpleGrass");
            spriteDirt = cm.Load<Texture2D>("SimpleDirt");
            spriteTree = cm.Load<Texture2D>("SimpleTree");
        }

        public Drawable CreateGrassMapEntity(Vector2 position)
        {
            MapEntityData data = CreateMapEntityData(spriteGrass);
            MapEntity grass = new MapEntity(data, position);
            return grass;
        }

        public Drawable CreateDirtMapEntity(Vector2 position)
        {
            MapEntityData data = CreateMapEntityData(spriteDirt);
            MapEntity dirt = new MapEntity(data, position);
            return dirt;
        }

        public Drawable CreateTreeMapEntity(Vector2 position)
        {
            MapEntityData data = CreateMapEntityData(spriteTree);
            CollidableMapEntity tree = new CollidableMapEntity(data, position);
            return tree;
        }

        private MapEntityData CreateMapEntityData(Texture2D texture)
        {
            MapEntityData data = new MapEntityData();
            data.Sprite = texture;
            return data;
        }
    }
}
