using System;
using System.Collections.Generic;
using System.Text;

namespace HelloDungeons
{
    public enum Scene
    {
        STARTMENU,
        CHARACTERSELECTION,
        ROOM1,
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
        CONSUMABLES,
        NONE
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

        private Item _basicSword;
        private Item _basicShield;
        private Item _knightArmor;
        private Item _basicBow;
        private Item _hunterTunic;
        private Item _arrow;
        private Item _stick;
        private Item _basicRobes;
        private Item _reinforcedShield;
        private Item _ironChestplate;

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
            IntitalizeItems();

        }

        public void IntitalizeItems()
        {
            //Knight Base Items
            _basicSword = new Item { Name = "Promising Sword", StatBoost = 10, Type = ItemType.SWORD };
            _basicShield = new Item { Name = "Wooden Shield", StatBoost = 10, Type = ItemType.SHIELD };
            _knightArmor = new Item { Name = "Knight's Armor", StatBoost = 5, Type = ItemType.ARMOR }; 

            //Archer Base Items
            _basicBow = new Item { Name = "Promising Bow", StatBoost = 14, Type = ItemType.BOW };
            _hunterTunic = new Item { Name = "Hunter's Tunic", StatBoost = 5, Type = ItemType.ARMOR };
            _arrow = new Item { Name = "Arrow", StatBoost = 1, Type = ItemType.CONSUMABLES };
            
            //Wizard Base Items
            _stick = new Item { Name = "Wooden Stick", StatBoost = 5, Type = ItemType.WAND };
            _basicRobes = new Item { Name = "Wizard's Robe", StatBoost = 5, Type = ItemType.ARMOR };

            //Tank Base Items
            _reinforcedShield = new Item { Name = "Reinforced Shield", StatBoost = 15, Type = ItemType.SHIELD };
            _ironChestplate = new Item { Name = "Iron Chestplate", StatBoost = 20, Type = ItemType.ARMOR };

            //Initalize arrays
            _knightItems = new Item[] { _basicSword, _basicShield, _knightArmor };
            _archerItems = new Item[] { _basicBow, _hunterTunic };
            _wizardItems = new Item[] { _stick, _basicRobes };
            _tankItems = new Item[] { _basicSword, _reinforcedShield, _ironChestplate };
            _shopInventory = new Item[] { _basicSword, _basicShield, _basicBow, _reinforcedShield, _arrow };
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
                    Battle();
                    Console.ReadKey(true);
                    Console.Clear();
                    break;

                case Scene.ROOM1:
                    Room1();
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
                Console.WriteLine("'Oh Hi! Whats your name?' A small child asks, looking expectantly.");
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
                Console.WriteLine("'Wow, small world, huh. My name is also Aeos, as is this Dungeon's name.' The child says, looking shocked.\n");
            }

            //Player chooses any other name
            else
            {
                Console.WriteLine("'Hello " + _playerName + ", and Welcome to the Aeos Dungeon.'\n");
                Console.WriteLine("'I am to be your assistant throughout this Dungeon, I am also named Aeos.'");
            }

            Console.WriteLine("'As you can guess, I was named after this Dungeon, or was it named after me?'\n" +
                "You question how useful they can be, but they are insistent on accompanying you, and you eventually yield.");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        void CharacterSelection()
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
                        _player = new Player(_playerName, 50, 25, 5, 20, _knightItems, "Knight");
                        break;
                    //Choose Archer
                    case 1:
                        _player = new Player(_playerName, 50, 25, 5, 20, _archerItems, "Archer");
                        _player.InitializeArrows();
                        break;
                    //Choose Wizard
                    case 2:
                        _player = new Player(_playerName, 50, 25, 5, 20, _wizardItems, "Wizard");
                        break;
                    //Choose Tank
                    case 3:
                        _player = new Player(_playerName, 50, 25, 5, 20, _tankItems, "Tank");
                        break;
                }
                //Ask player if the are okay with their class
                input = GetInput("Are you okay with this class?", "Yes", "No");
                //If yes
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

        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="character">The character that will have its stats shown</param>
        void DisplayStats(Entity character)
        {
            Console.WriteLine(character.Name + " Health: " + character.Health);
            Console.WriteLine(character.Name + " Attack: " + character.AttackPower);
            Console.WriteLine(character.Name + " Defence: " + character.DefensePower);
            Console.WriteLine();
        }
        private string[] GetInventory()
        {
            string[] itemNames = new string[_player.Items.Length + 1];

            //Copies the items name into the new array
            for (int i = 0; i < _player.Items.Length; i++)
            {
                itemNames[i] = _player.Items[i].Name;
            }

            itemNames[itemNames.Length] = "Cancel";
            //returns the new array
            return itemNames;
        }

        /// <summary>
        /// The Item Menu where you can use, unequip, or discard an item
        /// </summary>
        public void DisplayEquipMenu()
        {
            //Displays Items
            int input = GetInput("Items", GetInventory());


            //If input is within scope
            if (input < GetInventory().Length && input > 0)
            {   
                //If item is not a potion/consumable
                if (_player.Items[input].Type != ItemType.CONSUMABLES || _player.Items[input].Type != ItemType.POTION)
                { 
                    //Ask the player if they would like to Use, Unequip or Discard
                    int choice = GetInput("What would you like to do with this item?", "Use", "Unequip", "Discard");
                    //If the choose to equi[
                    if (choice == 0)
                    {
                        //Try to equip the item at given index
                        //If fails
                        if (!(_player.TryEquipItem(input)))
                        {
                            //Tell the player
                            Console.WriteLine("You are already holding that item!");
                        }
                        //else
                        else
                        {
                            //Print feedback
                            Console.WriteLine("You equipped " + _player.CurrentItem.Name + "!");
                        }
                    }
                    //If player chooses to unequip
                    else if (choice == 1)
                    {
                        //Call the unequip function
                        _player.TryUnequip();
                    }
                }

                //If player chooses an Potion/Consumable
                else if(_player.Items[input].Type == ItemType.CONSUMABLES || _player.Items[input].Type == ItemType.POTION)
                {
                    //Either use or discard
                    int choice = GetInput("What would you like to do with this item?", "Use", "Discard");
                }
                
            }
            //If input is the last option
            else if (input == GetInventory().Length)
            {
                //Leave the menu
            }
        }

        public void Battle()
        {
            float damageDealt = 0;

            DisplayStats(_player);
            DisplayStats(_currentEnemy);


            int input = GetInput("A " + _currentEnemy.Name + " stands in front of you. What will you do?", "Attack", "Inventory", "Save");
            if (input == 0)
            {
                damageDealt = _player.Attack(_currentEnemy);
                Console.WriteLine("You dealt " + damageDealt + " damage to " + _currentEnemy.Name + ".");
            }
            else if (input == 1)
            {
                Console.Clear();
                DisplayEquipMenu();
                Console.ReadKey(true);
                return;
            }
            else if (input == 2)
            {
                Console.Clear();
                //Save();
                Console.WriteLine("Saved Game");
                return;
            }

            damageDealt = _currentEnemy.Attack(_player);
            Console.WriteLine("You took " + damageDealt + " damage.");
            //CheckBattleResults();
        }

        void Room1()
        {
            Console.WriteLine("Aeos joyfully enters into the dungeon, as you follow, keeping careful notice of potential hazards.\n");

            int input = GetInput("You enter into a pitch black room, and all that can be seen is a dim light from the door on the other side of the room.\n What will you do?", "Walk Ahead", "Stay Put");
            if (input == 0)
            {
                Console.WriteLine("You walk ahead dispite your lack of vision, which proves to be a bad decision as you fall a long way down.\n " +
                    "Shortly, the lights come on, and you see towering walls above you, acting as a walkway.\n" +
                    "You then hear Aeos call down to you: 'I found the lights! There should be a way back up somewhere down there, I meet upwith you when you find it!'");
                int mazeLocation = 1;
                while (mazeLocation != -1)
                {
                    Console.Clear();
                    switch (mazeLocation)
                    { 
                        case 0:
                            Console.WriteLine("You Stumbled across a dead end");
                            break;
                        case 1:
                            input = GetInput("You look around and see a path both ahead and behind you. Which path will you take", "Forwards", "Backwards");
                            if (input == 0)
                            {
                                mazeLocation = 2;
                            }
                            else if (input == 1)
                            {
                                mazeLocation = 7;
                            }
                            break;
                        case 2:
                            input = GetInput("You walk ahead and come across a split path, you can continue forwards or head right.", "Forwards", "Right", "Back");
                            if (input == 0)
                            {
                                mazeLocation = 3;
                            }
                            else if (input == 1)
                            {
                                mazeLocation = 0;
                            }
                            else if (input == 2)
                            {
                                mazeLocation = 1;
                            }
                            break;
                        case 3:
                            input = GetInput("You continue forwards and find a intersection. Which way will you proceed", "Foward", "Left", "Right", "Back");
                            if(input == 0)
                            {
                                mazeLocation = 0;
                            }
                            else if(input == 1)
                            {
                                mazeLocation = 4;
                            }
                            else if (input == 2)
                            {
                                mazeLocation = 6;
                            }
                            else if (input == 3)
                            {
                                mazeLocation = 2;
                            }
                            break;
                        case 4:
                            input = GetInput("You head left from the intersection and come across a left or right turn. Which way will you proceed?", "Left", "Right", "Back");
                            if(input == 0)
                            {
                                mazeLocation = 0;
                            }
                            else if(input == 1)
                            {
                                mazeLocation = 5;
                            }
                            else if(input == 2)
                            {
                                mazeLocation = 3;
                            }
                            break;
                        case 5:
                            input = GetInput("You come across a dead end, but there are some vines that you could climb up.", "Climb", "Go Back");
                            if(input == 0)
                            {
                                mazeLocation = -1;
                            }
                            else if(input == 1)
                            {
                                mazeLocation = 4;
                            }
                            break;
                        case 6:
                            input = GetInput("You walk right from the intersection and come across a left or right turn. Which way will you proceed?", "Left", "Right", "Back");
                            if (input == 0)
                            {
                                mazeLocation = 0;
                            }
                            else if (input == 1)
                            {
                                mazeLocation = 0;
                            }
                            else if (input == 2)
                            {
                                mazeLocation = 3;
                            }
                            break;
                        case 7:
                            input = GetInput("You walk backwards to find an intersection. Which way will you proceed?", "Forwards", "Left", "Right", "Back");
                            if (input == 0)
                            {
                                mazeLocation = 8;
                            }
                            else if (input == 1)
                            {
                                mazeLocation = 0;
                            }
                            else if (input == 2)
                            {
                                mazeLocation = 10;
                            }
                            else if (input == 3)
                            {
                                mazeLocation = 1;
                            }
                            break;
                        case 8:
                            input = GetInput("You keep heading forwards and come across a left or right turn. Which way will you proceed?", "Left", "Right", "Back");
                            if (input == 0)
                            {
                                mazeLocation = 0;
                            }
                            else if (input == 1)
                            {
                                mazeLocation = 9;
                            }
                            else if (input == 2)
                            {
                                mazeLocation = 7;
                            }
                            break;
                        case 9:
                            input = GetInput("You reach a dead end, but there is a ladder that reaches up to the top.", "Climb", "Go Back");
                            if (input == 0)
                            {
                                mazeLocation = -1;
                            }
                            else if (input == 1)
                            {
                                mazeLocation = 8;
                            }
                            break;
                        case 10:
                            input = GetInput("You head right from the intersection and and reach a fork in the road. You can keep going forwards, or go left.", "Forwards", "Left", "Back");
                            if (input == 0)
                            {
                                mazeLocation = 0;
                            }
                            else if (input == 1)
                            {
                                mazeLocation = 0;
                            }
                            else if (input == 2)
                            {
                                mazeLocation = 7;
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid Scene Index");
                            break;
                    }
                }
                Console.Clear();
                Console.WriteLine("You climb up to the top and make your way to the door, where Aeos is waiting for you, waving.\n" +
                    "'What took you so long? I was getting bored.' They joke.");
            }
            else if (input == 1)
            {
                Console.Clear();
                Console.WriteLine("You stay still and take small movements around the room, and you feel a drop not to far in. You can't tell how far, but falling in would be dangerous.\n" +
                    "The lights suddenly go on as you look towards Aeos, who seems to have activated some contraption.\n\n 'I found the lights!' They say.\n" +
                    "You look back down and see how far the hole drops. The pit is very deep, such a fall could of been fatal. You begin to cross a maze-like bridge, admiring the chasm below.");
            }
            Console.WriteLine("You begin to head towards the next room, but a screech from below you echoes through the room.\n A large avian creature arise from the depths of the pit.");
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

