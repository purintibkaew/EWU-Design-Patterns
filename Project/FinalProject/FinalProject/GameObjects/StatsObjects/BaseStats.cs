using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class BaseStats : Stats
    {
        

        public BaseStats(int maxHP, int attack, int speed, Stats parent)
            : base(maxHP, attack, speed)
        {
            base.parent = parent;
            base.child = null;
        }

        public override Stats Inner
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
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

        public override void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
