using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ex05
{
    public class Game
    {
        public enum eFeedbackTypes
        {
            CorrectPosition,
            WrongPosition,
            NotPresent
        }

        private const int k_StringLength = 4;
        public const int k_NumberOfOptions = 8;
        public char[] m_CorrectGuess { get; set; } = new char[k_StringLength];
        public int m_MaxGuesses { get; set; } = 4;
        public int m_GuessNumber { get; set; } = 1;       
        private Random m_Random = new Random();
      
        public int StringLength
        {
            get { return k_StringLength; }
        }

        public int NumberOfOptions
        {
            get { return k_NumberOfOptions; }
        }


        public char[] GenerateString()
        {
            int[] selectedLettersPool = new int[k_StringLength];

            for (int i = 0; i < k_StringLength; i++)
            {
                int randomizedLetterAscii = m_Random.Next(k_NumberOfOptions);
                while (selectedLettersPool.Contains(randomizedLetterAscii))
                {
                    randomizedLetterAscii = m_Random.Next(k_NumberOfOptions);
                }

                selectedLettersPool[i] = randomizedLetterAscii;
                randomizedLetterAscii += 65; //To get the ASCII value of a letter between A-J
                m_CorrectGuess[i] = (char)randomizedLetterAscii;
                
            }

            return m_CorrectGuess;
        }    

        public List<eFeedbackTypes> EvaluateGuess(char[] i_guess)
        {
            List<eFeedbackTypes> result = new List<eFeedbackTypes>();

            for (int i = 0; i < i_guess.Length; i++)
            {
                if (i_guess[i].Equals(m_CorrectGuess[i]))
                {
                    result.Add(eFeedbackTypes.CorrectPosition);
                }
                else if (m_CorrectGuess.Contains(i_guess[i]))
                {
                    result.Add(eFeedbackTypes.WrongPosition);
                }
                else
                {
                    result.Add(eFeedbackTypes.NotPresent);
                }
            }

            result.Sort();

            return result;
        }

        public bool CheckIfBullseye(List<eFeedbackTypes> i_list)
        {
            int Vcounter = 0;

            foreach (eFeedbackTypes feedbackItem in i_list)
            {
                if (feedbackItem.Equals(eFeedbackTypes.CorrectPosition))
                {
                    Vcounter++;
                }
            }

            return Vcounter == k_StringLength;
        }

    }
}
