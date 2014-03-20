using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Percolation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Random r = new Random();
            int minDim = 2;
            int maxDim = 5;
            bool[,] test = new bool[r.Next(minDim, maxDim), r.Next(minDim, maxDim)];
            for (int row = 0; row < test.GetLength(0); row++)
            {
                for (int col = 0; col < test.GetLength(1); col++)
                {
                    if (r.Next(2) == 1)
                    {
                        test[row, col] = true;
                    }
                }
            }
            PrintField(test);
            bool[,] filled = FilledFieldComplete(test);
            PrintField(filled);
            Console.WriteLine(PercolatesTopToBottom(filled));
            Console.ReadKey();
        }

        private static bool PercolatesTopToBottom(bool[,] filledField)
        {
            bool result = false;
            for (int col = 0; col < filledField.GetLength(1); col++)
            {
                if (filledField[filledField.GetLength(0)-1,col])
                {
                    result = true;
                }
            }
            return result;
        }

        private static bool[,] FilledFieldComplete(bool[,] blockedField)
        {
            bool[,] filledField = new bool[blockedField.GetLength(0), blockedField.GetLength(1)];
            for (int col = 0; col < filledField.GetLength(1); col++)
            {
                Flow(0, col, filledField, blockedField);
            }
            return filledField;
        }

        private static bool[,] FilledFieldVertically(bool[,] blockedField)
        {
            bool[,] filledField = new bool[blockedField.GetLength(0), blockedField.GetLength(1)];
            for (int col = 0; col < filledField.GetLength(1); col++)
            {
                FlowVertically(0, col, filledField, blockedField);
            }
            return filledField;
        }

        private static void Flow(int row, int col, bool[,] filledField, bool[,] blockedField)
        {
            if (row >= 0 && row < blockedField.GetLength(0)
                && col >= 00 && col < blockedField.GetLength(1)
                )
            {
                if (!blockedField[row, col])
                {
                    if (filledField[row, col])
                    {
                        return;
                    }
                    filledField[row, col] = true;
                    Flow(row - 1, col, filledField, blockedField);
                    Flow(row + 1, col, filledField, blockedField);
                    Flow(row, col + 1, filledField, blockedField);
                    Flow(row, col - 1, filledField, blockedField);
                }
            }
        }

        private static void FlowVertically(int row, int col, bool[,] filledField, bool[,] blockedField)
        {
            if (row >= 0 && row < blockedField.GetLength(0))
            {
                if (!blockedField[row, col])
                {
                    if (filledField[row, col])
                    {
                        return;
                    }
                    filledField[row, col] = true;
                    FlowVertically(row + 1, col, filledField, blockedField);
                }
            }
        }

        private static void PrintField(bool[,] field)
        {
            Console.WriteLine();
            for (int row = 0; row < field.GetLength(0); row++)
            {
                string line = "";
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    if (field[row,col])
                    {
                        line += "X";
                    }
                    else
                    {
                        line += "O";
                    }
                    line += " ";
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }
}