using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    public class NullSprite : Texture2D
    {
        private static readonly int HEIGHT = 32;
        private static readonly int WIDTH = 32;
        private static readonly Color COLOR = Color.Purple;


        public NullSprite() : base(new GraphicsDevice(), HEIGHT, WIDTH)
        {
            this.SetData<Color>(CreateColorData());
        }

        private Color[] CreateColorData()
        {
            Color[] colorData = new Color[height * width];

            for (int i = 0; i < colorData.Length; i++)
                colorData[i] = COLOR;

            return colorData;
        }
    }
}
