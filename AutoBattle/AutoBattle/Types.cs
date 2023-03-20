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

        /*public struct CharacterSkills
        {
            string name;
            float damage;
            float damageMultiplier;
        }*/

        public enum CharacterClass : uint
        {
            Paladin = 1,
            Warrior = 2,
            Cleric = 3,
            Archer = 4
        }
    }
}
