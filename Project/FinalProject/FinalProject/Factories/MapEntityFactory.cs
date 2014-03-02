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
        private static Texture2D spriteGrass, spriteDirt, spriteTreeLarge, spriteTreeSmall, spriteGrassBlades, spriteBush, spriteChest;

        private static MapEntityFactory instance = new MapEntityFactory();

        public enum MAP_ENTITY { DIRT = 1, GRASS = 2, GRASS_BLADES = 3, BUSH = 4, TREE_LARGE = 5, TREE_SMALL = 6, CHEST = 7 };


        private MapEntityFactory() { }

        public static MapEntityFactory GetInstance()
        {
            return instance;
        }

        public static void LoadSprites(ContentManager cm)
        {
            spriteGrass = cm.Load<Texture2D>("Entities/Map/Grass");
            spriteDirt = cm.Load<Texture2D>("Entities/Map/Dirt");
            spriteTreeLarge = cm.Load<Texture2D>("Entities/Map/TreeLarge");
            spriteTreeSmall = cm.Load <Texture2D>("Entities/Map/TreeSmall");
            spriteGrassBlades = cm.Load <Texture2D>("Entities/Map/GrassBlades");
            spriteBush = cm.Load<Texture2D>("Entities/Map/Bush");
            spriteChest = cm.Load<Texture2D>("Entities/Map/Chest");
        }

        public MapEntity CreateMapEntity(MAP_ENTITY type, Vector2 position)
        {
            switch (type)
            {
                case MAP_ENTITY.DIRT:
                    return CreateDirtMapEntity(position);
                case MAP_ENTITY.GRASS:
                    return CreateGrassMapEntity(position);
                case MAP_ENTITY.GRASS_BLADES:
                    return CreateGrassBladesMapEntity(position);
                default:
                    throw new NotImplementedException();
            }
        }

        public CollidableMapEntity CreateCollidableMapEntity(MAP_ENTITY type, Vector2 position)
        {
            switch (type)
            {
                case MAP_ENTITY.TREE_LARGE:
                    return CreateTreeLargeMapEntity(position);
                case MAP_ENTITY.TREE_SMALL:
                    return CreateTreeSmallMapEntity(position);
                case MAP_ENTITY.BUSH:
                    return CreateBushMapEntity(position);
                case MAP_ENTITY.CHEST:
                    return CreateChestMapEntity(position);
                default:
                    throw new NotImplementedException();
            }
        }

        private MapEntity CreateGrassMapEntity(Vector2 position)
        {
            return new MapEntity(CreateMapEntityData(spriteGrass), position);
        }

        private MapEntity CreateGrassBladesMapEntity(Vector2 position)
        {
            return new MapEntity(CreateMapEntityData(spriteGrassBlades), position);
        }

        private MapEntity CreateDirtMapEntity(Vector2 position)
        {
            return new MapEntity(CreateMapEntityData(spriteDirt), position);
        }

        private CollidableMapEntity CreateTreeLargeMapEntity(Vector2 position)
        {
            return new CollidableMapEntity(CreateMapEntityData(spriteTreeLarge), position);
        }

        private CollidableMapEntity CreateTreeSmallMapEntity(Vector2 position)
        {
            return new CollidableMapEntity(CreateMapEntityData(spriteTreeSmall), position);
        }

        private CollidableMapEntity CreateBushMapEntity(Vector2 position)
        {
            return new CollidableMapEntity(CreateMapEntityData(spriteBush), position);
        }

        private CollidableMapEntity CreateChestMapEntity(Vector2 position)
        {
            return new CollidableMapEntity(CreateMapEntityData(spriteChest), position);
        }

        private MapEntityData CreateMapEntityData(Texture2D texture)
        {
            MapEntityData data = new MapEntityData();
            data.Sprite = texture;
            return data;
        }
    }
}
