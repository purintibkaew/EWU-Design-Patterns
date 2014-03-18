using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class InventoryContainer : Inventory
    {
        private List<Item> bag;

        private CollidableMapEntity ent;

        public InventoryContainer(CollidableMapEntity ent)
        {
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

        }

        public void AddNonEquippable(Item i)
        {
            bag.Add(i);
        }

        public void DropWeapon(Weapon w)
        {
            DroppedItem d = w.Drop(ent.Position);
            d.AddToWorld();
        }   

        public void DropNonEquippable(Item i)
        {
            if (bag.Contains(i))
            {
                bag.Remove(i);
                DroppedItem d = i.Drop(ent.Position);
                d.AddToWorld();
            }
        }

        public void DropAll()
        {
            for (int i = 0; i < bag.Count; i++)
            {
                Drop(bag[i]);
            }
        }
    }
}
