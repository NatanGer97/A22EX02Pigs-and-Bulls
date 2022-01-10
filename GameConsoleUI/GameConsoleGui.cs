using System;
using GameLogic;

namespace GameConsoleUI
{
    // $G$ CSS-027 (-3) Spaces are not kept as required after defying variables and before return statement in the class
    // $G$ DSN-002 (-20) No separation between the logical part of the game and the UI.

    internal class GameConsoleGui
    {
        private const char k_PlayerChoseToQuitTheGame = 'Q';
        private const char k_PlayerChoseNewGame = 'Y';
        private const char k_PlayerChoseToEndGame = 'N';
        private const int k_MinNumberOfTries = 4;
        private const int k_MaxNumberOfTries = 10;
        private Game m_Game;
        private int m_NumberOfTries;

        private void drawGameBoard()
        {
            Console.WriteLine("Current board status:");
            Console.WriteLine("|Pins:    |Result:|");
            Console.WriteLine("|=========|=======|");
            if (m_Game.IsGameFinished && m_Game.GameResult == eGameResult.PlayerLost)
            {
                Console.WriteLine(getFormatedRandomQuartet());
            }
            else
            {
                Console.WriteLine("| # # # # |       |");
            }

            for (int i = 0; i < m_NumberOfTries; i++)
            {
                Console.WriteLine("|=========|=======|");
                Console.WriteLine(getGuessRow(i));
            }

            Console.WriteLine("|=========|=======|");
            Console.WriteLine("Please type your next guess <ABCD> or 'Q' to quit");
        }

        private string getFormatedRandomQuartet()
        {
            string stringFormat = string.Format(
                "| {0} {1} {2} {3} |       |",
                m_Game.GeneratedRandomQuartet.RandomQuartetLetters[0],
                m_Game.GeneratedRandomQuartet.RandomQuartetLetters[1],
                m_Game.GeneratedRandomQuartet.RandomQuartetLetters[2],
                m_Game.GeneratedRandomQuartet.RandomQuartetLetters[3]);
            return stringFormat;
        }

        private string getGuessRow(int i_RowIndex)
        {
            string stringFormat = string.Format(
                "| {0} {1} {2} {3} |{4} {5} {6} {7}|",
                m_Game.PlayerGuessesForWholeGame[i_RowIndex, 0],
                m_Game.PlayerGuessesForWholeGame[i_RowIndex, 1],
                m_Game.PlayerGuessesForWholeGame[i_RowIndex, 2],
                m_Game.PlayerGuessesForWholeGame[i_RowIndex, 3],
                m_Game.GuessResultsForWholeGame[i_RowIndex, 0],
                m_Game.GuessResultsForWholeGame[i_RowIndex, 1],
                m_Game.GuessResultsForWholeGame[i_RowIndex, 2],
                m_Game.GuessResultsForWholeGame[i_RowIndex, 3]);
            return stringFormat;
        }

        public void GameStart()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            setNumberOfTries();
            Ex02.ConsoleUtils.Screen.Clear();
            m_Game = new Game(m_NumberOfTries);
            gameFlow();
        }

        private void gameFlow()
        {
            for (int i = 0; i < m_NumberOfTries; i++)
            {
                drawGameBoard();
                m_Game.SetPlayerCurrentGuess(i, getPlayerCurrentGuess());
                m_Game.SetSortedGuessResults(i);
                if (m_Game.IsGameFinished)
                {
                    gameEnd(i + 1);
                }

                Ex02.ConsoleUtils.Screen.Clear();
            }
        }

        private void setNumberOfTries()
        {
            Console.WriteLine(string.Format("Please enter the desired number of tries please {0} - {1}", k_MinNumberOfTries, k_MaxNumberOfTries));
            int.TryParse(Console.ReadLine(), out m_NumberOfTries);
            while (!ConsoleInputValidation.IsNumberOfTriesValid(m_NumberOfTries, k_MinNumberOfTries, k_MaxNumberOfTries))
            {
                int.TryParse(Console.ReadLine(), out m_NumberOfTries);
            }
        }

        private void gameEnd(int i_IndexOfTryWhenGameEnded)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            drawGameBoard();
            switch (m_Game.GameResult)
            {
                case eGameResult.PlayerWon:
                    string playerWonString = string.Format("You guessed after {0} steps!", i_IndexOfTryWhenGameEnded);
                    Console.WriteLine(playerWonString);
                    break;
                case eGameResult.PlayerLost:
                    Console.WriteLine("No more guesses allowed. You Lost.");
                    break;
            }

            Console.WriteLine("Would you like to start a new game? <Y/N>");
            char playerAnswer = playerChoiceWhenGameEnds();
            if (playerAnswer == k_PlayerChoseNewGame)
            {
                GameStart();
            }
            else
            {
                exitGameScreen();
            }
        }

        private char playerChoiceWhenGameEnds()
        {
            char playerAnswer;

            char.TryParse(Console.ReadLine(), out playerAnswer);
            while (!ConsoleInputValidation.IsPlayerChoiceWhenGameEndsValid(playerAnswer, k_PlayerChoseNewGame, k_PlayerChoseToEndGame))
            {
                char.TryParse(Console.ReadLine(), out playerAnswer);
            }

            return playerAnswer;
        }

        private char[] getPlayerCurrentGuess()
        {
            char[] currentGuess = Console.ReadLine().ToCharArray();

            if (currentGuess.Length != 0 && currentGuess[0] == k_PlayerChoseToQuitTheGame)
            {
                exitGameScreen();
            }

            while (!ConsoleInputValidation.IsPlayerGuessValid(currentGuess))
            {
                currentGuess = Console.ReadLine().ToCharArray();
                if (currentGuess.Length != 0 && currentGuess[0] == k_PlayerChoseToQuitTheGame)
                {
                    exitGameScreen();
                }
            }

            return currentGuess;
        }

        private void exitGameScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("GoodBye");
            System.Threading.Thread.Sleep(500);
            System.Environment.Exit(1);
        }
    }
}
