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

        public abstract int MaxHP;
        public abstract int Attack;
        public abstract int Speed;

        protected Stats(int maxHP, int attack, int speed)
        {
            this.maxHP = maxHP;
            this.attack = attack;
            this.speed = speed;
        }
    }
}
