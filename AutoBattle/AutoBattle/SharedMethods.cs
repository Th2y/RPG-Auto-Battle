using AutoBattle.Characters;
using System;
using System.Collections.Generic;

namespace AutoBattle
{
    public static class SharedMethods
    {
        public static int GetRandomInt(int min, int max)
        {
            var rand = new Random();
            int index = rand.Next(min, max);
            return index;
        }

        //Setting the CharacterSkill values by CharacterClass
        public static CharacterSkill GetCharacterSkill(CharacterClass characterClass)
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

        //Seting the health, damage and lifeRecovery of each class type
        public static void SetClassSpecifcValues(PlayerChoices playerChoices, CharacterMovements characterMovements)
        {
            List<CharacterClassSpecific> characterClassSpecifics = new List<CharacterClassSpecific>
                {
                    new CharacterClassSpecific(CharacterClass.Paladin),
                    new CharacterClassSpecific(CharacterClass.Warrior),
                    new CharacterClassSpecific(CharacterClass.Cleric),
                    new CharacterClassSpecific(CharacterClass.Archer)
                };

            playerChoices.GetPlayerChoices(characterMovements, characterClassSpecifics);
        }
    }
}
