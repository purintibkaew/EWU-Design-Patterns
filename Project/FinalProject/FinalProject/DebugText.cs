using Microsoft.Xna.Framework;
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
        public Vector2 PositionPerm { set; get; }
        public Color TextColor { set; get; }

        private static DebugText instance = new DebugText();
        private string text;
        private string permText;
        private Queue<string> permTextQueue;
        private static readonly int PERMTEXT_CAPACITY = 15;
        private bool displayText;

        public bool Active
        {
            get
            {
                return displayText;
            }
            set
            {
                displayText = value;
            }
        }

        private DebugText() 
        {
            Position = new Vector2(0, 0);
            PositionPerm = new Vector2(GraphicsDeviceManager.DefaultBackBufferWidth / 2, 0);
            TextColor = Color.Black;
            text = "";
            permText = "";
            permTextQueue = new Queue<string>(PERMTEXT_CAPACITY);
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
            text += s + "\n";
        }

        public void WritePerm(string s)
        {
            permTextQueue.Enqueue(s);
            UpdatePermText();
        }

        public void WriteLinePerm(string s)
        {
            permTextQueue.Enqueue(s + "\n");
            UpdatePermText();
        }

        public void UpdatePermText()
        {
            if(permTextQueue.Count > PERMTEXT_CAPACITY)
               permTextQueue.Dequeue();

            permText = ""; // clear permText then set it to the text inside of the queue

            foreach(string s in permTextQueue)
            {
                permText += s;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (displayText)
            {
                spriteBatch.DrawString(Font, text, Position, TextColor);
                spriteBatch.DrawString(Font, permText, PositionPerm, TextColor);
            }
            ClearText();    //Clear the text so we don't end up with text going off the bottom of the screen - since everything is updating every tick, this won't cause any information loss
        }

        public void ClearText() { text = ""; }

        public void StopDisplayingText() { this.displayText = false; }
        public void BeginDisplayingText() { this.displayText = true; }

    }
}
