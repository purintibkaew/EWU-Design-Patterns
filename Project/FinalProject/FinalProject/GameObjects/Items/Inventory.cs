using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    interface Inventory
    {
        void Pickup(Item i);
        void Drop(Item i);

        void DropAll();

        void AddWeapon(Weapon w);
        void AddNonEquippable(Item i);

        void DropWeapon(Weapon w);
        void DropNonEquippable(Item i);
    }
}
