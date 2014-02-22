using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    class StabbingAttack : AttackClose
    {
        int maxLength, curLength;

        public StabbingAttack(Rectangle boundingBox, MobileEntity attacker, float multiplier, int duration, int maxLength) : base(boundingBox, attacker, multiplier, duration)
        {
            this.maxLength = maxLength;
        }

        public override void Attack(Vector2 point, float direction, int amount)
        {
            
        }

        public override void Hit(int amount, int type)
        {

        }
    }
}
