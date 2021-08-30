namespace FourInARowLogic
{
    public class Player
    {
        private readonly ePlayerType r_PlayerType;
        private readonly string r_Name;
        private readonly char r_Sign;
        private int m_Score = 0;


        public Player(ePlayerType i_Type, char i_Sign, string i_Name)
        {
            r_PlayerType = i_Type;
            r_Name = i_Name;
            r_Sign = i_Sign;
        }

        public ePlayerType PlayerType
        {
            get
            {
                return r_PlayerType;
            }
        }

        public char Sign
        {
            get
            {
                return r_Sign;
            }
        }

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public bool IsHuman()
        {
            return this.PlayerType == ePlayerType.Player1 || this.PlayerType == ePlayerType.Player2;
        }

        public enum ePlayerType
        {
            Player1,
            Player2,
            Computer
        }
    }
}