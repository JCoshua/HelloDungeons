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
            string assitantName = "Aeos";
            int areaNumber = 1;
            int playerHealth = 20;
            int strength = 1;
            bool gameOver = false;
            float points = 0.0f;

            //Start Screen and Charater Creation
            Console.WriteLine("Enter your Name below.");
            Console.Write(">");
            charaterName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();

            //Name Check Choices
            //Player chose Aeos as Name
            if (charaterName == "Aeos")
            {
                Console.WriteLine("Wow, small world, huh. My name is also Aeos, as is this Dungeon's name.");
                Console.WriteLine();
            }
            //Player chose Player or Name as their name
            else if (charaterName == "Player" || charaterName == "Name")
            {
                Console.WriteLine("Oh, ok then. I guess not everyone is okay giving their names.");
                Console.WriteLine();
                Console.WriteLine("I am to be your assistant throughout the Aeos Dungeon, I am also named Aeos.");
            }
            //Player just types a
            else if (charaterName == "a")
            {
                Console.WriteLine("Um... hello " + charaterName + ". Has anyone told you that was a weird name.");
                Console.WriteLine();
                Console.WriteLine("Anyways, I am to be your assistant throughout the Aeos Dungeon, I am also named Aeos.");
            }
            //Player types nothing
            else if (charaterName == "")
            {
                Console.WriteLine("Uh... do you have a name?");
                Console.WriteLine("I need to call you something...");
                Console.WriteLine("Would 'player' suffice?");
                Console.WriteLine("Yes or No");
                Console.Write(">");
            }
            //Ignore this
            else if (charaterName == "cowboy beard")
            {
                Console.WriteLine("You win, not really, thats just a in-joke.");
                Console.WriteLine("Anyways, I am to be your assistant throughout the Aeos Dungeon, I am also named Aeos.");
            }
            //
            else if (charaterName == "your Name")
            {
                Console.WriteLine("Oh, its one of you... You know what '" + charaterName + "' I won't let you get your way, so have fun with your name from now on.\n");
                Console.WriteLine("Anyways, I am to be your assistant throughout the Aeos Dungeon, I am also named Aeos.");
            }
            else
            {
                Console.WriteLine("Hello " + charaterName + ", and Welcome to the Aeos Dungeon.\n");
                Console.WriteLine("I am to be your assistant throughout this Dungeon, I am also named Aeos.");
            }
            Console.WriteLine("As you can guess, I was named after this Dungeon, or was it named after me?\n");
            Console.WriteLine("Oh, but if you wish to refer to me as something else, you can.\n");
            assitantName = Console.ReadLine();
            if (assitantName == "Aeos" || assitantName == "")
            {
                Console.WriteLine();
                Console.WriteLine("Oh, ok. I do rather like my name.");
                if (charaterName == "Aeos")
                {
                    Console.WriteLine("But then what should we do about our shared name?");
                    Console.WriteLine("I'll call you just player from now on, ok!");
                    Console.Write(">");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(assitantName + "? I suppose it would take some time to get used to that name...");
                Console.WriteLine("Apologies, " + charaterName + ", it is time to begin your adventure.");
                Console.ReadLine();
            }
        }
    }
}
