using System;
using AutoBattle.Characters;
using AutoBattle.Grids;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Variables
            Grid grid;
            Character playerCharacter;
            Character enemyCharacter;
            //List<Character> allPlayers = new List<Character>();

            int currentTurn = 0;
            int gridSizeX = 0;
            int gridSizeY = 0;
            int waitTime = 0;
            bool isPlayerTime = true;

            CharacterClassSpecific classPaladin;
            CharacterClassSpecific classWarrior;
            CharacterClassSpecific classCleric;
            CharacterClassSpecific classArcher;
            #endregion

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Hi, welcome to the Thayane Carvalho RPG Auto Battle Game!");

            SetClassSpecifcValues();

            #region Methods
            //Seting the health, damage and lifeRecovery of each class type
            void SetClassSpecifcValues()
            {
                classPaladin = new CharacterClassSpecific(CharacterClass.Paladin, 80, 20, 5);
                classWarrior = new CharacterClassSpecific(CharacterClass.Warrior, 60, 40, 0);
                classCleric = new CharacterClassSpecific(CharacterClass.Cleric, 100, 10, 15);
                classArcher = new CharacterClassSpecific(CharacterClass.Archer, 80, 30, 0);

                //It is not necessary an method only calling other method
                GetPlayerChoices();
            }

            //Getting all of the player choices
            void GetPlayerChoices()
            {
                GetPlayerWaitTimeChoice();
                GetPlayerGridChoice(true);
                GetPlayerGridChoice(false);
                grid = new Grid(gridSizeX, gridSizeY);

                GetPlayerWantsToStart();
            }

            //Know if the player wants to start
            void GetPlayerWantsToStart()
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("Do you want to start?");
                Console.WriteLine("[1] - Yes, [2] - No");
                string choice = Console.ReadLine();
                int value;
                if (int.TryParse(choice, out value))
                {
                    if(value == 1)
                    {
                        isPlayerTime = true;
                        Console.Write(Environment.NewLine);
                        GetPlayerTypeChoice();
                    }
                    else if(value == 2)
                    {
                        isPlayerTime = false;
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

            //Getting the time for wait to show a new grid
            void GetPlayerWaitTimeChoice()
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(Environment.NewLine);
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Choose how many seconds you want to wait for an action to be performed automatically");
                Console.WriteLine("The minimum is 2 seconds");
                string choice = Console.ReadLine();
                int value;
                if (int.TryParse(choice, out value))
                {
                    if(value < 2) value = 2;

                    waitTime = value * 1000;
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
            void GetPlayerGridChoice(bool isXValue)
            {
                Console.WriteLine(isXValue ?
                    "Choose a number greater than or equal to 3" : "Choose another number greater than or equal to 3");
                string choice = Console.ReadLine();
                int value;
                if (int.TryParse(choice, out value))
                {
                    if (value >= 3)
                    {
                        if (isXValue)
                        {
                            gridSizeX = value;
                        }
                        else
                        {
                            gridSizeY = value;
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

            //Asks for the player to choose between for possible classes
            void GetPlayerTypeChoice()
            {
                Console.WriteLine("Choose between one of this classes:");
                Console.WriteLine($"[1] Paladin - Health: {classPaladin.health}, Damage: {classPaladin.damage}, Life recovery: {classPaladin.lifeRecovery}, Skill: Bleed (50% more damage on your first 2 attacks)");
                Console.WriteLine($"[2] Warrior - Health: {classWarrior.health}, Damage: {classWarrior.damage}, Life recovery: {classWarrior.lifeRecovery}, Skill: Strong Attack (Your first attack will be times 2)");
                Console.WriteLine($"[3] Cleric - Health: {classCleric.health}, Damage: {classCleric.damage}, Life recovery: {classCleric.lifeRecovery}, Skill: Heal (50% more life recovery on enemy's first 2 attacks)");
                Console.WriteLine($"[4] Archer - Health: {classArcher.health}, Damage: {classArcher.damage}, Life recovery: {classArcher.lifeRecovery}, Skill: Invisibility (You will become invisible in the first round)");
                
                //Store the player choice in a variable
                string choiceClass = Console.ReadLine();
                if(choiceClass == "1" || choiceClass == "2" || choiceClass == "3" || choiceClass == "4")
                {
                    CreatePlayerCharacter(Int32.Parse(choiceClass));
                }
                else
                {
                    Console.WriteLine("This is not a class!");
                    GetPlayerChoices();
                }
            }            

            CharacterClassSpecific GetCharacterClassSpecific(CharacterClass characterClass)
            {
                if (characterClass == CharacterClass.Paladin)
                {
                    return classPaladin;
                }
                else if (characterClass == CharacterClass.Warrior)
                {
                    return classWarrior;
                }
                else if(characterClass == CharacterClass.Cleric)
                {
                    return classCleric;
                }
                else
                {
                    return classArcher;
                }
            }

            //Setting the CharacterSkill values by CharacterClass
            CharacterSkill CharacterSkill(CharacterClass characterClass)
            {
                if (characterClass == CharacterClass.Paladin)
                {
                    return new CharacterSkill(CharacterSkills.Bleed);
                }
                else if (characterClass == CharacterClass.Warrior)
                {
                    return new CharacterSkill(CharacterSkills.StrongAttack);
                }
                else if (characterClass == CharacterClass.Cleric)
                {
                    return new CharacterSkill(CharacterSkills.Heal);
                }
                else
                {
                    return new CharacterSkill(CharacterSkills.Invisibility);
                }
            }

            void CreatePlayerCharacter(int classIndex)
            {
                Console.Write(Environment.NewLine);
                CharacterClass characterClass = (CharacterClass)classIndex;
                CharacterClassSpecific characterClassSpecific = GetCharacterClassSpecific(characterClass);
                Console.WriteLine($"Player class choice: {characterClass}");
                playerCharacter = new Character(characterClassSpecific, 0, CharacterSkill(characterClass));
                
                CreateEnemyCharacter();
            }

            void CreateEnemyCharacter()
            {
                //Randomly choose the enemy class
                CharacterClass enemyClass = (CharacterClass)GetRandomInt(1,4);
                CharacterClassSpecific enemyClassSpecific = GetCharacterClassSpecific(enemyClass);
                Console.WriteLine($"Enemy class choice: {enemyClass}");
                enemyCharacter = new Character(enemyClassSpecific, 1, CharacterSkill(enemyClass));

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
                
                StartTurn();
                StartGame();
            }

            void StartGame()
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

                //allPlayers.Add(playerCharacter);
                //allPlayers.Add(enemyCharacter);
            }

            void StartTurn()
            {
                if (isPlayerTime)
                {
                    isPlayerTime = false;
                    if (playerCharacter.hasAnActive)
                    {
                        playerCharacter.StartTurn(grid, false);
                        grid.DrawBattlefield(gridSizeX, gridSizeY, !isPlayerTime, playerCharacter, enemyCharacter);
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
                        grid.DrawBattlefield(gridSizeX, gridSizeY, !isPlayerTime, playerCharacter, enemyCharacter);
                    }
                    else
                    {
                        AlocateEnemyCharacter();
                    }
                }
             
                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                System.Threading.Thread.Sleep(waitTime);

                if (playerCharacter.health <= 0)
                {
                    playerCharacter.Die();

                    Console.Write(Environment.NewLine);
                    Console.WriteLine("Click on any key to start the next turn...");
                    Console.Write(Environment.NewLine);
                    Console.ReadKey();
                    Console.Write(Environment.NewLine);
                    GetPlayerChoices();

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
                    GetPlayerChoices();

                    return;
                }
                else
                {
                    StartTurn();
                }
            }

            int GetRandomInt(int min, int max)
            {
                var rand = new Random();
                int index = rand.Next(min, max);
                return index;
            }

            void AlocatePlayerCharacter()
            {
                isPlayerTime = true;
                playerCharacter.hasAnActive = true;
                int randomX = GetRandomInt(0, gridSizeX - 1);
                int randomY = GetRandomInt(0, gridSizeY - 1);

                GridBox randomLocation = grid.grids[randomX, randomY];
                if (!randomLocation.ocupied)
                {
                    randomLocation.ocupied = true;
                    randomLocation.character = playerCharacter;
                    grid.grids[randomX, randomY] = randomLocation;
                    playerCharacter.currentLocation = grid.grids[randomX, randomY];

                    if(currentTurn == 0 || currentTurn == 1)
                    {
                        currentTurn++;
                        if(playerCharacter.skill.skill == CharacterSkills.Invisibility)
                        {
                            playerCharacter.isInvisible = true;
                        }
                    }
                    else if(currentTurn > 3)
                    {
                        playerCharacter.isInvisible = false;
                    }

                    grid.DrawBattlefield(gridSizeX, gridSizeY, isPlayerTime, playerCharacter, enemyCharacter);
                    isPlayerTime = false;
                }
                else
                {
                    AlocatePlayerCharacter();
                }
            }

            void AlocateEnemyCharacter()
            {
                isPlayerTime = false;
                enemyCharacter.hasAnActive = true;
                int randomX = GetRandomInt(0, gridSizeX - 1);
                int randomY = GetRandomInt(0, gridSizeY - 1);

                GridBox randomLocation = grid.grids[randomX, randomY];
                if (!randomLocation.ocupied)
                {
                    randomLocation.ocupied = true;
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

                    grid.DrawBattlefield(gridSizeX, gridSizeY, isPlayerTime, playerCharacter, enemyCharacter);
                    isPlayerTime = true;
                }
                else
                {
                    AlocateEnemyCharacter();
                }                
            }
            #endregion
        }
    }
}
