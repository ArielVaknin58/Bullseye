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
            m_game.GenerateString();
            for (int i = 0; i < m_game.StringLength; i++)
            {
                Button button = new Button();
                button.Height = button.Width = k_ButtonSize;
                button.BackColor = Color.Black;
                button.Top = k_margin;
                button.Left = k_margin + 10 * i + i * button.Width;
                this.Controls.Add(button);
            }

            printGridOfButtons(m_game.MaxGuesses, m_game.StringLength, k_spacing);

        }

        private void printGridOfButtons(int i_rows = 4, int i_columns = 4, int i_spacing = 10)
        {
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
                    lastButtonInRow = guessButton;
                }

                arrowButton.Width = k_ButtonSize;
                arrowButton.Height = 30;
                arrowButton.Text = "--->";
                arrowButton.Left = lastButtonInRow.Right + i_spacing;
                arrowButton.Top = lastButtonInRow.Top + 10;
                arrowButton.Enabled = !true;
                arrowButton.Click += arrowButton.ArrowButton_OnClick;
                this.Controls.Add(arrowButton);

                for (int k = 0; k < i_columns / 2; k++)
                {
                    for (int z = 0; z < i_columns - (i_columns / 2); z++) // The rest of the buttons - works in the case stringLength is odd.
                    {
                        Button resultButton = new Button();
                        resultButton.Width = resultButton.Height = 20;
                        resultButton.Left = arrowButton.Right + 2 * i_spacing + (resultButton.Width + 10) * k;
                        resultButton.Top = 40 + (i + 1) * k_ButtonSize + i_spacing * i + 30 * z;
                        resultButton.Enabled = !true;
                        this.Controls.Add(resultButton);
                        arrowButton.AddColoredButton(resultButton);
                    }
                }
            }
        }
    }
}
