using System;
using GameLogic;

namespace GameConsoleUI
{

    public static class ConsoleInputValidation
    {
        public static bool IsNumberOfTriesValid(int i_NumberOfTries, int i_MinNumberOfTries, int i_MaxNumberOfTries)
        {
            bool isNumberOfTriesValid = i_NumberOfTries >= i_MinNumberOfTries && i_NumberOfTries <= i_MaxNumberOfTries;
            string stringFormat = string.Format("The number of tries has to be between {0} - {1}", i_MinNumberOfTries, i_MaxNumberOfTries);

            if (!isNumberOfTriesValid)
            {
                Console.WriteLine(stringFormat);
            }

            return isNumberOfTriesValid;
        }

        public static bool IsPlayerChoiceWhenGameEndsValid(char i_PlayerChoice, char i_NewGame, char i_EndGame)
        {
            bool isPlayerChoiceValid = false;

            if (i_PlayerChoice == i_EndGame || i_PlayerChoice == i_NewGame)
            {
                isPlayerChoiceValid = true;
            }
            else
            {
                Console.WriteLine("Invalid input , you have to enter Y/N");
            }

            return isPlayerChoiceValid;
        }

        internal static bool IsPlayerGuessValid(char[] i_CurrentGuessToValidate)
        {
            bool isGuessValid = false;

            switch (GuessValidation.InputValidation(i_CurrentGuessToValidate))
            {
                case eValidation.WrongLength:
                    string s = string.Format(
                        "The input is not valid , you have to enter {0} letters", 4);
                    Console.WriteLine(s);
                    break;
                case eValidation.NotInRange:
                    Console.WriteLine("The input is not valid , you need to enter letters between the range A-H");
                    break;
                case eValidation.NotLetter:
                    Console.WriteLine("The input is not valid , you have to enter letters");
                    break;
                case eValidation.NotUpperLetter:
                    Console.WriteLine("The input is not valid , you need to enter upper case letters");
                    break;
                case eValidation.LetterRepetition:
                    Console.WriteLine("The input is not valid , there has to be no letter repetition");
                    break;
                case eValidation.ValidInput:
                    isGuessValid = true;
                    break;
            }

            return isGuessValid;
        }
    }
}
