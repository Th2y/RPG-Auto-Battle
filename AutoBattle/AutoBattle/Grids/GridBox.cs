using AutoBattle.Characters;

namespace AutoBattle.Grids
{
    public class GridBox
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
}
