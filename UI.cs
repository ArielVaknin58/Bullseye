using Bullseye;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex02
{
    internal class UI
    {
        private Game m_game = new Game();
        private Form MainForm = new Form();
        private Form SecondForm = new Form();
        private const int k_FormWidth = 410;
        private const int k_MainFormHeight = 600;
        private const int k_ButtonSize = 50;
        private const int k_spacing = 10;
        private const int k_margin = 20;
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

        public void chanceButton_OnClick(object sender,EventArgs e)
        {
            m_game.MaxGuesses = m_game.MaxGuesses == 10 ? 4 : m_game.MaxGuesses + 1;
            (sender as Button).Text = $"Number of chances : {m_game.MaxGuesses}";

        }

        public void startButton_onClick(object sender,EventArgs e)
        {
            MainForm.Hide();
            SecondForm.Width = k_FormWidth;
            SecondForm.Height = k_MainFormHeight;
            SecondForm.Text = "Game window";
            m_game.GenerateString();

            for (int i = 0; i < m_game.StringLength; i++)
            {
                Button button = new Button();
                button.Height = button.Width = k_ButtonSize;
                button.BackColor = Color.Black;
                button.Top = k_margin;
                button.Left = k_margin + 10*i + i*button.Width;
                SecondForm.Controls.Add(button);
            }

            printGridOfButtons(m_game.MaxGuesses, m_game.StringLength, k_spacing);
            
            
            SecondForm.ShowDialog();
        }

        public void GuessButton_Onclick(object sender, EventArgs e)
        {
            Form colorSelectionForm = new Form();
            colorSelectionForm.Width = 320;
            colorSelectionForm.Height = 180;
            LetterToColorConverter converter = new LetterToColorConverter();
            char letter = 'A';
            for (int i = 0; i < m_game.NumberOfOptions / 4 ; i++)
            {
                for (int j = 0; j < m_game.NumberOfOptions / 2; j++ )
                {
                    ColorButton colorButton = new ColorButton((sender as Button), converter.ToColor(letter));
                    letter = (char)((int)letter + 1);
                    colorButton.Height = colorButton.Width = k_ButtonSize;
                    colorButton.Top = k_margin +i*(k_spacing + colorButton.Height);
                    colorButton.Left = k_margin + j * (k_ButtonSize + k_margin);
                    colorSelectionForm.Controls.Add(colorButton);

                }        
            }

            colorSelectionForm.ShowDialog();
        }

        private void arrowButton_OnClick(object sender, EventArgs e)
        {

        }
        private void printGridOfButtons(int i_rows = 4 , int i_columns = 4 , int i_spacing = 10)
        {
            for (int i = 0; i < i_rows; i++)
            {
                Button lastButtonInRow = new Button();
                for (int j = 0; j < i_columns; j++)
                {
                    GuessButton guessButton = new GuessButton();
                    guessButton.Left = k_margin + j * (k_ButtonSize + i_spacing);
                    guessButton.Top = i_spacing + 30 + (i + 1) * k_ButtonSize + i_spacing * i;// Difference between GuessButtons and the ones underneath - 30px
                    SecondForm.Controls.Add(guessButton);
                    guessButton.Click += GuessButton_Onclick;
                    lastButtonInRow = guessButton;
                }

                Button arrowButton = new Button();
                arrowButton.Width = k_ButtonSize;
                arrowButton.Height = 30;
                arrowButton.Text = "--->";
                arrowButton.Left = lastButtonInRow.Right + i_spacing;
                arrowButton.Top = lastButtonInRow.Top + 10;
                arrowButton.Enabled = !true;
                arrowButton.Click += arrowButton_OnClick;
                SecondForm.Controls.Add(arrowButton);

                for(int k = 0;  k < i_columns / 2; k++)
                {
                    for(int z = 0; z < i_columns - (i_columns/2); z++) // The rest of the buttons - works in the case stringLength is odd.
                    {
                        Button resultButton = new Button();
                        resultButton.Width = resultButton.Height = 20;
                        //button2.Left = 2 * i_spacing + i_columns * (k_ButtonSize + i_spacing) + k_ButtonSize + 30*k;
                        resultButton.Left = arrowButton.Right + 2*i_spacing + (resultButton.Width + 10) * k;                 
                        resultButton.Top = 40 + (i + 1) * k_ButtonSize + i_spacing * i + 30*z;
                        resultButton.Enabled = !true;
                        SecondForm.Controls.Add(resultButton);
                    }          
                }         
            }
        }

        private void GuessButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            MainForm.Width = k_FormWidth;
            MainForm.Height = 150;
            MainForm.Text = "Bool Pgia";

            Button chancesButton = new Button();
            //chancesButton.Left = 20;
            //chancesButton.Top = 20;
            chancesButton.Location = new Point(20, 20);
            chancesButton.Height = 30;
            chancesButton.Width = 240;
            chancesButton.Text = $"Number of chances : {m_game.MaxGuesses}";
            chancesButton.Click += chanceButton_OnClick;
            MainForm.Controls.Add(chancesButton);

            Button startButton = new Button();
            //startButton.Left = 180;
            //startButton.Top = 70;
            startButton.Location = new Point(180, 70);
            startButton.Width = 80;
            startButton.Height = 30;
            startButton.Text = "Start";
            startButton.Click += startButton_onClick;
            MainForm.Controls.Add(startButton);
            MainForm.ShowDialog();

            //while (true)
            //{
            //    form.Show();
            //    Game game = new Game();
            //    PrintLine("~~Hello ! Welcome to Ex02.~~");

            //    PrintLine("Please choose the number of max guesses (4-10):");
            //    string userInput = ReadLine();
            //    int parsedNumber;

            //    while(!game.ValidateNumOfGuesses(userInput, out parsedNumber))
            //    {
            //        PrintLine("Please enter a legal input - a number between 4 and 10 !");
            //        userInput = ReadLine();
            //    }
            //    game.GenerateString();
                

            //    bool v_WasGuessedCorrectly = !true;
            //    bool v_UserQuits = !true;

            //    for (game.GuessNumber = 1; game.GuessNumber <= game.MaxGuesses; game.GuessNumber++)
            //    {
            //        PrintLine($"Please enter a {game.StringLength}-character long string of letters between A-H :");
            //        string guessInput = ReadLine();

            //        while(!game.ValidateGuessInput(guessInput))
            //        {
            //            PrintLine($"Please enter a valid {game.StringLength}-character long string of letters between A-H :");
            //            guessInput = ReadLine();
            //        }


            //        if (game.toQuit(guessInput))
            //        {
            //            v_UserQuits = true;
            //            break;
            //        }

            //        //Insert ' ' in order to print according to format- not the Game class' responsibility
            //        StringBuilder guessBuilder = new StringBuilder();
            //        foreach (char c in guessInput.ToCharArray())
            //        {
            //            guessBuilder.Append(c);
            //            guessBuilder.Append(' ');
            //        }

            //        game.PreviousGuesses.Add(guessBuilder.ToString());

            //        List<char> feedback = game.EvaluateGuess(guessInput.ToCharArray());
            //        StringBuilder feedbackBuilder = new StringBuilder();
            //        foreach (char c in feedback)
            //        {
            //            feedbackBuilder.Append(c);
            //            feedbackBuilder.Append(' ');
            //        }

            //        game.PreviousFeedbacks.Add(feedbackBuilder.ToString());

            //        foreach (char c in feedback)
            //        {
            //            Print(c.ToString());
            //        }

            //        PrintLine();

            //        PrintBoard(game.CheckIfBullseye(feedback) || game.GuessNumber == game.MaxGuesses, game);

            //        if (game.CheckIfBullseye(feedback))
            //        {
            //            PrintLine("\nCongratulations ! You correctly guessed the string !");
            //            v_WasGuessedCorrectly = true;
            //            break;
            //        }

            //        PrintLine();
            //    }

            //    if (v_UserQuits)
            //    {
            //        PrintLine("~Goodbye !");
            //        break;
            //    }
            //    else if (!v_WasGuessedCorrectly)
            //    {
            //        PrintLine("No more guesses allowed. You lost.");
            //    }
            //    else
            //    {
            //        PrintLine($"You guessed after {game.GuessNumber} steps!");
            //    }

            //    PrintLine("Would you like to start a new game ? <Y/N>");
            //    string retryInput = ReadLine();

            //    while (!retryInput.Equals("N") && !retryInput.Equals("Y"))
            //    {
            //        PrintLine("Invalid input. Please enter Y/N");
            //        retryInput = ReadLine();
            //    }

            //    if (retryInput.Equals("N"))
            //    {
            //        PrintLine("~Goodbye !");
            //        break;
            //    }
            //}



        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
