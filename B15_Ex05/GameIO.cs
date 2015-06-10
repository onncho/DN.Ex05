using System;
using System.Collections.Generic;
using System.Text;

namespace B15_Ex05
{
    class GameIO
    {

        public static bool isSingleGame()
        {
            string errMsg = "Oops, seems like something happend please try again ",
                welcomeMsg = "Hello would you like to play with another player (Y/N)? ",
                inputString;
            bool returnVal = false;

            Console.WriteLine(welcomeMsg);
            inputString = Console.ReadLine();
            while (inputString.Length > 1 || (inputString != "Y" && inputString != "N"))
            {
                Console.WriteLine(errMsg);
                inputString = Console.ReadLine();
            }

            returnVal = inputString == "N" ? true : false;

            return returnVal;

        }

        public static string getNameInput()
        {
            string toPrint, userInput, inputParsed, errorString;

            errorString = "Seems like you entered invalid input , please try again";
            toPrint = "Please enter your name : ";

            Console.WriteLine(toPrint);

            userInput = Console.ReadLine();
            inputParsed = parseNameInput(userInput);

            if (inputParsed == null)
            {
                // Recursivly asks for another input if null is given as a result of an error
                Console.WriteLine(errorString);
                inputParsed = getNameInput();
            }

            return inputParsed;
        }

        public static string parseNameInput(string i_userInput)
        {
            string returnString;
            returnString = i_userInput;

            foreach (char charToCheck in i_userInput)
            {
                if (!Char.IsLetter(charToCheck))
                {
                    returnString = null;
                }
            }

            return returnString;
        }

        public static int[] getTargetFromPlayerInput(int i_boardSize)
        {
            string errorString, printString, targetInput;
            int[] parsedInput;

            errorString = "Seems like you choosed illegal target to place your disc please try again";
            printString = "Please choose the desired cell : ";

            Console.WriteLine(printString);

            targetInput = Console.ReadLine();
            quitFromGame(targetInput);

            while (targetInput.Length != 2)
            {
                Console.WriteLine(errorString);
                targetInput = Console.ReadLine();

                quitFromGame(targetInput);
            }

            parsedInput = parseTargetFromUser(targetInput, i_boardSize);

            if (parsedInput == null)
            {
                // Verify recursivly
                Console.WriteLine(errorString);

                parsedInput = getTargetFromPlayerInput(i_boardSize);
            }

            return parsedInput;

        }

        public static void printErrorForInvalidMove()
        {
            string errorString = "Seems like you choosed illegal target to place your disc please try again";
            Console.WriteLine(errorString);
        }

        private static int[] parseTargetFromUser(string i_move, int i_boardSize)
        {
            int charToRowIndexAsInt, charToColIndexAsInt;
            int[] indexesToReturn = null;
            char[] stringAsArray = i_move.ToCharArray();

            bool isLengthValid = i_move.Length == 2 ? true : false;
            bool isFirstCharIsLetter = Char.IsLetter(stringAsArray[0]);
            bool isLastCharIsNumber = Char.IsNumber(stringAsArray[1]);

            if (isLengthValid && isFirstCharIsLetter && isLastCharIsNumber)
            {
                charToRowIndexAsInt = Char.ToUpper(stringAsArray[0]) - 'A'; // mapping letters to numbers
                charToColIndexAsInt = stringAsArray[1] - 48; // substract 48(zero of ascii) but matrix starts from 1

                indexesToReturn = new int[2] { charToColIndexAsInt, charToRowIndexAsInt };
                Console.WriteLine("(" + charToColIndexAsInt + ", " + charToRowIndexAsInt + ")");
                if (charToRowIndexAsInt < 0 || charToRowIndexAsInt > i_boardSize ||
                    charToColIndexAsInt < 0 || charToColIndexAsInt > i_boardSize)
                {
                    indexesToReturn = null;
                }
            }

            return indexesToReturn;
        }

        public static void printCustomMessage(string i_msg)
        {
            Console.WriteLine(i_msg);
        }

        public static void printNextPlayerTurn(int i_playerIdentifier)
        {
            string playerName = i_playerIdentifier == 1 ? "Player One " : "Player Two ",
                printMessage = "its " + playerName + "turn! ";

            printCustomMessage(printMessage);

        }

        public static bool getBoradSizeFromPlayerInput()
        {
            string errorString, printString, targetInput;
            bool returnValue = false;

            errorString = "Seems like you choosed illegal board size, please choose one of the above : 6 / 8";
            printString = "Please choose your desire board size (options: 6 or 8) : ";

            Console.WriteLine(printString);

            targetInput = Console.ReadLine();

            if (targetInput == null)
            {
                // Verify recursivly
                Console.WriteLine(errorString);
                targetInput = Console.ReadLine();
            }

            returnValue = Convert.ToInt32(targetInput) == 8 ? true : false;

            return returnValue;
        }

        private static void quitFromGame(string i_input)
        {
            if (i_input == "q" || i_input == "Q")
            {
                System.Environment.Exit(0);
            }
        }
    }
}
