using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B15_Ex05
{
    class GameSettings : Form
    {
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

        private void m_ButtonAgainstComputer_Click(object sender, EventArgs e)
        {
            //TODD - call the graphic board
            throw new NotImplementedException();
        }

        private void m_ButtonAgainstPlayer_Click(object sender, EventArgs e)
        {
            //TODO - call the graphic board
            throw new NotImplementedException();
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


        
    }
}
