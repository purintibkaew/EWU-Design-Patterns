using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

/*
 * The idea here is to get around the double dispatch problem. Each item basically gives itself to the inventory, which can then work with specifics. This way, we can pick up an item from the ground and, if it's
 * a weapon, put it in a weapon slot, rather than in the regular inventory. A bit kludgy at the moment.
 */

namespace FinalProject
{
    class InventoryHumanoid : Inventory
    {
        private Weapon heldWeapon;

        private MobileEntity ent;

        private List<Item> bag;

        public InventoryHumanoid(MobileEntity ent)
        {
            heldWeapon = null;
            bag = new List<Item>();

            this.ent = ent; //super hacky, consider creating interface or something for ents that can have inventory (or just position, really) and moving up to Inventory
        }

        public void Pickup(Item i)
        {
            i.AddToInventory(this);
        }

        public void Drop(Item i)
        {
            i.DropFromInventory(this);
        }

        public void AddWeapon(Weapon w)
        {
            if (heldWeapon != null)
            {
                Drop(w);
            }

            heldWeapon = w;
        }

        public void AddNonEquippable(Item i)
        {
            bag.Add(i);
        }

        public void DropWeapon(Weapon w)
        {
            if (heldWeapon == w)
            {
                heldWeapon = null;
                w.Drop(ent.Position);
            }
        }

        public void DropNonEquippable(Item i)
        {
            if (bag.Contains(i))
            {
                bag.Remove(i);
                i.Drop(ent.Position);
            }
        }
    }
}
