using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HelloDungeons
{
    public enum Scene
    {
        STARTMENU,
        CHARACTERSELECTION,
        ROOM1,
        ROOM1MAZE,
        ROOM1BATTLE,
        ROOM2,
        ROOM2BATTLE,
        ROOM3,
        STAIRS,
        FINALBOSS,
        ENDING,
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

    class Game
    {
        private bool _gameOver;
        private Player _player;
        private Shop _shop;
        private Entity[] _enemies;
        private Entity[] _bosses;
        private string _playerName;
        private bool _finalBossPhaseTwo = false;
        private bool _finalBattle = false;

        private Scene _currentScene = 0;
        private int _currentArea;
        private Entity _currentEnemy;
        private Item[] _currentShop;

        private Item[] _knightItems;
        private Item[] _archerItems;
        private Item[] _wizardItems;
        private Item[] _tankItems;

        public Item[] _firstShopInventory;
        public Item[] _secondShopInventory;
        public Item[] _thirdShopInventory;

        private int _encounter;
        private int _mazeLocation = 1;
        private int _staircaseFloor = 0;

        /// <summary>
        /// Gets the Input of the player
        /// </summary>
        /// <param name="description">The context for the decision being made</param>
        /// <param name="options">The choices</param>
        /// <returns>The selected choice</returns>
        private int GetInput(string description, params string[] options)
        {
            
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
                string input = Console.ReadLine();

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
        /// Intializes every single Item in the game, as well as the shop inventories
        /// </summary>
        private void InitializeItems()
        {
            //Knight Base Items
            Item _basicSword = new Item ( "Promising Sword", 10, "A promising Sword that has got you through your travels.", 10, ItemType.SWORD );
            Item _knightArmor = new Item ( "Knight's Armor", 5, "A standard Knight's Armor.", 30, ItemType.ARMOR );

            //Archer Base Items
            Item _basicBow = new Item ("Promising Bow", 20, "A promising Bow that has got you through your travels.", 10, ItemType.BOW );
            Item _hunterTunic = new Item ("Hunter's Tunic", 5, "A tunic that has be tailored for hunting.", 25, ItemType.ARMOR);
            
            //Wizard Base Items
            Item _stick = new Item ("Wooden Stick", 10, "A Wooden Stick. A Wizard could still find a use for this.", 10, ItemType.WAND );
            Item _basicRobes = new Item ("Wizard's Robe", 10, "Casual Wizard Robes for a casual wizard.", 25, ItemType.ARMOR );

            //Tank Base Items(Tank gets the basic sword)
            Item _ironChestplate = new Item ("Iron Chestplate", 20, "Knight's Armor that has been reinforced with more iron, very heavy.", 100, ItemType.ARMOR  );

            //All Shop Items
            
            //First shop Items
            Item _steelSword = new Item ("Steel Sword", 20, "A Sword made from steel.", 50, ItemType.SWORD );
            Item _basicShield = new Item ("Wooden Shield", 5, "A Wooden shield. It's not effective", 25, ItemType.SHIELD );
            Item _tightenedBow = new Item ("Tightly-Strung Bow", 30, "A bow that has been tighten for more damage and range. Flies perfectly straight", 50, ItemType.BOW );
            Item _wand = new Item ("Bronze Wand", 20, "A bronze wand.", 50, ItemType.WAND );
            Item _adventurerGear = new Item ("Adventurer's Gear", 15, "A standard adventurer set that has all your basic needs...", 50, ItemType.ARMOR);
            Item _silkGloves = new Item ("Silk Gloves", 5, "Very Comfy Gloves.", 20, ItemType.GLOVES );
            Item _heavyDutyBoots = new Item ( "Heavy-Duty Boots", 10, "Boots designed for anything and everything.", 20, ItemType.BOOTS );
            Item _smallpotion = new Item ("Small Potion", 30, "A small potion to heal small wounds. Better to not ask how.", 30, ItemType.POTION );

            //Second Shop Items
            Item _magicSword = new Item ("Magic Sword", 35, "A Sword that has been imbued with magic.", 100, ItemType.SWORD );
            Item _reinforcedShield = new Item ("Reinforced Shield", 15, "A wood shield that is reinforced with many materials.", 50, ItemType.SHIELD );
            Item _doubleBow = new Item ( "Double-Shot Bow", 40, "A bow that was made specifically to fire two arrows. Be lucky you have infinite arrows", 100, ItemType.BOW );
            Item _staff = new Item ( "Gold Staff", 35, "A magic staff made from gold.", 100, ItemType.WAND );
            Item _dungeonGear = new Item ("Dungeoneer's Gear", 20, "Gear made to challenge dungeons.", 75, ItemType.ARMOR );
            Item _italianGloves = new Item ("Italian Gloves", 10, "These strange gloves make you feel like a plumber.", 50, ItemType.GLOVES );
            Item _potion = new Item ( "Potion", 60, "A normal potion that can heal wounds.", 50, ItemType.POTION );

            //FinalShop Items
            Item _heroSword = new Item ("Hero's Sword", 50, "A Sword for a true hero.", 250, ItemType.SWORD );
            Item _hylianShield = new Item ("Hylian Shield", 25, "A very effective shield, somehow familiar.", 175, ItemType.SHIELD);
            Item _heroBow = new Item ( "Hero's Bow", 60, "A legendary bow for a legendary hero!", 250, ItemType.BOOTS );
            Item _kamekwand = new Item ( "Kamek's Wand", 50, "A wand stol... I mean borrowed from Bowser's top general. Don't tell Kamek.", 250, ItemType.WAND );
            Item _heroGear = new Item ( "Hero's Gear", 25, "An outfit made for a true hero.", 100, ItemType.ARMOR );
            Item _ironBoots = new Item ( "Iron Boots", 15, "Really sturdy boots. Will sink when wearing them.", 50, ItemType.BOOTS );
            Item _largepotion = new Item ( "Large Potion", 100, "A really big potion that can heal all wounds, except the stomachache you'll get if you chug it.", 75, ItemType.POTION );

            //Initalize arrays
            //Player's Starting inventory
            _knightItems = new Item[] { _basicSword, _knightArmor };
            _archerItems = new Item[] { _basicBow, _hunterTunic };
            _wizardItems = new Item[] { _stick, _basicRobes };
            _tankItems = new Item[] { _basicSword, _basicShield, _ironChestplate };

            //Shop Invetories
            _firstShopInventory = new Item[] { _steelSword, _basicShield, _tightenedBow, _wand, _adventurerGear, _silkGloves, _heavyDutyBoots, _smallpotion};
            _secondShopInventory = new Item[] { _magicSword, _reinforcedShield, _doubleBow, _staff, _dungeonGear, _italianGloves, _smallpotion, _potion };
            _thirdShopInventory = new Item[] { _heroSword, _hylianShield, _heroBow, _kamekwand, _heroGear, _ironBoots, _potion, _largepotion };
        }

        /// <summary>
        /// Intializes every enemy and creates boss and enemy array
        /// </summary>
        private void InitializeEnemies()
        {
           //Bosses
            Entity _windShearer = new Entity("The Wind Shearer", 75, 75, 30, 15, 75);
            Entity _voidOgre = new Entity("Void Ogre", 100, 100, 40, 10, 100);
            Entity _dungeonCore = new Entity("The Dungeon's Core", 200, 200, 75, 50, 0);
            Entity _aeos = new Entity("Aeos", 500, 500, 100, 0, 1000);

            //Random Encounters
            Entity _windServant = new Entity("Wind Servant", 50, 50, 20, 10, 20);
            Entity _rockservant = new Entity("Rock Servant", 70, 70, 20, 20, 25);
            Entity _lizardWizard = new Entity("The Lizard Wizard", 50, 50, 30, 10, 25);
            Entity _trenchcoatFrogs = new Entity("Frogs in a Trenchcoat", 60, 60, 25, 15, 25);
            Entity _stoneKnight = new Entity("Stone Knight", 100, 100, 30, 20, 40);
            Entity _reptileSage = new Entity("Reptilian Sage", 80, 80, 40, 15, 40);
            Entity _shadyToad = new Entity("Shady Toad", 90, 90, 35, 25, 40);
            Entity _dungeonProtector = new Entity("Dungeon Protector", 125, 125, 50, 35, 65);
            Entity _dragonNecromancer = new Entity("Dragon Necromancer", 100, 100, 75, 25, 65);
            Entity _mysteriousTadpole = new Entity("Mysterious T.A.D.P.O.L.E", 110, 110, 60, 30, 65);

            //Intialize the bosses and enemies arrays
            _bosses = new Entity[] { _windShearer, _voidOgre, _dungeonCore, _aeos };
            _enemies = new Entity[] { _windServant, _rockservant, _lizardWizard, _trenchcoatFrogs, _stoneKnight, _reptileSage, _shadyToad, _dungeonProtector, _dragonNecromancer, _mysteriousTadpole};
        }
    
        /// <summary>
        /// Calls the appropriate function based on the current scene index
        /// </summary>
        private void DisplayCurrentScene()
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
                    DisplayShopMenu(_currentShop);
                    break;

                case Scene.ROOM1:
                    Room1();
                    break;

                case Scene.ROOM1MAZE:
                    Room1Maze();
                    break;

                case Scene.ROOM1BATTLE:
                    Room1Battle();
                    break;

                case Scene.ROOM2:
                    Room2();
                    break;

                case Scene.ROOM2BATTLE:
                    VoidOgreBattle();
                    break;

                case Scene.ROOM3:
                    Room3();
                    break;

                case Scene.STAIRS:
                    Staircase();
                    break;

                case Scene.FINALBOSS:
                    FinalBoss();
                    break;

                case Scene.ENDING:
                    Ending();
                    break;

                case Scene.RESTARTMENU:
                    DisplayRestartMenu();
                    break;

                default:
                    Console.WriteLine("Invalid Scene Index");
                    break;
            }
        }

        /// <summary>
        /// Loads the current Scene after Loading a game
        /// </summary>
        /// <param name="SceneName"></param>
        private void LoadCurrentScene(string SceneName)
        {
            if (SceneName == "BATTLE")
                _currentScene = Scene.BATTLE;
            else if (SceneName == "SHOP")
                _currentScene = Scene.SHOP;
            else if (SceneName == "ROOM1MAZE")
                _currentScene = Scene.ROOM1MAZE;
            else if (SceneName == "ROOM2")
                _currentScene = Scene.ROOM2;
            else if (SceneName == "ROOM3")
                _currentScene = Scene.ROOM3;
            else if (SceneName == "STAIRS")
                _currentScene = Scene.STAIRS;
        }

        /// <summary>
        /// Checks where the player was before entering combat or a shop.
        /// </summary>
        private void CheckLocation()
        {
            switch (_currentArea)
            {
                case 0:
                    //During the Maze
                    _currentScene = Scene.ROOM1MAZE;
                    break;
                case 1:
                    //The top of the Maze
                    _currentScene = Scene.ROOM1BATTLE;
                    break;
                case 2:
                    //End of Room 1
                    _currentScene = Scene.ROOM2;
                    break;
                case 3:
                    //The first shop
                    _currentScene = Scene.ROOM2BATTLE;
                    break;
                case 4:
                    //In room 3
                    _currentScene = Scene.ROOM3;
                    break;
                case 5:
                    //The Staircase Beginning
                    _currentScene = Scene.STAIRS;
                    break;
                case 6:
                    //The Final Boss
                    _currentScene = Scene.FINALBOSS;
                    break;
                case 7:
                    //Ending
                    _currentScene = Scene.ENDING;
                    break;
            }
        }

        /// <summary>
        /// The Opening Start Screen
        /// </summary>
        private void StartingScreen()
        {
            //Ask the player if they want to start or continue
            int choice = GetInput("Welcome to Aeos Dungeon", "Start New Game", "Load Game");

            //Player chooses to start
            if (choice == 0)
            {
                //Send them to the Character Creation Screen
                _currentScene = Scene.CHARACTERSELECTION;
            }
            //Player Load a file
            else if (choice == 1)
            {
                //Check if file loads
                if (Load())
                {
                    //File Loads
                    Console.WriteLine("Loading Successful");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                else
                {
                    //File Fails
                    Console.WriteLine("Woops, something messed up");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// The Character Creation Screen
        /// </summary>
        private void BeginningScene()
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
        private void GetPlayerName()
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
                //If yes
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
        private void CharacterSelection()
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
                        //Creates the Player as a Knight
                        _player = new Player(_playerName, 75, 75, 15, 15, 20, _knightItems);
                        //Equips their items
                        _player.TryEquipItem(0);
                        _player.TryEquipItem(1);
                        break;
                    //Choose Archer
                    case 1:
                        //Creates the Player as a Archer
                        _player = new Player(_playerName, 60, 60, 20, 10, 20, _archerItems);
                        //Equips their items
                        _player.TryEquipItem(0);
                        _player.TryEquipItem(1);
                        break;
                    //Choose Wizard
                    case 2:
                        //Creates the Player as a Wizard
                        _player = new Player(_playerName, 60, 60, 25, 5, 20, _wizardItems);
                        //Equips their items
                        _player.TryEquipItem(0);
                        _player.TryEquipItem(1);
                        break;
                    //Choose Tank
                    case 3:
                        //Creates the Player as a Tank
                        _player = new Player(_playerName, 100, 100, 10, 20, 20, _tankItems);
                        //Equips their items
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
        private void DisplayStats(Entity character)
        {
            Console.WriteLine(character.Name);
            Console.WriteLine("Health: " + character.Health);
            Console.WriteLine("Attack: " + character.AttackPower);
            Console.WriteLine("Defense: " + character.DefensePower);
            Console.WriteLine();
        }

        /// <summary>
        /// The Item Menu where you can use, unequip, or read the description of an item
        /// </summary>
        private void DisplayEquipMenu()
        {
            //Displays Items
            int input = GetInput("Items", GetInventory());


            //If input is within scope
            if (input < _player.Items.Length && input >= 0)
            {
                //If item is not a potion
                if (_player.Items[input].Type != ItemType.POTION)
                {
                    //Ask the player if they would like to Use, Unequip or read Description
                    int choice = GetInput("What would you like to do with this item?", "Equip", "Unequip", "Description");
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
                    //Player chooses to read Description
                    else if (choice == 2)
                    {
                        //Show the Item Stats
                        Console.Clear();
                        DisplayItemStats(_player.Items[input]);
                    }
                }

                //If player chooses a Potion
                else if (_player.Items[input].Type == ItemType.POTION)
                {
                    //Either use or get description
                    int choice = GetInput("What would you like to do with this item?", "Use", "Description");
                    //Player chooses use
                    if (choice == 0)
                    {
                        //Use the potion, then remove it 
                        float amountHealed = _player.HealDamage(_player.Items[input].StatBoost);
                        Console.WriteLine("You healed " + amountHealed + " damage.");
                        _player.UseConsumable(_player.Items[input]);
                    }
                    //Player chooses to read Description
                    else if (choice == 1)
                    {
                        //Show Stats
                        Console.Clear();
                        DisplayItemStats(_player.Items[input]);
                    }
                }

            }

            //If input is the last option (Cancel)
            else if (input == GetInventory().Length - 1)
            {
                //Leave the menu
                return;
            }
        }

        /// <summary>
        /// A function to get the names of the player's items
        /// </summary>
        /// <returns>An array containing all of the player's items</returns>
        private string[] GetInventory()
        {
            //Creates a new array
            string[] itemNames = new string[_player.Items.Length + 1];

            //Copies the items name into the new array
            for (int i = 0; i < _player.Items.Length; i++)
            {
                itemNames[i] = _player.Items[i].Name;
            }

            //Adds a cancel button
            itemNames[itemNames.Length - 1] = "Cancel";

            //returns the new array
            return itemNames;
        }

        /// <summary>
        /// Prints an item stats
        /// </summary>
        /// <param name="item">The item to be shown</param>
        private void DisplayItemStats(Item item)
        {
            Console.Clear();
            Console.WriteLine(item.Name);
            Console.WriteLine("\n" + item.Description);
            Console.WriteLine("\nItem Type: " + item.Type);
            //If Item is a Weapon
            if (item.Type == ItemType.SWORD || item.Type == ItemType.BOW || item.Type == ItemType.WAND)
                Console.WriteLine("Attack: +" + item.StatBoost);
            //If the item is a Defensive Item
            else if (item.Type == ItemType.SHIELD || item.Type == ItemType.ARMOR || item.Type == ItemType.HELMET || item.Type == ItemType.GLOVES || item.Type == ItemType.BOOTS)
                Console.WriteLine("Defense: +" + item.StatBoost);
            //If item is a Potion
            else if (item.Type == ItemType.POTION)
                Console.WriteLine("Heals " + item.StatBoost + " damage");
            Console.WriteLine();
        }

        /// <summary>
        /// The main battle function
        /// </summary>
        private void Battle()
        {
            float damageDealt;

            //Display's both combatants stats
            DisplayStats(_player);
            DisplayStats(_currentEnemy);

            //Ask player for what they want to do
            int input = GetInput("A " + _currentEnemy.Name + " stands in front of you. What will you do?", "Attack", "Inventory", "Save", "Quit");
            //Player Attacks
            if (input == 0)
            {
                //Calls attack function
                damageDealt = _player.Attack(_currentEnemy);
                Console.WriteLine("You dealt " + damageDealt + " damage to " + _currentEnemy.Name + ".");
            }
            //Player goes into inventory
            else if (input == 1)
            {
                //Enter Inventory
                Console.Clear();
                DisplayEquipMenu();
                return;
            }
            //Player Saves
            else if (input == 2)
            {
                //if in the final battle, disallow saving
                if (_finalBattle)
                    Console.WriteLine("You cannot save now.");
                //Otherwise...
                else
                {
                    //Call Save Function
                    Console.Clear();
                    Save();
                }
                return;
            }
            else if (input == 3)
            {
                _gameOver = true;
                return;
            }

            //Player takes damage
            damageDealt = _currentEnemy.Attack(_player);
            Console.WriteLine("You took " + damageDealt + " damage.");
            CheckBattleResults();
        }

        /// <summary>
        /// Checks if battle is over
        /// </summary>
        private void CheckBattleResults()
        {
            //If the player loses
            if (_player.Health <= 0)
            {
                //Go to restart menu
                Console.WriteLine("\nYou Died");
                _currentScene = Scene.RESTARTMENU;
            }

            //If the enemy dies...
            else if (_currentEnemy.Health <= 0)
            {
                //Give the player gold for winning and brings them to the area the were in
                Console.WriteLine("\nYou slayed the " + _currentEnemy.Name + "!");
                Console.WriteLine("You got " +_player.getMoney(_currentEnemy) + " gold!");
                CheckLocation();
            }
        }

        /// <summary>
        /// Displays the menu that allows the player to restart or quit the game
        /// </summary>
        private void DisplayRestartMenu()
        {
            int input = GetInput("Would you like to play again?", "Yes", "No");
            //If yes
            if (input == 0)
            {
                //Reinitialize Enemies and puts to the character creation
                InitializeEnemies();
                _currentScene = Scene.CHARACTERSELECTION;
            }
            //if no
            else if (input == 1)
            {
                //End Game
                _gameOver = true;
            }
        }

        /// <summary>
        /// The First room, pitch black with a maze-like bridge, and a maze below...
        /// </summary>
       private void Room1()
        {
            Console.WriteLine("Aeos joyfully enters into the dungeon, as you follow, keeping careful notice of potential hazards.");

            int input = GetInput("You enter into a pitch black room, and all that can be seen is a dim light from the door on the other side of the room.\n\nWhat will you do?", "Walk Ahead", "Stay Put");
            //If player goes forward
            if (input == 0)
            {
                //Player falls into maze
                Console.Clear();
                Console.WriteLine("You walk ahead dispite your lack of vision, which proves to be a bad decision as you fall a long way down.\n");
                Console.WriteLine("Shortly, the lights come on, and you see towering walls above you, acting as the walkway.\n");
                Console.WriteLine("You then hear Aeos call down to you: 'I found the lights! There should be a way back up somewhere down there,\nI meet up with you when you find it!'");
                Console.ReadKey(true);
                _currentScene = Scene.ROOM1MAZE;
                return;
            }
            //If player waits
            else if (input == 1)
            {
                //Player walks through the room
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
                Console.WriteLine("You are soon halted by a small avian creature, who readies to attack");
                _currentEnemy = _enemies[0];
                _currentArea = 1;
                _currentScene = Scene.BATTLE;
            }
        }
        
        /// <summary>
        /// The maze the player goes through upon falling in room 1
        /// </summary>
        private void Room1Maze()
        {
            //Sets the _encounter to -1 and Area to 0
            _encounter = -1;
            _currentArea = 0;

            while (_mazeLocation != -1)
            {
                
                //If player rolls a 0
                if (_encounter == 0)
                {
                    //Encounters a Wind Servant
                    _currentEnemy = _enemies[0];
                    _currentScene = Scene.BATTLE;
                    InitializeEnemies();
                    return;
                }

                //Rolls for _encounter
                _encounter = new Random().Next(0, 5);
                Console.Clear();

                //The maze. You choose between options that will lead you further in, or to a dead end
                //-1 is the exit
                switch (_mazeLocation)
                {
                    case 1:
                        int input = GetInput("You look around and see a path both ahead and behind you. Which path will you take", "Forwards", "Backwards", "Save", "Quit");
                        if (input == 0)
                            _mazeLocation = 2;
                        else if (input == 1)
                            _mazeLocation = 7;
                        else if (input == 2)
                            Save();
                        else if (input == 3)
                        {
                            _gameOver = true;
                            return;
                        }
                        break;
                    case 2:
                        input = GetInput("You walk ahead and come across a split path, you can continue forwards or head right.", "Forwards", "Right", "Back", "Save", "Quit");
                        if (input == 0)
                            _mazeLocation = 3;
                        else if (input == 1)
                        {
                            Console.WriteLine("You Stumbled across a dead end");
                            Console.ReadKey(true);
                        }
                        else if (input == 2)
                            _mazeLocation = 1;
                        else if (input == 3)
                            Save();
                        else if (input == 4)
                        {
                            _gameOver = true;
                            return;
                        }
                        break;
                    case 3:
                        input = GetInput("You continue forwards and find a intersection. Which way will you proceed", "Foward", "Left", "Right", "Back", "Save", "Quit"); 
                        if (input == 0)
                        {
                            Console.WriteLine("You Stumbled across a dead end");
                            Console.ReadKey(true);
                        }
                        else if (input == 1)
                            _mazeLocation = 4;
                        else if (input == 2)
                            _mazeLocation = 6;
                        else if (input == 3)
                            _mazeLocation = 2;
                        else if (input == 4)
                            Save();
                        else if (input == 5)
                        {
                            _gameOver = true;
                            return;
                        }
                        break;
                    case 4:
                        input = GetInput("You head left from the intersection and come across a left or right turn. Which way will you proceed?", "Left", "Right", "Back", "Save", "Quit");
                        if (input == 0)
                        {
                            Console.WriteLine("You Stumbled across a dead end");
                            Console.ReadKey(true);
                        }
                        else if (input == 1)
                            _mazeLocation = 5;
                        else if (input == 2)
                            _mazeLocation = 3;
                        else if (input == 3)
                            Save();
                        else if (input == 4)
                        {
                            _gameOver = true;
                            return;
                        }
                        break;
                    case 5:
                        input = GetInput("You come across a dead end, but there are some vines that you could climb up.", "Climb", "Go Back", "Save", "Quit");
                        if (input == 0)
                            _mazeLocation = -1;
                        else if (input == 1)
                            _mazeLocation = 4;
                        else if (input == 2)
                            Save();
                        else if (input == 3)
                        {
                            _gameOver = true;
                            return;
                        }
                        break;
                    case 6:
                        input = GetInput("You walk right from the intersection and come across a left or right turn. Which way will you proceed?", "Left", "Right", "Back", "Save", "Quit");
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
                            _mazeLocation = 3;
                        else if (input == 3)
                            Save();
                        else if (input == 4)
                        {
                            _gameOver = true;
                            return;
                        }
                        break;
                    case 7:
                        input = GetInput("You walk backwards to find an intersection. Which way will you proceed?", "Forwards", "Left", "Right", "Back","Save", "Quit");
                        if (input == 0)
                            _mazeLocation = 8;
                        else if (input == 1)
                        {
                            Console.WriteLine("You Stumbled across a dead end");
                            Console.ReadKey(true);
                        }
                        else if (input == 2)
                            _mazeLocation = 10;
                        else if (input == 3)
                            _mazeLocation = 1;
                        else if (input == 4)
                            Save();
                        else if (input == 5)
                        {
                            _gameOver = true;
                            return;
                        }
                        break;
                    case 8:
                        input = GetInput("You keep heading forwards and come across a left or right turn. Which way will you proceed?", "Left", "Right", "Back", "Save", "Quit");
                        if (input == 0)
                        {
                            Console.WriteLine("You Stumbled across a dead end");
                            Console.ReadKey(true);
                        }
                        else if (input == 1)
                            _mazeLocation = 9;
                        else if (input == 2)
                            _mazeLocation = 7;
                        else if (input == 3)
                            Save();
                        else if (input == 4)
                        {
                            _gameOver = true;
                            return;
                        }
                        break;
                    case 9:
                        input = GetInput("You reach a dead end, but there is a ladder that reaches up to the top.", "Climb", "Go Back", "Save", "Quit");
                        if (input == 0)
                            _mazeLocation = -1;
                        else if (input == 1)
                            _mazeLocation = 8;
                        else if (input == 2)
                            Save();
                        else if (input == 3)
                        {
                            _gameOver = true;
                            return;
                        }
                        break;
                    case 10:
                        input = GetInput("You head right from the intersection and and reach a fork in the road. You can keep going forwards, or go left.", "Forwards", "Left", "Back", "Save", "Quit");
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
                            _mazeLocation = 7;
                        else if (input == 3)
                            Save();
                        else if (input == 4)
                        {
                            _gameOver = true;
                            return;
                        }
                        break;
                }
            }

            //Finishes the room
            Console.Clear();
            Console.WriteLine("You climb up to the top and make your way to the door, where Aeos is waiting for you, waving.\n" +
                "'What took you so long? I was getting bored.' They joke.\n");
            Console.ReadKey(true);
            _currentScene = Scene.ROOM1BATTLE;
        }

        /// <summary>
        /// The battle with the Wind Shearer
        /// </summary>
        private void Room1Battle()
        {
            Console.WriteLine("You begin to head towards the next room, but a screech from below you echoes through the room.\n" +
                "A large avian creature arise from the depths of the pit, It lunges at you with tremendous force.");
            Console.ReadKey(true);
            //Sets the Area, Enemy, and Battle Scene
            _currentArea = 2;
            _currentEnemy = _bosses[0];
            _currentScene = Scene.BATTLE;
        }

        /// <summary>
        /// A function to display lever postition
        /// </summary>
        /// <param name="levers">The Levers</param>
        private void GetLeverPosistion(bool[] levers)
        {
            for(int i = 1; i <= 5; i++)
            {
                if (levers[i - 1])
                    //Display it as up
                    Console.WriteLine("Lever " + i + ": Up");
                else
                    //Display it as down
                    Console.WriteLine("Lever " + i + ": Down");
            }
        }

        /// <summary>
        /// The Second Room, consisting of a Lever Puzzle
        /// </summary>
        private void Room2()
        {
            Console.WriteLine("The Wind Shearer howls in agony while trying to stay airborne, before finally falling to the depths it emerged from.\n" +
                "'Wow, that was scary, but you sure are strong.' Aeos remarks.");
            Console.ReadKey(true);
            Console.Clear();

            Console.WriteLine("The two of you emerge into a second room. It is much smaller than the first, and doesn't seem to have an exit.\n" +
                "All you see before you are 5 switches\n" +
                "Aeos jumps from behind you and exclaims 'Wait, I know this room. This wall here is a locked door, " +
                "\na door that can only be openned by those levers." +
                "\nYou ask what combination opens the door, and they look embarrased and say: I don't actually know...'\n\n" +
                "You sigh and look at the levers...");
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
                bool[] levers = new bool[] { lever1, lever2, lever3, lever4, lever5 };
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
                GetLeverPosistion(levers);
                int input = GetInput("\nChoose a lever to pull", "Lever 1", "Lever 2", "Lever 3", "Lever 4", "Lever 5");
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
                //Completed the puzzle
                if (!lever1 && lever2 && lever3 && !lever4 && lever5)
                {
                    Console.Clear();
                    Console.WriteLine("The door opens and allows for further progress to be made.\n\n" +
                        "Aeos jumps with delight at your completion of the puzzle.\n" +
                        "'You did it!' They cheer.");
                    Console.ReadKey(true);
                    Console.Clear();

                    //Asks player if they want to enter the shop
                    input = GetInput("You go through the doorway and reach the other side.\n" +
                        "Where another strange door lies, but unlike the one you entered prior this door seems simple and normal.\n\nEnter?", "Yes", "No");
                    //If yes
                    if (input == 0)
                    {
                        //Saves Location
                        _currentArea = 3;
                        //Load shop
                        _currentShop = _firstShopInventory;
                        _shop = new Shop(_firstShopInventory);
                        _currentScene = Scene.SHOP;
                    }
                    //If no
                    else if(input == 1)
                    {
                        //Continue
                        Console.WriteLine("You decide to keep going, but you rest a bit to heal up.");
                        Console.ReadKey(true);
                        _player.HealDamage(999);
                        Console.Clear();
                        _currentScene = Scene.ROOM2BATTLE;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// The Void Ogre Battle
        /// </summary>
        private void VoidOgreBattle()
        {
            //Checks if player cleared puzzle
            //The player did
            if (_currentArea == 3)
            {
                Console.WriteLine("You make your way further in the dungeon. As you go along you hear loud banging and crashes in the background.");
                Console.ReadKey(true);
                Console.WriteLine("\nBut there getting closer...");
                Console.ReadKey(true);
            }
            //The player failed
            else
            {
                Console.WriteLine("The shaking intensifies tenfold, and the doorway explodes into rubble.");
                Console.ReadKey(true);
            }
            Console.WriteLine("\nYou are soon met with a terrifying sight. A massive beast ramages around the room and distorts the surrounding reality.");
            Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine("You are in for a tough battle!");
            Console.ReadKey(true);

            //Enter Boss Fight
            _currentArea = 4;
            _currentEnemy = _bosses[1];
            _currentScene = Scene.BATTLE;
        }

        /// <summary>
        /// Room 3, a long decending spiral staircase
        /// </summary>
        private void Room3()
        {
            if (_staircaseFloor < 31)
            {
                Console.WriteLine("The room spasm in a distorted fashion, forcing you to dodge rouge chunks.\n");
            Console.WriteLine("Eventually the Void Ogre fades, and the room anchors itself back to reality");
                Console.ReadKey(true);
                Console.Clear();

            Console.WriteLine("You and your companion enter in to the next room.\n" +
                "A deep, strange room lies before you. A spiral staircase leads seemingly all the way down, and several foes lie in wait.");
                Console.ReadKey(true);
                _currentScene = Scene.STAIRS;
            }
            else
            {
                _currentArea = 4;
                Console.WriteLine("You've finally reached the bottom of the staircase. You breathe out a sigh of relief\n" +
                "You look forwards at one final door. It's large frame seems to not fit into the rooms geography\n\n");
                int input = GetInput("WARNING: This is the final boss! Make sure to prepare yourself if you haven't!","Continue", "Fight Enemies", "Shop", "Save", "Quit");
                switch (input)
                {

                    case 0:
                        _currentScene = Scene.FINALBOSS;
                        break;
                    case 1:
                        int _encounter;
                        _encounter = new Random().Next(7, 9);
                        if (_encounter != 0)
                        {
                            _currentEnemy = _enemies[_encounter];
                            _currentScene = Scene.BATTLE;
                            InitializeEnemies();
                            return;
                        }
                        break;
                    case 2:
                        //Load shop
                        _shop = new Shop(_thirdShopInventory);
                        _currentShop = _thirdShopInventory;
                        _currentScene = Scene.SHOP;
                        break;
                    case 3:
                        Save();
                        break;
                    case 4:
                        _gameOver = true;
                        return;
                }
            }
        }

        /// <summary>
        /// The Long Decending Spiral Staircase
        /// </summary>
        private void Staircase()
        {
            if (_staircaseFloor == 31)
                _currentScene = Scene.ROOM3;
            Console.Clear();
            //Sets area for encounters
            _currentArea = 5;
            //The first ten floors of stairs
            while (_staircaseFloor < 10)
            {
                int input = GetInput("The Staircase shows no in in sight", "Continue Further", "Shop", "Save", "Quit");
                switch (input)
                {
                    //Player goes further down
                    case 0:
                        //Increments the floor
                        _staircaseFloor++;
                        //Rolls for _encounter
                        _encounter = new Random().Next(0, 3);
                        //Determines _encounter
                        if (_encounter != 0)
                        {
                            _currentEnemy = _enemies[_encounter];
                            _currentScene = Scene.BATTLE;
                            InitializeEnemies();
                        }
                        return;
                    //Player loads shop
                    case 1:
                        //Load shop
                        _shop = new Shop(_firstShopInventory);
                        _currentShop = _firstShopInventory;
                        _currentScene = Scene.SHOP;
                        return;
                    //Player Saves
                    case 2:
                        Save();
                        return;
                    //Player Quits the Game
                    case 3:
                        _gameOver = true;
                        return;
                }
            }
            while (_staircaseFloor >= 10 && _staircaseFloor < 20)
            {
                int input = GetInput("You've traveled a long way, but there are still more stairs...", "Continue Further", "Shop", "Save", "Quit");
                if(_staircaseFloor == 10)
                {
                    Console.WriteLine("New items have been added to the Shop!");
                }
                switch (input)
                {
                    //Player goes further down
                    case 0:
                        //Increments the floor
                        _staircaseFloor++;
                        //Rolls for _encounter
                        _encounter = new Random().Next(0,3);
                        //Determines _encounter
                        if (_encounter != 0)
                        {
                            _currentEnemy = _enemies[_encounter + 3];
                            _currentScene = Scene.BATTLE;
                            InitializeEnemies();
                        }
                        return;
                    //Player loads shop
                    case 1:
                        //Load shop
                        _shop = new Shop(_secondShopInventory);
                        _currentShop = _secondShopInventory;
                        _currentScene = Scene.SHOP;
                        return;
                    //Player Saves
                    case 2:
                        Save();
                        return;
                    //Player Quits the Game
                    case 3:
                        _gameOver = true;
                        return;
                }
            }

            while (_staircaseFloor >= 20 && _staircaseFloor <= 30)
            {
                int input = GetInput("You've finally see the bottom! But there is still a ways to go...", "Continue Further", "Shop", "Save", "Quit");
                if (_staircaseFloor == 20)
                {
                    Console.WriteLine("New items have been added to the Shop!");
                }
                switch (input)
                {
                    //Player goes further down
                    case 0:
                    //Increments the floor
                    _staircaseFloor++;
                        //Rolls for encounters
                        _encounter = new Random().Next(0, 3);
                        //Determines _encounter
                        if (_encounter != 0)
                        {
                            _currentEnemy = _enemies[_encounter + 6];
                            _currentScene = Scene.BATTLE;
                            InitializeEnemies();
                        }
                        return;
                    //Player loads shop
                    case 1:
                        //Load shop
                        _shop = new Shop(_thirdShopInventory);
                        _currentShop = _thirdShopInventory;
                        _currentScene = Scene.SHOP;
                        return;
                    //Player Saves
                    case 2:
                        Save();
                        return;
                    //Player Quits the Game
                    case 3:
                        _gameOver = true;
                        return;
                }
            }
        }

        /// <summary>
        /// The final boss, The Dungeon Core
        /// </summary>
        private void FinalBoss()
        {
            _finalBattle = true;
            if (!_finalBossPhaseTwo)
            {
                Console.WriteLine("You enter into the room. A powerful force pushes you back, and a faint ringing can be heard in your ears.\n" +
                    "The ringing becomes unbearable as you are faced with its source:");
                Console.ReadKey(true);
                Console.WriteLine("\nA Beating Mechanical Heart");
                Console.ReadKey(true);
                Console.WriteLine("'\n\nThats the Dungeon's Core.' Aeos speaks, seamingly uneffected by its power.\n" + 
                    "'That is what controls everything about this dungeon, from its contraptions to its inhabitants." +
                    "\nIts the last obsticale on your journey.'" +
                    "\nIf what they're saying is true, you have to defeat this thing to clear the dungeon.");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("This is your final challenge!");
                Console.ReadKey(true);

                //Enter Boss Fight
                _currentArea = 6;
                _currentEnemy = _bosses[2];
                _finalBossPhaseTwo = true;
                _currentScene = Scene.BATTLE;
            }
            else
            {
                Console.WriteLine("The Dungeon Core cracks as a loud shriek echoes through the room.\n");
                Console.ReadKey(true);
                Console.WriteLine("The room finally goes silent, as the heart shatters, reaveling a small child inside.\n" +
                    "The child looks just like your companion Aeos." +
                    "You turn to look at your companion, and they look rather emotionless.\n");
                Console.ReadKey(true);
                Console.WriteLine("The room still stays silent but the two Aeos stare down each other, standing stern.\n");
                Console.WriteLine("You stand in the middle of the confusion wondering what's going on." +
                    "Your companion finally speaks: 'I finally understand... They are the ruler of this dungeon, they are also Aeos, and older version of me. One that I was meant to replace.'");
                Console.WriteLine("\nThe other Aeos grows angry and starts to growl.\n" +
                    "But despite this, Aeos conntinues on. 'But he didn't want to be replaced, so he tried his hardest to keep me out.'" +
                    "\n'You have to strike him down before they lose more of themself and go on a rampage!");
                Console.ReadKey(true);
                _currentEnemy = _bosses[3];
                _currentArea = 7;
            }

            
        }

        /// <summary>
        /// The End Cutscene
        /// </summary>
        private void Ending()
        {
            Console.WriteLine("The opposing Aeos finally falls to their knees, defeated.");
            Console.ReadKey(true);
            Console.WriteLine("\nThe other Aeos walks up to his defeated other. 'Its over.'\n\n");
            Console.ReadKey(true);
            Console.WriteLine("'No! I can't lose' your foe screams. 'I don't want to go.'");
            Console.ReadKey(true);
            Console.WriteLine("\n'I'm sorry. But we don't last forever. Now, you know what has to happen.'");
            Console.ReadKey(true);
            Console.WriteLine("Aeos give his hand to his other, and the other took it, finally disapearing.\n" + 
                "'Its over, I'm now the new ruler of the dungeon. Thank you, now everything is right again.\n" +
                "You can move freely through this dungeon, I have no intention of stoping you.");
            Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine("You cleared the dungeon!");
            _currentScene = Scene.RESTARTMENU;
        }

        /// <summary>
        /// Gets the items that will be shown in the menu, and adds saving and exiting
        /// </summary>
        /// <returns>The options that will be displayed in the shop menu</returns>
        private string[] GetShopMenuOptions(Item[] ShopInventory)
        {
            //Creates a new string array two longer that the shopInventory array
            string[] itemNames = new string[ShopInventory.Length + 2];

            //Copies the names of the items
            for (int i = 0; i < ShopInventory.Length; i++)
            {
                itemNames[i] = ShopInventory[i].Name + ": " + ShopInventory[i].Cost + " Gold";
            }

            //Adds Save and Exit and returns the finished array
            itemNames[ShopInventory.Length] = "Save";
            itemNames[ShopInventory.Length + 1] = "Exit";
            return itemNames;
        }

        /// <summary>
        /// The Shop Menu
        /// </summary>
        private void DisplayShopMenu(Item[] ShopInventory)
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
            int input = GetInput("\nWhat would you like to puchase?", GetShopMenuOptions(ShopInventory));

            //if player selected an item
            if (input < ShopInventory.Length && input >= 0)
            {
                //Give player choice to buy or Check
                int choice = GetInput("\nWhat would you like to do?", "Buy", "Check");
                //IF player buys
                if (choice == 0)
                {
                    //Check if player can perform transaction
                    if (_shop.Sell(_player, input))
                        _player.Buy(ShopInventory[input]);
                }
                //If player checks
                else if (choice == 1)
                {
                    //Display Item's Stats
                    DisplayItemStats(ShopInventory[input]);
                    Console.ReadKey(true);
                }
            } 

            //If player selected save
            else if (input == ShopInventory.Length)
            {
                Console.WriteLine("Saved.");
            }

            //If player selects leave
            else if (input == ShopInventory.Length + 1)
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
                CheckLocation();
            }
            
        }

        /// <summary>
        /// The Save Function
        /// </summary>
        private void Save()
        {
            //Creates a new stream writer
            StreamWriter writer = new StreamWriter("SaveData.txt");

            //Saves current enemy Scene
            writer.WriteLine(_currentScene);

            //Save current last area
            writer.WriteLine(_currentArea);

            //Saves locations in the maze and staircase
            writer.WriteLine(_mazeLocation);
            writer.WriteLine(_staircaseFloor);

            //Saves player and Enemy data
            _player.Save(writer);

            //if in battle
            if (_currentScene == Scene.BATTLE)
            {
                //Save current enemy
                _currentEnemy.Save(writer);
                writer.WriteLine(_finalBattle);
            }
            //Closes after finishing
            writer.Close();

            Console.WriteLine("Saved Game");
        }

        /// <summary>
        /// The Load Function
        /// </summary>
        /// <returns></returns>
        private bool Load()
        {
            bool loadSuccessful = true;
            //File doesn't exist
            if (!File.Exists("SaveData.txt"))
                return false;

            //Creates new reader
            StreamReader reader = new StreamReader("SaveData.txt");

            //If the first line can't be converted into an int
            string _sceneName = reader.ReadLine();
            LoadCurrentScene(_sceneName);

            //Returns current area
            if (!int.TryParse(reader.ReadLine(), out _currentArea))
                loadSuccessful = false;

            //Returns Maze Loaction
            if (!int.TryParse(reader.ReadLine(), out _mazeLocation))
                loadSuccessful = false;

            //Return Staircase Level
            if (!int.TryParse(reader.ReadLine(), out _staircaseFloor))
                loadSuccessful = false;

            //Loads the player
            if (!_player.Load(reader))
                loadSuccessful = false;

            //if in battle
            if (_currentScene == Scene.BATTLE)
            {
                //Load current enemy
                _currentEnemy = new Entity();
                _currentEnemy.Load(reader);
                if (!bool.TryParse(reader.ReadLine(), out _finalBattle))
                    loadSuccessful = false;
            }
            
            //if shopping
            else if (_currentScene == Scene.SHOP)
            {
                if (_currentArea == 2)
                    _currentShop = _firstShopInventory;
                else if (_currentArea == 3)
                    _currentShop = _thirdShopInventory;
                else if (_currentArea == 4)
                {
                    if (_staircaseFloor < 10)
                        _currentShop = _firstShopInventory;
                    else if (_staircaseFloor >= 10 && _staircaseFloor < 20)
                        _currentShop = _secondShopInventory;

                }
                    
            }
            if (!loadSuccessful)
            _currentScene = Scene.STARTMENU;
            reader.Close();
            return loadSuccessful;
        }

        /// <summary>
        /// Intializes the game at the start of the game
        /// </summary>
        private void Start()
        {
            _gameOver = false;
            _player = new Player();
            InitializeItems();
            InitializeEnemies();
        }

        /// <summary>
        /// This function is called every time the game loops.
        /// </summary>
        private void Update()
        {
            DisplayCurrentScene();
            Console.Clear();
        }

        /// <summary>
        /// This function is called before the applications closes
        /// </summary>
        private void End()
        {
            Console.WriteLine("Farewell... Coward.");
        }

        /// <summary>
        /// The Run Function
        /// </summary>
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

