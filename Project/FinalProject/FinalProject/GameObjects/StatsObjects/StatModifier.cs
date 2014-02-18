using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class StatModifier : Stats
    {
        private Stats parent;
        private Stats child;

        public StatModifier(int maxHP, int attack, int speed, Stats child) 
            : base(maxHP, attack, speed)
        {
            this.parent = null;
            this.child = child;
        }


        public override Stats Child
        {
            get;
            set;
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

        public void Remove()
        {
            this.parent.Child = this.child;
            this.child.Parent = this.parent;
        }
    }
}
