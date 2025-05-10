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

        private int m_GuessNumber { get; set; } = 1;
        private List<string> m_PreviousGuesses { get; } = new List<string>();
        private List<string> m_PreviousFeedbacks { get; } = new List<string>();

        private ConsoleHandler m_Handler { get; set; } = new ConsoleHandler();
        public void Run()
        {
            while (true)
            {
                m_Handler.PrintLine("~~Hello ! Welcome to Bullseye.~~");
                m_Handler.PrintLine("Please choose the number of max guesses (4-10):");
                string GuessNumInput = m_Handler.ReadLine();
                int numericGuess;
                while (true)
                {
                    if (!int.TryParse(GuessNumInput, out numericGuess))
                    {
                        m_Handler.PrintLine("Please enter a legal input - a number between 4 and 10 !");

                    }
                    else if (numericGuess > 10 || numericGuess < 4)
                    {
                        m_Handler.PrintLine("The given number is not in range, must be between 4-10 !");
                    }
                    else
                    {
                        break;
                    }
                    GuessNumInput = m_Handler.ReadLine();
                }
                this.m_MaxGuesses = numericGuess;
                string TrueString = new string(this.GenerateString());
                m_Handler.PrintLine(TrueString);

                bool IfGuessedCorrect = false;
                bool Quits = false;
                for (m_GuessNumber = 1 ; m_GuessNumber <= this.m_MaxGuesses ; m_GuessNumber++)
                {
                    m_Handler.PrintLine($"Please enter a {this.getStringLength()}-character long string of letters between A-H :");
                    string myGuess = m_Handler.ReadLine();
                    if (this.toQuit(myGuess))
                    {
                        Quits = true;
                        break;
                    }
                    while (!this.CheckIfValidString(myGuess.ToCharArray()))
                    {
                        m_Handler.PrintLine($"Please enter a valid {this.getStringLength()}-character long string of letters between A-H :");
                        myGuess = m_Handler.ReadLine();
                        if (this.toQuit(myGuess))
                        {
                            Quits = true;
                            break;
                        }
                    }
                    if (Quits)
                    {
                        break;
                    }
                    m_Handler.PrintLine($"Your guess is : {myGuess}");
                    this.m_PreviousGuesses.Add(myGuess);
                    List<char> list = this.GuessCurrentString(myGuess.ToCharArray());
                    this.m_PreviousFeedbacks.Add(new string(list.ToArray()));
                    m_Handler.Print($"Feedback for the guess {myGuess} // ");
                    foreach (char c in list)
                    {
                        m_Handler.Print(c.ToString());
                    }
                    m_Handler.PrintLine();
                    PrintBoard();
                    if (this.CheckIfBullseye(list))
                    {
                        m_Handler.PrintLine("##Congratulations ! You correctly guessed the string !##");
                        IfGuessedCorrect = true;
                        break;
                    }
                    m_Handler.PrintLine();
                }
                if (Quits)
                {
                    m_Handler.PrintLine("~Goodbye !");
                }
                else if (!IfGuessedCorrect)
                {
                    m_Handler.PrintLine("Looks like you ran out of guesses, want to go again ? Y for yes or any other key for no :");
                    string RetryInput = m_Handler.ReadLine();
                    if (!RetryInput.Equals("Y"))
                    {
                        m_Handler.PrintLine("~Goodbye !");
                        break;
                    }

                }
            }

        }
        public int getStringLength() { return m_StringLength; }
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
            //Didn't use foreach loop because I compare elements in the same indices in the first condition
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
            return Vcounter == m_StringLength;
        }

        public bool CheckIfValidString(char[] i_StringInput)
        {
            List<int> PreviousGuesses = new List<int>();
            if (i_StringInput.Length != m_StringLength)
            {
                return false;
            }

            foreach (char c in i_StringInput)
            {
                if (c < 'A' || c > 'H')
                {
                    return false;
                }
                if (PreviousGuesses.Contains(c))
                {
                    return false;
                }
                PreviousGuesses.Add(c);
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

        public void PrintBoard()
        {
            m_Handler.PrintLine("| Pins:    | Result:  |");
            m_Handler.PrintLine("|=====================|");
            m_Handler.PrintLine("| # # # #  |          |");
            m_Handler.PrintLine("|=====================|");
            for(int j = 1 ; j <= m_GuessNumber ; j++)
            {
                m_Handler.PrintLine($"| {m_PreviousGuesses.ElementAt(j-1)}     | {m_PreviousFeedbacks.ElementAt(j-1)}     |");
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
