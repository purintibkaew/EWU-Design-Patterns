using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class StatModifier : Stats
    {

        private Stats inner;

        public StatModifier(int maxHP, int attack, int speed, Stats parent, Stats inner) 
            : base(maxHP, attack, speed)
        {
            base.parent = parent;
            base.child = base.parent.Child;
            base.child.Parent = this;
            base.parent.Child = this;
            this.inner = inner;
        }

        public override Stats Inner
        {
            get
            {
                return this.inner;
            }
            set
            {
                this.inner = value;
            }
        }


        public override int MaxHP
        {
            get
            {
                if (child != null)
                {
                    return this.child.MaxHP + base.maxHP;
                }
                return base.maxHP;
            }
        }

        public override int Speed
        {
            get
            {
                if (child != null)
                {
                    return this.child.Speed + base.speed;
                }
                return base.speed;
            }
        }

        public override int Attack
        {
            get
            {
                if (child != null)
                {
                    return this.child.Attack + base.attack;
                }
                return base.attack;
            }
        }

        public override void Remove()
        {
            this.parent.Child = this.child;
            this.child.Parent = this.parent;
        }
    }
}
