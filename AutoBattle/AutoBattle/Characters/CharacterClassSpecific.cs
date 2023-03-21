namespace AutoBattle.Characters
{
    public class CharacterClassSpecific
    {
        public CharacterClass characterClass;
        public float health;
        public float damage;
        public float lifeRecovery;

        public CharacterClassSpecific(CharacterClass characterClass, float health, float damage, float lifeRecovery)
        {
            this.characterClass = characterClass;
            this.health = health;
            this.damage = damage;
            this.lifeRecovery = lifeRecovery;
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
