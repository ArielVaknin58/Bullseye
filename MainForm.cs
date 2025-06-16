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

namespace Ex05
{
    public partial class MainForm : Form
    {
        private int m_maxGuesses = 4;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object i_sender, EventArgs i_e)
        {
            this.button1.Text = $"Number of chances : {m_maxGuesses}";
        }

        private void ChancesIncreaserButton_Click(object i_sender, EventArgs i_e)
        {
            m_maxGuesses = m_maxGuesses == 10 ? 4 : m_maxGuesses + 1;
            (i_sender as Button).Text = $"Number of chances : {m_maxGuesses}";
        }

        private void StartButton_Click(object i_sender, EventArgs i_e)
        {
            this.Hide();
            GameForm gameForm = new GameForm();
            gameForm.m_Game.m_MaxGuesses = m_maxGuesses;
            gameForm.ShowDialog();          
        }
    }
}
