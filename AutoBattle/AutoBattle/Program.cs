using System;
using static AutoBattle.Character;
using static AutoBattle.Grid;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid(5, 5);
            CharacterClass playerCharacterClass;
            GridBox playerCurrentLocation;
            GridBox enemyCurrentLocation;
            Character playerCharacter;
            Character enemyCharacter;
            List<Character> allPlayers = new List<Character>();
            int currentTurn = 0;
            int numberOfPossibleTiles = grid.grids.Count;

            //It is not necessary an method only calling other method
            GetPlayerChoice();

            void GetPlayerChoice()
            {
                //asks for the player to choose between for possible classes via console.
                Console.WriteLine("Choose between One of this Classes:");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
                //store the player choice in a variable
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "2":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "3":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    case "4":
                        CreatePlayerCharacter(Int32.Parse(choice));
                        break;
                    default:
                        Console.WriteLine("This is not a class!\n");
                        GetPlayerChoice();
                        break;
                }
            }

            void CreatePlayerCharacter(int classIndex)
            {               
                CharacterClass characterClass = (CharacterClass)classIndex;
                Console.WriteLine($"Player Class Choice: {characterClass}");
                playerCharacter = new Character(characterClass);
                playerCharacter.health = 100;
                playerCharacter.baseDamage = 20;
                playerCharacter.playerIndex = 0;
                
                CreateEnemyCharacter();
            }

            void CreateEnemyCharacter()
            {
                //randomly choose the enemy class and set up vital variables
                var rand = new Random();
                int randomInteger = rand.Next(1, 4);
                CharacterClass enemyClass = (CharacterClass)randomInteger;
                Console.WriteLine($"Enemy Class Choice: {enemyClass}");
                enemyCharacter = new Character(enemyClass);
                enemyCharacter.health = 100;
                playerCharacter.baseDamage = 20;
                playerCharacter.playerIndex = 1;
                StartGame();
            }

            void StartGame()
            {
                //populates the character variables and targets
                enemyCharacter.target = playerCharacter;
                playerCharacter.target = enemyCharacter;
                allPlayers.Add(playerCharacter);
                allPlayers.Add(enemyCharacter);
                AlocatePlayers();
                StartTurn();
            }

            void StartTurn(){

                if (currentTurn == 0)
                {
                    //AllPlayers.Sort();  
                }

                foreach(Character character in allPlayers)
                {
                    character.StartTurn(grid);
                }

                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                if(playerCharacter.health == 0)
                {
                    return;
                }
                else if (enemyCharacter.health == 0)
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    // endgame?

                    Console.Write(Environment.NewLine + Environment.NewLine);

                    return;
                }
                else
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Click on any key to start the next turn...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    ConsoleKeyInfo key = Console.ReadKey();
                    StartTurn();
                }
            }

            int GetRandomInt(int min, int max)
            {
                var rand = new Random();
                int index = rand.Next(min, max);
                return index;
            }

            void AlocatePlayers()
            {
                AlocatePlayerCharacter();
            }

            void AlocatePlayerCharacter()
            {
                int random = 0;
                GridBox randomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!randomLocation.ocupied)
                {
                    GridBox playerCurrentLocation = randomLocation;
                    randomLocation.ocupied = true;
                    grid.grids[random] = randomLocation;
                    playerCharacter.currentBox = grid.grids[random];
                    AlocateEnemyCharacter();
                }
                else
                {
                    AlocatePlayerCharacter();
                }
            }

            void AlocateEnemyCharacter()
            {
                int random = 24;
                GridBox randomLocation = (grid.grids.ElementAt(random));
                Console.Write($"{random}\n");
                if (!randomLocation.ocupied)
                {
                    enemyCurrentLocation = randomLocation;
                    randomLocation.ocupied = true;
                    grid.grids[random] = randomLocation;
                    enemyCharacter.currentBox = grid.grids[random];
                    grid.DrawBattlefield(5 , 5);
                }
                else
                {
                    AlocateEnemyCharacter();
                }                
            }
        }
    }
}
