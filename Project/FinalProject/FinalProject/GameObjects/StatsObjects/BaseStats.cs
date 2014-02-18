using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject.GameObjects.StatsObjects
{
    abstract class BaseStats : Stats
    {
        protected Stats parent;
        public abstract Stats Parent;

        public BaseStats(int maxHP, int attack, int speed)
            : base(maxHP, attack, speed)
        {
            //Do we need something here?
        }

        public int MaxHP
        {
            get { return base.maxHP; }
        }

        public int Attack
        {
            get { return base.attack; }
        }

        public int Speed
        {
            get { return base.speed; }
        }
    }
}
