using System;
using System.Collections.Generic;
using System.Text;

namespace HelloDungeons
{
    class Player:Entity
    {
        private Item[] _items;
        private Item _currentItem;
        private int _currentItemIndex;
        private string _job;
        private int _arrowCount;
        private int _currentConsumable;


        public int ArrowCount
        {
            get { return _arrowCount; }
        }

        public void Buy(ref Entity player, Item item)
        {
            float _playerGold = player.Gold;
            //Removes the cost of the item from the player
            _playerGold -= item.Cost;

            //Tells the player that the puchase was successful
            Console.Clear();
            Console.WriteLine("You bought a " + item.Name + "!");
            Console.ReadKey(true);

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
            _currentItem.Name = "nothing";
            _job = job;
            _items = new Item[0];
        }

        public void InitializeArrows()
        {
            _arrowCount = 15;
        }
    }
}
