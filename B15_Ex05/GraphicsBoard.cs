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
        private Button[,] m_gameMatrix;
        private Control[] m_GameControls;
        private int m_LastFilledIndex;
        private ViewModel m_ViewModel;
        private List<int[]> m_playerMoves;
        private bool m_FirstIteration = true;

        public GraphicsBoard(int i_boardSize, bool i_multiplayer)
        {
            m_BoardSize = i_boardSize;
            m_Multiplayer = i_multiplayer;
            m_gameMatrix = new Button[m_BoardSize, m_BoardSize];
            m_GameControls = new Control[m_BoardSize * m_BoardSize];
            m_LastFilledIndex = 0;
            m_ViewModel = new ViewModel(i_boardSize, m_Multiplayer);

            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_gameMatrix[i, j] = new Button();
                    m_gameMatrix[i, j].Enabled = false;
                    m_gameMatrix[i, j].AutoSize = true;
                    m_gameMatrix[i, j].Size = new Size(new Point(50, 50));
                    m_gameMatrix[i, j].Location = new Point(50 * j, 50 * i);
                    m_gameMatrix[i, j].Text = "(" + j + ", " + i + ")";
                    m_gameMatrix[i, j].Tag = new int[2] { i, j };
                    //m_gameMatrix[i, j].Click += new EventHandler(doSome);
                    m_GameControls[m_LastFilledIndex] = m_gameMatrix[i, j];
                    m_LastFilledIndex++;
                }
            }

            this.Controls.AddRange(m_GameControls);
            InitializeComponent();

             
            //register to event from viewModel
            m_ViewModel.BoardChanged += this.OnBoardChanged;

            //run game
            m_ViewModel.runGame();

            //Set background color
            this.BackColor = Color.LightGray;

            this.ShowDialog();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(m_BoardSize * 50, m_BoardSize * 50);
            this.Name = "GraphicsBoard";
            
            // @TODO: Check how to fix resize window with all forms
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ResumeLayout(false);

        }

        private void doSome(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                int[] tuple = button.Tag as int[];

                //call the move function with the clicked butotn index
                removeListeners();
                m_ViewModel.move(tuple);
            }

        }

        private void removeListeners()
        {
            foreach (int[] tuple in m_playerMoves)
            {
                m_gameMatrix[tuple[0], tuple[1]].BackColor = Color.LightGray;
                m_gameMatrix[tuple[0], tuple[1]].Enabled = false;
                m_gameMatrix[tuple[0], tuple[1]].Click -= this.doSome;
            }
        }

        // listen when the board changed and update it accordingly
        public void OnBoardChanged(object source, EventArgs args)
        {
            GameController gameControler = source as GameController;
            int[,] boardMatrix = gameControler.getMatrix();
            updateGraphicBoard(boardMatrix);
            if (!m_FirstIteration) 
            {
                m_ViewModel.m_FirstPlayerTurn = !m_ViewModel.m_FirstPlayerTurn;
                printTitleToForm();
            }
            else 
            {
                m_FirstIteration = false;
            }
            

            //m_playerMoves = m_ViewModel.getPlayerMoves();
            //updatePlayerAvailableMoves(m_playerMoves);

            // @TODO: check the logic when pc plays all the graphics are mass out

            if (m_Multiplayer || (!m_Multiplayer && m_ViewModel.m_FirstPlayerTurn))
            {
                m_playerMoves = m_ViewModel.getPlayerMoves();
                updatePlayerAvailableMoves(m_playerMoves);
            }
           
            else if (!m_Multiplayer && !m_ViewModel.m_FirstPlayerTurn)
            {
                gameControler.pcMove();
            }
        }

        private void printTitleToForm()
        {
            string playerTitle = m_ViewModel.m_FirstPlayerTurn ? "Black turn" : "White turn";
            this.Text = playerTitle;
        }

        private void updatePlayerAvailableMoves(List<int[]> playerMoves)
        {
            foreach (int[] tuple in playerMoves)
            {
                m_gameMatrix[tuple[0], tuple[1]].BackColor = Color.YellowGreen;
                m_gameMatrix[tuple[0], tuple[1]].Enabled = true;
                m_gameMatrix[tuple[0], tuple[1]].Click += new EventHandler(doSome);

            }
        }

        private void updateGraphicBoard(int[,] i_BoardMatrix)
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if (i_BoardMatrix[i, j] == 1)
                    {
                        m_gameMatrix[i, j].BackColor = Color.Black;
                        m_gameMatrix[i, j].ForeColor = Color.White;
                        m_gameMatrix[i, j].Text = "O";
                        m_gameMatrix[i, j].Enabled = false;
                    }
                    else if (i_BoardMatrix[i, j] == -1)
                    {
                        m_gameMatrix[i, j].BackColor = Color.White;
                        m_gameMatrix[i, j].ForeColor = Color.Black;
                        m_gameMatrix[i, j].Text = "O";
                        m_gameMatrix[i, j].Enabled = false;
                    }
                }
            }
        }



        public void OnHideBoard(object source, EventArgs args) 
        {
            this.Hide();

        }
    }
}
