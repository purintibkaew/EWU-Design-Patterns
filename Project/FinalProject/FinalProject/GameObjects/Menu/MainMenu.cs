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
    	private int currentPlayer;

        public MainMenu(ContentManager cm) : base(cm)
        {
            base.Buttons.Add(new MenuButton(new Rectangle(10,10,30,30), cm.Load<Texture2D>("Entities.Characters/BMOStanding"), "Test Button"));
        }

        protected override bool doInput(MenuButton buttonSelected)
        {
            if(buttonSelected.Text.Equals("Test Button"))
            {
                return true;
            }
            return false;
        }
    }

}
