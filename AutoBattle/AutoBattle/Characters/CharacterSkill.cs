namespace AutoBattle.Characters
{
    public class CharacterSkill
    {
        public CharacterSkills skill;
        public int turnsToWork;
        public float damageMultiplier;
        public float lifeRecovery;

        private static string description;

        public CharacterSkill(CharacterSkills skill)
        {
            this.skill = skill;

            if (skill == CharacterSkills.Bleed)
            {
                turnsToWork = 2;
                damageMultiplier = 1.5f;
                lifeRecovery = 1;
            }
            else if (skill == CharacterSkills.Heal)
            {
                turnsToWork = 2;
                damageMultiplier = 1;
                lifeRecovery = 1.5f;
            }
            else if (skill == CharacterSkills.StrongAttack)
            {
                turnsToWork = 1;
                damageMultiplier = 2;
                lifeRecovery = 1;
            }
            else
            {
                turnsToWork = 0;
                damageMultiplier = 1;
                lifeRecovery = 1;
            }
        }

        public static string GetDescription(CharacterClass characterClass)
        {
            if (characterClass == CharacterClass.Paladin)
            {
                description = "Bleed (50% more damage on your first 2 attacks)";
            }
            else if (characterClass == CharacterClass.Cleric)
            {
                description = "Heal (50% more life recovery on enemy's first 2 attacks)";
            }
            else if (characterClass == CharacterClass.Warrior)
            {
                description = "Strong Attack (Your first attack will be times 2)";
            }
            else
            {
                description = "Invisibility (You will become invisible in the first round)";
            }

            return description;
        }
    }

    public enum CharacterSkills
    {
        Bleed,
        Heal,
        StrongAttack,
        Invisibility
    }
}
