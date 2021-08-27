using System;
using System.Collections.Generic;
using System.Text;

namespace HelloDungeons
{
    class Game
    {
    //Defining and Initionaliziation
    string charaterName = "Player";
    string sideCharaterName = "Aeos";
    string className = "None";
    string weaponName = "None";
    int currentArea = 0;
    int weaponDamage = 0;
    int charLevel = 1;
    int playerHealth = 100;
    int assisantHealth = 100;
    int assisantStrength = 3;
    int strength = 5;
    int attempts = 5;
    float exp = 0.0f;
    string stringInput = "input";
    bool validInput = false;
    bool gameOver = false;
    float damage = 0.0f;
    int input = 0;


    //Pull Stats
    string OpenStatMenu()
    {
    if (stringInput == "stats")
    {
    Console.WriteLine(charaterName + "'s Stats");
    Console.WriteLine("HP: " + playerHealth);
    Console.WriteLine("Class: " + className);
    Console.WriteLine("Level: " + charLevel);
    Console.WriteLine("Exp: " + exp);
    Console.WriteLine();
    Console.WriteLine("Weapon: " + weaponName);
    Console.WriteLine("Damage: " + weaponDamage);
    Console.WriteLine("Strength: " + strength);
    }
    return "null";
    }
    /// <summary>
    /// If game is over.
    /// </summary>
    void GameOverCheck()
    {
    int input = GetInput("Would you like to play again?", "Yes", "No");
    if (input == 1)
    {
    gameOver = false;
    currentArea = 1;
    }
    else if (input == 2)
    {
    gameOver = true;
    }
    }
    /// <summary>
    /// Two Options Choices
    /// </summary>
    /// <param name="description">Event</param>
    /// <param name="option1">First Option</param>
    /// <param name="option2">Second Option</param>
    /// <returns>Option Chosen</returns>
    int GetInput(string description, string option1, string option2)
    {
    stringInput = "";
    input = 0;
    while (!(stringInput == "1" || stringInput == "2"))
    {
    //Option Menu
    Console.WriteLine(description + "\n" +
    "1." + option1 + "\n" +
    "2." + option2 + "\n");
    Console.Write(">");
    stringInput = Console.ReadLine();
    if (stringInput == "1" || stringInput.ToLower() == option1)
    {
    return 1;
    }
    else if (stringInput == "2" || stringInput.ToLower() == option2)
    {
    return 2;
    }
    }
    return 0;
    }

    //Three Option Selection
    int GetInputThreeOptions(string description, string option1, string option2, string option3)
    {
    stringInput = "";
    input = 0;
    while (!(stringInput == "1" || stringInput == "2" || stringInput == "3"))
    {   
    Console.WriteLine(description + "\n" +
    "1." + option1 + "\n" +
    "2." + option2 + "\n" +
    "3." + option3 + "\n");
    Console.Write(">");
    stringInput = Console.ReadLine();
    if (stringInput == "1" || stringInput == option1)
    {
    return 1;
    }
    else if (stringInput == "2" || stringInput == option2)
    {
    return 2;
    }
    else if (stringInput == "3" || stringInput == option3)
    {
    return 3;
    }
    }
    return 0;
    }

    void MazeLocation()
    {

    }
    void StartScreen()
    {
    //Start Screen and Charater Creation
    Console.WriteLine("Enter your Name below.");
    Console.Write(">");
    charaterName = Console.ReadLine();
    Console.WriteLine("\n");

    //Name Check Choices

    //Player chose Aeos as Name
    if (charaterName.ToLower() == "aeos")
    {
    Console.WriteLine("'Wow, small world, huh. My name is also Aeos, as is this Dungeon's name.\n'");
    }

    //Player chose Player or Name as their name
    else if (charaterName.ToLower() == "player" || charaterName.ToLower() == "name")
    {
    Console.WriteLine("'Oh, ok then. I guess not everyone is okay giving their names to strangers.\n'");
    Console.WriteLine("'I am to be your assistant throughout the Aeos Dungeon, I am also named Aeos.'");
    }

    //Player just types a
    else if (charaterName.ToLower() == "a")
    {
    Console.WriteLine("'Um... hello " + charaterName + ". Has anyone told you that was a weird name.\n'");
    Console.WriteLine("'Anyways, I am to be your assistant throughout the Aeos Dungeon, I am also named Aeos.'");
    }

    //Player types nothing
    else if (charaterName.ToLower() == "")
    {
    Console.WriteLine("'Uh... do you have a name?'");
    Console.ReadKey();
    Console.WriteLine("'I need to call you something...'");
    Console.WriteLine("'Would just 'player' suffice?'");
    Console.WriteLine("Yes or No");
    Console.Write(">");
    stringInput = Console.ReadLine();
    if (stringInput.ToLower() == "yes" || stringInput.ToLower() == "y")
    {
    charaterName = "player";
    }
    else
    {
    Console.WriteLine("'Then what should you be called?'");
    Console.Write(">");
    charaterName = Console.ReadLine();
    Console.WriteLine("Hello " + charaterName + ", and Welcome to the Aeos Dungeon.\n");
    Console.WriteLine("I am to be your assistant throughout this Dungeon, I am also named Aeos.");
    }
    }
    //If the player chooses 'your name'.
    else if (charaterName.ToLower() == "your name")
    {
    Console.WriteLine("'Oh, its one of you... You know what '" + charaterName + "' I won't let you get your way, so have fun with your name from now on.\n");
    Console.WriteLine("'Anyways, I am to be your assistant throughout the Aeos Dungeon, I am also named Aeos.'");
    }

    //Player chooses any other name
    else
    {
    Console.WriteLine("Hello " + charaterName + ", and Welcome to the Aeos Dungeon.\n");
    Console.WriteLine("I am to be your assistant throughout this Dungeon, I am also named Aeos.");
    }
    Console.WriteLine("As you can guess, I was named after this Dungeon, or was it named after me?\n");
    Console.ReadKey();
    Console.WriteLine("Oh, but if you wish to refer to me as something else, you can.\n");
    Console.Write(">");
    sideCharaterName = Console.ReadLine();

    //Name the Assisstant

    //Keep assisstant name
    if (sideCharaterName.ToLower() == "aeos" || sideCharaterName.ToLower() == "")
    {
    Console.WriteLine();
    Console.WriteLine("Oh, ok. I do rather like my name.");
    sideCharaterName = "Aeos";
    if (charaterName.ToLower() == "aeos")

    {
    Console.WriteLine("But then what should we do about our shared name? \nI'll call you just player from now on, ok!");
    Console.Write(">");
    stringInput = Console.ReadLine();
    if (stringInput.ToLower() == "ok" || stringInput.ToLower() == "sure" ||
    stringInput.ToLower() == "yes" || stringInput.ToLower() == "y")
    {
    charaterName = "Player";
    }
    else if (stringInput.ToLower() == "no" || stringInput.ToLower() == "n" || stringInput.ToLower() == "don't")
    {
    Console.WriteLine("'Well then you will have to change my name.'");
    while (validInput == false)
    {
    Console.Write(">");
    stringInput = Console.ReadLine();
    if (stringInput.ToLower() == "aeos")
    {
    Console.WriteLine("'I told you we can't have the same name, It'll get confusing");

    }
    else
    {
    sideCharaterName = stringInput;
    validInput = true;
    }
    }
    }
    else
    {

    }
    }
    }
    //Changed assistant name
    else
    {
    Console.WriteLine();
    Console.WriteLine("'" + sideCharaterName + "? I suppose it would take some time to get used to that name...\n" +
    "Apologies, " + charaterName + ", it is time to begin your adventure.'");
    Console.ReadKey();
    }
    Console.ReadKey();
    while (validInput == false)
    {
    //Choose Class
    Console.Clear();
    input = GetInputThreeOptions("'So, who exactly are you?' They ask.\n" +
    "Well obviously your " + charaterName + ", but what are you, like what do you do?",
    "1. Warrior", "2. Archer", "3. Tank");

    //Choose Warrior
    if (input == 1 )
    {
    Console.WriteLine("Warrior HP: 75\n" +
    "Warrior Strength:40\n" +
    "Warrior Weapon: Promising Sword - 10 Dmg");
    Console.WriteLine("Do you want to proceed as a Warrior?");
    Console.Write(">");
    stringInput = Console.ReadLine();
    if (stringInput == "y" || stringInput.ToLower() == "yes")
    {
    playerHealth = 75;
    strength = 40;
    className = "Warrior";
    weaponName = "Promising Sword";
    weaponDamage = 10;
    Console.WriteLine("'Oh, a Warrior. Neat.'");
    validInput = true;

    }
    else
    {

    }
    }
    //Choose Archer
    else if (input == 2)
    {
    Console.WriteLine("Archer HP: 90\n" +
    "Archer Strength:25\n" +
    "Archer Weapon: Promising Bow - 7 Dmg");
    Console.WriteLine("Do you want to proceed as an Archer?");
    Console.Write(">");
    stringInput = Console.ReadLine();
    if (stringInput == "y" || stringInput.ToLower() == "yes")
    {
    playerHealth = 90;
    strength = 25;
    className = "Archer";
    weaponName = "Promising Bow";
    weaponDamage = 7;
    Console.WriteLine("'Oh, a ranger. Neat.'");
    validInput = true;
    }
    else
    {

    }
    }
    //Choose Tank
    else if (input == 3)
    {
    Console.WriteLine("Tank HP: 150\n" +
    "Tank Strength:10\n" +
    "Tank Weapon: Promising Axe - 12 Dmg");
    Console.WriteLine("Do you want to proceed as a Tank?");
    Console.Write(">");
    stringInput = Console.ReadLine();
    if (stringInput == "y" || stringInput.ToLower() == "yes")
    {
    playerHealth = 150;
    strength = 10;
    className = "Tank";
    weaponName = "Promising Axe";
    weaponDamage = 12;
    Console.WriteLine("'Oh, a Tank. Neat.'");
    validInput = true;
    }
    }
    else Console.WriteLine("invalid input");
    }
    }


    //Room 1
    void Room1()
    {
    //Intro
    Console.Clear();
    input = GetInput("The two of you enter a strange room. It's dark and difficult to see, but a dim light cracks through the "
    + "door on the other side. \n Walk towards the door?", "Yes", "No");

    //Player goes forward, and falls
    if (input == 1)
    {
    Console.WriteLine("\n You walk forward, despite your lack of vision. \n" +
    "This proves to be a problem as you lose your footing only a few steps in and fall deep into a large pit.");
    playerHealth -= 50;
    Console.WriteLine("You took 50 Damage. \n" +
    "You have " + playerHealth + " HP remaining.");
    Console.WriteLine("'Hey, are you okay?' " + sideCharaterName + " calls to you.\n");
    Console.ReadKey();
    Console.WriteLine("Suddenly, the dim room lights up, revealing a maze-like bridge.\n");
    Console.WriteLine("'I found the lights! They were next to the door!' " + sideCharaterName + " shouts.");
    Console.WriteLine("'There might be a way up further on in the maze! \n" +
    "I'll cross up top and meet you on the other side.'\n");
    Console.ReadKey();
    Console.Clear();
    
    
    //Enter Maze
    //Maze Path = Left, Right, Forward
    int mazeLoction = 0;
    void MazeLocater()
        {
        while (mazeLoction != -1)
            {
            if (mazeLoction == 0)
                {
                    input = GetInput("You look ahead at the towering wall ahead, you can either go left or right.\n" +
                    "Which way will you go?", "Left", "Right");
                    if (input == 1)
                    {
                        mazeLoction = 1;
                    }
                    if (input == 2)
                    {
    
                    }
                }
            }
        }   
    input = GetInput("You look ahead at the towering wall ahead, you can either go left or right.\n" +
    "Which way will you go?", "Left", "Right");
    if (input == 1)
    {
    
    input = GetInput("You head left, and quickly hit a right turn.\n", "Right", "Back");
    if (input == 1)
    {
    
    input = GetInputThreeOptions("You take the path ahead, finally ending at a fork in the road, to continue straight," +
    "or take a right?\n", "Forward", "Right", "Back");
    if (input == 1)
    {
    input = GetInput("The path finishes with a large stretch of vines thick enough to climb on.\n" +
    "Climb up?", "Yes", "No");
    if (input == 1)
    {

    Console.WriteLine("You climb your way to the top, and you see " +sideCharaterName + "waiting for you.\n" +
        "'Hello.' They say, waving at you. 'You sure took your time, huh.' While their tone is annoyed, they have an innocent smile on their face.");
    
    }
    else if (input == 2)
    {
   
    }
        
    }
    else if (input == 2)
    {
    Console.WriteLine("The road leads into a dead end.");
    Console.ReadKey();
    Console.Clear();
    Console.Clear();    
    }
    else if (input == 3)
    {

    }
    
    }
    else if (input == 2)
    {
   
    }
    
    }
    else if (input == 2)
    {

    }
    
    }
    else if (input == 2)
    {
    validInput = true;
    }
    else
    {
    Console.WriteLine("Invalid input.");
    Console.ReadKey();
    Console.Clear();
    }
    Console.Clear();
    }

    void Room2()
    {
    Console.WriteLine("You enter a small room with a tightly sealed door infront of you.\n\n" +
    sideCharaterName + " perks up, exclaming 'Oh, I know this puzzle, I think.\n" +
    "Anyways you have to guess the correct order of the switches to open the doors ahead!'\n" +
    sideCharaterName + " is right, as you see five levers to your right all in random positions labeled 1 to 5.\n" +
    "Be careful, you only get 5 tries");
    Console.ReadKey();
    Console.Clear();
        for (int i = 0; i <= attempts; i++)
        {
        //Using Bools to change varables and write if statements easier
        //True = Closed - False = Open
        bool door1 = true;
        bool door2 = false;
        bool door3 = true;
        bool door4 = true;
        bool door5 = false;
        int attemptsRemain = attempts - i;
        bool eventComplete = false;
        if (door1 == false && door2 == false && door3 == false && door4 == false && door5 == false)
        {
        eventComplete = true;
        }
            if (eventComplete == false)
            {
                Console.WriteLine("Careful, you only have " + attemptsRemain + " attempts remaining");
                if (door1)
                {
                Console.WriteLine("Door 1: Closed");
                }
                else
                {
                Console.WriteLine("Door 1: Open");
                }
                if (door2)
                {
                Console.WriteLine("Door 2: Closed");
                }
                else
                {
                Console.WriteLine("Door 2: Open");
                }
                if (door3)
                {
                Console.WriteLine("Door 3: Closed");
                }
                else
                {
                Console.WriteLine("Door 3: Open");
                }
                if (door4)
                {
                Console.WriteLine("Door 4: Closed");
                }
                else
                {
                Console.WriteLine("Door 4: Open");
                }
                if (door5)
                {
                Console.WriteLine("Door 5: Closed");
                }
                else
                {
                Console.WriteLine("Door 5: Open");
                }
            Console.WriteLine("\nPull one of Levers.");
            Console.Write(">");
            Console.ReadLine();
            }
            else
            {
            break;
            }
        }
    }
        public void Run()
        {
            while (gameOver == false)
            {
            StartScreen();
            Room1();
            Room2();

                if (gameOver == true || currentArea == 3)
                {
                    
                }
            }
        }
    }
}

