using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class DroppedItem : Drawable, Collidable
    {
        private Texture2D sprite;
        private Vector2 position;
        private Item loot;
        private Rectangle boundingBox;
        private QuadTree<Collidable> collisionTree;

        public DroppedItem(Texture2D sprite, Vector2 position, Item loot)
        {
            this.sprite = sprite;
            this.position = position;
            this.loot = loot;

            this.boundingBox = this.sprite.Bounds;
            this.boundingBox.Location = new Point((int) this.position.X, (int) this.position.Y);
            this.collisionTree = GamePlayLogicManager.GetInstance().CollisionTree;
        }

        public Rectangle BoundingBox
        {
            get
            {
                return this.boundingBox;
            }
        }

        internal Item Item
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, Color.White);
        }

        protected bool Collide(Collidable toTest, Vector2 positionOffset)
        {
            Rectangle shiftedBoundingBox = boundingBox;
            shiftedBoundingBox.Offset((int)positionOffset.X, (int)positionOffset.Y);

            if (shiftedBoundingBox.Intersects(toTest.BoundingBox))
                return true;
            return false;
        }

        public void Hit(int amount, int type)
        {

        }
    }
}
