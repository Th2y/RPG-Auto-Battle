using AutoBattle.Characters;

namespace AutoBattle.Grids
{
    public class GridBox
    {
        public int xIndex;
        public int yIndex;
        public int index;
        public Character character;

        public GridBox(int x, int y, int index)
        {
            xIndex = x;
            yIndex = y;
            this.index = index;
            character = new Character(-1);
        }
    }
}
