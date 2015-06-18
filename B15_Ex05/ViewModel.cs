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
        public event EventHandler HideBoard;
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

            // update rounds
            if (gameScore[0] > gameScore[1])
            {
                m_BlackRoundWon++;
            }
            else if (gameScore[0] < gameScore[1])
            {
                m_WhiteRoundWon++;
            }
            else
            {
                m_BlackRoundWon++;
                m_WhiteRoundWon++;
            }

            initMessageBox(gameScore);
        }


        // publish hide board event when game over and user choose another game
        protected virtual void OnHideBoard()
        {
            if (HideBoard != null)
            {
                HideBoard(this, EventArgs.Empty);
            }
        }


        private void initMessageBox(int[] gameScore)
        {
            string winner = " Won!! ", anotherGame = " Would you like another round?";
            string winnerString = "", amountWon = "", differAmount = "";

            string scoreFormatted = "";

            winnerString = (gameScore[0] > gameScore[1] ? "Black" : "White") + winner;
            if (gameScore[0] == gameScore[1])
            {
                winnerString = "Draw!! ";
            }

            amountWon = "(" + (gameScore[0] >= gameScore[1] ? m_BlackRoundWon : m_WhiteRoundWon) + "/" + (m_BlackRoundWon + m_WhiteRoundWon) + ")";
            differAmount = "(" + (gameScore[0] >= gameScore[1] ? gameScore[0] : gameScore[1]) + "/" + (gameScore[0] + gameScore[1]) + ")";

            scoreFormatted = winnerString + differAmount + " " + amountWon + anotherGame;


            DialogResult messageResult = MessageBox.Show(scoreFormatted, "Othello", MessageBoxButtons.YesNo);
            if (messageResult == DialogResult.Yes)
            {
                OnHideBoard();
                
                //OthelloMainGameForm newGame = new OthelloMainGameForm(r_VsHuman, r_BoardSize);
            }

            //System.Environment.Exit(0);
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
