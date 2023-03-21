namespace AutoBattle.Characters
{
    public class CharacterClassSpecific
    {
        public CharacterClass characterClass;
        public float health;
        public float damage;
        public float lifeRecovery;

        public CharacterClassSpecific(CharacterClass characterClass)
        {
            this.characterClass = characterClass;

            if (characterClass == CharacterClass.Paladin)
            {
                health = 80;
                damage = 20;
                lifeRecovery = 5;
            }
            else if (characterClass == CharacterClass.Warrior)
            {
                health = 60;
                damage = 40;
                lifeRecovery = 0;
            }
            else if (characterClass == CharacterClass.Cleric)
            {
                health = 100;
                damage = 10;
                lifeRecovery = 15;
            }
            else
            {
                health = 80;
                damage = 30;
                lifeRecovery = 0;
            }
        }
    }

    public enum CharacterClass : uint
    {
        Paladin = 1,
        Warrior = 2,
        Cleric = 3,
        Archer = 4
    }
}
