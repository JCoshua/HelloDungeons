using System;
using System.Collections.Generic;
using System.Text;

namespace HelloDungeons
{
    class Shop
    {
        private int _gold;
        private Item[] _shopInventory;

        /// <summary>
        /// Shop constructor
        /// </summary>
        /// <param name="items"></param>
        public Shop(Item[] items)
        {
            _gold = 100;
            _shopInventory = items;

        }

        /// <summary>
        /// Checks if player can buy the item
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="itemIndex">The item that the player wants to buy</param>
        /// <returns></returns>
        public bool Sell(Player player, int itemIndex)
        {
            //Gets the item to be bought
            Item selectedItem = _shopInventory[itemIndex];

            //Checks if player can buy the item
            //IF the player has less money the the item cost
            if (player.Gold < selectedItem.Cost)
            {
                //Tell the player they don't have enough
                Console.Clear();
                Console.WriteLine("You cannot afford that.");
                Console.ReadKey(true);
                return false;
            }

            //otherwise return true
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Creates a new array that contains only the item names
        /// </summary>
        /// <returns>An array containing the names of all items sold at the shop</returns>
        public string[] GetItemNames()
        {
            //Creates a new array
            string[] itemNames = new string[_shopInventory.Length];

            //Copies the items' name into the arrays
            for (int i = 0; i < _shopInventory.Length; i++)
            {
                itemNames[i] = _shopInventory[i].Name;
            }

            return itemNames;
        }
    }
}
