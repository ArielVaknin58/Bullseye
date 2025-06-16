using Ex02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05
{
    public class GuessButton : Button
    {
        private const int k_ButtonSize = 50;
        private Game  m_game;
        private int m_margin;
        private int m_spacing;
        

        public GuessButton(Game i_game, int i_margin, int i_spacing) 
        {
            this.Height = this.Width = k_ButtonSize;
            this.m_margin = i_margin;
            this.m_spacing = i_spacing;
            this.m_game = i_game;
        }

        public void GuessButton_Onclick(object i_sender, EventArgs i_e)
        {
            Form colorSelectionForm = new Form();
            colorSelectionForm.Width = 320;
            colorSelectionForm.Height = 180;
            LettersAndColorsConverter converter = new LettersAndColorsConverter();
            char letter = 'A';

            for (int i = 0; i < m_game.NumberOfOptions / 4; i++)
            {
                for (int j = 0; j < m_game.NumberOfOptions / 2; j++)
                {
                    ColorButton colorButton = new ColorButton((i_sender as Button), converter.CharToColor(letter));

                    letter = (char)((int)letter + 1);
                    colorButton.Height = colorButton.Width = k_ButtonSize;
                    colorButton.Top = m_margin + i * (m_spacing + colorButton.Height);
                    colorButton.Left = m_margin + j * (m_spacing + k_ButtonSize);
                    colorSelectionForm.Controls.Add(colorButton);

                }
            }

            colorSelectionForm.ShowDialog();
        }
    }
}
