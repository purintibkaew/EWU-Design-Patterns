﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class DebugText
    {
        public SpriteFont Font { set; get; }
        public Vector2 Position { set; get; }
        public Color TextColor { set; get; }

        private static DebugText instance = new DebugText();
        private string text;
        private bool displayText;

        private DebugText() 
        {
            Position = new Vector2(0, 0);
            TextColor = Color.Black;
            text = "";
            displayText = true;
        }

        public static DebugText GetInstance()
        {
            return instance;
        }

        public void Write(string s)
        {
            text += s;
        }

        public void WriteLine(string s)
        {
            text += "\n" + s;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(displayText)
                spriteBatch.DrawString(Font, text, Position, TextColor);
        }

        public void ClearText() { text = ""; }

        public void StopDisplayingText() { this.displayText = false; }
        public void BeginDisplayingText() { this.displayText = true; }

    }
}