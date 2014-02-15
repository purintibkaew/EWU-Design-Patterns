using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class MapEntityData
    {
        public Texture2D sprite
        {
            set 
            {
                if (value != null)
                    sprite = value;
                else
                    throw new ArgumentNullException("MapEntityData's sprite property was passed a null object. That is not allowed.");
            }
            get { return sprite; }
        }
    }
}
