using System;
using System.Windows.Forms;
using System.Drawing;
using FourInARowLogic;
using System.Threading;

namespace FourInARowUI
{
    public class GameForm : Form
    {
        private readonly string r_Player1Name, r_Player2Name;
        private readonly bool r_IsPlayer2AI;
        private readonly int r_Rows, r_Cols;
        private Label labelPlayer1;
        private Label labelPlayer2;
        private Label labelScorePlayer1;
        private Label labelScorePlayer2;
        private Button[,] m_ButtonsOfTheGame = null;
        private FourInARow m_FourInARowGame = null;

        public GameForm(int i_BoardHeight, int i_BoardWidth, string i_Player1Name, string i_Player2Name, bool i_IsAI)
        {
            this.r_Rows = i_BoardHeight;
            this.r_Cols = i_BoardWidth;
            this.r_Player1Name = i_Player1Name;
            this.r_Player2Name = i_Player2Name;
            this.r_IsPlayer2AI = i_IsAI;
            this.InitializeButtons();
            this.InitializeComponent();

        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            FourInARow.eGameStyle gameStyle = r_IsPlayer2AI ? FourInARow.eGameStyle.PlayerVsComputer : FourInARow.eGameStyle.PlayerVsPlayer;
            m_FourInARowGame = new FourInARow(r_Rows, r_Cols, gameStyle, r_Player1Name, r_Player2Name);

            m_FourInARowGame.PlayerSwitch += changeBoldText;
            m_FourInARowGame.GameOver += FourInARow_GameOver;
        }

        private void InitializeComponent()
        {
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.labelScorePlayer1 = new System.Windows.Forms.Label();
            this.labelScorePlayer2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Top = m_ButtonsOfTheGame[r_Rows, 0].Bottom + 10;
            this.labelPlayer1.Left = m_ButtonsOfTheGame[r_Rows, 0].Left;
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.TabIndex = 0;
            this.labelPlayer1.Text = r_Player1Name + ": ";
            // 
            // labelScorePlayer1
            // 
            // this.labelScorePlayer1.AutoSize = true;
            this.labelScorePlayer1.Left = labelPlayer1.Left + labelPlayer1.Width;
            this.labelScorePlayer1.Top = labelPlayer1.Top;
            this.labelScorePlayer1.Name = "labelScorePlayer1";
            this.labelScorePlayer1.Size = new Size(21, 24);
            this.labelScorePlayer1.TabIndex = 2;
            this.labelScorePlayer1.Text = "0";
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Top = this.labelPlayer1.Top;
            this.labelPlayer2.Left = labelScorePlayer1.Left + 40;
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.TabIndex = 1;
            this.labelPlayer2.Text = r_Player2Name + ": ";
            // 
            // labelScorePlayer2
            // 
            this.labelScorePlayer2.Left = labelPlayer2.Left + labelPlayer2.Width;
            this.labelScorePlayer2.Top = labelPlayer2.Top;
            this.labelScorePlayer2.Name = "labelScorePlayer2";
            this.labelScorePlayer2.Size = new System.Drawing.Size(21, 24);
            this.labelScorePlayer2.TabIndex = 3;
            this.labelScorePlayer2.Text = "0";
            // 
            // GameForm
            // 
            // this.ClientSize = new System.Drawing.Size(278, 244);
            this.Controls.Add(this.labelScorePlayer2);
            this.Controls.Add(this.labelScorePlayer1);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.ClientSize = new Size(r_Cols * 60 + 20, (r_Rows + 1) * 60 + 50);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Name = "GameForm";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = "4 In A Raw !!";
            this.Load += GameForm_Load;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeButtons()
        {
            m_ButtonsOfTheGame = new Button[r_Rows + 1, r_Cols];
            for (int row = 0; row <= r_Rows; row++)
            {
                for (int col = 0; col < r_Cols; col++)
                {
                    m_ButtonsOfTheGame[row, col] = new Button();
                    if (row == 0)
                    {
                        m_ButtonsOfTheGame[row, col].Enabled = true;
                        m_ButtonsOfTheGame[row, col].Size = new Size(60, 35);
                        m_ButtonsOfTheGame[row, col].Text = (col + 1).ToString();
                        m_ButtonsOfTheGame[row, col].Click += new EventHandler(ColButton_OnClick);
                    }
                    else
                    {
                        m_ButtonsOfTheGame[row, col].Size = new Size(60, 45);
                        m_ButtonsOfTheGame[row, col].Enabled = false;
                    }

                    m_ButtonsOfTheGame[row, col].Location = new Point((60 * col) + 10, (60 * row) + 30);
                    this.Controls.Add(m_ButtonsOfTheGame[row, col]);
                }
            }
        }

        private void updateGameBoard(int i_Row, int i_Col, char i_PlayerSign)
        {
            Button buttonToUpdate = m_ButtonsOfTheGame[i_Row, i_Col - 1];
            buttonToUpdate.Text = i_PlayerSign.ToString();
            for(int col = 0; col < r_Cols; col++)
            {
                if(m_ButtonsOfTheGame[1,col].Text != "")
                {
                    m_ButtonsOfTheGame[0, col].Enabled = false;
                }
            }
        }

        private void ColButton_OnClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int col = int.Parse(button.Text);
            if (button != null)
            {
                char currentPlayerSign = m_FourInARowGame.CurrentPlayer.Sign;
                m_FourInARowGame.MakeMove(col, this.m_FourInARowGame.CurrentPlayer, out int o_RowInserted);
                updateGameBoard(o_RowInserted, col, currentPlayerSign);
                FourInARow.eStatesOfGame state = m_FourInARowGame.GetCurrentStateOfGame(o_RowInserted, col);

                if (state == FourInARow.eStatesOfGame.Lose || state == FourInARow.eStatesOfGame.Draw)
                {
                    m_FourInARowGame.RoundOver(m_FourInARowGame.CurrentPlayer);
                }
                else
                {
                    if (r_IsPlayer2AI)
                    {
                        //Thread.Sleep(1000);
                        makeAIMove();
                    }
                }
            }
        }

        private void FourInARow_GameOver()
        {
            string roundWinner = getRoundWinnerAndUpdateScore();
            string finalWinner = getFinalWinnerName(); // In case the game does not continue - gets the player with the highest score to show as winner
            Opacity = 0.8;
            if (endOfRoundMessageBox(roundWinner))
            {
                clearGameBoardButtons();
                m_FourInARowGame.CurrentState = FourInARow.eStatesOfGame.Continue;
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
            for(int row = 1; row <= r_Rows; row++)
            {
                for(int col = 0;col < r_Cols; col++)
                {
                    m_ButtonsOfTheGame[0, col].Enabled = true;
                    m_ButtonsOfTheGame[row, col].Text = string.Empty;
                }
            }            
        }

        private string getRoundWinnerAndUpdateScore()
        {
            string winnerName = string.Empty;
            if (m_FourInARowGame.CurrentState != FourInARow.eStatesOfGame.Draw)
            {
                winnerName = m_FourInARowGame.CurrentPlayer == m_FourInARowGame.Player1 ?
                             r_Player2Name : r_Player1Name;
                if (m_FourInARowGame.CurrentPlayer == m_FourInARowGame.Player1)
                {
                    labelScorePlayer2.Text = (int.Parse(labelScorePlayer2.Text) + 1).ToString();
                }
                else
                {
                    labelScorePlayer1.Text = (int.Parse(labelScorePlayer1.Text) + 1).ToString();
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
            //Thread.Sleep(1000);
            int aIColChoice = m_FourInARowGame.GetAiNextMove();
            char currentPlayerSign = m_FourInARowGame.CurrentPlayer.Sign;

            m_FourInARowGame.MakeMove(aIColChoice, m_FourInARowGame.Player2, out int o_RowInserted);
            FourInARow.eStatesOfGame state = m_FourInARowGame.GetCurrentStateOfGame(o_RowInserted, aIColChoice);
            updateGameBoard(o_RowInserted, aIColChoice, currentPlayerSign);
            if (state == FourInARow.eStatesOfGame.Lose || state == FourInARow.eStatesOfGame.Draw)
            {
                m_FourInARowGame.RoundOver(m_FourInARowGame.CurrentPlayer);
            }
        }

        private void changeBoldText()
        {
            if (labelPlayer1.Font.Bold)
            {
                labelPlayer2.Font = new Font(labelPlayer2.Font, FontStyle.Bold);
                labelPlayer2.ForeColor = Color.Blue;
                labelScorePlayer2.Font = new Font(labelScorePlayer2.Font, FontStyle.Bold);
                labelPlayer1.Font = new Font(labelPlayer1.Font, FontStyle.Regular);
                labelScorePlayer1.Font = new Font(labelScorePlayer1.Font, FontStyle.Regular);
                labelPlayer1.ForeColor = Color.Black;

            }
            else
            {
                labelPlayer1.Font = new Font(labelPlayer1.Font, FontStyle.Bold);
                labelPlayer1.ForeColor = Color.Blue;
                labelScorePlayer1.Font = new Font(labelScorePlayer1.Font, FontStyle.Bold);
                labelPlayer2.Font = new Font(labelPlayer2.Font, FontStyle.Regular);
                labelScorePlayer2.Font = new Font(labelScorePlayer2.Font, FontStyle.Regular);
                labelPlayer2.ForeColor = Color.Black;
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
