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

        private AttackClose passive, active;

        private Random entRNG;

        public TestMonster(Texture2D sprite, Vector2 position, Stats entStats) : base(sprite, position, entStats)
        {
            actionTimer = 0;

            passive = new PassiveAttack(new Rectangle(0, 0, sprite.Bounds.Width + 2, sprite.Bounds.Height + 2), this, .5f, 15);

            entRNG = new Random();
        }

        public override void Logic()
        {
            if (passive.TimeLeft == 0)
            {
                passive.Attack(position, 0, entStats.Attack);
            }
            else
                passive.DecrementTimeLeft();

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
            //DebugText.GetInstance().WriteLinePerm("Gunter hit for " + amount + " damage,  " + curHealth + " remaining.");

            GamePlayDrawManager.GetInstance().UI.AddElementA(new GameUIElement(GamePlayDrawManager.GetInstance().UI, "*THUNK*", position, 10));
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
                lm.RemoveUpdatable(this);
            }
        }
    }
}
