using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class HeadStat : Stats
    {
        private Stats inner;
        public HeadStat(Stats parent):base(0,0,0)
        {
            if(parent != null){
                base.parent = parent;
                base.child = base.parent.Child;
                base.child.Parent = this;
                base.parent.Child = this;
            }
        }

        public HeadStat(Stats parent, Stats inner)
            : base(0, 0, 0)
        {
            if (parent != null)
            {
                base.parent = parent;
                base.child = base.parent.Child;
                base.child.Parent = this;
                base.parent.Child = this;
                this.inner = inner;
            }
        }


        public override int Speed
        {
            get 
            { 
                return (this.inner != null) ? this.Inner.Speed : 0; 
            }
        }

        public override int Attack
        {
            get
            {
                return (this.inner != null) ? this.Inner.Attack : 0;
            }
        }

        public override int MaxHP
        {
            get
            {
                return (this.inner != null) ? this.Inner.MaxHP : 0;
            }
        }

        public override Stats Inner
        {
            get
            {
                return this.inner;
            }
            set
            {
                this.inner = null;
            }
        }


        public override void Remove()
        {
            this.parent.Child = this.child;
            this.child.Parent = this.parent;
        }
    }
}
