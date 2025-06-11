using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullseye
{
    public class LetterToColorConverter
    {

        public Color ToColor(char i_charInput)
        {
            if (i_charInput >= 'A' && i_charInput <= 'H')
            {
                switch (i_charInput)
                {
                    case 'A':
                        return Color.Purple;
                    case 'B':
                        return Color.Red;
                    case 'C':
                        return Color.ForestGreen;
                    case 'D':
                        return Color.Aqua;
                    case 'E':
                        return Color.Blue;
                    case 'F':
                        return Color.Yellow;
                    case 'G':
                        return Color.Brown;
                    case 'H':
                        return Color.White;

                }
            }

            return Color.Black;
        }
    }
}
