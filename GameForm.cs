using Ex02;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex02
{
    public partial class GameForm : Form
    {
        public Game m_game { get; private set; } = new Game();
        private List<ArrowButton> m_ArrowButtonsList = new List<ArrowButton>();
        public List<Button> m_FinalResultsButtons { get; private set; } = new List<Button>();
        private const int k_ButtonSize = 50;
        private const int k_spacing = 10;
        private const int k_margin = 20;
        

        public GameForm()
        {     
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            this.Height += (this.m_game.MaxGuesses - 3) * (k_spacing + k_ButtonSize);
            System.Console.WriteLine(m_game.GenerateString());
            for (int i = 0; i < m_game.StringLength; i++)
            {
                Button button = new Button();
                button.Height = button.Width = k_ButtonSize;
                button.BackColor = Color.Black;
                button.Top = k_margin;
                button.Left = k_margin + 10 * i + i * button.Width;
                button.Enabled = !true;
                this.Controls.Add(button);
                this.m_FinalResultsButtons.Add(button);
            }

            printGridOfButtons(m_game.MaxGuesses, m_game.StringLength, k_spacing);

        }

        private void ArrowButton_BullseyeAchieved(object sender, EventArgs e)
        {
            LettersAndColorsConverter converter = new LettersAndColorsConverter();
            for (int k = 0; k < m_game.StringLength; k++)
            {
                this.m_FinalResultsButtons[k].BackColor = converter.CharToColor(m_game.CorrectGuess[k]);
            }
            MessageBox.Show("You won the game!", "Victory");
        }


        private void printGridOfButtons(int i_rows = 4, int i_columns = 4, int i_spacing = 10)
        {
            ArrowButton previousArrowButton = null;
            for (int i = 0; i < i_rows; i++)
            {
                Button lastButtonInRow = new Button();
                ArrowButton arrowButton = new ArrowButton(this.m_game);

                for (int j = 0; j < i_columns; j++)
                {
                    GuessButton guessButton = new GuessButton(this.m_game, k_margin, i_spacing);
                    guessButton.Left = k_margin + j * (k_ButtonSize + i_spacing);
                    guessButton.Top = i_spacing + 30 + (i + 1) * k_ButtonSize + i_spacing * i;// Difference between GuessButtons and the ones underneath - 30px
                    this.Controls.Add(guessButton);
                    arrowButton.AddAssociatedButton(guessButton);
                    guessButton.Click += guessButton.GuessButton_Onclick;
                    guessButton.BackColorChanged += arrowButton.ArrowButton_NotifyBackColorChanged;
                    if(previousArrowButton != null)
                    {
                        previousArrowButton.AddNextLineGuessButton(guessButton);
                        guessButton.Enabled = !true;
                    }
                    lastButtonInRow = guessButton;
                }

                arrowButton.Left = lastButtonInRow.Right + i_spacing;
                arrowButton.Top = lastButtonInRow.Top + 10;
                arrowButton.e_BoolPgiaAchieved += ArrowButton_BullseyeAchieved;
                this.Controls.Add(arrowButton);

                for (int k = 0; k < i_columns / 2; k++)
                {
                    for (int z = 0; z < i_columns - (i_columns / 2); z++) // The rest of the buttons - works in the case stringLength is odd.
                    {
                        Button resultButton = new Button();
                        resultButton.Width = resultButton.Height = 20;
                        resultButton.Left = arrowButton.Right + 2 * i_spacing + (resultButton.Width + 10) * z;
                        resultButton.Top = 40 + (i + 1) * k_ButtonSize + i_spacing * i + 30 * k;
                        resultButton.Enabled = !true;
                        this.Controls.Add(resultButton);
                        arrowButton.AddColoredButton(resultButton);                  
                    }
                }

                previousArrowButton = arrowButton;
            }
        }
    }
}
