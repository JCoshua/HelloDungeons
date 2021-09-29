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
        ROOM2,
        ROOM2BATTLE,
        ROOM3,
        BATTLE,
        SHOP,
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
        NONE
    }

    public struct Item
    {
        public string Name;
        public float StatBoost;
        public ItemType Type;
        public int Cost;
        public string Description;
    }

    class Game
    {
        private bool _gameOver;
        private Scene _currentScene = 0;
        private Player _player;
        private Shop _shop;
        private Entity[] _enemies;
        private string _playerName;

        private int _currentArea;
        private Entity _currentEnemy;

        private Item[] _knightItems;
        private Item[] _archerItems;
        private Item[] _wizardItems;
        private Item[] _tankItems;

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
            InitializeItems();
            InitializeEnemies();

        }

        public void InitializeItems()
        {
            //Knight Base Items
            Item _basicSword = new Item { Name = "Promising Sword", StatBoost = 10, Type = ItemType.SWORD, Cost = 10, Description = "A promising Sword that has got you through your travels." };
            Item _knightArmor = new Item { Name = "Knight's Armor", StatBoost = 5, Type = ItemType.ARMOR, Cost = 30, Description = "A standard Knight's Armor." };

            //Archer Base Items
            Item _basicBow = new Item { Name = "Promising Bow", StatBoost = 20, Type = ItemType.BOW, Cost = 10, Description = "A promising Bow that has got you through your travels." };
            Item _hunterTunic = new Item { Name = "Hunter's Tunic", StatBoost = 5, Type = ItemType.ARMOR, Cost = 25, Description = "A tunic that has be tailored for hunting." };
            
            //Wizard Base Items
            Item _stick = new Item { Name = "Wooden Stick", StatBoost = 10, Type = ItemType.WAND, Cost = 10, Description = "A Wooden Stick. A Wizard could still find a use for this." };
            Item _basicRobes = new Item { Name = "Wizard's Robe", StatBoost = 10, Type = ItemType.ARMOR, Cost = 25, Description = "Casual Wizard Robes for a casual wizard." };

            //Tank Base Items
            Item _ironChestplate = new Item { Name = "Iron Chestplate", StatBoost = 20, Type = ItemType.ARMOR, Cost = 100, Description = "Knight's Armor that has been reinforced with more iron, very heavy." };

            //Shop Items
            Item _smallpotion = new Item { Name = "Small Potion", StatBoost = 30, Type = ItemType.POTION, Cost = 30, Description = "A small potion to heal small wounds. Better to not ask how." };
            Item _potion = new Item { Name = "Potion", StatBoost = 60, Type = ItemType.POTION, Cost = 50, Description = "A normal potion that can heal wounds." };
            Item _largepotion = new Item { Name = "Large Potion", StatBoost = 100, Type = ItemType.POTION, Cost = 75, Description = "A really big potion that can heal all wounds, except the stomachache you'll get if you chug it." };

            //First shop
            Item _steelSword = new Item { Name = "Steel Sword", StatBoost = 20, Type = ItemType.SWORD, Cost = 50, Description = "A Sword made from steel." };
            Item _basicShield = new Item { Name = "Wooden Shield", StatBoost = 5, Type = ItemType.SHIELD, Cost = 25, Description = "A Wooden shield. It's not effective" };
            Item _tightenedBow = new Item { Name = "Tightly-Strung Bow", StatBoost = 30, Type = ItemType.BOW, Cost = 50, Description = "A bow that has been tighten for more damage and range. Flies perfectly straight" };
            Item _wand = new Item { Name = "Bronze Wand", StatBoost = 20, Type = ItemType.WAND, Cost = 50, Description = "A bronze wand." };
            Item _adventurerGear = new Item { Name = "Adventurer's Gear", StatBoost = 15, Type = ItemType.ARMOR, Cost = 50, Description = "A standard adventurer set that has all your basic needs.." };
            Item _silkGloves = new Item { Name = "Silk Gloves", StatBoost = 5, Type = ItemType.GLOVES, Cost = 20, Description = "Very Comfy Gloves." };
            Item _heavyDutyBoots = new Item { Name = "Heavy-Duty Boots", StatBoost = 10, Type = ItemType.BOOTS, Cost = 20, Description = "Boots designed for anything and everything." };

            //Second Shop
            Item _magicSword = new Item { Name = "Magic Sword", StatBoost = 35, Type = ItemType.SWORD, Cost = 100, Description = "A Sword that has been imbued with magic." };
            Item _reinforcedShield = new Item { Name = "Reinforced Shield", StatBoost = 15, Type = ItemType.SHIELD, Cost = 50, Description = "A wood shield that is reinforced with many materials." };
            Item _doubleBow = new Item { Name = "Double-Shot Bow", StatBoost = 40, Type = ItemType.BOW, Cost = 100, Description = "A bow that was made specifically to fire two arrows. Be lucky you have infinite arrows" };
            Item _staff = new Item { Name = "Gold Staff", StatBoost = 35, Type = ItemType.WAND, Cost = 100, Description = "A magic staff made from gold." };
            Item _dungeonGear = new Item { Name = "Dungeoneer's Gear", StatBoost = 20, Type = ItemType.ARMOR, Cost = 75, Description = "Gear made to challenge dungeons." };
            Item _italianGloves = new Item { Name = "Italian Gloves", StatBoost = 10, Type = ItemType.GLOVES, Cost = 50, Description = "These strange gloves make you feel like a plumber." };

            //FinalShop
            Item _heroSword = new Item { Name = "Hero's Sword", StatBoost = 50, Type = ItemType.SWORD, Cost = 250, Description = "A Sword for a true hero." };
            Item _hylianShield = new Item { Name = "Hylian Shield", StatBoost = 25, Type = ItemType.SHIELD, Cost = 175, Description = "A very effective shield, somehow familiar." };
            Item _kamekwand = new Item { Name = "Kamek's Wand", StatBoost = 50, Type = ItemType.WAND, Cost = 250, Description = "A wand stol... I mean borrowed from Bowser's top general. Don't tell Kamek." };
            Item _heroGear = new Item { Name = "Hero's Gear", StatBoost = 25, Type = ItemType.ARMOR, Cost = 100, Description = "An outfit made for a true hero." };
            Item _ironboots = new Item { Name = "Iron Boots", StatBoost = 15, Type = ItemType.BOOTS, Cost = 50, Description = "Really sturdy boots. Will sink when wearing them." };



            //Initalize arrays
            _knightItems = new Item[] { _basicSword, _knightArmor, _smallpotion };
            _archerItems = new Item[] { _basicBow, _hunterTunic };
            _wizardItems = new Item[] { _stick, _basicRobes };
            _tankItems = new Item[] { _basicSword, _basicShield, _ironChestplate };
            _shopInventory = new Item[] { _basicSword, _steelSword, _basicShield, _reinforcedShield, _basicBow, };
        }

        public void InitializeEnemies()
        {
            Entity windShearer = new Entity("The Wind Shearer", 75, 75, 30, 15, 75);
            Entity voidOgre = new Entity("Void Ogre", 100, 100, 30, 10, 100);

            _enemies = new Entity[] { windShearer, voidOgre };
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
                    break;

                case Scene.SHOP:
                    DisplayShopMenu();
                    break;

                case Scene.ROOM1:
                    Room1();
                    break;

                case Scene.ROOM2:
                    Room2();
                    break;

                case Scene.ROOM2BATTLE:
                    VoidOgreBattle();
                    break;

                case Scene.RESTARTMENU:
                    DisplayRestartMenu();
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

            //Changes Scene
            _currentScene = Scene.ROOM1;
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
                        _player = new Player(_playerName, 75, 75, 15, 15, 20, _knightItems, "Knight");
                        _player.TryEquipItem(0);
                        _player.TryEquipItem(1);
                        _player.TryEquipItem(2);
                        break;
                    //Choose Archer
                    case 1:
                        _player = new Player(_playerName, 60, 60, 20, 10, 20, _archerItems, "Archer");
                        _player.TryEquipItem(0);
                        _player.TryEquipItem(1);
                        break;
                    //Choose Wizard
                    case 2:
                        _player = new Player(_playerName, 60, 60, 25, 5, 20, _wizardItems, "Wizard");
                        _player.TryEquipItem(0);
                        _player.TryEquipItem(1);
                        break;
                    //Choose Tank
                    case 3:
                        _player = new Player(_playerName, 100, 100, 10, 20, 20, _tankItems, "Tank");
                        _player.TryEquipItem(0);
                        _player.TryEquipItem(1);
                        _player.TryEquipItem(2);
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
            Console.WriteLine(character.Name);
            Console.WriteLine("Health: " + character.Health);
            Console.WriteLine("Attack: " + character.AttackPower);
            Console.WriteLine("Defense: " + character.DefensePower);
            Console.WriteLine();
        }

        /// <summary>
        /// Prints an item stats
        /// </summary>
        /// <param name="item">The item to be shown</param>
        void DisplayItemStats(Item item)
        {
            Console.WriteLine(item.Name);
            Console.WriteLine("\n" + item.Description);
            Console.WriteLine("\nItem Type: " + item.Type);
            if (item.Type == ItemType.SWORD || item.Type == ItemType.BOW || item.Type == ItemType.WAND)
                Console.WriteLine("Attack: +" + item.StatBoost);
            else if (item.Type == ItemType.SHIELD || item.Type == ItemType.ARMOR || item.Type == ItemType.HELMET || item.Type == ItemType.GLOVES || item.Type == ItemType.BOOTS)
                Console.WriteLine("Defense: +" + item.StatBoost);
            else if (item.Type == ItemType.POTION)
                Console.WriteLine("Heals " + item.StatBoost + " damage");
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

            itemNames[itemNames.Length - 1] = "Cancel";
            //returns the new array
            return itemNames;
        }

        /// <summary>
        /// The Item Menu where you can use, unequip, or read the description of an item
        /// </summary>
        public void DisplayEquipMenu()
        {
            //Displays Items
            int input = GetInput("Items", GetInventory());


            //If input is within scope
            if (input < GetInventory().Length && input >= 0)
            {   
                //If item is not a potion/consumable
                if (_player.Items[input].Type != ItemType.POTION)
                { 
                    //Ask the player if they would like to Use, Unequip or Discard
                    int choice = GetInput("What would you like to do with this item?", "Use", "Unequip", "Description");
                    //If the choose to equip
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
                        _player.TryUnequip(input);
                    }
                    else if (choice == 2)
                    {
                        Console.Clear();
                        DisplayItemStats(_player.Items[input]);
                    }
                }

                //If player chooses an Potion/Consumable
                else if(_player.Items[input].Type == ItemType.POTION)
                {
                    //Either use or discard
                    int choice = GetInput("What would you like to do with this item?", "Use", "Description");
                    if (choice == 0)
                    {
                        float amountHealed = _player.HealDamage(_player.Items[input].StatBoost);
                        Console.WriteLine("You healed " + amountHealed + " damage.");
                        _player.UseConsumable(_player.Items[input]);
                    }
                    else if (choice == 1)
                    {
                        Console.Clear();
                        DisplayItemStats(_player.Items[input]);
                    }
                }
                
            }
            //If input is the last option
            else if (input == GetInventory().Length - 1)
            {   
                //Leave the menu
                return;
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
                return;
            }
            else if (input == 2)
            {
                Console.Clear();
                //Save();
                Console.WriteLine("Saved Game");
                Console.Clear();
                return;
            }

            damageDealt = _currentEnemy.Attack(_player);
            Console.WriteLine("You took " + damageDealt + " damage.");
            CheckBattleResults();
        }

        void CheckBattleResults()
        {
            //If the player loses
            if (_player.Health <= 0)
            {
                Console.WriteLine("\nYou Died");
                _currentScene = Scene.RESTARTMENU;
            }

            //If the enemy dies...
            else if (_currentEnemy.Health <= 0)
            {
                Console.WriteLine("\nYou slayed the " + _currentEnemy.Name + "!");
                _player.getMoney(_currentEnemy);
                CheckLocation(_currentArea);
            }
        }

        /// <summary>
        /// Displays the menu that allows the player to restart or quit the game
        /// </summary>
        void DisplayRestartMenu()
        {
            int input = GetInput("Would you like to play again?", "Yes", "No");
            if (input == 0)
            {
                InitializeEnemies();
                _currentScene = Scene.CHARACTERSELECTION;
            }
            else if (input == 1)
            {
                _gameOver = true;
            }
        }

        void CheckLocation(int currentArea)
        {
            switch(currentArea)
            {
                case 1:
                    _currentScene = Scene.ROOM2;
                    break;
                case 2:
                    _currentScene = Scene.ROOM2BATTLE;
                    break;
                case 3:
                    _currentScene = Scene.ROOM3;
                    break;
            }
        }
        void Room1()
        {
            Console.WriteLine("Aeos joyfully enters into the dungeon, as you follow, keeping careful notice of potential hazards.");

            int input = GetInput("You enter into a pitch black room, and all that can be seen is a dim light from the door on the other side of the room.\n\nWhat will you do?", "Walk Ahead", "Stay Put");
            if (input == 0)
            {
                Console.Clear();
                Console.WriteLine("You walk ahead dispite your lack of vision, which proves to be a bad decision as you fall a long way down.\n");
                Console.WriteLine("Shortly, the lights come on, and you see towering walls above you, acting as the walkway.\n");
                Console.WriteLine("You then hear Aeos call down to you: 'I found the lights! There should be a way back up somewhere down there,\nI meet up with you when you find it!'");
                Console.ReadKey(true);
                int mazeLocation = 1;
                while (mazeLocation != -1)
                {
                    Console.Clear();
                    switch (mazeLocation)
                    { 
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
                                Console.WriteLine("You Stumbled across a dead end");
                                Console.ReadKey(true);
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
                                Console.WriteLine("You Stumbled across a dead end");
                                Console.ReadKey(true);
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
                                Console.WriteLine("You Stumbled across a dead end");
                                Console.ReadKey(true);
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
                                Console.WriteLine("You Stumbled across a dead end");
                                Console.ReadKey(true);
                            }
                            else if (input == 1)
                            {
                                Console.WriteLine("You Stumbled across a dead end");
                                Console.ReadKey(true);
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
                                Console.WriteLine("You Stumbled across a dead end");
                                Console.ReadKey(true);
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
                                Console.WriteLine("You Stumbled across a dead end");
                                Console.ReadKey(true);
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
                                Console.WriteLine("You Stumbled across a dead end");
                                Console.ReadKey(true);
                            }
                            else if (input == 1)
                            {
                                Console.WriteLine("You Stumbled across a dead end");
                                Console.ReadKey(true);
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
                    "'What took you so long? I was getting bored.' They joke.\n");
                Console.ReadKey(true);
            }
            else if (input == 1)
            {
                Console.Clear();
                Console.WriteLine("You stay still and take small movements around the room, and you feel a drop not to far in.\n" +
                    "You can't tell how far, but falling in would be dangerous.\n");
                Console.ReadKey(true);
                Console.WriteLine("The lights suddenly go on as you look towards Aeos, who seems to have activated some contraption.\n" +
                    "'I found the lights!' They say.\n");
                Console.ReadKey(true);
                Console.WriteLine("You look back down and see how far the hole drops.\nThe pit is very deep, such a fall could of been fatal.\n");
                Console.ReadKey(true);
                Console.WriteLine("You begin to cross a maze-like bridge, admiring the chasm below.\n");
                Console.ReadKey(true);
            }
            Console.WriteLine("You begin to head towards the next room, but a screech from below you echoes through the room.\n" +
                "A large avian creature arise from the depths of the pit, It lunges at you with tremendous force.");
            Console.ReadKey(true);
            _currentArea = 1;
            _currentEnemy = _enemies[0];
            _currentScene = Scene.BATTLE;
        }
        
        void Room2()
        {
            Console.WriteLine("The Wind Shearer howls in agony while trying to stay airborne, before finally falling to the depths it emerged from.\n" +
                "'Wow, that was scary, but you sure are strong.' Aeos remarks.");
            Console.ReadKey(true);
            Console.Clear();

            Console.WriteLine("The two of you emerge into a second room. It is much smaller than the first, and doesn't seem to have an exit.\n" +
                "All you see before you are 5 switches\n\n" +
                "Aeos jumps from behind you and exclaims 'Wait, I know this room. This wall here is a locked door, a door that can only be openned by those levers." +
                "\nYou ask what combination opens the door, and they look embarrased and say: I don't actually know...'\n\n" +
                "You sigh and look at the levers");
            Console.ReadKey(true);
            Console.Clear();

            //True = Up
            //False = Down
            bool lever1 = true;
            bool lever2 = false;
            bool lever3 = true;
            bool lever4 = false;
            bool lever5 = false;
            //Amount of attempts remaining
            for (int i = 0; i <= 5; i++)
            {
                Console.Clear();
                if(i == 2)
                {
                    //First warning
                    Console.WriteLine("You feel a strange rumble in the floor.\n");
                }
                else if(i == 3)
                {
                    //Second Waring
                    Console.WriteLine("Banging echoes can be hear in the distance\n");
                }
                else if(i == 4)
                {
                    //Final Warning
                    Console.WriteLine("The whole room is shaking!\n");
                }
                else if(i == 5)
                {
                    _currentScene = Scene.ROOM2BATTLE;
                    break;
                }

                Console.WriteLine("You see 5 levers before you all in these positions");
                //Lever 1 Position
                //If a lever is true
                if (lever1)
                    //Display it as up
                    Console.WriteLine("Lever 1: Up");
                else
                    //Display it as down
                    Console.WriteLine("Lever 1: Down");

                //Lever 2 Position
                if (lever2)
                    Console.WriteLine("Lever 2: Up");
                else
                    Console.WriteLine("Lever 2: Down");

                //Lever 3 Position
                if (lever3)
                    Console.WriteLine("Lever 3: Up");
                else
                    Console.WriteLine("Lever 3: Down");

                //Lever 4 Position
                if (lever4)
                    Console.WriteLine("Lever 4: Up");
                else
                    Console.WriteLine("Lever 4: Down");

                //Lever 2 Position
                if (lever5)
                    Console.WriteLine("Lever 5: Up");
                else
                    Console.WriteLine("Lever 5: Down");

                int input = GetInput("Choose a lever to pull", "Lever 1", "Lever 2", "Lever 3", "Lever 4", "Lever 5");
                switch (input)
                {
                    //Lever 1 is swapped
                    case 0:
                        Console.WriteLine("\nYou pulled the Lever.");
                        Console.ReadKey(true);
                        lever1 = !lever1;
                        break;
                    //Lever 2 is swapped
                    case 1:
                        Console.WriteLine("\nYou pulled the Lever.");
                        Console.ReadKey(true);
                        lever2 = !lever2;
                        break;
                    //Lever 3 is swapped
                    case 2:
                        Console.WriteLine("\nYou pulled the Lever.");
                        Console.ReadKey(true);
                        lever3 = !lever3;
                        break;
                    //Lever 4 is swapped
                    case 3:
                        Console.WriteLine("\nYou pulled the Lever.");
                        Console.ReadKey(true);
                        lever4 = !lever4;
                        break;
                    //Lever 5 is swapped
                    case 4:
                        Console.WriteLine("\nYou pulled the Lever.");
                        Console.ReadKey(true);
                        lever5 = !lever5;
                        break;
                }
                if (!lever1 && lever2 && lever3 && !lever4 && lever5)
                {
                    Console.Clear();
                    Console.WriteLine("The door opens and allows for further progress to be made.\n\n" +
                        "Aeos jumps with delight at your completion of the puzzle.\n" +
                        "'You did it!' They cheer.");
                    Console.ReadKey(true);
                    Console.Clear();
                    input = GetInput("You go through the doorway and reach the other side.\n" +
                        "Where another strange door lies, but unlike the one you entered prior this door seems simple and normal.\n\nEnter?", "Yes", "No");
                    if (input == 0)
                    {
                        _currentArea = 2;
                        _shop = new Shop(_shopInventory);
                        _currentScene = Scene.SHOP;
                    }
                    else if(input == 1)
                    {
                        Console.WriteLine("You decide to keep going");
                    }
                    break;
                }
            }
        }

        private void VoidOgreBattle()
        {
            if (_currentArea == 2)
            {
                Console.WriteLine("You make your way further in the dungeon. As you go along you hear loud banging and crashes in the background.");
                Console.ReadKey(true);
                Console.WriteLine("\nBut there getting closer...");
            }
            else
            {
                Console.WriteLine("The shaking intensifies tenfold, and the doorway explodes into rubble.");
                Console.ReadKey(true);
            }
            Console.WriteLine("\n\nYou are soon met with a terrifying sight. A massive beast ramages around the room and distorts the surrounding reality.");
            Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine("You're in for a tough fight");
            Console.ReadKey(true);

            _currentEnemy = _enemies[1];
            _currentScene = Scene.BATTLE;
        }

        /// <summary>
        /// Gets the items that will be shown in the menu, and adds saving and exiting
        /// </summary>
        /// <returns>The options that will be displayed in the shop menu</returns>
        private string[] GetShopMenuOptions()
        {
            //Creates a new string array two longer that the shopInventory array
            string[] itemNames = new string[_shopInventory.Length + 2];

            //Copies the names of the items
            for (int i = 0; i < _shopInventory.Length; i++)
            {
                itemNames[i] = _shopInventory[i].Name + ": " + _shopInventory[i].Cost + " Gold";
            }

            //Adds Save and Exit and returns the finished array
            itemNames[_shopInventory.Length] = "Save";
            itemNames[_shopInventory.Length + 1] = "Exit";
            return itemNames;
        }

        /// <summary>
        /// The Shop Menu
        /// </summary>
        private void DisplayShopMenu()
        {
            //Displays the players gold
            Console.WriteLine("Your Gold: " + _player.Gold);

            //Displays the player's Inventory
            Console.WriteLine("Your Inventory:");

            //Displays each item in the player's inventory
            for (int i = 0; i < _player.GetItemNames().Length; i++)
            {
                Console.WriteLine(_player.GetItemNames()[i]);
            }

            //Displays the options
            int input = GetInput("\nWhat would you like to puchase?", GetShopMenuOptions());

            //if player selected an item
            if (input < _shopInventory.Length && input >= 0)
            {
                //Give player choice to buy or Check
                int choice = GetInput("\nWhat would you like to do?", "Buy", "Check");
                //IF player buys
                if (input == 0)
                {
                    //Check if player can perform transaction
                    if (_shop.Sell(_player, input))
                        _player.Buy(_shopInventory[input]);
                }
                //If player checks
                else if (input == 1)
                {
                    //Display Item's Stats
                    DisplayItemStats(_shopInventory[input]);
                }
            }

            //If player selected save
            else if (input == _shopInventory.Length)
            {
                Console.WriteLine("Saved.");
            }

            //If player selects leave
            else if (input == _shopInventory.Length + 1)
            {
                //Tells player goodbye
                Console.WriteLine("Have a nice day.");
                Console.ReadKey(true);

                //Heals player if not at full
                if(_player.Health != _player.MaxHealth)
                {
                    _player.HealDamage(999);
                    Console.WriteLine("\nYou have been healed.");
                    Console.ReadKey(true);
                }
                //Transports them back to current area
                CheckLocation(_currentArea);
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

