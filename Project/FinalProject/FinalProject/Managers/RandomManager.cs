using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    static class RandomManager
    {
        private static Random rand = new Random();

        public static Random GetRandom()
        {
            return rand;
        }
    }
}
