using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05
{
    public partial class GameForm : Form
    {
        public Game m_Game { get; private set; } = new Game();
        private readonly List<ArrowButton> r_ArrowButtonsList = new List<ArrowButton>();
        private readonly List<Button> r_FinalResultsButtons  = new List<Button>();
        private const int k_ButtonSize = 50;
        private const int k_Spacing = 10;
        private const int k_Margin = 20;
        

        public GameForm()
        {     
            InitializeComponent();
        }

        private void GameForm_Load(object i_sender, EventArgs i_e)
        {
            this.Height += (this.m_Game.m_MaxGuesses - 3) * (k_Spacing + k_ButtonSize);
            m_Game.GenerateString();

            for (int i = 0; i < m_Game.StringLength; i++)
            {
                Button button = new Button();
                button.Height = button.Width = k_ButtonSize;
                button.BackColor = Color.Black;
                button.Top = k_Margin;
                button.Left = k_Margin + 10 * i + i * k_ButtonSize;
                button.Enabled = !true;
                this.Controls.Add(button);
                this.r_FinalResultsButtons.Add(button);
            }

            printGridOfButtons(m_Game.m_MaxGuesses, m_Game.StringLength, k_Spacing);

        }

        private void GameForm_BullseyeAchieved(object i_sender, EventArgs i_e)
        {
            for (int k = 0; k < m_Game.StringLength; k++)
            {
                this.r_FinalResultsButtons[k].BackColor = new LettersAndColorsConverter().CharToColor(m_Game.m_CorrectGuess[k]);
            }
        }


        private void printGridOfButtons(int i_rows = 4, int i_columns = 4, int i_spacing = 10)
        {
            ArrowButton previousArrowButton = null;

            for (int i = 0; i < i_rows; i++)
            {
                Button lastButtonInRow = new Button();
                ArrowButton arrowButton = new ArrowButton(this.m_Game);

                for (int j = 0; j < i_columns; j++)
                {
                    GuessButton guessButton = new GuessButton(this.m_Game, k_Margin, i_spacing);

                    guessButton.Left = k_Margin + j * (k_ButtonSize + i_spacing);
                    guessButton.Top = i_spacing + 30 + (i + 1) * k_ButtonSize + i_spacing * i;// Difference between GuessButtons and the ones underneath - 30px
                    this.Controls.Add(guessButton);
                    arrowButton.AddAssociatedButton(guessButton);
                    guessButton.Click += guessButton.GuessButton_Onclick;
                    guessButton.BackColorChanged += arrowButton.ArrowButton_NotifyBackColorChanged;

                    if(previousArrowButton != null)
                    {
                        previousArrowButton.AddNextLineGuessButton(guessButton);      
                    }

                    guessButton.Enabled = (previousArrowButton == null);
                    lastButtonInRow = guessButton;
                }

                arrowButton.Left = lastButtonInRow.Right + i_spacing;
                arrowButton.Top = lastButtonInRow.Top + 10;
                arrowButton.BullseyeAchieved += GameForm_BullseyeAchieved;
                this.Controls.Add(arrowButton);

                for (int k = 0; k < i_columns / 2; k++)
                {
                    for (int z = 0; z < i_columns - (i_columns / 2); z++) // The rest of the buttons. It alsworks in the case stringLength is odd.
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
