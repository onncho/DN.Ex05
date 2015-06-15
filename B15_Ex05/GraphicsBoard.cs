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
        private Control[] m_GameControls;
        private int m_LastFilledIndex;

        public GraphicsBoard(int i_boardSize, bool i_multiplayer)
        {
            m_BoardSize = i_boardSize;
            m_Multiplayer = i_multiplayer;
            m_gameMatrix = new Button[m_BoardSize, m_BoardSize];
            m_GameControls = new Control[m_BoardSize * m_BoardSize];
            m_LastFilledIndex = 0;
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_gameMatrix[i, j] = new Button();
                    m_gameMatrix[i, j].Size = new Size(new Point( 35 ,   35 ));
                    m_gameMatrix[i, j].Location = new Point(35 * j , 35 * i );
                    m_gameMatrix[i, j].Text = "(" + j + ", " + i + ")";
                    
                    m_GameControls[m_LastFilledIndex] = m_gameMatrix[i, j];
                    m_LastFilledIndex++;
                }
            }
            this.Controls.AddRange(m_GameControls);

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
