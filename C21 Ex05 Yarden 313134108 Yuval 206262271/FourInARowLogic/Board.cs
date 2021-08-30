using System;

namespace FourInARowLogic
{
    public class Board
    {
        private readonly int r_RowSize, r_ColSize;
        private char[,] m_Board = null;

        public Board(int i_Rows, int i_Cols)
        {
            r_ColSize = i_Cols;
            r_RowSize = i_Rows;
            // $G$ DSN-001 (-5) Coins should be represented by either a struct, a class or an enum.
            m_Board = new char[i_Rows, i_Cols];
            ClearBoard();
        }

        public int Column
        {
            get
            {
                return r_ColSize;
            }
        }

        public int Row
        {
            get
            {
                return r_RowSize;
            }
        }

        public static bool IsValidSize(int i_Size)
        {
            return i_Size >= 4 && i_Size <= 8;
        }

        public void ClearBoard()
        {
            for (int i = 0; i < r_RowSize; i++)
            {
                for (int j = 0; j < r_ColSize; j++)
                {
                    m_Board[i, j] = ' ';
                }
            }
        }

        public char GetValueInCell(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col];
        }

        public void AddMove(int i_Col, char i_Sign, out int o_Row)
        {
            int row = r_RowSize;
            bool setDone = false;

            while (row > 0 && !setDone)
            {
                if (m_Board[row - 1, i_Col - 1] != ' ')
                {
                    row--;
                }
                else
                {
                    SetCell(row - 1, i_Col - 1, i_Sign);
                    setDone = true;
                }
            }

            o_Row = row;
        }

        public void SetCell(int i_Row, int i_Col, char i_Sign)
        {
            this.m_Board[i_Row, i_Col] = i_Sign;
        }

        public bool IsDraw()
        {
            bool isDraw = true;

            for (int i = 0; i < r_RowSize && isDraw; i++)
            {
                for (int j = 0; j < r_ColSize && isDraw; j++)
                {
                    if (this.m_Board[i, j] == ' ')
                    {
                        isDraw = false;
                    }
                }
            }

            return isDraw;
        }

        public bool IsWinnerMove(int i_Row, int i_Col)
        {
            return isPartOf4InCol(i_Row, i_Col) || isPartOf4InRow(i_Row, i_Col)
                                                || isPartOf4InRightDiagonal(i_Row, i_Col)
                                                || isPartOf4InLeftDiagonal(i_Row, i_Col);
        }

        private bool isPartOf4InRow(int i_Row, int i_Col)
        {
            char sign = m_Board[i_Row - 1, i_Col - 1];
            int countSameSign = 1;

            for (int i = i_Col - 1; i > 0; i--)
            {
                if (m_Board[i_Row - 1, i - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            for (int i = i_Col + 1; i <= r_ColSize; i++)
            {
                if (m_Board[i_Row - 1, i - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            return countSameSign >= 4;
        }

        private bool isPartOf4InCol(int i_Row, int i_Col)
        {
            char sign = m_Board[i_Row - 1, i_Col - 1];
            int countSameSign = 1;

            for (int i = i_Row - 1; i > 0; i--)
            {
                if (this.m_Board[i - 1, i_Col - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            for (int i = i_Row + 1; i <= r_RowSize; i++)
            {
                if (this.m_Board[i - 1, i_Col - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            return countSameSign >= 4;
        }

        private bool isPartOf4InRightDiagonal(int i_Row, int i_Col)
        {
            char sign = m_Board[i_Row - 1, i_Col - 1];
            int countSameSign = 1;

            for (int i = i_Row - 1, j = i_Col + 1; i > 0 && j <= r_ColSize; i--, j++)
            {
                if (m_Board[i - 1, j - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            for (int i = i_Row + 1, j = i_Col - 1; i <= r_RowSize && j > 0; i++, j--)
            {
                if (m_Board[i - 1, j - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            return countSameSign >= 4;
        }

        private bool isPartOf4InLeftDiagonal(int i_Row, int i_Col)
        {
            char sign = this.m_Board[i_Row - 1, i_Col - 1];
            int countSameSign = 1;

            for (int i = i_Row - 1, j = i_Col - 1; i > 0 && j > 0; i--, j--)
            {
                if (m_Board[i - 1, j - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            for (int i = i_Row + 1, j = i_Col + 1; i <= r_RowSize && j <= r_ColSize; i++, j++)
            {
                if (m_Board[i - 1, j - 1] != sign)
                {
                    break;
                }

                countSameSign++;
            }

            return countSameSign >= 4;
        }

        public int ScoreOfBoard()
        {
            int score = 0;

            score += this.checkRowsSequences();
            score += this.checkColsSequences();
            score += this.checkUpToDownDiagonalSequences();
            score += this.checkDownToUpDiagonalSequences();

            return score;
        }

        private int checkRowsSequences()
        {
            int score = 0;
            for (int i = 0; i < r_RowSize; i++)
            {
                for (int j = 0; j < r_ColSize - 3; j++)
                {
                    char[] seq = new char[4]
                                     {
                                         this.m_Board[i, j], this.m_Board[i, j + 1], this.m_Board[i, j + 2],
                                         this.m_Board[i, j + 3]
                                     };
                    score += getSequenceScore(seq);
                }
            }

            return score;
        }

        private int checkColsSequences()
        {
            int score = 0;
            for (int i = 0; i < r_ColSize; i++)
            {
                for (int j = 0; j < r_RowSize - 3; j++)
                {
                    char[] seq = new char[4]
                                     {
                                         this.m_Board[j, i], this.m_Board[j + 1, i], this.m_Board[j + 2, i],
                                         this.m_Board[j + 3, i]
                                     };
                    score += getSequenceScore(seq);
                }
            }

            return score;
        }

        private int checkUpToDownDiagonalSequences()
        {
            int score = 0;
            for (int i = 0; i < r_RowSize - 3; i++)
            {
                for (int j = 0; j < r_ColSize - 3; j++)
                {
                    char[] seq = new char[4]
                                     {
                                         this.m_Board[i, j], this.m_Board[i + 1, j + 1], this.m_Board[i + 2, j + 2],
                                         this.m_Board[i + 3, j + 3]
                                     };
                    score += getSequenceScore(seq);
                }
            }

            return score;
        }

        private int checkDownToUpDiagonalSequences()
        {
            int score = 0;
            for (int i = r_ColSize - 1; i >= 3; i--)
            {
                for (int j = 0; j < r_RowSize - 3; j++)
                {
                    char[] seq = new char[4]
                                     {
                                         this.m_Board[j, i], this.m_Board[j + 1, i - 1], this.m_Board[j + 2, i - 2],
                                         this.m_Board[j + 3, i - 3]
                                     };
                    score += getSequenceScore(seq);
                }
            }

            return score;
        }

        private int getSequenceScore(char[] i_SeqArray)
        {
            int player = 0;
            int computer = 0;
            int empty = 0;
            int resScore = 0;

            foreach (char value in i_SeqArray)
            {
                switch (value)
                {
                    case 'X':
                        player++;
                        break;
                    case 'O':
                        computer++;
                        break;
                    default:
                        empty++;
                        break;
                }
            }

            if (computer == 3 && empty == 1)
            {
                resScore = 100;
            }
            else if (player == 3 && empty == 1)
            {
                resScore = -200;
            }
            else if (computer == 2 && empty == 2)
            {
                resScore = 50;
            }
            else if (player == 2 && empty == 2)
            {
                resScore = -50;
            }

            return resScore;
        }

        public bool IsValidCol(int i_Col)
        {
            return i_Col >= 1 && i_Col <= this.r_ColSize && m_Board[0, i_Col - 1] == ' ';
        }
    }
}
