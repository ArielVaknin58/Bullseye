using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05
{
    public class ArrowButton : Button
    {
        // Each ArrowButton object has its own different set of buttons, so they're saved within the button object.
        private readonly List<Button> r_GuessesButtonsList = new List<Button>(); // The big 4 GuessButtons on the left of the arrowButton
        private readonly List<Button> r_FeedbackButtonsList = new List<Button>();  // The small 4 buttons on the right of the arrowButton 
        private readonly List<Button> r_NextLineGuessButtons = new List<Button>(); // The 4 GuessButtons of the next line, will become enabled when arrowButton will decide.
        private Game m_Game;
        public event EventHandler BullseyeAchieved;
    

        public ArrowButton(Game i_game)
        {
            m_Game = i_game;
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
        public void OnBullseyeAchieved()
        {
            BullseyeAchieved.Invoke(this, EventArgs.Empty);
        }

        private Color resultToColor(Game.eFeedbackTypes i_charResult)
        {
            Color resultColor = Button.DefaultBackColor;

            if(i_charResult.Equals(Game.eFeedbackTypes.CorrectPosition))
            {
                resultColor = Color.Black;
            }
            else if(i_charResult.Equals(Game.eFeedbackTypes.WrongPosition))
            {
                resultColor = Color.Yellow;
            }

            return resultColor;
        }

        public void ArrowButton_NotifyBackColorChanged(object i_sender, EventArgs i_e)
        {
            bool ifAllInColor = true;
            bool noColorRepeated = true;
            List<Color> colorsPool = new List<Color>();

            foreach (Button button in r_GuessesButtonsList)
            {

                if (button.BackColor.Equals(Button.DefaultBackColor))
                {
                    ifAllInColor = !true;
                }
                else if (colorsPool.Contains(button.BackColor))
                {
                    noColorRepeated = !true;
                }
                else
                {
                    colorsPool.Add(button.BackColor);
                }

            }
           
            this.Enabled = ifAllInColor && noColorRepeated;

        }

        public void ArrowButton_OnClick(object i_sender, EventArgs i_e) 
        {
            this.Enabled = !true;
            char[] charsFromColorsArray = new char[m_Game.StringLength];

            for (int i = 0; i < m_Game.StringLength; i++)
            {
                charsFromColorsArray[i] = new LettersAndColorsConverter().ColorToChar(r_GuessesButtonsList[i].BackColor);
            }

            List<Game.eFeedbackTypes> feedbackList = m_Game.EvaluateGuess(charsFromColorsArray);

            for (int j = 0; j < m_Game.StringLength; j++)
            {
                r_FeedbackButtonsList[j].BackColor = resultToColor(feedbackList[j]);
            }

            if (m_Game.CheckIfBullseye(feedbackList))
            {
                OnBullseyeAchieved();
            }
            else
            {       
                foreach (Button nextLineGuessButton in r_NextLineGuessButtons)
                {
                    nextLineGuessButton.Enabled = true;
                }
            }

            foreach (Button guessButton in r_GuessesButtonsList)
            {
                guessButton.Enabled = !true;
            }

        }
        
    }
}
