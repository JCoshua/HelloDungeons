using System;
using System.Collections.Generic;
using System.Text;

namespace HelloDungeons
{
    public enum Scene
    {
        STARTMENU,
        CHARACTERSELECTION,
        BATTLE,
        RESTARTMENU
    }

    public enum ItemType
    {
        SWORD,
        BOW,
        WAND,
        SHIELD,
        HELMET,
        ARMOR,
        GLOVES,
        BOOTS,
        POTION,
        CONSUMABLES
    }

    public struct Item
    {
        public string Name;
        public float StatBoost;
        public ItemType Type;
        public int Cost;
    }

    class Game
    {
        private bool _gameOver;
        private Scene _currentScene = 0;
        private Player _player;
        private Shop _shop;
        private Entity[] _enemies;
        private string _playerName;

        private int _currentEnemyIndex = 0;
        private Entity _currentEnemy;

        private Item[] _knightItems;
        private Item[] _archerItems;
        private Item[] _wizardItems;
        private Item[] _tankItems;

        private Item _sword;
        private Item _shield;
        private Item _arrow;
        private Item _jewel;

        public Item[] _shopInventory;

        /// <summary>
        /// Gets the Input of the player
        /// </summary>
        /// <param name="description">The context for the decision being made</param>
        /// <param name="options">The choices</param>
        /// <returns>The selected choice</returns>
        int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputReceived = -1;

            while (inputReceived == -1)
            {
                //Print options
                Console.WriteLine(description);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + options[i]);
                }
                Console.Write("> ");

                //Get input from player
                input = Console.ReadLine();

                //If the player typed an int...
                if (int.TryParse(input, out inputReceived))
                {
                    //...decrement the input and check if it's within the bounds of the array
                    inputReceived--;
                    if (inputReceived < 0 || inputReceived >= options.Length)
                    {
                        //Set input received to be the default value
                        inputReceived = -1;
                        //Display error message
                        Console.WriteLine("Invalid Input. Not an Option");
                        Console.ReadKey(true);
                    }
                }
                //If the player didn't type an int
                else
                {
                    //Set input received to be the default value
                    inputReceived = -1;
                    //Display error message
                    Console.WriteLine("Invalid Input. Not a Number");
                    Console.ReadKey(true);
                }
            }
            return inputReceived;
        }

        /// <summary>
        /// Intializes the game at the start of the game
        /// </summary>
        private void Start()
        {
            _gameOver = false;

        }

        public void IntitalizeItems()
        {
            //Knight Base Items
            Item _basicSword = new Item { Name = "Promising Sword", StatBoost = 10, Type = ItemType.SWORD };
            Item _basicShield = new Item { Name = "Wooden Shield", StatBoost = 10, Type = ItemType.SHIELD };
            Item _knightArmor = new Item { Name = "Knight's Armor", StatBoost = 5, Type = ItemType.ARMOR }; 

            //Archer Base Items
            Item _basicBow = new Item { Name = "Promising Bow", StatBoost = 14, Type = ItemType.BOW };
            Item _hunterTunic = new Item { Name = "Hunter's Tunic", StatBoost = 5, Type = ItemType.ARMOR };
            Item _arrow = new Item { Name = "Arrow", StatBoost = 1, Type = ItemType.CONSUMABLES };
            
            //Wizard Base Items
            Item _stick = new Item { Name = "Wooden Stick", StatBoost = 5, Type = ItemType.WAND };
            Item _basicRobes = new Item { Name = "Wizard's Robe", StatBoost = 5, Type = ItemType.ARMOR };

            //Tank Base Items
            Item _reinforcedShield = new Item { Name = "Reinforced Shield", StatBoost = 15, Type = ItemType.SHIELD };
            Item _ironChestplate = new Item { Name = "Iron Chestplate", StatBoost = 20, Type = ItemType.ARMOR };

            //Initalize arrays
            _knightItems = new Item[] { _basicSword, _basicShield, _knightArmor };
            _archerItems = new Item[] { _basicBow, _hunterTunic };
            _wizardItems = new Item[] { _stick, _basicRobes };
            _tankItems = new Item[] { _basicSword, _reinforcedShield, _ironChestplate };
        }

        /// <summary>
        /// This function is called every time the game loops.
        /// </summary>
        public void Update()
        {
            DisplayCurrentScene();
            Console.Clear();
        }

        /// <summary>
        /// This function is called before the applications closes
        /// </summary>
        public void End()
        {
            Console.WriteLine("Farewell... Coward.");
        }

        /// <summary>
        /// Calls the appropriate function(s) based on the current scene index
        /// </summary>
        void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                case Scene.STARTMENU:
                    StartingScreen();
                    break;

                case Scene.CHARACTERSELECTION:
                    BeginningScene();
                    break;

                case Scene.BATTLE:
                    Console.ReadKey(true);
                    Console.Clear();
                    break;

                case Scene.RESTARTMENU:
                    break;

                default:
                    Console.WriteLine("Invalid Scene Index");
                    break;
            }
        }
        public void StartingScreen()
        {
            int choice = GetInput("Welcome to Aeos Dungeon", "Start New Game", "Load Game");

            if (choice == 0)
            {
                _currentScene = Scene.CHARACTERSELECTION;
            }
            else if (choice == 1)
            {
                if (_gameOver)
                {
                    Console.WriteLine("Loading Successful");
                    Console.ReadKey(true);
                    Console.Clear();
                    _currentScene = Scene.BATTLE;
                }
                else
                {
                    Console.WriteLine("Woops, something messed up");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
        }

        void BeginningScene()
        {
            //Start Screen and Charater Creation
            //Name Check Choices
            GetPlayerName();

            //Choose Class
            CharacterSelection();
        }

        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {
            bool validName = false;
            //Loops while player has not confirmed their name
            while (validName == false)
            {
                //Ask player their name
                Console.WriteLine("Hello. Please enter your Name.");
                _playerName = Console.ReadLine();

                //Ask player if the are okay with their name
                int input = GetInput("Are you okay with this name?", "Yes", "No");
                //IF yes
                if (input == 0)
                {
                    //End Loop
                    validName = true;
                }
                //If no
                else if (input == 1)
                {
                    //Continue Looping
                }
            }
            //Player chose Aeos as Name
            if (_playerName.ToLower() == "aeos")
            {
                Console.WriteLine("'Wow, small world, huh. My name is also Aeos, as is this Dungeon's name.\n'");
            }

            //Player chooses any other name
            else
            {
                Console.WriteLine("Hello " + _playerName + ", and Welcome to the Aeos Dungeon.\n");
                Console.WriteLine("I am to be your assistant throughout this Dungeon, I am also named Aeos.");
            }

            Console.WriteLine("As you can guess, I was named after this Dungeon, or was it named after me?\n");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            Console.Clear();
            bool validJob = false;
            //Loops while player has not confirmed their name
            while (validJob == false)
            {
                int input = GetInput("'So I presume you are an adventurer.' They say, 'So what do you specialize in?'", "Knight", "Archer", "Wizard", "Tank");
                switch (input)
                {
                    //Choose Knight
                    case 0:
                        _player = new Player(_playerName, 50, 25, 5, _knightItems, "Knight");
                        break;
                    //Choose Archer
                    case 1:
                        _player = new Player(_playerName, 50, 25, 5, _archerItems, "Archer");
                        _player.InitializeArrows();
                        break;
                    //Choose Wizard
                    case 2:
                        _player = new Player(_playerName, 50, 25, 5, _wizardItems, "Wizard");
                        break;
                    //Choose Tank
                    case 3:
                        _player = new Player(_playerName, 50, 25, 5, _tankItems, "Tank");
                        break;
                }
                //Ask player if the are okay with their class
                input = GetInput("Are you okay with this class?", "Yes", "No");
                //IF yes
                if (input == 0)
                {
                    //End Loop
                    validJob = true;
                }
                //If no
                else if (input == 1)
                {
                    //Continue Looping
                }
            }
        }

        public void Run()
        {
            Start();

            while (_gameOver == false)
            {
                Update();
            }

            End();
        }
    }
}

