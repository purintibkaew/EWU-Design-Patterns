using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

// basic passive attack - similar to Legend of Zelda or similar, where running into an enemy hurts you.

namespace FinalProject
{
    class PassiveAttack : AttackClose
    {
        public PassiveAttack(Rectangle boundingBox, MobileEntity attacker, float multiplier, int duration) : base(boundingBox, attacker, multiplier, duration)
        {
            
        }

        public override void Attack(Vector2 point, float direction, int amount)
        {
            Rectangle temporaryBoundingBox = boundingBox;
            temporaryBoundingBox.Location = new Point((int)point.X - 1, (int)point.Y - 1); //set the location to one up and to the left of the given point (since the bounding box is going to be one larger than the entity)
           
            if (!isAttacking)  //if the entity isn't already attacking
            {
                isAttacking = true;
                timeLeft = duration;

                Collidable[] toTest = GamePlayLogicManager.GetInstance().CollisionTree.GetItems(temporaryBoundingBox);

                foreach (Collidable c in toTest)
                {
                    if (c != attacker)  //need to make sure we don't hit ourselves
                        c.Hit((int)(amount * multiplier), 0);
                }
            }

            timeLeft--; //decrement time left

            if (timeLeft == 0)  //if attack is over, set flag
                isAttacking = false;
            
        }

        public override void Hit(int amount, int type)
        {

        }
    }
}
