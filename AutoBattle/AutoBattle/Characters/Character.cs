using System;
using AutoBattle.Grids;

namespace AutoBattle.Characters
{
    public class Character
    {
        public string name;
        public float health;
        public float damage;
        public float lifeRecovery;
        public int playerIndex;
        public GridBox currentLocation;
        public Character target;
        public bool hasAnActive = true;
        public bool isInvisible = false;
        public CharacterSkill skill;

        private readonly float defaultDamage;
        private readonly float defaultLifeRecovery;

        //Creating an empty character
        public Character(int playerIndex)
        {
            this.playerIndex = playerIndex;
        }

        //Creating an player or enemy character
        public Character(CharacterClassSpecific characterClass, int playerIndex, CharacterSkill skill)
        {
            health = characterClass.health;
            damage = characterClass.damage;
            defaultDamage = damage;
            lifeRecovery = characterClass.lifeRecovery;
            defaultLifeRecovery = lifeRecovery;
            this.playerIndex = playerIndex;
            this.skill = skill;
        }

        public void TakeDamage(float amount)
        {
            hasAnActive = false;

            health -= amount;

            if (health <= 0)
            {
                return;
            }

            if (skill.turnsToWork > 0)
            {
                skill.turnsToWork--;
                lifeRecovery *= skill.lifeRecovery;
            }
            else
            {
                lifeRecovery = defaultLifeRecovery;
            }

            //Life recovery is as if the player had taken a potion after taking damage
            health += lifeRecovery;
            return;
        }

        public void Die()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(playerIndex == 1 ? "You win!" : "You lost!");
        }

        public void StartTurn(Grid battlefield, bool targetIsPlayer)
        {
            if (CheckHaveNearbyTarget(battlefield, targetIsPlayer ? 0 : 1))
            {
                Attack(battlefield);
            }
            else
            {
                //If there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                CheckTargetPosition(battlefield, targetIsPlayer ? 0 : 1);
            }
        }

        // Check in x and y directions if there is any character close enough to be a target
        private bool CheckHaveNearbyTarget(Grid battlefield, int targetIndex)
        {
            if (target != null && !target.isInvisible)
            {
                //left
                if (currentLocation.xIndex - 1 >= 0)
                {
                    if (battlefield.grids[currentLocation.xIndex - 1, currentLocation.yIndex].character.playerIndex == targetIndex)
                    {
                        target = battlefield.grids[currentLocation.xIndex - 1, currentLocation.yIndex].character;
                        return true;
                    }
                }

                //right
                if (currentLocation.xIndex + 1 <= battlefield.xLenght)
                {
                    if (battlefield.grids[currentLocation.xIndex + 1, currentLocation.yIndex].character.playerIndex == targetIndex)
                    {
                        target = battlefield.grids[currentLocation.xIndex + 1, currentLocation.yIndex].character;
                        return true;
                    }
                }

                //up
                if (currentLocation.yIndex - 1 >= 0)
                {
                    if (battlefield.grids[currentLocation.xIndex, currentLocation.yIndex - 1].character.playerIndex == targetIndex)
                    {
                        target = battlefield.grids[currentLocation.xIndex, currentLocation.yIndex - 1].character;
                        return true;
                    }
                }

                //down
                if (currentLocation.yIndex + 1 <= battlefield.yLength)
                {
                    if (battlefield.grids[currentLocation.xIndex, currentLocation.yIndex + 1].character.playerIndex == targetIndex)
                    {
                        target = battlefield.grids[currentLocation.xIndex, currentLocation.yIndex + 1].character;
                        return true;
                    }
                }
            }

            return false;
        }

        private void CheckTargetPosition(Grid battlefield, int targetIndex)
        {
            if (target != null && !target.isInvisible)
            {
                foreach (GridBox gridBox in battlefield.grids)
                {
                    if (gridBox.character.playerIndex == targetIndex)
                    {
                        target = gridBox.character;
                        int diffLeft = target.currentLocation.xIndex - currentLocation.xIndex;
                        int diffRight = currentLocation.xIndex - target.currentLocation.xIndex;
                        int diffUp = target.currentLocation.yIndex - currentLocation.yIndex;
                        int diffDown = currentLocation.yIndex - target.currentLocation.yIndex;

                        //left
                        if (diffLeft <= diffRight && diffLeft <= diffUp && diffLeft <= diffDown)
                        {
                            if (currentLocation.xIndex - 1 >= 0)
                            {
                                SetActualValuesGridBox(currentLocation.xIndex - 1, currentLocation.yIndex);
                                return;
                            }
                        }
                        //right
                        if (diffRight <= diffUp && diffRight <= diffDown)
                        {
                            if (currentLocation.xIndex + 1 <= battlefield.xLenght)
                            {
                                SetActualValuesGridBox(currentLocation.xIndex + 1, currentLocation.yIndex);
                                return;
                            }
                        }
                        //up
                        if (diffUp <= diffDown)
                        {
                            if (currentLocation.yIndex - 1 >= 0)
                            {
                                SetActualValuesGridBox(currentLocation.xIndex, currentLocation.yIndex - 1);
                                return;
                            }
                        }
                        //down
                        else
                        {
                            if (currentLocation.yIndex + 1 <= battlefield.yLength)
                            {
                                SetActualValuesGridBox(currentLocation.xIndex, currentLocation.yIndex + 1);
                                return;
                            }
                        }
                    }
                }
            }

            SortAnGridBoxToMovement();

            void SetActualValuesGridBox(int x, int y)
            {
                battlefield.grids[currentLocation.xIndex, currentLocation.yIndex].character = new Character(-1);
                currentLocation = battlefield.grids[x, y];
                battlefield.grids[x, y].character = this;
            }

            void SortAnGridBoxToMovement()
            {
                Random rand = new Random();
                int index = rand.Next(0, 3);

                //left
                if (index == 0)
                {
                    if (currentLocation.xIndex - 1 >= 0 &&
                        battlefield.grids[currentLocation.xIndex - 1, currentLocation.yIndex].character.playerIndex == -1)
                    {
                        currentLocation = battlefield.grids[currentLocation.xIndex - 1, currentLocation.yIndex];
                    }
                    else
                    {
                        SortAnGridBoxToMovement();
                    }
                }
                //right
                else if (index == 1)
                {
                    if (currentLocation.xIndex + 1 <= battlefield.xLenght && 
                        battlefield.grids[currentLocation.xIndex + 1, currentLocation.yIndex].character.playerIndex == -1)
                    {
                        currentLocation = battlefield.grids[currentLocation.xIndex + 1, currentLocation.yIndex];
                    }
                    else
                    {
                        SortAnGridBoxToMovement();
                    }
                }
                //up
                else if (index == 2)
                {
                    if (currentLocation.yIndex - 1 >= 0 &&
                        battlefield.grids[currentLocation.xIndex, currentLocation.yIndex - 1].character.playerIndex == -1)
                    {
                        currentLocation = battlefield.grids[currentLocation.xIndex, currentLocation.yIndex - 1];
                    }
                    else
                    {
                        SortAnGridBoxToMovement();
                    }
                }
                //down
                else
                {
                    if (currentLocation.yIndex + 1 <= battlefield.yLength &&
                        battlefield.grids[currentLocation.xIndex, currentLocation.yIndex + 1].character.playerIndex == -1)
                    {
                        currentLocation = battlefield.grids[currentLocation.xIndex, currentLocation.yIndex + 1];
                    }
                    else
                    {
                        SortAnGridBoxToMovement();
                    }
                }
            }
        }

        public void Attack(Grid battlefield)
        {
            if (skill.turnsToWork > 0)
            {
                skill.turnsToWork--;
                damage *= skill.damageMultiplier;
            }
            else
            {
                damage = defaultDamage;
            }

            target.TakeDamage(damage);
            battlefield.grids[currentLocation.xIndex, currentLocation.yIndex].character = new Character(-1);
            currentLocation = battlefield.grids[target.currentLocation.xIndex, target.currentLocation.yIndex];
            battlefield.grids[target.currentLocation.xIndex, target.currentLocation.yIndex].character = this;

            if (playerIndex == 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Player is attacking the enemy and did {damage} damage");
                Console.WriteLine($"Enemy health: {target.health}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Enemy is attacking the player and did {damage} damage");
                Console.WriteLine($"Player health: {target.health}");
            }
        }
    }
}
