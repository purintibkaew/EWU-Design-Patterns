//This is just a test object (ostensibly a temporary one), set up, at the moment, to just sit there and do nothing but be run into by mobiles.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace FinalProject
{
    class CollidableTestObject : MobileEntity
    {
        public CollidableTestObject(Texture2D sprite, Vector2 position) : base(sprite, position)
        {
            
        }

        public override void Logic()
        {
            
        }

        public override void CheckStatus()
        {
            
        }

        public override void Hit(int amount, int type)
        {
            DebugText.GetInstance().WriteLinePerm("Entity was hit");
        }
    }
}
