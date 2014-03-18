using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class ChestMapEntity : CollidableMapEntity, Drawable
    {
        private Inventory inv;

        private MapEntityData data;

        public ChestMapEntity(MapEntityData data, Vector2 position, List<Item> items) : base(data, position)
        {
            inv = new InventoryContainer(this);

            this.data = data;

            foreach (Item i in items)
                inv.AddNonEquippable(i);
        }

        public override void Hit(int amount, int type)
        {
            health -= amount;

            if (health <= 0)
            {
                GamePlayLogicManager.GetInstance().RemoveCollidable(this);
                GamePlayDrawManager.GetInstance().Remove(this, GamePlayDrawManager.DRAW_LIST_LEVEL.ENTITY);

                inv.DropAll();
            }
            //DebugText.GetInstance().WriteLinePerm("Amount: " + amount + " type: " + type);

            GamePlayDrawManager.GetInstance().UI.AddElementA(new GameUIElement(GamePlayDrawManager.GetInstance().UI, "*THUNK*", position, 10));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(data.Sprite, boundingBox, Color.White);
        }
    }
}
