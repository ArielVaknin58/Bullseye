using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Bullseye
{
    internal class Game
    {
        private const int k_StringLength = 4;
        private char[] m_CorrectGuess { get; set; } = new char[k_StringLength];
        private int m_MaxGuesses { get; set; } = 10;
        private int m_GuessNumber { get; set; } = 1;
        private List<string> m_PreviousGuesses { get; set; }
        private List<string> m_PreviousFeedbacks { get; set; }
        private ConsoleHandler m_Handler { get; set; } = new ConsoleHandler();
        public void Run()
        {
            while (true)
            {
                m_Handler.PrintLine("~~Hello ! Welcome to Bullseye.~~");
                m_Handler.PrintLine("Please choose the number of max guesses (4-10):");

                string userInput = m_Handler.ReadLine();
                int parsedNumber;

                while (true)
                {
                    if (!int.TryParse(userInput, out parsedNumber))
                    {
                        m_Handler.PrintLine("Please enter a legal input - a number between 4 and 10 !");
                    }
                    else if (parsedNumber < 4 || parsedNumber > 10)
                    {
                        m_Handler.PrintLine("The given number is not in range, must be between 4-10 !");
                    }
                    else
                    {
                        break;
                    }

                    userInput = m_Handler.ReadLine();
                }

                m_MaxGuesses = parsedNumber;
                GenerateString();
                m_PreviousGuesses = new List<string>();
                m_PreviousFeedbacks = new List<string>();

                bool v_WasGuessedCorrectly = false;
                bool v_UserQuits = false;

                for (m_GuessNumber = 1; m_GuessNumber <= m_MaxGuesses; m_GuessNumber++)
                {
                    m_Handler.PrintLine($"Please enter a {getStringLength()}-character long string of letters between A-H :");
                    string guessInput = m_Handler.ReadLine();

                    if (toQuit(guessInput.ToUpper()))
                    {
                        v_UserQuits = true;
                        break;
                    }

                    while (!IsValidGuess(guessInput.ToCharArray()))
                    {
                        m_Handler.PrintLine($"Please enter a valid {getStringLength()}-character long string of letters between A-H :");
                        guessInput = m_Handler.ReadLine();
                        if (toQuit(guessInput))
                        {
                            v_UserQuits = true;
                            break;
                        }
                    }

                    if (v_UserQuits)//If wants to quit during the previous while loop and not the if before- another break is needed to exit the main loop
                    {
                        break;
                    }

                    StringBuilder guessBuilder = new StringBuilder();
                    foreach(char c in guessInput.ToCharArray())
                    {
                        guessBuilder.Append(c);
                        guessBuilder.Append(' ');
                    }

                    m_PreviousGuesses.Add(guessBuilder.ToString());

                    List<char> feedback = EvaluateGuess(guessInput.ToCharArray());
                    StringBuilder feedbackBuilder = new StringBuilder();
                    foreach (char c in feedback)
                    {
                        feedbackBuilder.Append(c);
                        feedbackBuilder.Append(' ');
                    }

                    m_PreviousFeedbacks.Add(feedbackBuilder.ToString());

                    foreach (char c in feedback)
                    {
                        m_Handler.Print(c.ToString());
                    }

                    m_Handler.PrintLine();

                    PrintBoard(CheckIfBullseye(feedback) || m_GuessNumber == m_MaxGuesses);

                    if (CheckIfBullseye(feedback))
                    {
                        m_Handler.PrintLine("\nCongratulations ! You correctly guessed the string !");
                        v_WasGuessedCorrectly = true;
                        break;
                    }

                    m_Handler.PrintLine();
                }

                if (v_UserQuits)
                {
                    m_Handler.PrintLine("~Goodbye !");
                    break;
                }
                else if (!v_WasGuessedCorrectly)
                {
                    m_Handler.PrintLine("No more guesses allowed. You lost.");
                }
                else
                {
                    m_Handler.PrintLine($"You guessed after {m_GuessNumber} steps!");
                }

                m_Handler.PrintLine("Would you like to start a new game ? <Y/N>");
                string retryInput = m_Handler.ReadLine();

                while (!retryInput.Equals("N") && !retryInput.Equals("Y"))
                {
                    m_Handler.PrintLine("Invalid input. Please enter Y/N");
                    retryInput = m_Handler.ReadLine();
                }

                if (retryInput.Equals("N"))
                {
                    m_Handler.PrintLine("~Goodbye !");
                    break;
                }
            }

        }
        public int getStringLength() 
        { 
            return k_StringLength; 
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

        public bool IsValidGuess(char[] i_StringInput)
        {
            List<int> selectedLettersPool = new List<int>();

            if (i_StringInput.Length != k_StringLength)
            {
                return false;
            }

            foreach (char c in i_StringInput)
            {
                if (c < 'A' || c > 'H')//If the letter is not in range A-H
                {
                    return false;
                }

                if (selectedLettersPool.Contains(c))//If a letter appears more than once
                {
                    return false;
                }

                selectedLettersPool.Add(c);
            }

            return true;
        }

        public bool toQuit(string i_input)
        {
            if (i_input.Equals("Q"))
            {
                return true;
            }

            return false;
        }

        public void PrintBoard(bool i_IsDone)
        {

            Ex02.ConsoleUtils.Screen.Clear();
            m_Handler.PrintLine("| Pins:    | Result:  |");
            m_Handler.PrintLine("|=====================|");

            if (i_IsDone) 
            {
                string correctGuess = new string(m_CorrectGuess);
                StringBuilder CorrectGuessBuilder = new StringBuilder();
                foreach (char c in m_CorrectGuess)
                {
                    CorrectGuessBuilder.Append(c);
                    CorrectGuessBuilder.Append(' ');
                }
                m_Handler.PrintLine($"| {CorrectGuessBuilder.ToString()} |          |");
            }
            else
            {
                m_Handler.PrintLine("| # # # #  |          |");
            }

            m_Handler.PrintLine("|=====================|");

            for(int j = 1; j <= m_GuessNumber; j++)
            {
                m_Handler.PrintLine($"| {m_PreviousGuesses.ElementAt(j-1)} | {m_PreviousFeedbacks.ElementAt(j-1)} |");
                m_Handler.PrintLine("|=====================|");
            }

            for (int i = m_GuessNumber; i < m_MaxGuesses; i++) 
            {
                m_Handler.PrintLine("|          |          |");
                m_Handler.PrintLine("|=====================|");
            }

        }

    }
}
