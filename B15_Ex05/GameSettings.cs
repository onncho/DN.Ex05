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
        string m_SixSize = "6x6";

        public GameSettings()
        {
            this.Size = new Size(400, 200);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            m_ButtonAgainstComputer.Text = "Play Against Computer";
            m_ButtonAgainstPlayer.Text = "Play Against Your Friend";
            m_ButtonBoardSize.Text = "Board Size: " + m_SixSize + "(click to increase)";

            m_ButtonBoardSize.Size = new Size(new Point(this.Width * 85 / 100, this.Height * 20 / 100));
            m_ButtonAgainstComputer.Size = new Size(new Point(this.Width * 40 / 100, this.Height * 15 / 100));
            m_ButtonAgainstPlayer.Size = new Size(new Point(this.Width * 40 / 100, this.Height * 15 / 100));
            m_ButtonAgainstComputer.Location = new Point(this.Width * 5 / 100, this.Height * 50 / 100);
            m_ButtonAgainstPlayer.Location = new Point(this.Width * 50 / 100, this.Height * 50 / 100);
            m_ButtonBoardSize.Location = new Point(this.Width * 5 / 100, this.Height * 20 / 100);
            this.Controls.Add(m_ButtonAgainstComputer);
            this.Controls.Add(m_ButtonAgainstPlayer);
            this.Controls.Add(m_ButtonBoardSize);

        }
    }
}
