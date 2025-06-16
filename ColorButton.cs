using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05
{
    public class ColorButton : Button
    {
        public Button m_TargetGuessButton { get; set; }
        public Color m_Color { get; set; }

        public ColorButton(Button i_targetButton, Color i_color)
        {
            m_TargetGuessButton = i_targetButton;
            m_Color = i_color;
            this.BackColor = i_color;
            this.Width = this.Height = 50;
            this.Click += ColorButton_OnClick;
        }

        private void ColorButton_OnClick(object i_sender, EventArgs i_e)
        {
            if (m_TargetGuessButton != null)
            {
                m_TargetGuessButton.BackColor = m_Color;
                this.FindForm().Hide();
            }
        }
    }


}
