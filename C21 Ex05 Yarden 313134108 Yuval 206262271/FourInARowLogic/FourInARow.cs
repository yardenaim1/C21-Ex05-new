using System;

namespace FourInARowLogic
{
    public class FourInARow
    {
        private readonly Board r_Board;
        private eStatesOfGame m_CurrentState = eStatesOfGame.Continue;
        private Player m_Player1, m_Player2, m_CurrentPlayer, m_LastWinner;

        public event Action PlayerSwitch;

        public event Action GameOver;

        public FourInARow(int i_Row, int i_Col, eGameStyle i_GameStyle, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            r_Board = new Board(i_Row, i_Col);
            m_Player1 = new Player(Player.ePlayerType.Player1, 'X', i_FirstPlayerName);
            m_Player2 = i_GameStyle == eGameStyle.PlayerVsComputer ?
                            new Player(Player.ePlayerType.Computer, 'O', i_SecondPlayerName) :
                            new Player(Player.ePlayerType.Player2, 'O', i_SecondPlayerName);
            this.m_CurrentPlayer = this.m_Player1;
        }

        public Board board
        {
            get
            {
                return this.r_Board;
            }
        }

        public Player Player1
        {
            get
            {
                return m_Player1;
            }

            set
            {
                m_Player1 = value;
            }
        }

        public Player Player2
        {
            get
            {
                return m_Player2;
            }

            set
            {
                m_Player2 = value;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }

            set
            {
                m_CurrentPlayer = value;
            }
        }

        public Player LastWinner
        {
            get
            {
                return m_LastWinner;
            }

            set
            {
                m_LastWinner = value;
            }
        }

        public eStatesOfGame CurrentState
        {
            get
            {
                return m_CurrentState;
            }

            set
            {
                m_CurrentState = value;
            }
        }

        public void RoundOver(Player i_CurrentPlayer)
        {
            if (m_CurrentState != eStatesOfGame.Draw)
            {
                if(i_CurrentPlayer == m_Player1)
                {
                    m_Player2.Score++;
                    this.m_LastWinner = this.m_Player2;
                }
                else
                { 
                     m_Player1.Score++;
                     this.m_LastWinner = this.m_Player1;
                }
            }

            r_Board.ClearBoard();
            m_CurrentPlayer = m_Player1;
            OnGameOver();
        }

        private void OnGameOver()
        {
            if (GameOver != null)
            {
                GameOver.Invoke();
            }
        }

        private bool IsValidInput(int i_ColumnFromUser)
        {
            return r_Board.IsValidCol(i_ColumnFromUser);
        }

        public int GetAiNextMove()
        {
            int bestScore = int.MinValue;
            int bestMove = 0;

            for (int col = 1; col <= this.r_Board.Column; col++)
            {
                if (!this.IsValidInput(col))
                {
                    continue;
                }

                r_Board.AddMove(col, this.m_CurrentPlayer.Sign, out int o_Row);
                if (this.r_Board.IsWinnerMove(o_Row, col))
                {
                    bestMove = col;
                    this.r_Board.SetCell(o_Row - 1, col - 1, ' ');
                    break;
                }

                int score = this.miniMax(this.r_Board, 4, false, col, o_Row);
                this.r_Board.SetCell(o_Row - 1, col - 1, ' ');
                if (score <= bestScore)
                {
                    continue;
                }

                bestScore = score;
                bestMove = col;
            }

            return bestMove;
        }

        private int miniMax(Board i_GameBoard, int i_Depth, bool i_IsMaximizing, int i_LastCol, int i_LastRow)
        {
            int bestScore;
            bool isPlayerWin = i_GameBoard.IsWinnerMove(i_LastRow, i_LastCol);
            bool isDraw = i_GameBoard.IsDraw();
            bool isOver = isPlayerWin || isDraw || i_Depth == 0;

            if (isOver)
            {
                if (isPlayerWin)
                {
                    bestScore = i_IsMaximizing ? -1000000 : 1000000;
                }
                else if (isDraw)
                {
                    bestScore = 0;
                }
                else
                {
                    bestScore = i_GameBoard.ScoreOfBoard();
                }
            }
            else
            {
                if (i_IsMaximizing)
                {
                    bestScore = int.MinValue;
                    for (int col = 1; col <= i_GameBoard.Column; col++)
                    {
                        if (!IsValidInput(col))
                        {
                            continue;
                        }

                        i_GameBoard.AddMove(col, 'O', out int o_Row);
                        int score = this.miniMax(i_GameBoard, i_Depth - 1, false, col, o_Row);
                        i_GameBoard.SetCell(o_Row - 1, col - 1, ' ');
                        bestScore = Math.Max(bestScore, score);
                    }
                }
                else
                {
                    bestScore = int.MaxValue;
                    for (int col = 1; col <= i_GameBoard.Column; col++)
                    {
                        if (!IsValidInput(col))
                        {
                            continue;
                        }

                        i_GameBoard.AddMove(col, 'X', out int o_Row);
                        int score = this.miniMax(i_GameBoard, i_Depth - 1, true, col, o_Row);
                        i_GameBoard.SetCell(o_Row - 1, col - 1, ' ');
                        bestScore = Math.Min(bestScore, score);
                    }
                }
            }

            return bestScore;
        }

        public void MakeMove(int i_ColumnFromUser, Player i_Player, out int o_RowInserted)
        {
            r_Board.AddMove(i_ColumnFromUser, i_Player.Sign, out o_RowInserted);
            this.switchPlayer();
        }

        public void UpdateCurrentState(int i_LastRowInserted, int i_LastColInserted)
        {
            eStatesOfGame resultState = m_CurrentState;

            if (r_Board.IsWinnerMove(i_LastRowInserted, i_LastColInserted))
            {
                resultState = eStatesOfGame.Lose;
            }
            else if (r_Board.IsDraw())
            {
                resultState = eStatesOfGame.Draw;
            }

            m_CurrentState = resultState;
        }

        private void switchPlayer()
        {
            if (m_Player2.IsHuman())
            {
                OnPlayerSwitch();
            }

            CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
        }

        private void OnPlayerSwitch()
        {
            if (PlayerSwitch != null)
            {
                PlayerSwitch.Invoke();
            }
        }

        public enum eGameStyle
        {
            PlayerVsPlayer = 1,
            PlayerVsComputer
        }

        public enum eStatesOfGame
        {
            Continue,
            ReTry,
            GameOver,
            Draw,
            Quit,
            Lose
        }
    }
}
