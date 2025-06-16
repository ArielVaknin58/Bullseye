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
        // Each ArrowButton object has its own different set of buttons, so they're saved within the button object.
        private readonly List<Button> r_GuessesButtonsList = new List<Button>(); // The big 4 GuessButtons on the left of the arrowButton
        private readonly List<Button> r_FeedbackButtonsList = new List<Button>();  // The small 4 buttons on the right of the arrowButton 
        private readonly List<Button> r_NextLineGuessButtons = new List<Button>(); // The 4 GuessButtons of the next line, will become enabled when arrowButton will decide.
        private int m_ColoredGuessButtons = 0;
        private Game m_game;

        public event EventHandler e_BoolPgiaAchieved;

        public void OnBoolPgiaAchieved()
        {
            e_BoolPgiaAchieved.Invoke(this, EventArgs.Empty);
        }

        public ArrowButton(Game i_game)
        {
            m_game = i_game;
            this.Width = 50;
            this.Height = 30;
            this.Text = "--->";
            this.Enabled = !true;
            this.Click += this.ArrowButton_OnClick;
        }
        public void AddAssociatedButton(GuessButton i_button)
        {
            r_GuessesButtonsList.Add(i_button);
        }

        public void AddColoredButton(Button i_button)
        {
            r_FeedbackButtonsList.Add(i_button);
        }

        public void AddNextLineGuessButton(Button i_button)
        {
            r_NextLineGuessButtons.Add(i_button);
        }
        private Color resultToColor(char i_charResult)
        {
            Color resultColor = Button.DefaultBackColor;
            if(i_charResult.Equals('X'))
            {
                resultColor = Color.Black;
            }
            else if(i_charResult.Equals('V'))
            {
                resultColor = Color.Yellow;
            }

            return resultColor;
        }

        public void ArrowButton_NotifyBackColorChanged(object sender, EventArgs e)
        {
            bool ifAllInColor = true;
            bool NoColorRepeated = true;
            List<Color> colorsPool = new List<Color>();
            foreach (Button button in r_GuessesButtonsList)
            {
                if (button.BackColor.Equals(Button.DefaultBackColor))
                {
                    ifAllInColor = !true;
                }
                else if (colorsPool.Contains(button.BackColor))
                {
                    NoColorRepeated = !true;
                }
                else
                {
                    colorsPool.Add(button.BackColor);
                }

            }
           
            this.Enabled = ifAllInColor && NoColorRepeated;

        }


        public void ArrowButton_OnClick(object sender, EventArgs e)
        {
            this.Enabled = !true;
            char[] chars = new char[m_game.StringLength];
            for(int i = 0 ; i < m_game.StringLength; i++ )
            {
                chars[i] = new LettersAndColorsConverter().ColorToChar(r_GuessesButtonsList[i].BackColor);
            }

            List<char> results = m_game.EvaluateGuess(chars);

            for(int j = 0; j < m_game.StringLength; j++)
            {
                r_FeedbackButtonsList[j].BackColor = resultToColor(results[j]);
                r_FeedbackButtonsList[j].Text = results[j].ToString();
            }

            
            if(m_game.CheckIfBullseye(results.ToList()))
            {
                OnBoolPgiaAchieved();               
            }
            else
            {
                if(r_NextLineGuessButtons.Count == 0)
                {
                    MessageBox.Show("You ran out of guesses ! better luck next time..", "Game Over");
                }
                foreach (Button guessButton in r_NextLineGuessButtons)
                {
                    guessButton.Enabled = true;
                }
            }

            foreach (Button button in r_GuessesButtonsList)
            {
                button.Enabled = !true;
            }
        }
    }
}
