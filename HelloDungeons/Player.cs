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
                    defensePower = base.DefensePower + CurrentShield.StatBoost;

                if (_currentArmor.Type == ItemType.ARMOR)
                    defensePower = base.DefensePower + CurrentArmor.StatBoost;

                if (_currentHelmet.Type == ItemType.HELMET)
                    defensePower = base.DefensePower + CurrentHelmet.StatBoost;

                if (_currentGloves.Type == ItemType.GLOVES)
                    defensePower = base.DefensePower + CurrentGloves.StatBoost;

                if (_currentBoots.Type == ItemType.BOOTS)
                    defensePower = base.DefensePower + CurrentBoots.StatBoost;

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

        public override float DamageInflicted(float damageAmount)
        {
            float damageTaken;
            if (_currentWeapon.Type == ItemType.NONE)
            {
                damageTaken = damageAmount - DefensePower;
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
        public void TryUnequip()
        {
                _currentItemIndex = -1;
                _currentItem = new Item();
                _currentItem.Name = "Nothing";
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

        public Player(string name, float health, float attackPower, float defensePower, float gold, Item[] items, string job) : base(name, health, attackPower, defensePower, gold)
        {
            _items = items;
            _currentWeapon.Name = "nothing";
            _currentArmor.Name = "nothing";
            _currentHelmet.Name = "nothing";
            _currentGloves.Name = "nothing";
            _currentBoots.Name = "nothing";
            _job = job;
            _items = new Item[0];
        }

        public void InitializeArrows()
        {
            _arrowCount = 15;
        }
    }
}
