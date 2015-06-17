using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace B15_Ex05
{
    internal class ViewModel
    {

        private int m_BoardSize;
        private bool v_PlayAgainstPc = false;

        private GameController m_GameControler;
        internal Player m_PlayerOne, m_PlayerTwo;

        internal bool m_FirstPlayerTurn = false;



        public ViewModel(int i_BoardSize, bool i_PlayAgainstPc)
        {
            m_BoardSize = i_BoardSize;
            v_PlayAgainstPc = i_PlayAgainstPc;
           
            
        }

        public event EventHandler BoardChanged;
        public event EventHandler GameOver;

        internal void runGame()
        {
            m_GameControler = new GameController(m_BoardSize);
            m_PlayerOne = new Player(1, "1", false);
            m_PlayerTwo = v_PlayAgainstPc ? new Player(-1, "2", true) : new Player(-1, "2", false);
            m_GameControler.ModelBoardChanged += this.OnModelBoardChanged;
            m_GameControler.initPlayers(m_PlayerOne, m_PlayerTwo, v_PlayAgainstPc);
        }

        internal void move(int[] i_PlayerWantedMove)
        {
            // add player move here the connection between the prees and logic

            //send to a method that checks if the move is valid, if it is a board change event need to occur.
            //m_GameControler.CHECKIT

            // change to other player becuase if we got here the user pressed legal move;
            Player player = m_FirstPlayerTurn ? m_PlayerOne : m_PlayerTwo;
            m_GameControler.playerMoveFlow(player, true, i_PlayerWantedMove);
        }

        // listener to model board change from the logic layer
        public void OnModelBoardChanged(object source, EventArgs args)
        {
            GameController boardMatrix = source as GameController;
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
