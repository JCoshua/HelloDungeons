using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HelloDungeons
{
    class Entity
    {
        private string _name;
        private float _health;
        private float _attackPower;
        private float _defensePower;
        private float _gold;

        public string Name
        {
            get { return _name; }
        }

        public float Health
        {
            get { return _health; }
        }

        public virtual float AttackPower
        {
            get { return _attackPower; }
        }

        public virtual float DefensePower
        {
            get { return _defensePower; }
        }

        public float Gold
        {
            get { return _gold; }
        }

        /// <summary>
        /// Creates a default instance of an Entity
        /// </summary>
        public Entity()
        {
            _name = "Default";
            _health = 0;
            _attackPower = 0;
            _defensePower = 0;
        }

        /// <summary>
        /// A entity constructor
        /// </summary>
        /// <param name="name">Entity's name</param>
        /// <param name="health">The Entity's HP</param>
        /// <param name="attackPower">The Entity's Attack Power</param>
        /// <param name="defensePower">The Entity's Defence Power</param>
        public Entity(string name, float health, float attackPower, float defensePower, float gold)
        {
            _name = name;
            _health = health;
            _attackPower = attackPower;
            _defensePower = defensePower;
            _gold = gold;
        }

        public void Pay(Item item)
        {

            //Removes the cost of the item from the player
            _gold -= item.Cost;

            //Tells the player that the puchase was successful
            Console.Clear();
            Console.WriteLine("You bought a " + item.Name + "!");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Makes an Entity take damage
        /// </summary>
        /// <param name="damageAmount"> Amount of damage that the Entity would take</param>
        /// <returns>How much damage the Entity actually takes</returns>
        public virtual float DamageInflicted(float damageAmount)
        {
            float damageTaken = damageAmount - DefensePower;

            if (damageTaken <= 0)
            {
                damageTaken = 1;
            }

            return damageTaken;
        }

        public float TakeDamage(float damageTaken)
        {
            _health -= damageTaken;
            return damageTaken;
        }
        /// <summary>
        /// Makes the entity take damage
        /// </summary>
        /// <param name="defender">The Entity that is taking damage</param>
        /// <returns>Damage taken</returns>
        public virtual float Attack(Entity defender)
        {
            float damage = DamageInflicted(AttackPower);
            return defender.TakeDamage(damage);
        }

        /// <summary>
        /// Saves the entity
        /// </summary>
        public virtual void Save(StreamWriter writer)
        {
            writer.WriteLine(_name);
            writer.WriteLine(_health);
            writer.WriteLine(_attackPower);
            writer.WriteLine(_defensePower);
        }

        /// <summary>
        /// Loads the Entity's data
        /// </summary>
        /// <returns>If the Load is successful</returns>
        public virtual bool Load(StreamReader reader)
        {
            _name = reader.ReadLine();

            if (!float.TryParse(reader.ReadLine(), out _health))
                return false;

            if (!float.TryParse(reader.ReadLine(), out _attackPower))
                return false;

            if (!float.TryParse(reader.ReadLine(), out _defensePower))
                return false;

            return true;
        }
    }
}