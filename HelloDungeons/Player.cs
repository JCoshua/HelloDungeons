using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HelloDungeons
{
    class Player:Entity
    {
        private Item[] _items;
        private Item[] _equipment = new Item[6];
        private Item _currentItem;

        /// <summary>
        /// Base Player constructor
        /// </summary>
        public Player(string name, float maxHealth, float health, float attackPower, float defensePower, float gold, Item[] items) : base(name, maxHealth, health, attackPower, defensePower, gold)
        {
            for (int i = 0; i < _equipment.Length; i++)
                _equipment[i] = new Item();
            _items = items;
        }

        /// <summary>
        /// The Player's inventory consructor
        /// </summary>
        public Player(Item[] items) : base()
        {
            _items = items;
        }

        /// <summary>
        /// The Player's invetory constuctor
        /// </summary>
        public Player() : base()
        {
            _items = new Item[0];
        }

        /// <summary>
        /// Changes the Attack value if player is weilding weapon
        /// </summary>
        public override float AttackPower
        {
            get
            {
                float attackPower = base.AttackPower;

                attackPower = _equipment[0].EquipmentCalculation(attackPower);

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

                for (int i = 1; i < _equipment.Length; i++)
                    defensePower = _equipment[i].EquipmentCalculation(defensePower);

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

        /// <summary>
        /// Changes Damage based on weapon type
        /// </summary>
        public override float DamageInflicted(float damageAmount, float Defender)
        {
            int rngDamage = new Random().Next(-5, 5);
            float damageTaken = 0;
            if (_equipment[0].Type == ItemType.NONE)
            {
                damageTaken = (damageAmount + rngDamage) - Defender ;
            }
            else if (_equipment[0].Type == ItemType.SWORD)
            {
                damageTaken = ((damageAmount * 2) + rngDamage) - (Defender * 2) ;
            }
            else if (_equipment[0].Type == ItemType.BOW)
            {
                damageTaken = (damageAmount + rngDamage + rngDamage) - Defender ; 
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

        /// <summary>
        /// Gives the player the item they bought and puts it in their inventory
        /// </summary>
        /// <param name="item">Item Bought</param>
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

        /// <summary>
        /// Uses an item
        /// </summary>
        /// <param name="item"></param>
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
        /// <returns>False if outside the bounds of the array or if item is already equipped</returns>
        public bool TryEquipItem(int index)
        {
            //If the index is out of bounds
            if (index >= _items.Length || index < 0)
                return false;

            //Sets the current item 
            _currentItem = _items[index];
            if (_currentItem.Type == ItemType.SWORD || _currentItem.Type == ItemType.BOW || _currentItem.Type == ItemType.WAND)
            {
                if (_equipment[0].Name != null && _equipment[0].Name != "")
                    return false;
                else
                    _equipment[0] = _currentItem;
            }
            else if (_currentItem.Type == ItemType.SHIELD && (_equipment[0].Type != ItemType.BOW || _equipment[0].Type != ItemType.WAND))
            {
                if (_equipment[1].Name != null && _equipment[1].Name != "")
                    return false;
                else
                    _equipment[1] = _currentItem;
            }
            else if (_currentItem.Type == ItemType.ARMOR)
            {
                if (_equipment[2].Name != null && _equipment[2].Name != "")
                    return false;
                else
                    _equipment[2] = _currentItem;
            }
            else if (_currentItem.Type == ItemType.HELMET)
            {
                if (_equipment[3].Name != null && _equipment[3].Name != "")
                    return false;
                else
                    _equipment[3] = _currentItem;
            }
            else if (_currentItem.Type == ItemType.GLOVES)
            {
                if (_equipment[4].Name != null && _equipment[4].Name != "")
                    return false;
                else
                    _equipment[4] = _currentItem;
            }
            else if (_currentItem.Type == ItemType.BOOTS)
            {
                if (_equipment[5].Name != null && _equipment[5].Name != "")
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
            }
            else if (_items[item].Type == ItemType.SHIELD)
            {
                _equipment[1] = new Item();
            }
            else if (_items[item].Type == ItemType.ARMOR)
            {
                _equipment[2] = new Item();
            }
            else if (_items[item].Type == ItemType.HELMET)
            {
                _equipment[3] = new Item();
            }
            else if (_items[item].Type == ItemType.GLOVES)
            {
                _equipment[4] = new Item();
            }
            else if (_items[item].Type == ItemType.BOOTS)
            {
                _equipment[5] = new Item();
            }
            Console.WriteLine("You unequipped " + _items[item].Name);
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
        public override void Save(StreamWriter writer)
        {
            //Saves Entity Class Stats
            base.Save(writer);

            //Saves Inventory and Lenght
            writer.WriteLine(_items.Length);
            for (int i = 0; i < _items.Length; i++)
            {
                writer.WriteLine(_items[i].Name);
                writer.WriteLine(_items[i].StatBoost);
                writer.WriteLine(_items[i].Type);
                writer.WriteLine(_items[i].Description);
            }

            //Saves the Equipped Items
            for (int i = 0; i < _equipment.Length; i++)
            {
                writer.WriteLine(_equipment[i].Name);
            }
                
        }

        public override bool Load(StreamReader reader)
        {
            // If the base Load Function fails
            if (!base.Load(reader))
                return false;

            if (!int.TryParse(reader.ReadLine(), out int items))
                return false;

            _items = new Item[items];
            for (int i = 0; i < items; i++)
            {
                _items[i] = new Item();
                if (_items[i].LoadInventory(reader).Name == "")
                    return false;
            }


            for (int i = 0; i < _equipment.Length; i++)
            {
                _equipment[i] = new Item();
                _equipment[i].LoadEquipment(reader);
            }

            for (int i = 0; i < _items.Length; i++)
            {
                if (_equipment[0].Name == _items[i].Name)
                    _equipment[0] = _items[i];
                else if (_equipment[1].Name == _items[i].Name)
                    _equipment[1] = _items[i];
                else if (_equipment[2].Name == _items[i].Name)
                    _equipment[2] = _items[i];
                else if (_equipment[3].Name == _items[i].Name)
                    _equipment[3] = _items[i];
                else if (_equipment[4].Name == _items[i].Name)
                    _equipment[4] = _items[i];
                else if (_equipment[5].Name == _items[i].Name)
                    _equipment[5] = _items[i];
            }
            return true;
        }
    }
}
