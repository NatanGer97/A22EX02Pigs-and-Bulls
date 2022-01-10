namespace GameLogic
{
    public class Game
    {
        // $G$ NTT-005 (-5) You should use properties. Fields should be encapsulated.
        public const int k_NumberOfLettersToGuess = 4;
        private const char k_CorrectPositionAndLetter = 'V';
        private const char k_CorrectLetter = 'X';
        private const char k_EmptySymbol = ' ';
        private readonly int r_NumberOfTries;
        private RandomQuartet m_GeneratedRandomQuartet;
        private int m_NumberOfCorrectLetterAndPos;
        private int m_NumberOfCorrectLetter;
        private char[,] m_GuessResultsForWholeGame;
        private char[,] m_PlayerGuessesForWholeGame;
        private bool m_IsGameFinished;
        private eGameResult m_GameResult;

        public char[,] PlayerGuessesForWholeGame
        {
            get
            {
                return m_PlayerGuessesForWholeGame;
            }
        }

        public char[,] GuessResultsForWholeGame
        {
            get
            {
                return m_GuessResultsForWholeGame;
            }
        }

        public bool IsGameFinished
        {
            get
            {
                return m_IsGameFinished;
            }

            set
            {
                m_IsGameFinished = value;
            }
        }

        public int NumberOfTries
        {
            get
            {
                return r_NumberOfTries;
            }
        }

        public RandomQuartet GeneratedRandomQuartet
        {
            get
            {
                return m_GeneratedRandomQuartet;
            }
        }

        public eGameResult GameResult
        {
            get
            {
                return m_GameResult;
            }
        }

        public Game(int i_NumberOfTries)
        {
            r_NumberOfTries = i_NumberOfTries;
            m_GeneratedRandomQuartet = new RandomQuartet(k_NumberOfLettersToGuess);
            m_GuessResultsForWholeGame = new char[r_NumberOfTries, k_NumberOfLettersToGuess];
            m_PlayerGuessesForWholeGame = new char[r_NumberOfTries, k_NumberOfLettersToGuess];
            m_IsGameFinished = false;
        }

        private void analyseCurrentGuess(int i_Index)
        {
            for (int i = 0; i < k_NumberOfLettersToGuess; i++)
            {
                if (m_PlayerGuessesForWholeGame[i_Index, i] == m_GeneratedRandomQuartet.RandomQuartetLetters[i])
                {
                    m_NumberOfCorrectLetterAndPos++;
                }
                else
                {
                    if (m_GeneratedRandomQuartet.IsCharContainedInRandomArray(m_PlayerGuessesForWholeGame[i_Index, i]))
                    {
                        m_NumberOfCorrectLetter++;
                    }
                }
            }
        }

        public void SetSortedGuessResults(int i_Index)
        {
            analyseCurrentGuess(i_Index);
            if (m_NumberOfCorrectLetterAndPos == k_NumberOfLettersToGuess)
            {
                m_GameResult = eGameResult.PlayerWon;
                m_IsGameFinished = true;
            }
            else
            {
                if (i_Index + 1 == r_NumberOfTries)
                {
                    m_GameResult = eGameResult.PlayerLost;
                    m_IsGameFinished = true;
                }
            }

            for (int i = 0; i < k_NumberOfLettersToGuess; i++)
            {
                if (m_NumberOfCorrectLetterAndPos > 0)
                {
                    m_GuessResultsForWholeGame[i_Index, i] = k_CorrectPositionAndLetter;
                    m_NumberOfCorrectLetterAndPos--;
                }
                else
                {
                    if (m_NumberOfCorrectLetter > 0)
                    {
                        m_GuessResultsForWholeGame[i_Index, i] = k_CorrectLetter;
                        m_NumberOfCorrectLetter--;
                    }
                    else
                    {
                        m_GuessResultsForWholeGame[i_Index, i] = k_EmptySymbol;
                    }
                }
            }
        }

        public void SetPlayerCurrentGuess(int i_Index, char[] i_ValidGuess)
        {
            for (int i = 0; i < k_NumberOfLettersToGuess; i++)
            {
                m_PlayerGuessesForWholeGame[i_Index, i] = i_ValidGuess[i];
            }
        }
    }
}
