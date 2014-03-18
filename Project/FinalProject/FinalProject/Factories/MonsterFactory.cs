using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    class MonsterFactory
    {
        private static MonsterFactory instance = new MonsterFactory();

        public enum MONSTER_TYPE { Gunter }

        private MonsterFactory() { }

        public static MonsterFactory GetInstance() { return instance; }

        public MobileEntity CreateMonster(MONSTER_TYPE type, Vector2 position)
        {
            switch(type)
            {
                default:
                case MONSTER_TYPE.Gunter:
                    return GetGunter(position);
            }
        }

        private MobileEntity GetGunter(Vector2 position)
        {
            return new TestMonster(SpriteFlyweightFactory.GetSpriteFlyweight().GetSprite("Entities/Characters/Gunterstanding"), position, new BaseStats(30, 10, 5));
        }
    }
}
