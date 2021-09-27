using System;
using System.Collections.Generic;
using System.Text;

namespace HelloDungeons
{
    class Player:Entity
    {
        private Item[] _items;
        private Item _currentWeapon;
        private Item _currentShield;
        private Item _currentArmor;
        private Item _currentHelmet;
        private Item _currentGloves;
        private Item _currentBoots;
        private Item _currentItem;
        private int _currentItemIndex;
        private string _job;
        private int _arrowCount;
        private int _currentConsumable;


        /// <summary>
        /// Changes the Attack value if player is weilding weapon
        /// </summary>
        public override float AttackPower
        {
            get
            {
                float attackPower = base.AttackPower;

                if (_currentWeapon.Type == ItemType.SWORD || _currentWeapon.Type == ItemType.BOW || _currentWeapon.Type == ItemType.WAND)
                    attackPower = base.AttackPower + CurrentWeapon.StatBoost;

                return attackPower;
            }

        }

        /// <summary>
        /// Changes the Defense value if player is wearing armor
        /// </summary>
        public override float DefensePower
        {
            get
            {
                float defensePower = base.DefensePower;

                if (_currentShield.Type == ItemType.SHIELD)
                    defensePower += _currentShield.StatBoost;

                if (_currentArmor.Type == ItemType.ARMOR)
                    defensePower += _currentArmor.StatBoost;

                if (_currentHelmet.Type == ItemType.HELMET)
                    defensePower += _currentHelmet.StatBoost;

                if (_currentGloves.Type == ItemType.GLOVES)
                    defensePower += _currentGloves.StatBoost;

                if (_currentBoots.Type == ItemType.BOOTS)
                    defensePower += _currentBoots.StatBoost;

                return defensePower;
            }

        }

        public Item[] Items
        {
            get
            { return _items; }
        }

        public Item CurrentWeapon
        {
            get
            { return _currentWeapon; }
        }

        public Item CurrentShield
        {
            get
            { return _currentShield; }
        }

        public Item CurrentArmor
        {
            get
            { return _currentArmor; }
        }

        public Item CurrentHelmet
        {
            get
            { return _currentHelmet; }
        }
        public Item CurrentGloves
        {
            get
            { return _currentGloves; }
        }

        public Item CurrentBoots
        {
            get { return _currentBoots; }
        }

        public Item CurrentItem
        {
            get { return _currentItem; }
        }
        public int ArrowCount
        {
            get { return _arrowCount; }
        }

        public override float DamageInflicted(float damageAmount, float Defender)
        {
            Random rnd = new Random();
            int rngDamage = new Random().Next(-5, 5);
            float damageTaken = 0;
            if (_currentWeapon.Type == ItemType.NONE)
            {
                damageTaken = damageAmount - Defender + rngDamage;
            }
            else if (_currentWeapon.Type == ItemType.SWORD)
            {
                damageTaken = (damageAmount * 2) - (Defender * 2) + rngDamage;
            }
            else if (_currentWeapon.Type == ItemType.BOW)
            {
                damageTaken = damageAmount - Defender + rngDamage + rngDamage; 
            }
            else if (_currentWeapon.Type == ItemType.WAND)
            {
                damageTaken = (damageAmount + rngDamage) / 2;
            }

            if (damageTaken <= 0)
            {
                damageTaken = 1;
            }

            return damageTaken;
        }


        /// <summary>
        /// Sets the item given at the current index
        /// </summary>
        /// <param name="index">The index of the item in the array</param>
        /// <returns>False if outside the bounds of the array</returns>
        public bool TryEquipItem(int index)
        {
            //If the index is out of bounds
            if (index >= _items.Length || index < 0)
                return false;

            _currentItemIndex = index;

            //Sets the current item 
            _currentItem = _items[index];
            if (_currentItem.Type == ItemType.SWORD || _currentItem.Type == ItemType.BOW || _currentItem.Type == ItemType.WAND)
            {
                _currentWeapon = _currentItem;
            }
            else if (_currentItem.Type == ItemType.SHIELD && (_currentWeapon.Type != ItemType.BOW || _currentWeapon.Type != ItemType.WAND))
            {
                _currentShield = _currentItem;
            }
            else if (_currentItem.Type == ItemType.ARMOR)
            {
                _currentArmor = _currentItem;
            }
            else if (_currentItem.Type == ItemType.HELMET)
            {
                _currentHelmet = _currentItem;
            }
            else if (_currentItem.Type == ItemType.GLOVES)
            {
                _currentGloves = _currentItem;
            }
            else if (_currentItem.Type == ItemType.BOOTS)
            {
                _currentBoots = _currentItem;
            }
            return true;
        }

        /// <summary>
        /// Unequips the current item
        /// </summary>
        /// <returns>false if there is no item</returns>
        public void TryUnequip(int item)
        {
            if(_items[item].Type == ItemType.SWORD || _items[item].Type == ItemType.BOW || _items[item].Type == ItemType.WAND)
            {
                _currentWeapon = new Item();
                _currentWeapon.Name = "Nothing";
            }
            else if (_items[item].Type == ItemType.SHIELD)
            {
                _currentShield = new Item();
                _currentShield.Name = "Nothing";
            }
            else if (_items[item].Type == ItemType.HELMET)
            {
                _currentHelmet = new Item();
                _currentHelmet.Name = "Nothing";
            }
            else if (_items[item].Type == ItemType.BOOTS)
            {
                _currentBoots = new Item();
                _currentBoots.Name = "Nothing";
            }
            else if (_items[item].Type == ItemType.GLOVES)
            {
                _currentGloves = new Item();
                _currentGloves.Name = "Nothing";
            }
        }

        public void ItemBought(Item item)
        {
            //Creates a new array to add the Item
            Item[] TempArray = new Item[_items.Length + 1];

            //Copies the old inventory into the array
            for (int i = 0; i < _items.Length; i++)
            {
                TempArray[i] = _items[i];
            }

            //Add the bought item
            TempArray[TempArray.Length - 1] = item;

            //Edits the old values to the new values
            _items = TempArray;
        }

        /// <summary>
        /// Creates an array that contains the names of the inventory
        /// </summary>
        /// <returns></returns>
        public string[] GetItemNames()
        {
            //Creates a new array
            string[] itemNames = new string[_items.Length];

            //Copies the items name into the new array
            for (int i = 0; i < _items.Length; i++)
            {
                itemNames[i] = _items[i].Name;
            }

            //returns the new array
            return itemNames;
        }

        public Player(string name, float maxHealth, float health, float attackPower, float defensePower, float gold, Item[] items, string job) : base(name, maxHealth, health, attackPower, defensePower, gold)
        {
            _items = items;
            _job = job;
        }

        public void InitializeArrows()
        {
            _arrowCount = 15;
        }
    }
}
