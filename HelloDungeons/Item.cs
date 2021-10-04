using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HelloDungeons
{
    class Item
    {
        private string _name;
        private float _statBoost;
        private string _description;
        private int _cost;
        private ItemType _type;


        public string Name
        {
            get { return _name; }
        }

        public float StatBoost
        {
            get { return _statBoost; }
        }

        public ItemType Type
        {
            get { return _type; }
        }

        public int Cost
        {
            get { return _cost; }
        }

        public string Description
        {
            get { return _description; }
        }

        public Item()
        {
            _name = null;
            _statBoost = 0;
            _description = null;
            _cost = 0;
            _type = ItemType.NONE;
        }

        public Item(string name, float statBoost, string description, int cost, ItemType type)
        {
            _name = name;
            _statBoost = statBoost;
            _description = description;
            _cost = cost;
            _type = type;
        }

        public float EquipmentCalculation(float baseStats)
        {
            if (Type != ItemType.NONE)
                baseStats += StatBoost;

            return baseStats;
        }

        /// <summary>
        /// Loads the Item's type
        /// </summary>
        /// <param name="itemType">The item being loaded</param>
        /// <returns>the item's Type</returns>
        public ItemType LoadItemType(string itemType)
        {
            if (itemType == "SWORD")
                return ItemType.SWORD;
            else if (itemType == "BOW")
                return ItemType.BOW;
            else if (itemType == "WAND")
                return ItemType.WAND;
            else if (itemType == "SHIELD")
                return ItemType.SHIELD;
            else if (itemType == "ARMOR")
                return ItemType.ARMOR;
            else if (itemType == "HELMET")
                return ItemType.HELMET;
            else if (itemType == "GLOVES")
                return ItemType.GLOVES;
            else if (itemType == "BOOTS")
                return ItemType.BOOTS;
            else if (itemType == "POTION")
                return ItemType.POTION;
            else
                return ItemType.NONE;
        }

        public Item LoadInventory (StreamReader reader)
        {
            Item items = new Item();
                items._name = reader.ReadLine();
                float.TryParse(reader.ReadLine(), out items._statBoost);
                string itemType = reader.ReadLine();
                items._type = LoadItemType(itemType);
                items._description = reader.ReadLine();

            return items;
        }

        public Item LoadEquipment(StreamReader reader)
        {
            Item equipment = new Item();
            equipment._name = reader.ReadLine();

            return equipment;
        }
    }
}
