using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05
{
    internal class UI
    {
        private Game m_game = new Game();
        public void Run()
        {

            MainForm form = new MainForm();

            form.ShowDialog();

        }

        public void chanceButton_OnClick(object i_sender,EventArgs i_e)
        {
            m_game.m_MaxGuesses = m_game.m_MaxGuesses == 10 ? 4 : m_game.m_MaxGuesses + 1;
            (i_sender as Button).Text = $"Number of chances : {m_game.m_MaxGuesses}";

        }      
    }
}
