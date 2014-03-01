using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class MapEntity : Drawable
    {
        /*
         * Size of the smallest sprite image, this way our map knows how to fill in the playing area
         */
        public static readonly int MAP_ENTITY_BASE_SIZE = 16;
 
        private MapEntityData mapEntityData;
        private Vector2 position;

        public Rectangle SpriteRectangle { get { return mapEntityData.Sprite.Bounds; } }


        public MapEntity(MapEntityData mapEntityData, Vector2 position)
        {
            this.mapEntityData = mapEntityData;
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mapEntityData.Sprite, position, Color.White);
        }
    }
}
