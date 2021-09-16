using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using FourInARowLogic;

namespace FourInARowUI
{
    public class GameForm : Form
    {
        private const int k_ButtonSize = 60;
        private readonly string r_Player1Name, r_Player2Name;
        private readonly bool r_IsPlayer2AI;
        private readonly int r_Rows, r_Cols;
        private Label m_LabelPlayer1;
        private Label m_LabelPlayer2;
        private Label m_LabelScorePlayer1;
        private Label m_LabelScorePlayer2;
        private GameButton[,] m_ButtonsOfTheGame = null;
        private FourInARow m_FourInARowGame = null;

        public GameForm(int i_BoardHeight, int i_BoardWidth, string i_Player1Name, string i_Player2Name, bool i_IsAI)
        {
            this.r_Rows = i_BoardHeight;
            this.r_Cols = i_BoardWidth;
            this.r_Player1Name = i_Player1Name;
            this.r_Player2Name = i_Player2Name;
            this.r_IsPlayer2AI = i_IsAI;
            this.initializeButtons();
            this.InitializeComponent();
        }

        private void gameForm_Load(object sender, EventArgs e)
        {
            FourInARow.eGameStyle gameStyle = r_IsPlayer2AI ? FourInARow.eGameStyle.PlayerVsComputer : FourInARow.eGameStyle.PlayerVsPlayer;

            m_FourInARowGame = new FourInARow(r_Rows, r_Cols, gameStyle, r_Player1Name, r_Player2Name);
            m_FourInARowGame.PlayerSwitch += changeTextBoldAndColor;
            m_FourInARowGame.GameOver += fourInARow_GameOver;
            m_FourInARowGame.GameBoard.WinSequenceFoundAction += Board_WinSequnceFoundAction;
        }

        private void InitializeComponent()
        {
            this.m_LabelPlayer1 = new System.Windows.Forms.Label();
            this.m_LabelPlayer2 = new System.Windows.Forms.Label();
            this.m_LabelScorePlayer1 = new System.Windows.Forms.Label();
            this.m_LabelScorePlayer2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            // m_LabelPlayer1
            this.m_LabelPlayer1.AutoSize = true;
            this.m_LabelPlayer1.Top = m_ButtonsOfTheGame[r_Rows, 0].Bottom + 10;
            this.m_LabelPlayer1.Left = m_ButtonsOfTheGame[r_Rows, 0].Left;
            this.m_LabelPlayer1.Name = "m_LabelPlayer1";
            this.m_LabelPlayer1.TabIndex = 0;
            this.m_LabelPlayer1.Text = r_Player1Name + ": ";
            this.m_LabelPlayer1.ForeColor = Color.Blue;

            // m_LabelScorePlayer1
            this.m_LabelScorePlayer1.AutoSize = true;
            this.m_LabelScorePlayer1.Left = m_LabelPlayer1.Left + m_LabelPlayer1.Width;
            this.m_LabelScorePlayer1.Top = m_LabelPlayer1.Top;
            this.m_LabelScorePlayer1.Name = "m_LabelScorePlayer1";
            this.m_LabelScorePlayer1.Size = new Size(21, 24);
            this.m_LabelScorePlayer1.TabIndex = 2;
            this.m_LabelScorePlayer1.Text = "0";
            
            // m_LabelPlayer2 
            this.m_LabelPlayer2.AutoSize = true;
            this.m_LabelPlayer2.Top = this.m_LabelPlayer1.Top;
            this.m_LabelPlayer2.Left = m_LabelScorePlayer1.Left + 40;
            this.m_LabelPlayer2.Name = "m_LabelPlayer2";
            this.m_LabelPlayer2.TabIndex = 1;
            this.m_LabelPlayer2.Text = r_Player2Name + ": ";
            
            // m_LabelScorePlayer2 
            this.m_LabelScorePlayer2.Left = m_LabelPlayer2.Left + m_LabelPlayer2.Width;
            this.m_LabelScorePlayer2.Top = m_LabelPlayer2.Top;
            this.m_LabelScorePlayer2.Name = "m_LabelScorePlayer2";
            this.m_LabelScorePlayer2.Size = new System.Drawing.Size(21, 24);
            this.m_LabelScorePlayer2.TabIndex = 3;
            this.m_LabelScorePlayer2.Text = "0";
            
            // GameForm
            this.Controls.Add(this.m_LabelScorePlayer2);
            this.Controls.Add(this.m_LabelScorePlayer1);
            this.Controls.Add(this.m_LabelPlayer2);
            this.Controls.Add(this.m_LabelPlayer1);
            this.ClientSize = new Size(r_Cols * k_ButtonSize + 20, (r_Rows + 1) * k_ButtonSize + 50);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Name = "GameForm";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = "4 In A Raw !!";
            this.Load += gameForm_Load;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void initializeButtons()
        {
            m_ButtonsOfTheGame = new GameButton[r_Rows + 1, r_Cols];
            for (int row = 0; row <= r_Rows; row++)
            {
                for (int col = 0; col < r_Cols; col++)
                {
                    m_ButtonsOfTheGame[row, col] = new GameButton(row, col);
                    if (row == 0)
                    {
                        m_ButtonsOfTheGame[row, col].Enabled = true;
                        m_ButtonsOfTheGame[row, col].Size = new Size(k_ButtonSize, 35);
                        m_ButtonsOfTheGame[row, col].Text = (col + 1).ToString();
                        m_ButtonsOfTheGame[row, col].Click += new EventHandler(colButton_OnClick);
                        m_ButtonsOfTheGame[row, col].BackColor = Color.Red;

                    }
                    else
                    {
                        m_ButtonsOfTheGame[row, col].Size = new Size(k_ButtonSize, 45);
                        m_ButtonsOfTheGame[row, col].Enabled = false;
                        m_ButtonsOfTheGame[row, col].BackColor = Color.Gray;

                    }

                    m_ButtonsOfTheGame[row, col].Location = new Point((k_ButtonSize * col) + 10, k_ButtonSize * row);
                    this.Controls.Add(m_ButtonsOfTheGame[row, col]);
                }
            }
        }

        private void updateGameButtons(int i_Row, int i_Col, char i_PlayerSign)
        {
            GameButton buttonToUpdate = m_ButtonsOfTheGame[i_Row, i_Col - 1];
         
            buttonToUpdate.Text = i_PlayerSign.ToString();
            for (int col = 0; col < r_Cols; col++)
            {
                if (m_ButtonsOfTheGame[1, col].Text != "")
                {
                    m_ButtonsOfTheGame[0, col].Enabled = false;
                }
            }
        }
       
        private void makePlayerMove(object sender)
        {
            Button button = sender as Button;
            int col = int.Parse(button.Text);

            if (button != null)
            {
                char currentPlayerSign = m_FourInARowGame.CurrentPlayer.Sign;
                m_FourInARowGame.MakeMove(col, this.m_FourInARowGame.CurrentPlayer, out int o_RowInserted);
                updateGameButtons(o_RowInserted, col, currentPlayerSign);
                m_FourInARowGame.UpdateCurrentState(o_RowInserted, col);
            }
        }
      
        private void colButton_OnClick(object sender, EventArgs e)
        {
            makePlayerMove(sender);
            FourInARow.eStatesOfGame state = m_FourInARowGame.CurrentState;

            if (state == FourInARow.eStatesOfGame.Lose || state == FourInARow.eStatesOfGame.Draw)
            {
                m_FourInARowGame.RoundOver(m_FourInARowGame.CurrentPlayer);
            }
            else
            {
                if (r_IsPlayer2AI)
                {
                    makeAIMove();
                }
            }
        }

        private void Board_WinSequnceFoundAction(List<Board.Cell> i_WinSeq)
        {
            foreach (Board.Cell boardCell in i_WinSeq)
            {
                m_ButtonsOfTheGame[boardCell.Row + 1, boardCell.Col].BackColor = Color.Green;
            }
        }

        private void fourInARow_GameOver()
        {
            string roundWinner = getRoundWinnerAndUpdateScore();
            string finalWinner = getFinalWinnerName();
            
            Opacity = 0.8;
            if (endOfRoundMessageBox(roundWinner))
            {
                clearGameBoardButtons();
                m_FourInARowGame.CurrentState = FourInARow.eStatesOfGame.Continue;
                Opacity = 1;
                m_LabelPlayer1.ForeColor = Color.Blue;
                m_LabelPlayer2.ForeColor = Color.Black;
            }
            else
            {
                MessageBox.Show(string.Format(
                                       "{0}",
                                       finalWinner != string.Empty ? string.Format("The winner is {0}", finalWinner) : "A draw between the players!"));
                Close();
            }
        }

        private void clearGameBoardButtons()
        {
            foreach(GameButton gameButton in m_ButtonsOfTheGame)
            {
                if(gameButton.Row == 0)
                {
                    gameButton.Enabled = true;
                }
                else
                {
                    gameButton.Text = string.Empty;
                    gameButton.BackColor = Color.Gray;
                }
            }        
        }

        private string getRoundWinnerAndUpdateScore()
        {
            string winnerName = string.Empty;

            if (m_FourInARowGame.CurrentState != FourInARow.eStatesOfGame.Draw)
            {
                winnerName = m_FourInARowGame.LastWinner.Name;
                if (m_FourInARowGame.LastWinner == m_FourInARowGame.Player1)
                {
                    m_LabelScorePlayer1.Text = (int.Parse(m_LabelScorePlayer1.Text) + 1).ToString();
                }
                else
                {
                    m_LabelScorePlayer2.Text = (int.Parse(m_LabelScorePlayer2.Text) + 1).ToString();
                }
            }

            return winnerName;
        }

        private string getFinalWinnerName()
        {
            string winnerName = string.Empty;
           
            if (m_FourInARowGame.Player1.Score > m_FourInARowGame.Player2.Score)
            {
                winnerName = r_Player1Name;
            }
            else if(m_FourInARowGame.Player2.Score > m_FourInARowGame.Player1.Score) 
            {
                winnerName = r_Player2Name;
            }

            return winnerName;
        }
       
        private void makeAIMove()
        {
            int aIColChoice = m_FourInARowGame.GetAiNextMove();
            char currentPlayerSign = m_FourInARowGame.CurrentPlayer.Sign;

            m_FourInARowGame.MakeMove(aIColChoice, m_FourInARowGame.Player2, out int o_RowInserted);
            m_FourInARowGame.UpdateCurrentState(o_RowInserted, aIColChoice);
            FourInARow.eStatesOfGame state = m_FourInARowGame.CurrentState;
            updateGameButtons(o_RowInserted, aIColChoice, currentPlayerSign);
            if (state == FourInARow.eStatesOfGame.Lose || state == FourInARow.eStatesOfGame.Draw)
            {
                m_FourInARowGame.RoundOver(m_FourInARowGame.CurrentPlayer);
            }
        }

        private void changeTextBoldAndColor()
        {
            if (m_LabelPlayer1.Font.Bold)
            {
                m_LabelPlayer2.Font = new Font(m_LabelPlayer2.Font, FontStyle.Bold);
                m_LabelPlayer2.ForeColor = Color.Blue;
                m_LabelScorePlayer2.Font = new Font(m_LabelScorePlayer2.Font, FontStyle.Bold);
                m_LabelPlayer1.Font = new Font(m_LabelPlayer1.Font, FontStyle.Regular);
                m_LabelScorePlayer1.Font = new Font(m_LabelScorePlayer1.Font, FontStyle.Regular);
                m_LabelPlayer1.ForeColor = Color.Black;
            }
            else
            {
                m_LabelPlayer1.Font = new Font(m_LabelPlayer1.Font, FontStyle.Bold);
                m_LabelPlayer1.ForeColor = Color.Blue;
                m_LabelScorePlayer1.Font = new Font(m_LabelScorePlayer1.Font, FontStyle.Bold);
                m_LabelPlayer2.Font = new Font(m_LabelPlayer2.Font, FontStyle.Regular);
                m_LabelScorePlayer2.Font = new Font(m_LabelScorePlayer2.Font, FontStyle.Regular);
                m_LabelPlayer2.ForeColor = Color.Black;
            }
        }

        private bool endOfRoundMessageBox(string i_WinnerName)
        {
            string drawOrWin = i_WinnerName == string.Empty ? "A draw between the two players!" :
                                               string.Format(
                                                      "The winner is {0}!",
                                                       i_WinnerName);
            string message = string.Format("{0}{1}Would you like to play another round?", drawOrWin, Environment.NewLine);
            string title = i_WinnerName == string.Empty ? "A draw!" : "A win!";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            return result == DialogResult.Yes;
        }
    }
}
