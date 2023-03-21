namespace AutoBattle.Characters
{
    public class CharacterSkill
    {
        public CharacterSkills skill;
        public int turnsToWork;
        public float damageMultiplier;
        public float lifeRecovery;

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
    }

    public enum CharacterSkills
    {
        Bleed,
        Heal,
        StrongAttack,
        Invisibility
    }
}
