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
            if (parent != null && child != null)
            {
                base.parent = parent;
                base.child = base.parent.Child;
                base.child.Parent = this;
                base.parent.Child = this;
            }
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
                    return this.child.MaxHP + inner.MaxHP;
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
                    return this.child.Speed + inner.Speed;
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
                    return this.child.Attack + inner.Attack;
                }
                return base.attack;
            }
        }

        public void Insert(Stats parent, Stats child)
        {
            parent.Child = this;
            child.Parent = this;
            this.child = child;
            this.parent = parent;
        }

        public override void Remove()
        {
            if (this.parent != null && this.child != null)
            {
                this.parent.Child = this.child;
                this.child.Parent = this.parent;
            }
        }
    }
}
