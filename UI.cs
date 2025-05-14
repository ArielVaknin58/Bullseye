using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class UI
    {
        public void PrintLine(string i_Message = "\n")
        {
            Console.WriteLine(i_Message);
        }

        public void Print(string i_Message = "\n")
        { 
            Console.Write(i_Message); 
        }
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Run()
        {
            while (true)
            {
                Game game = new Game();
                PrintLine("~~Hello ! Welcome to Ex02.~~");

                PrintLine("Please choose the number of max guesses (4-10):");
                string userInput = ReadLine();
                int parsedNumber;

                while(!game.ValidateNumOfGuesses(userInput, out parsedNumber))
                {
                    PrintLine("Please enter a legal input - a number between 4 and 10 !");
                    userInput = ReadLine();
                }
                game.GenerateString();
                

                bool v_WasGuessedCorrectly = !true;
                bool v_UserQuits = !true;

                for (game.GuessNumber = 1; game.GuessNumber <= game.MaxGuesses; game.GuessNumber++)
                {
                    PrintLine($"Please enter a {game.StringLength}-character long string of letters between A-H :");
                    string guessInput = ReadLine();

                    while(!game.ValidateGuessInput(guessInput))
                    {
                        PrintLine($"Please enter a valid {game.StringLength}-character long string of letters between A-H :");
                        guessInput = ReadLine();
                    }


                    if (game.toQuit(guessInput))
                    {
                        v_UserQuits = true;
                        break;
                    }

                    //Insert ' ' in order to print according to format- not the Game class' responsibility
                    StringBuilder guessBuilder = new StringBuilder();
                    foreach (char c in guessInput.ToCharArray())
                    {
                        guessBuilder.Append(c);
                        guessBuilder.Append(' ');
                    }

                    game.PreviousGuesses.Add(guessBuilder.ToString());

                    List<char> feedback = game.EvaluateGuess(guessInput.ToCharArray());
                    StringBuilder feedbackBuilder = new StringBuilder();
                    foreach (char c in feedback)
                    {
                        feedbackBuilder.Append(c);
                        feedbackBuilder.Append(' ');
                    }

                    game.PreviousFeedbacks.Add(feedbackBuilder.ToString());

                    foreach (char c in feedback)
                    {
                        Print(c.ToString());
                    }

                    PrintLine();

                    PrintBoard(game.CheckIfBullseye(feedback) || game.GuessNumber == game.MaxGuesses, game);

                    if (game.CheckIfBullseye(feedback))
                    {
                        PrintLine("\nCongratulations ! You correctly guessed the string !");
                        v_WasGuessedCorrectly = true;
                        break;
                    }

                    PrintLine();
                }

                if (v_UserQuits)
                {
                    PrintLine("~Goodbye !");
                    break;
                }
                else if (!v_WasGuessedCorrectly)
                {
                    PrintLine("No more guesses allowed. You lost.");
                }
                else
                {
                    PrintLine($"You guessed after {game.GuessNumber} steps!");
                }

                PrintLine("Would you like to start a new game ? <Y/N>");
                string retryInput = ReadLine();

                while (!retryInput.Equals("N") && !retryInput.Equals("Y"))
                {
                    PrintLine("Invalid input. Please enter Y/N");
                    retryInput = ReadLine();
                }

                if (retryInput.Equals("N"))
                {
                    PrintLine("~Goodbye !");
                    break;
                }
            }



        }
        public void PrintBoard(bool i_IsDone,Game i_CurrentGame)
        {

            Ex02.ConsoleUtils.Screen.Clear();
            PrintLine("| Pins:    | Result:  |");
            PrintLine("|=====================|");

            if (i_IsDone)
            {
                string correctGuess = new string(i_CurrentGame.CorrectGuess);
                StringBuilder CorrectGuessBuilder = new StringBuilder();
                foreach (char c in i_CurrentGame.CorrectGuess)
                {
                    CorrectGuessBuilder.Append(c);
                    CorrectGuessBuilder.Append(' ');
                }
                PrintLine($"| {CorrectGuessBuilder.ToString()} |          |");
            }
            else
            {
                PrintLine("| # # # #  |          |");
            }

            PrintLine("|=====================|");

            for (int j = 1; j <= i_CurrentGame.GuessNumber; j++)
            {
                PrintLine($"| {i_CurrentGame.PreviousGuesses.ElementAt(j - 1)} | {i_CurrentGame.PreviousFeedbacks.ElementAt(j - 1)} |");
                PrintLine("|=====================|");
            }

            for (int i = i_CurrentGame.GuessNumber; i < i_CurrentGame.MaxGuesses; i++)
            {
                PrintLine("|          |          |");
                PrintLine("|=====================|");
            }

        }
    }
}
