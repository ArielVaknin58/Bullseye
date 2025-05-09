using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bullseye
{
    internal class Game
    {
        private const int m_StringLength = 4;
        private char[] m_CorrectGuess { get; set; } = new char[m_StringLength];
        private int m_MaxGuesses { get; set; } = 10;
        private int m_GuessCounter { get; set; } = 0;


        public char[] GuessString()
        {
            Random random = new Random();
            int[] PreviousGuesses = new int[m_StringLength];
            for (int i = 0; i < m_StringLength; i++)
            {
                int Letter = random.Next(8);
                while (PreviousGuesses.Contains(Letter))
                {
                    Letter = random.Next(8);
                }
                PreviousGuesses[i] = Letter;
                Letter += 65; //To get the ASCII value of a letter between A-J
                m_CorrectGuess[i] = (char)Letter;
                
            }

            return m_CorrectGuess;
        }

    }
}
