using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    class TestMonster : MobileEntity
    {
        private int actionTimer;

        private Random entRNG;

        public TestMonster(Texture2D sprite, Vector2 position, Stats entStats) : base(sprite, position, entStats)
        {
            actionTimer = 0;

            entRNG = new Random();
        }

        public override void Logic()
        {
            if (actionTimer == 0)
            {
                int action = entRNG.Next(0, 7);

                if (action == 0) //move up
                    velocity = new Vector2(0, -entStats.Speed);
                else if (action == 1) //move down
                    velocity = new Vector2(0, entStats.Speed);
                else if (action == 2) //move left
                    velocity = new Vector2(-entStats.Speed, 0);
                else if (action == 3) //move right
                    velocity = new Vector2(entStats.Speed, 0);
                else    //wait
                    velocity = new Vector2();
            }
            
            actionTimer++;

            if (actionTimer == 30)
                actionTimer = 0;

            HandleCollisions();
        }

        public override void Hit(int amount, int type)
        {
            curHealth -= amount;
            DebugText.GetInstance().WriteLinePerm("Gunter hit for " + amount + " damage,  " + curHealth + " remaining.");

            
        }

        public override void CheckStatus()
        {
            if (curHealth <= 0)
            {

                GamePlayDrawManager dm = GamePlayDrawManager.GetInstance();
                GamePlayLogicManager lm = GamePlayLogicManager.GetInstance();

                dm.Remove(this, GamePlayDrawManager.DRAW_LIST_LEVEL.ENTITY);
                lm.Remove(this);
                lm.RemoveCollidable(this);
                lm.RemoveSEntity(this);
            }
        }
    }
}
