using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bullseye
{
    internal class GuessButton : Button
    {
        private const int k_ButtonSize = 50;

        public GuessButton() 
        {
            this.Height = this.Width = k_ButtonSize;
        }

    }
}
