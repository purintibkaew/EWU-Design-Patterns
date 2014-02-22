using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    class SwingingAttack : AttackClose
    {
        private float curAngleMax, curAngleMin, curAngle, maxAngle;

        public SwingingAttack(Rectangle boundingBox, MobileEntity attacker, float multiplier, int duration, float maxAngle) : base(boundingBox, attacker, multiplier, duration)
        {
            this.maxAngle = maxAngle;
        }

        public override void Attack(Vector2 point, float direction, int amount)
        {
            
        }

        public override void Hit(int amount, int type)
        {

        }
    }
}
