using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace B15_Ex05
{
    public class GraphicsBoard : Form 
    {
        private int m_BoardSize;
        private bool m_Multiplayer;
        private int m_ButtonSize = 35;
        private Button[,] m_gameMatrix;

        public GraphicsBoard(int i_boardSize, bool i_multiplayer)
        {
            m_BoardSize = i_boardSize;
            m_Multiplayer = i_multiplayer;
            m_gameMatrix = 
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
