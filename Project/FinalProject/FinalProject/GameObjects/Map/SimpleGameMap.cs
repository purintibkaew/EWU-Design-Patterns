using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FinalProject.GameObjects.Map
{

    /*
     * 128 x 128
     * 
     * 
     * 
     */
    class SimpleGameMap : GameMap
    {
        private ArrayList mapEntities;

        public SimpleGameMap()
        {
            this.Height = 128;
            this.Width = 128;
            this.mapEntities = new ArrayList();
        }

        public override void LoadContent()
        {
            MapEntityFactory factory = MapEntityFactory.GetInstance();

            AddToEntityList(factory.CreateDirtMapEntity(new Vector2(0, 0)));
            AddToEntityList(factory.CreateGrassMapEntity(new Vector2(0, 32)));
            AddToEntityList(factory.CreateGrassMapEntity(new Vector2(0, 64)));
            AddToEntityList(factory.CreateGrassMapEntity(new Vector2(0, 96)));
            AddToEntityList(factory.CreateGrassMapEntity(new Vector2(32, 0)));
            AddToEntityList(factory.CreateGrassMapEntity(new Vector2(32, 32)));
            AddToEntityList(factory.CreateGrassMapEntity(new Vector2(32, 64)));
            AddToEntityList(factory.CreateGrassMapEntity(new Vector2(32, 96)));
            AddToEntityList(factory.CreateGrassMapEntity(new Vector2(64, 0)));
            AddToEntityList(factory.CreateGrassMapEntity(new Vector2(64, 32)));
            AddToEntityList(factory.CreateGrassMapEntity(new Vector2(64, 64)));
            AddToEntityList(factory.CreateGrassMapEntity(new Vector2(64, 96)));
            AddToEntityList(factory.CreateTreeMapEntity(new Vector2(96, 0)));
            AddToEntityList(factory.CreateTreeMapEntity(new Vector2(96, 128)));
        }

        public override void ClearContent()
        {
            foreach(Drawable d in this.mapEntities)
            {
                if (d == null)
                    continue;

                GamePlayDrawManager.GetInstance().Remove(d);
            }

            ClearEntityList();
        }

        private void AddToEntityList(Drawable d)
        {
            this.mapEntities.Add(d);
            GamePlayDrawManager.GetInstance().Add(d);
        }

        private void ClearEntityList()
        {
            this.mapEntities.Clear();
        }
    }
}
