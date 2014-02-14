using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameObjects.Map
{
    class CollidableMapEntity : Collidable
    {
        private MapEntityData mapEntityData;
        private Vector2 position;

        public CollidableMapEntity(MapEntityData mapEntityData, Vector2 position)
        {
            this.mapEntityData = mapEntityData;
            this.position = position;
        }
    }
}
