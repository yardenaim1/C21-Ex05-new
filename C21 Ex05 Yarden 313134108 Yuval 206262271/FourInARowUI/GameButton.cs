
using System.Windows.Forms;
using System.Drawing;

namespace FourInARowUI
{
    public class GameButton : Button
    {
        private readonly int r_Row;
        private readonly int r_Col;

        public GameButton(int i_Row, int i_Col)
        {
            r_Row = i_Row;
            r_Col = i_Col;
        }

        public int Row
        {
            get { return r_Row; }
        }

        public int Col
        {
            get { return r_Col; }
        }
    }
}
