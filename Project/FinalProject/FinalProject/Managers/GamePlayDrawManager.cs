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
        private static readonly int SCREEN_OFFSET = 50;

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

        public enum DRAW_LIST_LEVEL { MAP_BACKGROUND = 0, MAP_TOPOFBACKGROUND = 1, MAP_FOREGROUND = 2, ENTITY = 3, PROJECTILE = 4 };

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
            drawLists = new List<Drawable>[5];

            for (int i = 0; i < drawLists.Length; i++) //need to instantiate every list in the array.
            {
                drawLists[i] = new List<Drawable>();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerManager pm = PlayerManager.GetInstance();

            //update camera position to the average of the positions of all players
            camera.Position = pm.GetPlayerAveragePosition();

            //update the camera zoom if players are a certain distance apart
            Rectangle playerRectangle = pm.GetPlayerDistanceRectangle();

            double zoomThreshhold = 0.75;
            float zoomAmount = 1.0f;

            if (playerRectangle.Width > gd.Viewport.Width * zoomThreshhold)
            {
                float tempZoom = (float)(playerRectangle.Width / (gd.Viewport.Width * zoomThreshhold));

                if (tempZoom > zoomAmount)
                    zoomAmount = tempZoom;
            }
            if (playerRectangle.Height > gd.Viewport.Height * zoomThreshhold)
            {
                float tempZoom = (float)(playerRectangle.Height / (gd.Viewport.Height * zoomThreshhold));

                if (tempZoom > zoomAmount)
                    zoomAmount = tempZoom;
            }

            camera.Zoom = 1 / zoomAmount;
            

            //moved spritebatch.begin to here to support camera
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera.GetCameraTransform(this.gd));

            int screenWidthDivideBy2 = (GraphicsDeviceManager.DefaultBackBufferWidth + 1 + SCREEN_OFFSET) / 2;
            int screenHeightDivideBy2 = (GraphicsDeviceManager.DefaultBackBufferHeight + 1 + SCREEN_OFFSET) / 2;

            Rectangle screenRect = new Rectangle((int)(camera.Position.X - screenWidthDivideBy2),
                                                 (int)(camera.Position.Y - screenHeightDivideBy2),
                                                 GraphicsDeviceManager.DefaultBackBufferWidth + SCREEN_OFFSET,
                                                 GraphicsDeviceManager.DefaultBackBufferHeight + SCREEN_OFFSET);

            List<List<Drawable>> list = GamePlayMapManager.GetInstance().GetDrawablesInArea(screenRect.X, screenRect.Y, screenRect.Width, screenRect.Height);

            //DebugText.GetInstance().WriteLine("Need to draw: " + list.LongCount());

            foreach (List<Drawable> l in list)
            {
                foreach (Drawable d in l)
                    d.Draw(spriteBatch);
            }


            foreach (Drawable d in drawLists[(int)DRAW_LIST_LEVEL.ENTITY])
                d.Draw(spriteBatch);


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
            if (drawLists[(int)level].Contains(d))
                drawLists[(int)level].Remove(d);
        }
    }
}
