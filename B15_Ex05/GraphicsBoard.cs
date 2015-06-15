using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B15_Ex05
{
    public class GraphicsBoard : Form 
    {
        private int m_boardSize;
        private bool m_multiplayer;

        public GraphicsBoard(int i_boardSize, bool i_multiplayer)
        {
            m_boardSize = i_boardSize;
            m_multiplayer = i_multiplayer;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GraphicsBoard
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "GraphicsBoard";
            this.Load += new System.EventHandler(this.GraphicsBoard_Load);
            this.ResumeLayout(false);

        }

        private void GraphicsBoard_Load(object sender, EventArgs e)
        {

        }
    }
}
