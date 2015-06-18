using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace B15_Ex05
{
    internal class ViewModel
    {

        private int m_BoardSize;
        private bool v_Multiplayer = false;

        private GameController m_GameControler;
        internal Player m_PlayerOne, m_PlayerTwo;
        
        private int m_BlackRoundWon = 0;
        private int m_WhiteRoundWon = 0;

        internal bool m_FirstPlayerTurn = true;

        public ViewModel(int i_BoardSize, bool i_multiplayer)
        {
            m_BoardSize = i_BoardSize;
            v_Multiplayer = i_multiplayer;
        }

        public event EventHandler BoardChanged;
        //public event EventHandler GameOver;

        internal void runGame()
        {
            m_GameControler = new GameController(m_BoardSize);
            m_PlayerOne = new Player(1, "1", false);
            m_PlayerTwo = v_Multiplayer ? new Player(-1, "2", false) : new Player(-1, "2", true);
            m_GameControler.ModelBoardChanged += this.OnModelBoardChanged;
            m_GameControler.GameOver += this.OnGameOver;
            m_GameControler.initPlayers(m_PlayerOne, m_PlayerTwo, !v_Multiplayer);
        }

        internal void move(int[] i_PlayerWantedMove)
        {
            // change to other player becuase if we got here the user pressed legal move;
            Player player = m_FirstPlayerTurn ? m_PlayerOne : m_PlayerTwo;
            m_GameControler.playerMoveFlow(player, true, i_PlayerWantedMove);
        }

        //publish game over
        public void OnGameOver(object source, EventArgs args)
        {
            int[] gameScore = m_GameControler.getScore();

            initMessageBox(gameScore);


        }

        private void initMessageBox(int[] gameScore)
        {
            //List<int> scores = m_MainOthelloLogic.GetScore();
            string finalScoreText = "";
            if (gameScore[0] != gameScore[1])
            {
                finalScoreText = string.Format("{0} Won!! ({1}/{2}) ({3}/{4}) {5}Would you like another round?",


                m_MainOthelloLogic.GetWinner(),
                scores[0],
                scores[1],
                s_Player1Victories,
                s_Player2Victories,
                Environment.NewLine);
            }
            else
            {
                summary = string.Format(
                "Draw!! ({0}/{1}) ({2}/{3}) {4}Would you like another round?",
                scores[0],
                scores[1],
                s_Player1Victories,
                s_Player2Victories,
                Environment.NewLine);
            }

            DialogResult messageResult = MessageBox.Show(summary, "Othello", MessageBoxButtons.YesNo);
            if (messageResult == DialogResult.Yes)
            {
                this.Hide();
                OthelloMainGameForm newGame = new OthelloMainGameForm(r_VsHuman, r_BoardSize);
            }

            System.Environment.Exit(0);


        }



        // listener to model board change from the logic layer
        public void OnModelBoardChanged(object source, EventArgs args)
        {
            //GameController boardMatrix = source as GameController;
            OnBoardChanged();
        }

        // publish the board matrix who ever want to recevie the change and present it
        protected virtual void OnBoardChanged()
        {
            if (BoardChanged != null)
            {
                BoardChanged(this.m_GameControler, EventArgs.Empty);
            }
        }

        internal List<int[]> getPlayerMoves()
        {
            return (m_FirstPlayerTurn ? m_GameControler.getMovesByPlayer(m_PlayerOne) :
                    m_GameControler.getMovesByPlayer(m_PlayerTwo));
        }
    }
}
