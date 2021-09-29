using System;
using System.Collections.Generic;
using System.Text;

namespace HelloDungeons
{
    class Player:Entity
    {
        private Item[] _items;
        private Item[] _equipment = new Item[6];
        private Item _currentItem;
        private int _currentItemIndex;
        private string _job;

        public Player(string name, float maxHealth, float health, float attackPower, float defensePower, float gold, Item[] items, string job) : base(name, maxHealth, health, attackPower, defensePower, gold)
        {
            _items = items;
            _job = job;
        }

        /// <summary>
        /// Changes the Attack value if player is weilding weapon
        /// </summary>
        public override float AttackPower
        {
            get
            {
                float attackPower = base.AttackPower;

                if (_equipment[0].Type == ItemType.SWORD || _equipment[0].Type == ItemType.BOW || _equipment[0].Type == ItemType.WAND)
                    attackPower = base.AttackPower + _equipment[0].StatBoost;

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

                if (_equipment[1].Type == ItemType.SHIELD)
                    defensePower += _equipment[1].StatBoost;

                if (_equipment[2].Type == ItemType.ARMOR)
                    defensePower += _equipment[2].StatBoost;

                if (_equipment[3].Type == ItemType.HELMET)
                    defensePower += _equipment[3].StatBoost;

                if (_equipment[4].Type == ItemType.GLOVES)
                    defensePower += _equipment[4].StatBoost;

                if (_equipment[5].Type == ItemType.BOOTS)
                    defensePower += _equipment[5].StatBoost;

                return defensePower;
            }

        }

        public Item[] Items
        {
            get
            { return _items; }
        }

        public Item CurrentItem
        {
            get { return _currentItem; }
        }

        public override float DamageInflicted(float damageAmount, float Defender)
        {
            Random rnd = new Random();
            int rngDamage = new Random().Next(-5, 5);
            float damageTaken = 0;
            if (_equipment[0].Type == ItemType.NONE)
            {
                damageTaken = damageAmount - Defender + rngDamage;
            }
            else if (_equipment[0].Type == ItemType.SWORD)
            {
                damageTaken = (damageAmount * 2) - (Defender * 2) + rngDamage;
            }
            else if (_equipment[0].Type == ItemType.BOW)
            {
                damageTaken = damageAmount - Defender + rngDamage + rngDamage; 
            }
            else if (_equipment[0].Type == ItemType.WAND)
            {
                damageTaken = (damageAmount + rngDamage) / 2;
            }

            if (damageTaken <= 0)
            {
                damageTaken = 1;
            }

            return damageTaken;
        }

        //Gives the player the item they bought and puts it in their inventory
        public void Buy(Item item)
        {
            Pay(item);
            //Creates a new array to add the Item
            Item[] TempArray = new Item[_items.Length + 1];

            //Copies the old inventory into the array
            for (int i = 0; i < _items.Length; i++)
            {
                TempArray[i] = _items[i];
            }

            //Add the bought item
            TempArray[TempArray.Length - 1] = item;

            //Makes the inventory becomes the new array
            _items = TempArray;
        }

        //Gives the player the item they bought and puts it in their inventory
        public void UseConsumable(Item item)
        {
            //Creates a new array to remove the Item
            Item[] TempArray = new Item[_items.Length - 1];

            //Copies the old inventory into the array
            for (int i = 0; i <= TempArray.Length; i++)
            {
                if(_items[i].Name != item.Name)
                    TempArray[i] = _items[i];
            }

            //Makes the inventory becomes the new array
            _items = TempArray;
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
                if (_equipment[0].Name != null)
                    return false;
                else
                    _equipment[0] = _currentItem;
            }
            else if (_currentItem.Type == ItemType.SHIELD && (_equipment[0].Type != ItemType.BOW || _equipment[0].Type != ItemType.WAND))
            {
                if (_equipment[1].Name != null)
                    return false;
                else
                    _equipment[1] = _currentItem;
            }
            else if (_currentItem.Type == ItemType.ARMOR)
            {
                if (_equipment[2].Name != null)
                    return false;
                else
                    _equipment[2] = _currentItem;
            }
            else if (_currentItem.Type == ItemType.HELMET)
            {
                if (_equipment[3].Name != null)
                    return false;
                else
                    _equipment[3] = _currentItem;
            }
            else if (_currentItem.Type == ItemType.GLOVES)
            {
                if (_equipment[4].Name != null)
                    return false;
                else
                    _equipment[4] = _currentItem;
            }
            else if (_currentItem.Type == ItemType.BOOTS)
            {
                if (_equipment[5].Name != null)
                    return false;
                else
                    _equipment[5] = _currentItem;
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
                _equipment[0] = new Item();
                _equipment[0].Name = null;
            }
            else if (_items[item].Type == ItemType.SHIELD)
            {
                _equipment[1] = new Item();
                _equipment[1].Name = null;
            }
            else if (_items[item].Type == ItemType.ARMOR)
            {
                _equipment[2] = new Item();
                _equipment[2].Name = null;
            }
            else if (_items[item].Type == ItemType.HELMET)
            {
                _equipment[3] = new Item();
                _equipment[3].Name = null;
            }
            else if (_items[item].Type == ItemType.GLOVES)
            {
                _equipment[4] = new Item();
                _equipment[4].Name = null;
            }
            else if (_items[item].Type == ItemType.BOOTS)
            {
                _equipment[5] = new Item();
                _equipment[5].Name = null;
            }
            Console.WriteLine("You unequipped " + _items[item].Name);
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

        
    }
}
