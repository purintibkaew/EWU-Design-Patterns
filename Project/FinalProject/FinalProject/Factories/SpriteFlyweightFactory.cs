using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public static class SpriteFlyweightFactory
    {
        private static ISpriteFlyweight spriteFlyweight;

        public static ISpriteFlyweight GetSpriteFlyweight()
        {
            if(spriteFlyweight == null)
                spriteFlyweight = new SpriteFlyweight();

            return spriteFlyweight;
        }
    }
}
