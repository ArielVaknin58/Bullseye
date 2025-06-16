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

namespace Bullseye
{
    public partial class MainForm : Form
    {
        private int m_maxGuesses = 4;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.button1.Text = $"Number of chances : {m_maxGuesses}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_maxGuesses = m_maxGuesses == 10 ? 4 : m_maxGuesses + 1;
            (sender as Button).Text = $"Number of chances : {m_maxGuesses}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            GameForm gameForm = new GameForm();
            gameForm.m_game.MaxGuesses = m_maxGuesses;
            gameForm.ShowDialog();          
        }
    }
}
