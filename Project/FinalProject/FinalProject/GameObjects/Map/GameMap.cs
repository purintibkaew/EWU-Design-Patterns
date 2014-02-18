using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    abstract class GameMap
    {
        public int BASE_SIZE { get { return 128; } }

        public int Height 
        {
            get
            {
                return height;
            }

            set
            {
                if(value >= BASE_SIZE && value % BASE_SIZE == 0)
                    height = value;
                else
                    height = (value / BASE_SIZE) * BASE_SIZE; // set it to the lowest nearest divisible of BASE_SIZE
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                if (value >= BASE_SIZE && value % BASE_SIZE == 0)
                    width = value;
                else
                    width = (value / BASE_SIZE) * BASE_SIZE; // set it to the lowest nearest divisible of BASE_SIZE
            }
        }


        private int height;
        private int width;


        public abstract void LoadContent();
    }
}
