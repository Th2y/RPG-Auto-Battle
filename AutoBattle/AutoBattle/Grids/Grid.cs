using AutoBattle.Characters;
using System;

namespace AutoBattle.Grids
{
    public class Grid
    {
        public GridBox[,] grids;
        public int xLenght;
        public int yLength;

        public Grid(int lines, int columns)
        {
            grids = new GridBox[lines, columns];
            xLenght = lines - 1;
            yLength = columns - 1;

            Console.WriteLine("--------------------------------");
            Console.WriteLine("The battle field has been created!");
            Console.WriteLine("--------------------------------");
            Console.Write(Environment.NewLine);

            for (int x = 0; x < lines; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    GridBox newBox = new GridBox(x, y, x + y);
                    grids[x, y] = newBox;
                }
            }
        }

        //Prints the matrix that indicates the tiles of the battlefield
        public void DrawBattlefield(int lines, int columns, bool isPlayerTime, Character player, Character enemy)
        {
            if (isPlayerTime)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Player in {player.currentLocation.xIndex}, {player.currentLocation.yIndex}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Enemy in {enemy.currentLocation.xIndex}, {enemy.currentLocation.yIndex}");
            }

            for (int x = 0; x < lines; x++)
            {
                for (int y = 0; y < columns; y++)
                {
                    if (grids[x, y].character.playerIndex == 0)
                    {
                        Console.Write("[P]\t");
                    }
                    else if (grids[x, y].character.playerIndex == 1)
                    {
                        Console.Write("[E]\t");
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine);
            }
            Console.Write(Environment.NewLine);
        }
    }
}
