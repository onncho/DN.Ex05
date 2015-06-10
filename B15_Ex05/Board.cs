using System;
using System.Collections.Generic;
using System.Text;

namespace B15_Ex05
{
    class Board
    {
        private int boardSize;
        private string columnLetters = "    A     B     C     D     E     F     G     H\n";
        private string columnLettersSmaller = "    A     B     C     D     E     F\n";
        private string lineSeperator;

        public Board(bool toSetMaxSize)
        {
            this.boardSize = toSetMaxSize ? 8 : 6;
            this.lineSeperator = "";

            if (boardSize == 6)
            {
                columnLetters = columnLettersSmaller;
            }

            for (int i = 0; i < columnLetters.Length; i++)
            {
                lineSeperator += "=";
            }

            lineSeperator += '\n';
        }

        public void printBoard(int[,] boardMatrix)
        {
            string boardAsString = columnLetters;
            boardAsString += lineSeperator;

            for (int i = 0; i < boardSize; i++)
            {
                boardAsString += printRow(i, boardMatrix) + this.lineSeperator;
            }
  
            Console.Write(boardAsString);
        }

        public string printRow(int rowIndex, int[,] boardMatrix)
        {
            string rowToPrint = rowIndex + "|";

            for (int i = 0; i < boardSize; i++)
            {
                rowToPrint += printChip(rowIndex, i, boardMatrix) + "|";
            }

            rowToPrint += "\n";

            return rowToPrint;
        }

        public string printChip(int rowIndex, int colIndex, int[,] boardMatrix)
        {
            string chip = "     ";
            if (boardMatrix[rowIndex, colIndex] == 1)
            {
                chip = "  X  ";
            }
            else if (boardMatrix[rowIndex, colIndex] == -1)
            {
                chip = "  O  ";
            }

            return chip;
        }
    }
}
