using AutoBattle.Characters;
using System;

namespace AutoBattle
{
    class Program
    {
        //This class is only responsible for initialization of the game
        static void Main(string[] args)
        {
            CharacterMovements characterMovements = new CharacterMovements();
            PlayerChoices playerChoices = new PlayerChoices();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Hi, welcome to the Thayane Carvalho RPG Auto Battle Game!");

            SharedMethods.SetClassSpecifcValues(playerChoices, characterMovements);
        }
    }
}
