using System;
using System.Collections.Generic;
using System.Text;

namespace B15_Ex05
{

    class GameController
    {
        public event EventHandler ModelBoardChanged;

        // What we need to pass : 
        // 1. updated matrix
        // 2. who's turn
        // 3. possible moves (green)
        // 4. end game

        //private int[] m_PlayerWantedMove;

        private int m_BoardSize;
        private int[,] m_gameMatrix; // 0 - empty cell , 1 - player 1 chip , -1 player 2 chip
        private List<int[]> m_emptyCellsCollection;
        private Player m_playerOne, m_playerTwo;
        private Board m_gameBoard;

        internal Player PlayerOne
        {
            get
            {
                return m_playerOne;
            }

            set
            {
                m_playerOne = value;
            }
        }

        internal Player PlayerTwo
        {
            get
            {
                return m_playerTwo;
            }

            set
            {
                m_playerTwo = value;
            }
        }

        public GameController(int i_matrixSize)
        {
            this.m_BoardSize = i_matrixSize;// ? 8 : 6; //@TODO: need to support more sizes
            this.m_gameMatrix = new int[m_BoardSize, m_BoardSize]; // initiliaze it automaticaly as 0 - none player
            int centeredChips = 0, centeredChipsMinusOne = 0;

            m_emptyCellsCollection = new List<int[]>();

            centeredChips = (m_BoardSize / 2);
            centeredChipsMinusOne = centeredChips - 1;

            //initiate all matrix cells with 0;
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_gameMatrix[i, j] = 0;
                    if (i != centeredChipsMinusOne && j != centeredChipsMinusOne ||
                        i != centeredChipsMinusOne && j != centeredChips ||
                        i != centeredChips && j != centeredChipsMinusOne ||
                        i != centeredChips && j != centeredChips)
                    {
                        m_emptyCellsCollection.Add(new int[2] { i, j }); //matrix[i][j] == 0
                    }
                }
            }

            m_gameMatrix[centeredChipsMinusOne, centeredChipsMinusOne] = -1; // size / 2 -1
            m_gameMatrix[centeredChips, centeredChips] = -1;
            m_gameMatrix[centeredChipsMinusOne, centeredChips] = 1;
            m_gameMatrix[centeredChips, centeredChipsMinusOne] = 1;

            
        }


        // publish board change
        protected virtual void OnModelBoardChanged()
        {
            if (ModelBoardChanged != null)
            {
                ModelBoardChanged(this, EventArgs.Empty);
            }
        }

        public int[,] getMatrix()
        {
            return m_gameMatrix;
        }

        public void initPlayers(Player i_playerOne, Player i_playerTwo, bool i_isSingle)
        {
            this.m_playerOne = new Player(i_playerOne.getPlayerIdentifier(), i_playerOne.getPlayerName(), false);


            // place agaist pc or agaist friend 
            if (i_isSingle)
            {
                this.m_playerTwo = new Player(i_playerTwo.getPlayerIdentifier(), i_playerTwo.getPlayerName(), true);
            }
            else
            {
                this.m_playerTwo = new Player(i_playerTwo.getPlayerIdentifier(), i_playerTwo.getPlayerName(), false);
            }

            OnModelBoardChanged();
        }

        //public void runGame()
        //{
        //    bool firstPlayer = true; // If not first player then second player turn

        //    //this.m_gameBoard.printBoard(this.getMatrix());// @TODO: this method prints to console

        //    while (true)
        //    {
        //        bool invalidInput = false;

        //        bool isThereAnyMoveToPlayerOne = isThereAnyMovesLeft(m_playerOne.getPlayerIdentifier()),
        //            isThereAnyMoveToPlayerTwo = isThereAnyMovesLeft(m_playerTwo.getPlayerIdentifier());

        //        if (!isThereAnyMoveToPlayerOne && !isThereAnyMoveToPlayerTwo)
        //        {
        //            // no one has any moves left
        //            endGame();
        //            break;
        //        }
        //        else if (firstPlayer)
        //        {
                    

        //            // @TODO: cancel the matrix print 
        //            GameIO.printNextPlayerTurn(m_playerOne.getPlayerIdentifier());

        //            if (playerMoveFlow(m_playerOne, isThereAnyMoveToPlayerOne, m_PlayerWantedMove))
        //            {
        //                firstPlayer = !firstPlayer;
        //                Console.Clear();
        //            }
        //            else
        //            {
        //                invalidInput = true;
        //            }
        //        }
        //        else
        //        {
        //            // @TODO: cancel the matrix print
        //            if (!m_playerTwo.isPlayerPC())
        //            {
                        
        //                GameIO.printNextPlayerTurn(m_playerTwo.getPlayerIdentifier());

        //                if (playerMoveFlow(m_playerTwo, isThereAnyMoveToPlayerTwo, m_PlayerWantedMove))
        //                {
        //                    firstPlayer = !firstPlayer;
        //                    Console.Clear();

        //                }
        //                else
        //                {
        //                    invalidInput = true;
        //                }
        //            }
        //            else if (m_playerTwo.isPlayerPC())
        //            {
        //                // @TODO: cancel the matrix print
        //                Console.WriteLine("PC turn");
        //                List<int[]> validPoints = isThereAnyMovesLeftList(m_playerTwo.getPlayerIdentifier());
        //                int[] pointToInsertTo = getRandomPointForPC(validPoints);
        //                List<int[]> validDirectionsForPoint = chooseMoveForPC(pointToInsertTo, m_playerTwo.getPlayerIdentifier());
        //                Console.WriteLine("(" + pointToInsertTo[0] + "," + pointToInsertTo[1] + ")");
        //                executePlayerMove(validDirectionsForPoint, m_playerTwo, pointToInsertTo);
        //                firstPlayer = !firstPlayer;
        //                Console.Clear();
        //            }
        //        }
        //        Console.Clear();
        //        this.m_gameBoard.printBoard(this.getMatrix());
        //        if (invalidInput)
        //        {
        //            GameIO.printErrorForInvalidMove();
        //        }
        //    }
        //}


        internal void pcMove()
        {
            List<int[]> validPoints = isThereAnyMovesLeftList(m_playerTwo.getPlayerIdentifier());
            int[] pointToInsertTo = getRandomPointForPC(validPoints);
            List<int[]> validDirectionsForPoint = chooseMoveForPC(pointToInsertTo, m_playerTwo.getPlayerIdentifier());
            executePlayerMove(validDirectionsForPoint, m_playerTwo, pointToInsertTo);
        }

        private int[] getRandomPointForPC(List<int[]> i_validPoints)
        {
            int amountOfMoves = i_validPoints.Count;
            int[] moveToReturn = null;

            if (amountOfMoves >= 1)
            {
                Random rand = new Random();
                int randNumber = rand.Next(0, amountOfMoves);
                moveToReturn = i_validPoints[randNumber];

            }

            return moveToReturn;
        }

        //@TODO: An unhandled exception of type 'System.NullReferenceException' occurred in B15_Ex05.exe 
        //Additional information: Object reference not set to an instance of an object.
        // recieves a point to work with and returns list of directions
        private List<int[]> chooseMoveForPC(int[] i_point, int i_playerIdetifier)
        {
            List<int[]> validPathes = guessMoves(i_point[0], i_point[1], i_playerIdetifier);
            return validPathes;
        }

        // returns a list of possible moves points
        public List<int[]> isThereAnyMovesLeftList(int i_playerIdentifier)
        {
            List<int[]> tuplesList = new List<int[]>();
            List<int[]> pointsToReturn = new List<int[]>();

            // Iterating on every empty cell and checking if there exist some move
            foreach (int[] tuple in this.m_emptyCellsCollection)
            {
                tuplesList = guessMoves(tuple[0], tuple[1], i_playerIdentifier);
                if (tuplesList.Count >= 1)
                {
                    int[] tupleToVerify = tuplesList[0];

                    // init as -2
                    if (tupleToVerify[0] != -2 && tupleToVerify[1] != -2)
                    {
                        // moveExist = true;
                        //head->[EMPTY POINT (i,j)] [Directions (iDirection, jDirection)]->next
                        pointsToReturn.Add(tuple);
                    }
                }
            }

            return pointsToReturn;
        }

        // @TODO - support endgame event
        private void endGame()
        {
            int playerOnePoints = 0,
                playerTwoPoints = 0;
            bool winner = false; //true = player one

            for (int i = 0; i < m_gameMatrix.Length; i++)
            {
                for (int j = 0; j < m_gameMatrix.Length; j++)
                {
                    if (m_gameMatrix[i, j] == 1)
                    {
                        playerOnePoints++;
                    }
                    else if (m_gameMatrix[i, j] == -1)
                    {
                        playerTwoPoints++;
                    }
                }
            }
            winner = playerOnePoints >= playerTwoPoints ? true : false;


            // @TODO: cancel the matrix print
            if (winner)
            {
                GameIO.printCustomMessage("Player one Won!!");
            }
            else
            {
                GameIO.printCustomMessage("Player two Won!!");
            }

            Console.WriteLine("Exit");
        }

        // get the input form user to next move
        public int[] OnPulishCorrectUserInputFromGraphicBoard(object source){
            
            int[] buttonIndex = source as int[];

            return buttonIndex;
        }

        public bool playerMoveFlow(Player i_player, bool i_isThereAnyMovesToPlayer, int[] i_PlayerWantedMove)
        {
            bool validStepByPlayer = false;

            List<int[]> collectionOfDirections;

            if (i_isThereAnyMovesToPlayer)
            {
                //i_PlayerWantedMove = GameIO.getTargetFromPlayerInput(m_boardSize);

                collectionOfDirections = guessMoves(i_PlayerWantedMove[0], i_PlayerWantedMove[1], i_player.getPlayerIdentifier());
                validStepByPlayer = executePlayerMove(collectionOfDirections, i_player, i_PlayerWantedMove);
            }

            return validStepByPlayer;
        }
        
        private bool executePlayerMove(List<int[]> i_listOfDirections, Player i_player, int[] i_playerWantedMove)
        {
            bool canExecute = false;
            int iStart = i_playerWantedMove[0],
                jStart = i_playerWantedMove[1];

            if (i_listOfDirections.Count != 0)
            {
                canExecute = true;

                foreach (int[] tuple in i_listOfDirections)
                {
                    int iDirection = tuple[0],
                        jDirection = tuple[1];

                    flipChips(iStart, jStart, iDirection, jDirection, i_player.getPlayerIdentifier());
                    removeEmptyCellFromList(new int[2] { iStart, jStart });

                }

                OnModelBoardChanged();
            }

            return canExecute;
        }

        public bool isThereAnyMovesLeft(int i_playerIdentifier)
        {
            List<int[]> tupleToReturn = new List<int[]>();
            bool moveExist = false;

            // Iterating on every empty cell and checking if there exist some move
            foreach (int[] tuple in this.m_emptyCellsCollection)
            {
                tupleToReturn = guessMoves(tuple[0], tuple[1], i_playerIdentifier);
                if (tupleToReturn.Count >= 1)
                {
                    int[] tupleToVerify = tupleToReturn[0];

                    if (tupleToVerify[0] != -2 && tupleToVerify[1] != -2)
                    {
                        moveExist = true;
                        break;
                    }
                }
            }
            return moveExist;
        }

        public List<int[]> guessMoves(int i_iStart, int i_jStart, int i_playerIdentifier)
        {
            int[] tupleToReturn = new int[2] { -2, -2 };
            List<int[]> tuplesList = new List<int[]>();
            bool isStatringPointInsideBoard = tupleInsideBoard(i_iStart, i_jStart),
                isCellEmpty = false,
                isCurrentDirectionValid = false;

            if (isStatringPointInsideBoard)
            {
                isCellEmpty = m_gameMatrix[i_iStart, i_jStart] == 0;// must be zero as a stating point   
            }

            for (int iDirection = -1; iDirection < 2; iDirection++)
            {
                for (int jDirection = -1; jDirection < 2; jDirection++)
                {
                    bool isNextInside = tupleInsideBoard(i_iStart + iDirection, i_jStart + jDirection);
                    //skip the 0,0 permutation
                    if (iDirection == 0 && jDirection == 0)
                    {
                        continue;
                    }

                    else if (isCellEmpty && prepareForPathCheck(i_iStart, i_jStart, iDirection, jDirection, i_playerIdentifier))
                    {
                        isCurrentDirectionValid = IsValidMove(i_iStart, i_jStart, iDirection, jDirection, i_playerIdentifier);

                        if (isCurrentDirectionValid)
                        {
                            tupleToReturn = new int[2] { -2, -2 };
                            tupleToReturn[0] = iDirection;
                            tupleToReturn[1] = jDirection;
                            tuplesList.Add(tupleToReturn);
                            break;
                        }
                    }
                }
            }

            return tuplesList;
        }

        // need to add a check that the starting point is empty and inside the matrix
        public bool IsValidMove(int i_iStart, int i_jStart, int i_iDirection, int i_jDirection, int i_playerColor)
        {
            int iToCheck = i_iStart + i_iDirection,
                jToCheck = i_jStart + i_jDirection;

            bool returnValue = false,
                 isCellEmpty = false;

            if (tupleInsideBoard(iToCheck, jToCheck))
            {
                isCellEmpty = m_gameMatrix[iToCheck, jToCheck] == 0;

                if (isCellEmpty)
                {
                    returnValue = false;
                }

                else
                {
                    while (m_gameMatrix[iToCheck, jToCheck] == i_playerColor * (-1))
                    {
                        iToCheck += i_iDirection;
                        jToCheck += i_jDirection;

                        if (!tupleInsideBoard(iToCheck, jToCheck))
                        {
                            returnValue = false;
                            break;
                        }
                    }

                    if (tupleInsideBoard(iToCheck, jToCheck))
                    {
                        if (m_gameMatrix[iToCheck, jToCheck] == i_playerColor)
                        {
                            returnValue = true;
                        }
                    }
                }
            }

            //check if we are inside the matrix limits
            if (!tupleInsideBoard(iToCheck, jToCheck))
            {
                returnValue = false;
            }

            return returnValue;
        }

        private bool tupleInsideBoard(int i_row, int i_col)
        {
            bool isRowLegit = i_row < m_BoardSize && i_row > -1,
                 isColLegit = i_col < m_BoardSize && i_col > -1,
                 isLegit = false;

            if (isRowLegit && isColLegit)
            {
                isLegit = true;
            }

            return isLegit;
        }

        private bool prepareForPathCheck(int io_iStart, int io_jStart, int io_iDirection, int io_jDirection, int io_playerIdenitifier)
        {
            bool isStartingPointInside = tupleInsideBoard(io_iStart, io_jStart),
                isNextPointInside = tupleInsideBoard(io_iStart + io_iDirection, io_jStart + io_jDirection),
                isFirstCellEmpty = false,
                isSecondCellIsOtherPlayer = false,
                isValid = false;

            if (isStartingPointInside && isNextPointInside)
            {

                int firstCell = m_gameMatrix[io_iStart, io_jStart],
                    secondCell = m_gameMatrix[io_iStart + io_iDirection, io_jStart + io_jDirection];

                isFirstCellEmpty = firstCell == 0;
                isSecondCellIsOtherPlayer = secondCell == io_playerIdenitifier * (-1);

                isValid = isFirstCellEmpty && isSecondCellIsOtherPlayer;
            }

            return isValid;
        }

        private bool isLastCellIsSatisfysRule(int i_row, int i_col, int i_playerId)
        {
            int cellIdentifier = m_gameMatrix[i_row, i_col];
            bool cellSatify = false;

            if (cellIdentifier == 0)
            {
                cellSatify = true;
            }

            return cellSatify;
        }


        // @TODO: brings back event that we need to update the board
        public void flipChips(int i_iStart, int i_jStart, int i_iDirection, int i_jDirection, int i_playerIdentifier)
        {
            int currentRowIndex = i_iStart,
                currentColIndex = i_jStart;

            m_gameMatrix[currentRowIndex, currentColIndex] = i_playerIdentifier;
            currentRowIndex += i_iDirection;
            currentColIndex += i_jDirection;

            //@TODO: -1 exception
            while (m_gameMatrix[currentRowIndex, currentColIndex] != 0)
            {
                m_gameMatrix[currentRowIndex, currentColIndex] = i_playerIdentifier;
                currentRowIndex += i_iDirection;
                currentColIndex += i_jDirection;
            }

        }

        public bool removeEmptyCellFromList(int[] i_cellToRemove)
        {
            int[] toDelete = new int[2];
            bool isCellExists = false,
                operationSucceeded = false;
            foreach (int[] emptyTuple in this.m_emptyCellsCollection)
            {
                if (emptyTuple[0] == i_cellToRemove[0] && emptyTuple[1] == i_cellToRemove[1])
                {
                    toDelete = emptyTuple;
                    isCellExists = true;
                }
            }
            if (isCellExists)
            {
                operationSucceeded = this.m_emptyCellsCollection.Remove(toDelete);
            }

            return operationSucceeded;
        }
        
        internal List<int[]> getMovesByPlayer(Player player)
        {
            return isThereAnyMovesLeftList(player.getPlayerIdentifier());
        }
    }
}
