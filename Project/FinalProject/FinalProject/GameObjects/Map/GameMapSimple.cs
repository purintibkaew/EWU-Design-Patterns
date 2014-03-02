using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FinalProject
{

    /*
     * 128 x 128
     * 
     * 
     * 
     */
    class GameMapSimple : GameMap
    {
        public GameMapSimple()
        {
            this.Height = 256;
            this.Width = 256;
        }

        public override void LoadContent()
        {
            MapEntityFactory factory = MapEntityFactory.GetInstance();
            
            AddToDrawList(factory.CreateDirtMapEntity(new Vector2(0, 0)));
            AddToDrawList(factory.CreateGrassMapEntity(new Vector2(0, 32)));
            AddToDrawList(factory.CreateGrassMapEntity(new Vector2(0, 64)));
            AddToDrawList(factory.CreateGrassMapEntity(new Vector2(0, 96)));

            AddToDrawList(factory.CreateGrassMapEntity(new Vector2(32, 0)));
            AddToDrawList(factory.CreateGrassMapEntity(new Vector2(32, 32)));
            AddToDrawList(factory.CreateGrassMapEntity(new Vector2(32, 64)));
            AddToDrawList(factory.CreateGrassMapEntity(new Vector2(32, 96)));

            AddToDrawList(factory.CreateGrassMapEntity(new Vector2(64, 0)));
            AddToDrawList(factory.CreateGrassMapEntity(new Vector2(64, 32)));
            AddToDrawList(factory.CreateGrassMapEntity(new Vector2(64, 64)));
            AddToDrawList(factory.CreateGrassMapEntity(new Vector2(64, 96)));

            CollidableMapEntity tree = factory.CreateTreeMapEntity(new Vector2(96, 0));
            AddToDrawList(tree);
            AddToCollideList(tree);
            tree = factory.CreateTreeMapEntity(new Vector2(96, 128));
            AddToDrawList(tree);
            AddToCollideList(tree);
        }
    }
}
