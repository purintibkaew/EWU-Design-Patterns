using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class MapEntityData
    {
        private Texture2D spriteField;

        public Texture2D Sprite
        {
            set 
            {
                if (value != null)
                    spriteField = value;
                else
                    throw new ArgumentNullException("MapEntityData's sprite property was passed a null object. That is not allowed.");
            }
            get { return spriteField; }
        }
    }
}
