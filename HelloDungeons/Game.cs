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
            string charaterName = "Name";
            int areaNumber = 1;
            int playerHealth = 20;
            int strength = 1;
            bool gameOver = false;
            float points = 0.0f;

            //Start Screen and Charater Creation
            Console.WriteLine("Enter your Name below.");
            charaterName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Hello " + charaterName + ".");
            Console.ReadLine();
        }
    }
}
