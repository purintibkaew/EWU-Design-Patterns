using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    abstract class AttackClose : Collidable
    {
        protected Rectangle boundingBox;    //ASSUME BOX IS FOR ATTACK TO LEFT OR RIGHT
        protected MobileEntity attacker;

        protected float multiplier;

        protected int duration, timeLeft;

        public Rectangle BoundingBox
        {
            get
            {
                return this.boundingBox;
            }
        }

        protected bool isAttacking;

        public bool IsAttacking
        {
            get
            {
                return this.isAttacking;
            }
        }

        public int TimeLeft
        {
            get
            {
                if(isAttacking)
                    return this.timeLeft;
                return 0;
            }
        }

        public AttackClose(Rectangle boundingBox, MobileEntity attacker, float multiplier, int duration)
        {
            this.boundingBox = boundingBox;
            this.attacker = attacker;
            this.multiplier = multiplier;
            this.duration = duration;
            this.isAttacking = false;
            this.timeLeft = 0;  
        }

        public abstract void Attack(Vector2 point, float direction, int amount);  //point is the point the attack will happen from, direction is a the direction as an angle (probably just U/D/L/R - enum?)

        public abstract void Hit(int amount, int type); //probably won't do anything here - could play a sound effect or something on hitting a sword or whatever

        public void DecrementTimeLeft()
        {
            if (timeLeft > 0)
                timeLeft--;
            if (timeLeft == 0)
                isAttacking = false;
        }
    }
}
