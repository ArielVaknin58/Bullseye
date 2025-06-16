using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ex02
{
    public class ColorButton : Button
    {
        public Button m_TargetGuessButton { get; set; }
        public Color m_Color { get; set; }

        public ColorButton(Button targetButton, Color color)
        {
            m_TargetGuessButton = targetButton;
            m_Color = color;
            this.BackColor = color;
            this.Width = this.Height = 50;
            this.Click += ColorButton_OnClick;
        }

        private void ColorButton_OnClick(object sender, EventArgs e)
        {
            if (m_TargetGuessButton != null)
            {
                m_TargetGuessButton.BackColor = m_Color;
                this.FindForm().Hide();
            }
        }
    }


}
