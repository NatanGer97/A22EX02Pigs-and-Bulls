using System;
using System.Text;

namespace GameLogic
{
    // $G$ DSN-002 (-3) This class shoukd be in the UI. Input validation is not part of the game logic
    public static class GuessValidation
    {
        // $G$ DSN-999 (-3) The const variables should be const members of the class
        // $G$ CSS-027 (-2) Unnecessary blank line. (line 15)
        public static eValidation InputValidation(char[] i_InputToValidate)
        {
            int i;
            const char leftBoundry = 'A';
            const char rightBoundry = 'H';

            StringBuilder stringBuilder = new StringBuilder();
            eValidation validationResult = eValidation.WrongLength;

            if (i_InputToValidate.Length == 0)
            {
                validationResult = eValidation.WrongLength;
            }

            for (i = 0; i < i_InputToValidate.Length; i++)
            {
                if (i_InputToValidate.Length != Game.k_NumberOfLettersToGuess)
                {
                    validationResult = eValidation.WrongLength;
                    break;
                }

                if (char.IsDigit(i_InputToValidate[i]))
                {
                    validationResult = eValidation.NotLetter;
                    break;
                }

                if (char.IsLower(i_InputToValidate[i]))
                {
                    validationResult = eValidation.NotUpperLetter;
                    break;
                }

                if (i_InputToValidate[i] < leftBoundry || i_InputToValidate[i] > rightBoundry)
                {
                    validationResult = eValidation.NotInRange;
                    break;
                }

                if (stringBuilder.ToString().Contains(i_InputToValidate[i].ToString()))
                {
                    validationResult = eValidation.LetterRepetition;
                    break;
                }

                stringBuilder.Append(i_InputToValidate[i]);
            }

            if (i == Game.k_NumberOfLettersToGuess)
            {
                validationResult = eValidation.ValidInput;
            }

            return validationResult;
        }
    }
}
