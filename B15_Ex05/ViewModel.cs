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
            m_GameControler.ModelBoardChanged += this.OnModelBoardChanged;
            m_GameControler.runGame();
        }

        internal void move(int[] i_PlayerWantedMove)
        {
            // add player move here the connection between the prees and logic

            //send to a method that checks if the move is valid, if it is a board change event need to occur.
            //m_GameControler.CHECKIT
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
                BoardChanged(this, EventArgs.Empty);
            }
        }
    }
}
