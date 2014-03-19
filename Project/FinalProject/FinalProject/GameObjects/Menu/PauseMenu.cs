using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    public class PauseMenu : Menu
    {
        private ContentManager cm;

        public PauseMenu(ContentManager cm) : base(cm)
        {
            this.cm = cm;
            Texture2D resumeSprite = cm.Load<Texture2D>("Buttons/ButtonResume");
            Rectangle resumeBounds = new Rectangle(29, 285, 742, 73);
            base.Buttons.Add(new MenuButton(resumeBounds, resumeSprite, "Resume"));
        }

        protected override bool doInput(MenuButton buttonSelected)
        {
            if (buttonSelected.Text == "Resume")
            {
                return true;
            }
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            SpriteFont sf = cm.Load<SpriteFont>("Fonts/ArialFont-Big");
            string text = "Game Paused";
            Vector2 textBox = sf.MeasureString(text);
            float textBoxPosX = (spriteBatch.GraphicsDevice.Viewport.Width / 2) - (textBox.X / 2);
            spriteBatch.DrawString(sf, text, new Vector2(textBoxPosX, 10), Color.Black);


            base.Draw(spriteBatch);
        }
    }
}
