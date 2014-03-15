using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FinalProject
{
    public class MainMenu : Menu
    {
    	private int currentPlayer = 1;
        private ContentManager cm;

        public MainMenu(ContentManager cm) : base(cm)
        {
            this.cm = cm;
            makeButtons();
        }

        protected override bool doInput(MenuButton buttonSelected)
        {
            if(buttonSelected.Text.Equals("Start"))
            {
                return true;
            }
            else if (currentPlayer > 3)
            {
                return true;
            }
            else if (buttonSelected.Text == "BMO")
            {
                //Set current player to BMO
                this.currentPlayer++;
            }
            else if (buttonSelected.Text == "NEPTR")
            {
                //Set current player to BMO
                this.currentPlayer++;
            }
            return false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpriteFont sf = cm.Load<SpriteFont>("Fonts/ArialFont-Big");
            Vector2 textBox = sf.MeasureString("Player " + this.currentPlayer.ToString() + " - Choose Your Character");
            float textBoxPosX = (spriteBatch.GraphicsDevice.Viewport.Width / 2) - (textBox.X / 2);
            spriteBatch.DrawString(sf, "Player "+ this.currentPlayer.ToString() +" - Choose Your Character", new Vector2(textBoxPosX, 10), Color.Black);
            //DebugText.GetInstance().WriteLine("Screen Size: " + spriteBatch.GraphicsDevice.Viewport.Width.ToString() + " x " + spriteBatch.GraphicsDevice.Viewport.Height.ToString());
            base.Draw(spriteBatch);
        }

        //Setting up the buttons for this menu
        private void makeButtons()
        {
            int characterButtonWidth = 155;
            int characterButtonHeight = 223;
            int characterButtonTop = 107;
            //BMO Button
            base.Buttons.Add(new MenuButton(new Rectangle(30, characterButtonTop, characterButtonWidth, characterButtonHeight), cm.Load<Texture2D>("Buttons/ButtonBMO"), "BMO"));
            //NEPTR Button
            base.Buttons.Add(new MenuButton(new Rectangle(225, characterButtonTop, characterButtonWidth, characterButtonHeight), cm.Load<Texture2D>("Buttons/ButtonNEPTR"), "NEPTR"));
            //Another Button
            base.Buttons.Add(new MenuButton(new Rectangle(421, characterButtonTop, characterButtonWidth, characterButtonHeight), cm.Load<Texture2D>("Buttons/ButtonBMO"), "BMO"));
            //Another Button
            base.Buttons.Add(new MenuButton(new Rectangle(616, characterButtonTop, characterButtonWidth, characterButtonHeight), cm.Load<Texture2D>("Buttons/ButtonNEPTR"), "NEPTR"));
            //Start Button
            base.Buttons.Add(new MenuButton(new Rectangle(29, 385, 742, 73), cm.Load<Texture2D>("Buttons/ButtonStart"), "Start"));
        }
    }

}
