using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ex02
{
    public class Game
    {
        private const int k_StringLength = 4;
        private char[] m_CorrectGuess  = new char[k_StringLength];
        private int m_MaxGuesses  = 10;
        private int m_GuessNumber = 1;
        private List<string> m_PreviousGuesses = new List<string>();
        private List<string> m_PreviousFeedbacks = new List<string>();

        public char[] CorrectGuess
        { 
            get { return m_CorrectGuess; }
            private set { m_CorrectGuess = value; }
        }

        public int MaxGuesses
        {
            get { return m_MaxGuesses; }
            private set { m_MaxGuesses = value; }
        }

        public int GuessNumber
        {
            get { return m_GuessNumber; }
            set { m_GuessNumber = value; }
        }

        public List<string> PreviousGuesses
        {
            get { return m_PreviousGuesses; }
            private set { m_PreviousGuesses = value; }
        }

        public List<string> PreviousFeedbacks
        {
            get { return m_PreviousFeedbacks; }
            private set { m_PreviousGuesses = value; }
        }

        public bool ValidateNumOfGuesses(string i_userInput, out int io_parsedNumber)
        {
            bool isValid;
            if (!int.TryParse(i_userInput, out io_parsedNumber) || (io_parsedNumber < 4 || io_parsedNumber > 10))
            {
                isValid = !true;
            }
            else 
            {
                isValid = true;
            }

            if (isValid) 
            {
                this.MaxGuesses = io_parsedNumber;
            }
            return isValid;
        }

        public bool ValidateGuessInput(string io_guessInput)
        {
            bool isValid = true;
            if (toQuit(io_guessInput.ToUpper()))
            {
                io_guessInput = "Q";
                isValid = true;
            }

            if (!IsValidGuess(io_guessInput.ToCharArray()))
            {
                isValid = !true;
            }

            return isValid;
        }
        
        public int StringLength
        {
            get { return k_StringLength; }
        }

        public char[] GenerateString()
        {
            Random random = new Random();
            int[] selectedLettersPool = new int[k_StringLength];

            for (int i = 0; i < k_StringLength; i++)
            {
                int randomizedLetterAscii = random.Next(8);
                while (selectedLettersPool.Contains(randomizedLetterAscii))
                {
                    randomizedLetterAscii = random.Next(8);
                }

                selectedLettersPool[i] = randomizedLetterAscii;
                randomizedLetterAscii += 65; //To get the ASCII value of a letter between A-J
                m_CorrectGuess[i] = (char)randomizedLetterAscii;
                
            }

            return m_CorrectGuess;
        }

        public List<char> EvaluateGuess(char[] i_guess)
        {
            List<char> result = new List<char>();
            //Didn't use foreach loop because I compare elements in the same indices in the first condition
            for(int i = 0 ; i < i_guess.Length; i++)
            {   
                if (i_guess[i].Equals(m_CorrectGuess[i]))
                {
                    result.Add('V');
                }             
                else if(m_CorrectGuess.Contains(i_guess[i]))
                {
                    result.Add('X');
                }
                else
                {
                    result.Add('Z');// Placeholder for the ' '. space has an ascii of 32 so it will be sorted to the front, but I need it in the back
                }
            }

            result.Sort();// First V's, then X's and then 'Z's.

            for(int j = 0; j < i_guess.Length; j++)
            {
                if (result[j].Equals('Z'))
                {
                    result[j] = ' ';//Place ' ' instead of the Z's in the end of the array
                }
            }

            return result;
        }

        public bool CheckIfBullseye(List<char> i_list)
        {
            int Vcounter = 0;
            foreach (char c in i_list)
            {
                if (c.Equals('V'))
                {
                    Vcounter++;
                }
            }

            return Vcounter == k_StringLength;
        }

        public bool IsValidGuess(char[] io_StringInput)
        {
            bool isValid = true;
            List<int> selectedLettersPool = new List<int>();

            if (io_StringInput.Length != k_StringLength)
            {
                isValid = !true;
            }

            foreach (char c in io_StringInput)
            {
                if (c < 'A' || c > 'H')//If the letter is not in range A-H
                {
                    isValid = !true;
                }

                if (selectedLettersPool.Contains(c))//If a letter appears more than once
                {
                    isValid = !true;
                }

                selectedLettersPool.Add(c);
            }

            return isValid;
        }

        public bool toQuit(string i_input)
        {
            bool toQuit = !true;
            if (i_input.Equals("Q"))
            {
                toQuit = true;
            }

            return toQuit;
        }

        

    }
}
