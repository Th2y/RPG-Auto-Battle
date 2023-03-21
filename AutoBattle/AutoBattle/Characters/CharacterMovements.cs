using AutoBattle.Grids;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace AutoBattle.Characters
{
    public class CharacterMovements
    {
        private Grid grid;
        private Character playerCharacter;
        private Character enemyCharacter;
        private AllPlayerChoices allPlayerChoices;
        private List<CharacterClassSpecific> characterClassSpecifics;
        private bool isPlayerTime;
        private int currentTurn = 0;

        public void CreatePlayerCharacter(List<CharacterClassSpecific> characterClassSpecifics, AllPlayerChoices allPlayerChoices, int classIndex)
        {
            this.allPlayerChoices = allPlayerChoices;
            this.characterClassSpecifics = characterClassSpecifics;
            grid = new Grid(allPlayerChoices.gridSizeX, allPlayerChoices.gridSizeY);
            isPlayerTime = allPlayerChoices.playerStartsTheGame;

            Console.Write(Environment.NewLine);
            CharacterClass characterClass = (CharacterClass)classIndex;
            CharacterClassSpecific characterClassSpecific = GetCharacterClassSpecific(characterClass);
            Console.WriteLine($"Player class choice: {characterClass}");
            playerCharacter = new Character(characterClassSpecific, 0, SharedMethods.GetCharacterSkill(characterClass));

            CreateEnemyCharacter();
        }

        private void CreateEnemyCharacter()
        {
            //Randomly choose the enemy class
            CharacterClass enemyClass = (CharacterClass)SharedMethods.GetRandomInt(1, 4);
            CharacterClassSpecific enemyClassSpecific = GetCharacterClassSpecific(enemyClass);
            Console.WriteLine($"Enemy class choice: {enemyClass}");
            enemyCharacter = new Character(enemyClassSpecific, 1, SharedMethods.GetCharacterSkill(enemyClass));

            if (isPlayerTime)
            {
                AlocatePlayerCharacter();
                AlocateEnemyCharacter();
            }
            else
            {
                AlocateEnemyCharacter();
                AlocatePlayerCharacter();
            }

            enemyCharacter.target = playerCharacter;
            playerCharacter.target = enemyCharacter;

            StartTurn();
            StartGame();
        }

        private CharacterClassSpecific GetCharacterClassSpecific(CharacterClass characterClass)
        {
            foreach (CharacterClassSpecific classSpecific in characterClassSpecifics)
            {
                if(classSpecific.characterClass == characterClass)
                {
                    return classSpecific;
                }
            }

            return null;
        }

        private void AlocatePlayerCharacter()
        {
            isPlayerTime = true;
            playerCharacter.hasAnActive = true;
            int randomX = SharedMethods.GetRandomInt(0, allPlayerChoices.gridSizeX - 1);
            int randomY = SharedMethods.GetRandomInt(0, allPlayerChoices.gridSizeY - 1);

            GridBox randomLocation = grid.grids[randomX, randomY];
            if (randomLocation.character.playerIndex == -1)
            {
                randomLocation.character = playerCharacter;
                grid.grids[randomX, randomY] = randomLocation;
                playerCharacter.currentLocation = grid.grids[randomX, randomY];

                if (currentTurn == 0 || currentTurn == 1)
                {
                    currentTurn++;
                    if (playerCharacter.skill.skill == CharacterSkills.Invisibility)
                    {
                        playerCharacter.isInvisible = true;
                    }
                }
                else if (currentTurn > 3)
                {
                    playerCharacter.isInvisible = false;
                }

                grid.DrawBattlefield(allPlayerChoices.gridSizeX, allPlayerChoices.gridSizeY, isPlayerTime, playerCharacter, enemyCharacter);
                isPlayerTime = false;
            }
            else
            {
                AlocatePlayerCharacter();
            }
        }

        private void AlocateEnemyCharacter()
        {
            isPlayerTime = false;
            enemyCharacter.hasAnActive = true;
            int randomX = SharedMethods.GetRandomInt(0, allPlayerChoices.gridSizeX - 1);
            int randomY = SharedMethods.GetRandomInt(0, allPlayerChoices.gridSizeY - 1);

            GridBox randomLocation = grid.grids[randomX, randomY];
            if (randomLocation.character.playerIndex == -1)
            {
                randomLocation.character = enemyCharacter;
                grid.grids[randomX, randomY] = randomLocation;
                enemyCharacter.currentLocation = grid.grids[randomX, randomY];

                if (currentTurn == 0 || currentTurn == 1)
                {
                    currentTurn++;
                    if (enemyCharacter.skill.skill == CharacterSkills.Invisibility)
                    {
                        enemyCharacter.isInvisible = true;
                    }
                }
                else
                {
                    enemyCharacter.isInvisible = false;
                }

                grid.DrawBattlefield(allPlayerChoices.gridSizeX, allPlayerChoices.gridSizeY, isPlayerTime, playerCharacter, enemyCharacter);
                isPlayerTime = true;
            }
            else
            {
                AlocateEnemyCharacter();
            }
        }

        private void StartGame()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Environment.NewLine);
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Let's start the game!");
            Console.WriteLine("--------------------------------");
            Console.Write(Environment.NewLine);

            //populates the character variables and targets
            enemyCharacter.target = playerCharacter;
            playerCharacter.target = enemyCharacter;
        }

        private void StartTurn()
        {
            if (isPlayerTime)
            {
                isPlayerTime = false;
                if (playerCharacter.hasAnActive)
                {
                    playerCharacter.StartTurn(grid, false);
                    grid.DrawBattlefield(allPlayerChoices.gridSizeX, allPlayerChoices.gridSizeY, !isPlayerTime, playerCharacter, enemyCharacter);
                }
                else
                {
                    AlocatePlayerCharacter();
                }
            }
            else
            {
                isPlayerTime = true;
                if (enemyCharacter.hasAnActive)
                {
                    enemyCharacter.StartTurn(grid, true);
                    grid.DrawBattlefield(allPlayerChoices.gridSizeX, allPlayerChoices.gridSizeY, !isPlayerTime, playerCharacter, enemyCharacter);
                }
                else
                {
                    AlocateEnemyCharacter();
                }
            }

            currentTurn++;
            HandleTurn();
        }

        private void HandleTurn()
        {
            System.Threading.Thread.Sleep(allPlayerChoices.waitTime);

            if (playerCharacter.health <= 0)
            {
                playerCharacter.Die();

                Console.Write(Environment.NewLine);
                Console.WriteLine("Click on any key to start the next turn...");
                Console.Write(Environment.NewLine);
                Console.ReadKey();
                Console.Write(Environment.NewLine);

                SharedMethods.SetClassSpecifcValues(new PlayerChoices(), new CharacterMovements());

                return;
            }
            else if (enemyCharacter.health <= 0)
            {
                enemyCharacter.Die();

                Console.Write(Environment.NewLine);
                Console.WriteLine("Click on any key to start the next turn...");
                Console.Write(Environment.NewLine);
                Console.ReadKey();
                Console.Write(Environment.NewLine);

                SharedMethods.SetClassSpecifcValues(new PlayerChoices(), new CharacterMovements());

                return;
            }
            else
            {
                StartTurn();
            }
        }
    }
}
