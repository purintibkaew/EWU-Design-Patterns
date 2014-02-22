using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

//simple attack - just spawn a rectangle of a fixed size in a given direction for a set duration

namespace FinalProject
{
    class SimpleAttack : AttackClose
    {
        public SimpleAttack(Rectangle boundingBox, MobileEntity attacker, float multiplier, int duration) : base(boundingBox, attacker, multiplier, duration)
        {
            
        }

        public override void Attack(Vector2 point, float direction, int amount)
        {
            Rectangle temporaryBoundingBox = new Rectangle();   //consider moving to AttackClose or just class level to avoid recalculation if exists
            if (direction == 0) //right
            {
                temporaryBoundingBox = boundingBox;
                temporaryBoundingBox.Location = new Point((int)point.X, (int)point.Y - boundingBox.Height / 2);   //locate halfway down to right of point
            }
            else if (direction == 90) //up
            {
                temporaryBoundingBox = new Rectangle(0, 0, boundingBox.Height, boundingBox.Width); //effectively rotate bounding box
                temporaryBoundingBox.Location = new Point((int)point.X - boundingBox.Width / 2, (int)point.Y - boundingBox.Height); //locate above point, centered
            }
            else if (direction == 180) //left
            {
                temporaryBoundingBox = boundingBox;
                temporaryBoundingBox.Location = new Point((int)point.X - boundingBox.Width, (int)point.Y - boundingBox.Height / 2);   //locate halfway down to left of point
            }
            else if (direction == 270) //down
            {
                temporaryBoundingBox = new Rectangle(0, 0, boundingBox.Height, boundingBox.Width); //effectively rotate bounding box
                temporaryBoundingBox.Location = new Point((int)point.X - boundingBox.Width / 2, (int)point.Y); //locate below point, centered
            }
            else
                throw new NotImplementedException();    //until we get proper angles in here (or if we don't) - probably won't get reached

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
