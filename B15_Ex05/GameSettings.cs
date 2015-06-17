using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B15_Ex05
{

    class GameSettings : Form
    {
        // Define Delegate to raise events
        //public event EventHandler BoardSizeEventHandler BoardSizeChosen;

        public event EventHandler GameOptionChoiceEventHandler;

        Button m_ButtonBoardSize = new Button();
        Button m_ButtonAgainstComputer = new Button();
        Button m_ButtonAgainstPlayer = new Button();

        const string k_SixSize = "Board Size: 6x6 (click to increase)";
        const string k_EightSize = "Board Size: 8x8 (click to increase)";
        const string k_TenSize = "Board Size: 10x10 (click to increase)";
        const string k_TwelveSize = "Board Size: 12x12 (click to reset)";

        int m_BoardSizeIndex = 6;

        public GameSettings()
        {
            this.Size = new Size(400, 200);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";          
        }


        //publish which game mode the user chose.
        internal virtual void OnGameOptionChoiceEventHandler()
        {
            if (GameOptionChoiceEventHandler != null)
            {
                GameOptionChoiceEventHandler(this, EventArgs.Empty);
            }
        }




        /// <summary>
        /// This method will be called once, just before the first time the form is displayed
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            InitControls();
        }

        /// <summary>
        /// Layouting the controls (textboxes, lables, buttons) on the form
        /// </summary>
        private void InitControls()
        {
            m_ButtonAgainstComputer.Text = "Play Against Computer";
            m_ButtonAgainstPlayer.Text = "Play Against Your Friend";
            m_ButtonBoardSize.Text = k_SixSize;

            m_ButtonBoardSize.Size = new Size(new Point(this.Width * 85 / 100, this.Height * 20 / 100));
            m_ButtonAgainstComputer.Size = new Size(new Point(this.Width * 40 / 100, this.Height * 15 / 100));
            m_ButtonAgainstPlayer.Size = new Size(new Point(this.Width * 40 / 100, this.Height * 15 / 100));
            m_ButtonAgainstComputer.Location = new Point(this.Width * 5 / 100, this.Height * 50 / 100);
            m_ButtonAgainstPlayer.Location = new Point(this.Width * 50 / 100, this.Height * 50 / 100);
            m_ButtonBoardSize.Location = new Point(this.Width * 5 / 100, this.Height * 20 / 100);

            this.Controls.AddRange(new Control[] { m_ButtonBoardSize, m_ButtonAgainstPlayer, m_ButtonAgainstComputer });

            m_ButtonBoardSize.Click += new EventHandler(m_ButtonBoardSize_Click);
            m_ButtonAgainstPlayer.Click += new EventHandler(m_ButtonAgainstPlayer_Click);
            m_ButtonAgainstComputer.Click += new EventHandler(m_ButtonAgainstComputer_Click);

        }

        //protected virtual void OnBoardSizeChosen()
        //{
        //    if (BoardSizeChosen != null)
        //    {
        //        BoardSizeChosen(this, EventArgs.Empty);
        //    }
        //} 

        private void m_ButtonAgainstComputer_Click(object sender, EventArgs e)
        {
        //    GameController gc = new GameController(false);
           
        //    //TODD - call the graphic board
        //    GraphicsBoard board = new GraphicsBoard(m_BoardSizeIndex, false);
            
            //not need it because the graphic does that
            //ViewModel gameFlow = new ViewModel(this.m_BoardSizeIndex, false);
            //gameFlow.runGame();

            GraphicsBoard gameBoard = new GraphicsBoard(this.m_BoardSizeIndex, false);
            //this.OnGameOptionChoiceEventHandler += gameFlow.OnGameOptionChoiceEventHandler;
            //board.UserClickedButtonEventHandler += gc.OnUserClickedButtonEventHandler;

            //board.ShowDialog();

        }

        private void m_ButtonAgainstPlayer_Click(object sender, EventArgs e)
        {
            //ViewModel gameflow = new ViewModel(this.m_BoardSizeIndex, true); // ALREADY IN GAMEBOARD
            GraphicsBoard gameBoard = new GraphicsBoard(this.m_BoardSizeIndex,true);
        }

        private void m_ButtonBoardSize_Click(object sender, EventArgs e)
        {
            if (m_BoardSizeIndex == 6)
            {
                m_ButtonBoardSize.Text = k_EightSize;
            }
            else if (m_BoardSizeIndex == 8)
            {
                m_ButtonBoardSize.Text = k_TenSize;
            }
            else if (m_BoardSizeIndex == 10)
            {
                m_ButtonBoardSize.Text = k_TwelveSize;
            }

            if (m_BoardSizeIndex < 12)
            {
                m_BoardSizeIndex += 2;
            }
            else
            {
                m_BoardSizeIndex = 6;
                m_ButtonBoardSize.Text = k_SixSize;
            }
            
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GameSettings
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "GameSettings";
            this.Load += new System.EventHandler(this.GameSettings_Load);
            this.ResumeLayout(false);

        }

        private void GameSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
