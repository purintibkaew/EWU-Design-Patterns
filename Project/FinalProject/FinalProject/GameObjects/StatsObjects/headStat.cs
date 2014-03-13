using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class HeadStat : Stats
    {
        private Stats inner;
        public HeadStat(Stats child):base(0,0,0)
        {
            if(child != null){
                base.child = child;
                base.parent = null;
                base.child.Parent = this;
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
