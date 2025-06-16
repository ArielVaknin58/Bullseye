using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Ex02
{
    public class ArrowButton : Button
    {
        private readonly List<Button> r_GuessesButtonsList = new List<Button>();
        private readonly List<Button> r_ColorsButtonsList = new List<Button>();
        private readonly LettersAndColorsConverter l_converter = new LettersAndColorsConverter();
        private Game m_game;

        public ArrowButton(Game i_game)
        {
            m_game = i_game;
        }
        public void AddAssociatedButton(GuessButton button)
        {
            r_GuessesButtonsList.Add(button);
        }

        public void AddColoredButton(Button button)
        {
            r_ColorsButtonsList.Add(button);
        }

        public void ArrowButton_OnClick(object sender, EventArgs e)
        {
            for (int i = 0; i < r_GuessesButtonsList.Count; i++)
            {
                Color charAsColor = l_converter.CharToColor(m_game.CorrectGuess[i]);
                Color currentButtonColor = r_GuessesButtonsList[i].BackColor;

                if (charAsColor.Equals(currentButtonColor))
                {
                    this.r_ColorsButtonsList[i].BackColor = Color.Magenta;
                }
                else
                {
                    foreach(Button button in r_GuessesButtonsList)
                    {
                        if(button.BackColor.Equals(charAsColor))
                        {
                            this.r_ColorsButtonsList[i].BackColor = Color.LimeGreen;
                        }
                    }

                    this.r_ColorsButtonsList[i].BackColor = Color.Black;

                } 

            }
        }
    }
}
