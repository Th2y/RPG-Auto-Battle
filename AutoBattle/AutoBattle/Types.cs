namespace AutoBattle
{
    public class Types
    {
        public struct CharacterClassSpecific
        {
            public CharacterClass characterClass;
            public float health;
            public float damage;
            public float lifeRecovery;
            //CharacterSkills[] skills;

            public CharacterClassSpecific(CharacterClass characterClass, float health, float damage, float lifeRecovery)
            {
                this.characterClass = characterClass;
                this.health = health;
                this.damage = damage;
                this.lifeRecovery = lifeRecovery;
            }
        }

        public struct GridBox
        {
            public int xIndex;
            public int yIndex;
            public bool ocupied;
            public int index;
            public Character character;

            public GridBox(int x, int y, bool ocupied, int index)
            {
                xIndex = x;
                yIndex = y;
                this.ocupied = ocupied;
                this.index = index;
                character = new Character(-1);
            }
        }

        public struct CharacterSkill
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
                    this.turnsToWork = 2;
                    this.damageMultiplier = 1.5f;
                    this.lifeRecovery = 1;
                }
                else if(skill == CharacterSkills.Heal)
                {
                    this.turnsToWork = 2;
                    this.damageMultiplier = 1;
                    this.lifeRecovery = 1.5f;
                }
                else if(skill == CharacterSkills.StrongAttack)
                {
                    this.turnsToWork = 1;
                    this.damageMultiplier = 2;
                    this.lifeRecovery = 1;
                }
                else
                {
                    this.turnsToWork = 0;
                    this.damageMultiplier = 1;
                    this.lifeRecovery = 1;
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

        public enum CharacterClass : uint
        {
            Paladin = 1,
            Warrior = 2,
            Cleric = 3,
            Archer = 4
        }
    }
}
