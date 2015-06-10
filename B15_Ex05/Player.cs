using System;
using System.Collections.Generic;
using System.Text;

namespace B15_Ex05
{
    class Player
    {
        private int m_playerIdentifier;
        public string m_playerName;
        public bool m_isPC;
        public int m_amountOfDiscs;

        public Player(int i_playerIdentifier, string i_playerName, bool i_isPC)
        {
            this.m_playerIdentifier = i_playerIdentifier;
            this.m_playerName = i_playerName;
            this.m_isPC = i_isPC;
            this.m_amountOfDiscs = 2;

        }



        public int getPlayerIdentifier()
        {
            return this.m_playerIdentifier;
        }

        public string getPlayerName()
        {
            return this.m_playerName;
        }

        public bool isPlayerPC()
        {
            return this.m_isPC;
        }

    }
}
