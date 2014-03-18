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
        private static MapEntityFactory instance = new MapEntityFactory();

        public enum MAP_ENTITY { DIRT = 1, GRASS = 2, GRASS_BLADES = 3, BUSH = 4, TREE_LARGE = 5, TREE_SMALL = 6, CHEST = 7};


        private MapEntityFactory() { }

        public static MapEntityFactory GetInstance()
        {
            return instance;
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
            return new MapEntity(CreateMapEntityData(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Map/Grass")), position);
        }

        private MapEntity CreateGrassBladesMapEntity(Vector2 position)
        {
            return new MapEntity(CreateMapEntityData(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Map/GrassBlades")), position);
        }

        private MapEntity CreateDirtMapEntity(Vector2 position)
        {
            return new MapEntity(CreateMapEntityData(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Map/Dirt")), position);
        }

        private CollidableMapEntity CreateTreeLargeMapEntity(Vector2 position)
        {
            return new CollidableMapEntity(CreateMapEntityData(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Map/TreeLarge")), position);
        }

        private CollidableMapEntity CreateTreeSmallMapEntity(Vector2 position)
        {
            return new CollidableMapEntity(CreateMapEntityData(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Map/TreeSmall")), position);
        }

        private CollidableMapEntity CreateBushMapEntity(Vector2 position)
        {
            return new CollidableMapEntity(CreateMapEntityData(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Map/Bush")), position);
        }

        private CollidableMapEntity CreateChestMapEntity(Vector2 position)
        {
            return new CollidableMapEntity(CreateMapEntityData(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Map/Chest")), position);
        }

        private MapEntityData CreateMapEntityData(Texture2D texture)
        {
            MapEntityData data = new MapEntityData();
            data.Sprite = texture;
            return data;
        }
    }
}
