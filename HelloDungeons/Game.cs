using System;
using System.Collections.Generic;
using System.Text;

namespace HelloDungeons
{
    class Game
    {
        public void Run()
        {
            //Defining and Initionaliziation
            string charaterName = "Player";
            string sideCharaterName = "Aeos";
            string className = "None";
            string weaponName = "None";
            int weaponDamage = 0;
            int areaNumber = 1;
            int playerHealth = 100;
            int assisantHealth = 100;
            int assisantStrength = 3;
            int strength = 5;
            int attempts = 5;
            bool gameOver = false;
            float points = 0.0f;
            string input = "input";
            bool validInput = false;
            float damage = 0.0f;

            //Game Over Check
            if (playerHealth <= 0)
            {
                gameOver = true;
            }

            if (gameOver == true)
            {
                Console.Clear();
            }

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
                input = Console.ReadLine();
                if (input.ToLower() == "yes"|| input.ToLower() == "y")
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
           
            //This is a joke option
            else if (charaterName.ToLower() == "cowboy beard")
            {
                Console.WriteLine("You win!");
                Console.ReadKey();
                Console.WriteLine("Not really, thats just a in-joke.");
                Console.WriteLine("Anyways, I am to be your assistant throughout the Aeos Dungeon, I am also named Aeos.");
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
            sideCharaterName = Console.ReadLine();
           
            //Name the Assisstant

            //Keep name
            if (sideCharaterName.ToLower() == "aeos" || sideCharaterName.ToLower() == "")
            {
                Console.WriteLine();
                Console.WriteLine("Oh, ok. I do rather like my name.");
                sideCharaterName = "Aeos";
                if (charaterName.ToLower() == "aeos")

                {
                    Console.WriteLine("But then what should we do about our shared name? \nI'll call you just player from now on, ok!");
                    Console.Write(">");
                    input = Console.ReadLine();
                    if (input.ToLower() == "ok" || input.ToLower() == "sure" || input.ToLower() == "yes" || input.ToLower() == "y")
                    {
                        charaterName = "Player";    
                    }
                    else if (input.ToLower() == "no" || input.ToLower() == "n" || input.ToLower() == "don't")
                    {
                        Console.WriteLine("'Well then you will have to change my name.'");
                        while (validInput == false)
                        {
                            Console.Write(">");
                            input = Console.ReadLine();
                            if (input.ToLower() == "aeos")
                            {
                                Console.WriteLine("'I told you we can't have the same name, It'll get confusing");
                                
                            }
                            else
                            {
                                sideCharaterName = input;
                                validInput = true;
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }
            //Change name
            else
            {
                Console.WriteLine();
                Console.WriteLine("'" + sideCharaterName + "? I suppose it would take some time to get used to that name...\n Apologies, " + charaterName + ", it is time to begin your adventure.'");
                Console.ReadKey();
            }
            Console.WriteLine("'So, who exactly are you?' They ask.\n" +
                "Well obviously your " + charaterName + ", but what are you, like what do you do?");
            validInput = false;
            while (validInput == false)
            {
                Console.WriteLine("Choose a class:\n" +
                    "1. Warrior\n" +
                    "2. Archer \n" +
                    "3. Tank");
                Console.Write(">");
                input = Console.ReadLine();
                if (input == "1" || input.ToLower() == "warrior")
                {
                    Console.WriteLine("Warrior HP: 75\n" +
                                      "Warrior Strength:40\n" +
                                      "Warrior Weapon: Promising Sword - 10 Dmg");
                    Console.WriteLine("Do you want to proceed as a Warrior?");
                    Console.Write(">");
                    input = Console.ReadLine();
                    if (input == "y" || input.ToLower() == "yes")
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
                else if (input == "2" || input.ToLower() == "archer")
                {
                    Console.WriteLine("Archer HP: 90\n" +
                                      "Archer Strength:25\n" +
                                      "Archer Weapon: Promising Bow - 7 Dmg");
                    Console.WriteLine("Do you want to proceed as an Archer?");
                    Console.Write(">");
                    input = Console.ReadLine();
                    if (input == "y" || input.ToLower() == "yes")
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
                else if (input == "3" || input.ToLower() == "tank")
                {
                    Console.WriteLine("Tank HP: 150\n" +
                                      "Tank Strength:10\n" +
                                      "Tank Weapon: Promising Axe - 12 Dmg");
                    Console.WriteLine("Do you want to proceed as a Tank?");
                    Console.Write(">");
                    input = Console.ReadLine();
                    if (input == "y" || input.ToLower() == "yes")
                    {
                        playerHealth = 150;
                        strength = 10;
                        className = "Tank";
                        weaponName = "Promising Axe";
                        weaponDamage = 12;
                        Console.WriteLine("'Oh, a Tank. Neat.'");
                        validInput = true;
                    }
                    else
                    {

                    }
                }
                else if (input.ToLower() == "no")
                {
                    Console.WriteLine("Oh a rebel huh, thats fine.");
                    Console.WriteLine("Class changed to Rebel.");
                    Console.WriteLine("Rebel HP: 1\n" +
                                  "Rebel Strength:1\n" +
                                  "Tank Weapon: Rebel's Audacity - 100 Dmg");
                    playerHealth = 1;
                    strength = 1;
                    className = "Rebel";
                    weaponName = "Rebel's Audacity";
                    weaponDamage = 100;
                    validInput = true;
                }
                else Console.WriteLine("invalid input");
            }
            Console.WriteLine(className);
                Console.ReadKey();
            Console.Clear();
            validInput = false;
            while (validInput == false)
            {
                Console.Clear();
                Console.WriteLine("HP:" + playerHealth + "\n");
                Console.WriteLine("The two of you enter a strange room. It's dark and difficult to see, but a dim light cracks through the door on the other side. \n" +
                    "Walk towards the door?");
                Console.Write(">");
                input = Console.ReadLine();
                if (input.ToLower() == "yes" || input.ToLower() == "y")
                {
                    validInput = true;
                    Console.WriteLine("\n You walk forward, despite your lack of vision. \n" +
                        "This proves to be a problem as you lose your footing only a few steps in and fall deep into a large pit.");
                    playerHealth -= 75;
                    Console.WriteLine("You took 75 Damage. \n" +
                        "You have " + playerHealth + " HP remaining.");
                    if (playerHealth <= 0)
                    {
                        gameOver = true;
                    }
                    else { }
                    if (gameOver)
                    {
                        Console.WriteLine("You died. What a loser.");
                        break;
                    }
                    Console.WriteLine("'Hey, are you okay?' " + sideCharaterName + " calls to you.\n");
                    Console.ReadKey();
                    Console.WriteLine("Suddenly, the dim room lights up, revealing a maze-like bridge.\n");
                    Console.WriteLine("'I found the lights! They were next to the door!' " + sideCharaterName + " shouts.");
                    Console.WriteLine("'There might be a way up further on in the maze! \n" +
                        "I'll cross up top and meet you on the other side.'\n");
                    Console.ReadKey();
                    Console.WriteLine("You look ahead at the towering wall ahead, you can either go left or right.\n" +
                        "Which way will you go?");
                    input = Console.ReadLine();
                    if (input.ToLower() == "left" || input.ToLower() == "l")
                    {
                        Console.WriteLine("You head left, and quickly hit a turn right.\n" +
                            "go back or right?");
                    }
                }
                else if (input.ToLower() == "no" || input.ToLower() == "n")
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
                Console.WriteLine("You enter a small room with a tightly sealed door infront of you.\n\n" +
                    sideCharaterName + " perks up, exclaming 'Oh, I know this puzzle, I think.\n" +
                    "Anyways you have to guess the correct order of the switches to open the doors ahead!'\n" +
                    sideCharaterName + " is right, as you see five levers to your right all in random order.\n" +
                    "Be careful, you only get 5 tries");
                Console.Clear();
                for (int i=0; i<=attempts; i++)
                {
                    int attemptsRemain = attempts - i;
                    Console.WriteLine("Careful, you only have " + attemptsRemain + " attempts remaining");


                }
            }
        }
    }
}
