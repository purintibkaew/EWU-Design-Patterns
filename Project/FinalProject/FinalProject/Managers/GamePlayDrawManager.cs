using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class GamePlayDrawManager
    {
        private static GamePlayDrawManager instance;

        public static GamePlayDrawManager GetInstance()
        {
            if (instance == null)
                instance = new GamePlayDrawManager();
            return instance;
        }

        private List<Drawable>[] drawLists;
        
        private Camera2D camera;
        private GraphicsDevice gd;

        public GraphicsDevice ManagerGraphicsDevice
        {
            set
            {
                gd = value;
            }
        }

        public enum DRAW_LIST_LEVEL { MAP_BACKGROUND = 0, MAP_FOREGROUND = 1, ENTITY = 2, PROJECTILE = 3 };

        private GamePlayDrawManager()
        {
            camera = new Camera2D();

            /*
             * Initially setting this to an array of four lists - can change later.
             * 0 - Background Map - the map sprites that aren't collidable
             * 1 - Foreground Map - the map sprites that are collidable (trees, maybe chests, depending on how we do it)
             * 2 - Entities - Players, monsters, etc
             * 3 - Projectiles, effects, maybe items, etc
             */
            drawLists = new List<Drawable> [4];

            for (int i = 0; i < drawLists.Length; i++ ) //need to instantiate every list in the array.
            {
                drawLists[i] = new List<Drawable>();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //update camera position to the average of the positions of all players
            camera.Position = GamePlayPlayerManager.GetInstance().GetPlayerAveragePosition();

            //moved spritebatch.begin to here to support camera
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera.GetCameraTransform(this.gd));

            //need to draw back to front here
            foreach (List<Drawable> l in drawLists)
            {
                foreach (Drawable d in l)
                {
                    d.Draw(spriteBatch);
                }
            }

            spriteBatch.End();
        }

        public void Add(Drawable d, DRAW_LIST_LEVEL level)
        {
            drawLists[(int)level].Add(d);
        }

        public void Add(Drawable d)
        {
            Add(d, DRAW_LIST_LEVEL.MAP_BACKGROUND);
        }

        public void Remove(Drawable d, DRAW_LIST_LEVEL level)
        {
            if(drawLists[(int)level].Contains(d))
                drawLists[(int)level].Remove(d);
        }
    }
}
