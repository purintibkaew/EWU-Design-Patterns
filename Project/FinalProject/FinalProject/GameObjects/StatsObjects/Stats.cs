using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * MaxHP
 * Attack
 * Speed
 * 
 * BaseStats Ab - parent only
 * Stat Modifier C - parent and child
 */
namespace FinalProject
{
    abstract class Stats
    {
        protected Stats parent;
        protected Stats child;

        protected int maxHP;
        protected int attack;
        protected int speed;

        public Stats Parent 
        { 
            get; 
            set;  
        }
        public Stats Child 
        { 
            get; 
            set; 
        }

        public abstract Stats Inner { get; set; }
        public abstract int MaxHP { get; }
        public abstract int Attack { get; }
        public abstract int Speed { get; }

        public abstract void Remove();

        protected Stats(int maxHP, int attack, int speed)
        {
            this.maxHP = maxHP;
            this.attack = attack;
            this.speed = speed;
        }
    }
}
