using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B15_Ex05
{
    public class GraphicsBoard : Form 
    {
        Button m_ButtonBoardSize = new Button();
        Button m_ButtonAgainstComputer = new Button();
        Button m_ButtonAgainstPlayer = new Button();

        public GraphicsBoard()
        {
            /*
            this.Size = new Size(400, 200);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Othello";
            m_ButtonAgainstComputer.Text = "Play Against Computer";
            m_ButtonAgainstPlayer.Text = "Play Against Your Friend";
            m_ButtonAgainstComputer.Size = new Size(new Point(this.Width * 40 / 100, this.Height * 15 / 100));
            m_ButtonAgainstPlayer.Size = new Size(new Point(this.Width * 40 / 100, this.Height * 15 / 100));
            m_ButtonAgainstComputer.Location = new Point(this.Width * 5 / 100, this.Height * 60 / 100);
            m_ButtonAgainstPlayer.Location = new Point(this.Width * 50 / 100, this.Height * 60 / 100);
            this.Controls.Add(m_ButtonAgainstComputer);
            this.Controls.Add(m_ButtonAgainstPlayer); */
        }

    }
}
