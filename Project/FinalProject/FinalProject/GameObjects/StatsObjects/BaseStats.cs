using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameObjects.StatsObjects
{
    abstract class BaseStats : Stats
    {
        protected Stats parent;

        public BaseStats(int maxHP, int attack, int speed)
            : base(maxHP, attack, speed)
        {
            //Do we need something here?
        }

        public override Stats Child
        {
            get
            {
                return null;
            }
        }

        public override int MaxHP
        {
            get { return base.maxHP; }
        }

        public override int Attack
        {
            get { return base.attack; }
        }

        public override int Speed
        {
            get { return base.speed; }
        }
    }
}
