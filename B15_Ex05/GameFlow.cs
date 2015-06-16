using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace B15_Ex05
{
    class GameFlow
    {
        private GraphicsBoard m_GraphicBoard;
        private GameController m_GameControler;
        private bool v_PlayAgainstPC = false;
        private int m_boardSize;
        

        public GameFlow(int i_boardSize, bool i_PlayAgainstPc) {
            
            m_boardSize = i_boardSize;
            v_PlayAgainstPC = i_PlayAgainstPc;

            m_GraphicBoard = new GraphicsBoard(m_boardSize, !v_PlayAgainstPC);
       

            if (m_boardSize == 12)
            {
                m_GameControler = new GameController(true);
            }

            m_GameControler = new GameController(false);


            //register events
            m_GraphicBoard.UserClickedButtonEventHandler += this.OnUserClickedButtonEventHandler;

            m_GameControler.PublishLegalMovesAndAddListener += this.OnPublishLegalMovesAndAddListener;
            m_GameControler.PublishLegalMovesAndRemoveListener += this.OnPublishLegalMovesAndRemoveListener;

            m_GraphicBoard.ShowDialog();
        }


        public void OnUserClickedButtonEventHandler(object source, EventArgs args)
        {
            
            int[] buttonIndex = source as int[];

            Console.WriteLine(buttonIndex[0].ToString() + "," + buttonIndex[1].ToString());
            Console.WriteLine("entered");
            //m_PlayerWantedMove = buttonIndex;
        }


        internal void addListener(List<int[]> i_PossibleMoves)
        {
            foreach (int[] item in i_PossibleMoves)
            {
                //register for event for every button
                m_GraphicBoard.addEvent(item[0],item[1]);
            }
        }


        internal void removeListener(List<int[]> i_Moves)
        {
            foreach (int[] move in i_Moves)
            {
                m_GraphicBoard.removeEvent(move[0], move[1]);
            }
        }


        public void initAgainstPc()
        {

        }

        public void initAgainstPlayer()
        {
        }

        public void OnPublishLegalMovesAndRemoveListener(object source, EventArgs args)
        {
            List<int[]> possibleMoves = source as List<int[]>;
            removeListener(possibleMoves);
        }

        public void OnPublishLegalMovesAndAddListener(object source, EventArgs args)
        {
            List<int[]> possibleMoves = source as List<int[]>;
            addListener(possibleMoves);
        }

        //// listen to game mode choice
        //public void OnGameOptionChoiceEventHandler(object source, EventArgs args)
        //{
        //    Button boardSize = source as Button;
        //    (boardSize.Text
        //    m_GameControler = new GameController(false);
        //}
        
        
    }
}
