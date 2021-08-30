using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourInARowUI;

namespace FourInARowUI
{
    public class GameSettingsForm : Form
    {
        private const string k_AIName = "[Computer]";
        private const int k_MaxRowsAndCols = 8;
        private const int k_MinRowsAndCols = 4;

        private Label labelPlayers;
        private Label labelPlayer1;
        private CheckBox checkBoxPlayer2;
        private TextBox textBoxPlayer1;
        private TextBox textBoxPlayer2;
        private Label labelBoardSize;
        private NumericUpDown numericUpDownRows;
        private NumericUpDown numericUpDownCols;
        private Label labelRows;
        private Label labelCols;
        private Button buttonStartGame;


        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.numericUpDownRows = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCols = new System.Windows.Forms.NumericUpDown();
            this.labelRows = new System.Windows.Forms.Label();
            this.labelCols = new System.Windows.Forms.Label();
            this.buttonStartGame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownCols)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPlayers
            // 
            this.labelPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayers.Location = new System.Drawing.Point(21, 19);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(77, 20);
            this.labelPlayers.TabIndex = 0;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Location = new System.Drawing.Point(45, 52);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(84, 20);
            this.labelPlayer1.TabIndex = 1;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(48, 78);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(110, 24);
            this.checkBoxPlayer2.TabIndex = 1;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxSecondPlayer_Click);
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.Location = new System.Drawing.Point(132, 49);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(100, 26);
            this.textBoxPlayer1.TabIndex = 0;
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.Location = new System.Drawing.Point(132, 76);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new Size(100, 26);
            this.textBoxPlayer2.TabIndex = 2;
            this.textBoxPlayer2.Text = k_AIName;
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelBoardSize.Location = new System.Drawing.Point(21, 139);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(125, 25);
            this.labelBoardSize.TabIndex = 6;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // numericUpDownRows
            // 
            this.numericUpDownRows.Location = new System.Drawing.Point(89, 172);
            this.numericUpDownRows.Maximum = k_MaxRowsAndCols;
            this.numericUpDownRows.Minimum = k_MinRowsAndCols;
            this.numericUpDownRows.Name = "numericUpDownRows";
            this.numericUpDownRows.Size = new System.Drawing.Size(35, 26);
            this.numericUpDownRows.TabIndex = 3;
            this.numericUpDownRows.Value = k_MinRowsAndCols;
            // 
            // numericUpDownCols
            // 
            this.numericUpDownCols.Location = new System.Drawing.Point(210, 172);
            this.numericUpDownCols.Maximum = k_MaxRowsAndCols;
            this.numericUpDownCols.Minimum = k_MinRowsAndCols;
            this.numericUpDownCols.Name = "numericUpDownCols";
            this.numericUpDownCols.Size = new System.Drawing.Size(35, 26);
            this.numericUpDownCols.TabIndex = 4;
            this.numericUpDownCols.Value = k_MinRowsAndCols;
            // 
            // labelRows
            // 
            this.labelRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelRows.Location = new System.Drawing.Point(31, 172);
            this.labelRows.Name = "labelRows";
            this.labelRows.Size = new System.Drawing.Size(52, 25);
            this.labelRows.TabIndex = 9;
            this.labelRows.Text = "Rows";
            // 
            // labelCols
            // 
            this.labelCols.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelCols.Location = new System.Drawing.Point(162, 172);
            this.labelCols.Name = "labelCols";
            this.labelCols.Size = new System.Drawing.Size(42, 25);
            this.labelCols.TabIndex = 10;
            this.labelCols.Text = "Cols";
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.buttonStartGame.Location = new System.Drawing.Point(24, 217);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(221, 23);
            this.buttonStartGame.TabIndex = 5;
            this.buttonStartGame.Text = "Start!";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // GameSettingsForm
            // 
            this.AcceptButton = this.buttonStartGame;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new Size(284, 261);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.labelCols);
            this.Controls.Add(this.labelRows);
            this.Controls.Add(this.numericUpDownCols);
            this.Controls.Add(this.numericUpDownRows);
            this.Controls.Add(this.labelBoardSize);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.labelPlayers);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCols)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void checkBoxSecondPlayer_Click(object sender, System.EventArgs e)
        {
            this.textBoxPlayer2.Enabled = !this.textBoxPlayer2.Enabled;
            textBoxPlayer2.Text = textBoxPlayer2.Enabled ? string.Empty : k_AIName;
        }

        private void buttonStart_Click(object sender, System.EventArgs e)
        {
            if (this.textBoxPlayer1.Text == string.Empty || this.textBoxPlayer2.Text == string.Empty)
            {
                MessageBox.Show("Please enter proper names (no blanks)", "Error");
            }
            else if (this.textBoxPlayer1.Text == this.textBoxPlayer2.Text)
            {
                MessageBox.Show("Players names should be different, please change one of the names and try again.", "Error");
            }
            else
            {
                GameForm gameForm = new GameForm(BoardRows, BoardCols, Player1Name, Player2Name, IsPlayer2AI);
                this.Dispose();
                gameForm.ShowDialog();
            }
        }

        public int BoardRows
        {
            get { return (int)this.numericUpDownRows.Value; }
        }

        public int BoardCols
        {
            get { return (int)numericUpDownCols.Value; }
        }

        public string Player1Name
        {
            get { return this.textBoxPlayer1.Text; }
        }

        public string Player2Name
        {
            get { return textBoxPlayer2.Text; }
        }

        public bool IsPlayer2AI
        {
            get { return !this.checkBoxPlayer2.Checked; }
        }
    }
}
