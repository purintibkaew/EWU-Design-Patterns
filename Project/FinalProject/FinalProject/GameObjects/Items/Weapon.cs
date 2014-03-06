using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    class Weapon : Item
    {
        public Weapon(Texture2D sprite, string name, Stats weaponStats) : base(sprite, name, weaponStats)
        {

        }

        public override void AddToInventory(Inventory i)
        {
            i.AddWeapon(this);
        }

        public override void DropFromInventory(Inventory i)
        {
            i.DropWeapon(this);
        }
    }
}
