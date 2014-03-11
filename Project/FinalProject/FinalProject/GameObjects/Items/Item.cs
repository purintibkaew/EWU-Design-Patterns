using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    //For now, I'm doing a concrete class, we may want some abstraction later
    abstract class Item
    {
        private Stats stats;
        private Texture2D sprite; //Though, this class is never actually drawn, it passes its sprite to the DroppedItem container when it is dropped
        private string name;

        public Item(Texture2D sprite, string name, Stats stats)
        {
            this.sprite = sprite;
            this.stats = stats;
            this.name = name;
        }

        public string Name
        {
            get;
            set;
        }

        public Stats Stats
        {
            get
            {
                return this.stats;
            }
            set
            {
                this.stats = value;
            }
        }

        internal Stats ItemStats
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        
        public DroppedItem Drop(Vector2 position) //May need to add more functionality
        {
            this.stats.Remove();
            return new DroppedItem(this.sprite, position, this);
        }

        public abstract void AddToInventory(Inventory i);
        public abstract void DropFromInventory(Inventory i);
    }
}
