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
        protected int maxHP;
        protected int attack;
        protected int speed;

        public Stats Parent 
        { 
            get; 
            set;  
        }
        public abstract Stats Child { get; set; }
        public abstract int MaxHP { get; }
        public abstract int Attack { get; }
        public abstract int Speed { get; }

        protected Stats(int maxHP, int attack, int speed)
        {
            this.maxHP = maxHP;
            this.attack = attack;
            this.speed = speed;
        }
    }
}
