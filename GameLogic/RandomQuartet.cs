using System;
using System.Linq;

namespace GameLogic
{
    public class RandomQuartet
    {
        private readonly char[] r_RandomQuartetLetters;

        public RandomQuartet(int i_NumberOfLetters)
        {
            r_RandomQuartetLetters = buildRandomQuartetArray(i_NumberOfLetters);
        }

        public char[] RandomQuartetLetters
        {
            get
            {
                return r_RandomQuartetLetters;
            }
        }

        public bool IsCharContainedInRandomArray(char i_LetterToCheck)
        {
            bool isContained = false;

            for (int i = 0; i < r_RandomQuartetLetters.Length; i++)
            {
                if (r_RandomQuartetLetters[i] == i_LetterToCheck)
                {
                    isContained = true;
                    break;
                }
            }

            return isContained;
        }

        // $G$ NTT-007 (-10) There's no need to re-instantiate the Random instance each time it is used.
        // $G$ CSS-027 (-3) Spaces are not kept as required after defying variables.
        private char[] buildRandomQuartetArray(int i_NumberOfLettersToGenerate)
        {
            Random rnd = new Random();
            char[] randomCharsArray = new char[i_NumberOfLettersToGenerate];

            for (int i = 0; i < i_NumberOfLettersToGenerate; i++)
            {
                char randomChar = (char)rnd.Next('A', 'I');
                if (randomCharsArray.Contains(randomChar))
                {
                    i--;
                    continue;
                }

                randomCharsArray[i] = randomChar;
            }

            return randomCharsArray;
        }
    }
}
