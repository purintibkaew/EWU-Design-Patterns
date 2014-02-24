using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class CollidableMapEntity : MobileEntity
    {
        private Rectangle boundingBox;

        private MapEntityData mapEntityData;
        private Vector2 position;

        public Rectangle BoundingBox
        {
            get
            {
                return this.boundingBox;
            }
        }

        public CollidableMapEntity(MapEntityData mapEntityData, Vector2 position) : base(mapEntityData.Sprite, position) // a tree shouldn't be a mobile entity
        {
            this.mapEntityData = mapEntityData;
            this.position = position;
            this.boundingBox = mapEntityData.Sprite.Bounds;                         //the bounding box can be set to (temporarily, at least) the sprite bounding rectangle
            this.boundingBox.Location = new Point((int)position.X, (int)position.Y);//then need to move to the actual position, since the sprite is technically at (0, 0)
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mapEntityData.Sprite, position, Color.White);
        }

        public override void Hit(int amount, int type)
        {
            
        }

        public override void Logic()
        {
            //throw new NotImplementedException();
        }

        public override void CheckStatus()
        {
            //throw new NotImplementedException();
        }
    }
}
