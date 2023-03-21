using System;
using System.Collections.Generic;

namespace AutoBattle.Characters
{
    public class PlayerChoices
    {
        private List<CharacterClassSpecific> characterClassSpecifics;
        private CharacterMovements characterMovements;
        private AllPlayerChoices allPlayerChoices = new AllPlayerChoices();

        //Getting all of the player choices
        public void GetPlayerChoices(CharacterMovements characterMovements, List<CharacterClassSpecific> characterClassSpecifics)
        {
            this.characterMovements = characterMovements;
            this.characterClassSpecifics = characterClassSpecifics;

            GetPlayerWaitTimeChoice();
            GetPlayerGridChoice(true);
            GetPlayerGridChoice(false);
            GetPlayerWantsToStart();
        }

        //Getting the time for wait to show a new grid
        private void GetPlayerWaitTimeChoice()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(Environment.NewLine);
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Choose how many seconds you want to wait for an action to be performed automatically");
            Console.WriteLine("The minimum is 2 seconds");
            string choice = Console.ReadLine();
            if (int.TryParse(choice, out int value))
            {
                if (value < 2) value = 2;

                allPlayerChoices.waitTime = value * 1000;
                Console.WriteLine("--------------------------------");
                Console.Write(Environment.NewLine);
            }
            else
            {
                Console.WriteLine("You need to choose a number!");
                Console.Write(Environment.NewLine);
                GetPlayerWaitTimeChoice();
            }
        }

        //Asks for the player the size of the grid
        private void GetPlayerGridChoice(bool isXValue)
        {
            Console.WriteLine(isXValue ?
                "Choose a number greater than or equal to 3" : "Choose another number greater than or equal to 3");
            string choice = Console.ReadLine();
            if (int.TryParse(choice, out int value))
            {
                if (value >= 3)
                {
                    if (isXValue)
                    {
                        allPlayerChoices.gridSizeX = value;
                    }
                    else
                    {
                        allPlayerChoices.gridSizeY = value;
                    }
                    Console.Write(Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("You need to choose a number greater than or equal to 3!");
                    Console.Write(Environment.NewLine);
                    GetPlayerGridChoice(isXValue);
                }
            }
            else
            {
                Console.WriteLine("You need to choose a number greater than or equal to 3!");
                Console.Write(Environment.NewLine);
                GetPlayerGridChoice(isXValue);
            }
        }

        //Know if the player wants to start
        private void GetPlayerWantsToStart()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine("Do you want to start?");
            Console.WriteLine("[1] - Yes, [2] - No");
            string choice = Console.ReadLine();
            if (int.TryParse(choice, out int value))
            {
                if (value == 1)
                {
                    allPlayerChoices.playerStartsTheGame = true;
                    Console.Write(Environment.NewLine);
                    GetPlayerTypeChoice();
                }
                else if (value == 2)
                {
                    allPlayerChoices.playerStartsTheGame = false;
                    Console.Write(Environment.NewLine);
                    GetPlayerTypeChoice();
                }
                else
                {
                    Console.Write("Incorrect value!");
                    Console.Write(Environment.NewLine);
                    GetPlayerWantsToStart();
                }
            }
            else
            {
                Console.Write("Incorrect value!");
                Console.Write(Environment.NewLine);
                GetPlayerWantsToStart();
            }
        }

        //Asks for the player to choose between for possible classes
        private void GetPlayerTypeChoice()
        {
            Console.WriteLine("Choose between one of this classes:");

            for (int i = 0; i < characterClassSpecifics.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {characterClassSpecifics[i].characterClass} - Health: {characterClassSpecifics[i].health}, Damage: {characterClassSpecifics[i].damage}, Life recovery: {characterClassSpecifics[i].lifeRecovery}, Skill: {CharacterSkill.GetDescription(characterClassSpecifics[i].characterClass)}");
            }

            //Store the player choice in a variable
            string choiceClass = Console.ReadLine();
            if (choiceClass == "1" || choiceClass == "2" || choiceClass == "3" || choiceClass == "4")
            {
                characterMovements.CreatePlayerCharacter(characterClassSpecifics, allPlayerChoices, int.Parse(choiceClass));
            }
            else
            {
                Console.WriteLine("This is not a class!");
                GetPlayerTypeChoice();
            }
        }
    }

    public struct AllPlayerChoices
    {
        public int gridSizeX;
        public int gridSizeY;
        public int waitTime;
        public bool playerStartsTheGame;

        public AllPlayerChoices(int gridSizeX, int gridSizeY, int waitTime, bool playerStartsTheGame)
        {
            this.gridSizeX = gridSizeX;
            this.gridSizeY = gridSizeY;
            this.waitTime = waitTime;
            this.playerStartsTheGame = playerStartsTheGame;
        }
    }
}
