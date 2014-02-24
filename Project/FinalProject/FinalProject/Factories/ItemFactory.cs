using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class ItemFactory
    {
        public enum itemType { Sword=0, Slingshot, PhysicsTextbook, Pie, RunningShoes};
        public enum effectType { Fire=0, Super, Curesd, ExtraHP };
        private static ItemFactory instance = new ItemFactory();


        private ItemFactory() { }

        internal Item Item
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        internal Stats GeneratedStats
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public ItemFactory getInstatnce()
        {
            return instance;
        }

        //Generate a completely random item
        public Item generateItem()
        {
            Random rand = new Random();
            itemType type = (itemType) rand.Next(5);

            Item loot = getBaseItem(type);

            int numOfModifiers = rand.Next(3);
            for (int i = 0; i < numOfModifiers; i++)
            {
                addStatModifier(loot, (effectType)rand.Next(4));
            }
            loot.Stats = new headStat(null, loot.Stats);

            return loot;
        }

        public Item generateItem(itemType type)
        {
            Item loot = getBaseItem(type);

            Random rand = new Random();
            int numOfModifiers = rand.Next(3);
            for (int i = 0; i < numOfModifiers; i++)
            {
                addStatModifier(loot, (effectType)rand.Next(4));
            }
            loot.Stats = new headStat(null, loot.Stats);

            return loot;
        }

        public Item generateItem(itemType type, params effectType[] effects)
        {
            Item loot = getBaseItem(type);

            foreach (effectType effect in effects)
            {
                addStatModifier(loot, effect);
            }

            return loot;
        }


        private Item getBaseItem(itemType type){
            BaseStats basestat;
            string name;
            switch (type)
            {
                case itemType.Sword:
                    name = "Sword";
                    basestat = new BaseStats(0, 4, 2);
                    break;
                case itemType.Slingshot:
                    name = "Slingshot";
                    basestat = new BaseStats(0, 2, 1);
                    break;
                case itemType.PhysicsTextbook:
                    name = "Physics Textbook";
                    basestat = new BaseStats(0, 5, 0);
                    break;
                case itemType.Pie:
                    name = "Pie";
                    basestat = new BaseStats(0, 2, 0);
                    break;
                default:
                    name = "Running Shoes";
                    basestat = new BaseStats(0, 0, 5);
                    break;
            }
            GamePlayContentManager cm = GamePlayContentManager.GetInstance();
            return new Item(null, name, basestat);
        }

        private void addStatModifier(Item item, effectType effect)
        {
            if (item == null)
            {
                throw new Exception("Cannot work with null item!");
            }
            if (item.Stats == null)
            {
                throw new Exception("This item does not have a base stat!");
            }


            switch (effect)
            {
                case effectType.Fire:
                    item.Name = "Fire " + item.Name;
                    item.Stats = new StatModifier(0, 3, 0, null, item.Stats);
                    break;
                case effectType.Super:
                    item.Name = "Super " + item.Name;
                    item.Stats = new StatModifier(3, 5, 3, null, item.Stats);
                    break;
                case effectType.Curesd:
                    item.Name = "Cursed " + item.Name;
                    item.Stats = new StatModifier(-1, -3, -2, null, item.Stats);
                    break;
                default:
                    item.Name = "Extra HP " + item.Name;
                    item.Stats = new StatModifier(5, 0, 0, null, item.Stats);
                    break;
            }
        }
    }
}
