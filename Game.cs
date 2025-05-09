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

       
        public char[] GenerateString()
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

        public List<char> GuessCurrentString(char[] i_guess)
        {
            List<char> result = new List<char>();
            for(int i = 0 ;i < i_guess.Length; i++)
            {
                if (i_guess[i].Equals(m_CorrectGuess[i]))
                {
                    result.Add('V');
                }             
                else if(m_CorrectGuess.Contains(i_guess[i]))
                {
                    result.Add('X');
                }
            }

            result.Sort();
            return result;
        }



    }
}
