using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05
{
    public class LettersAndColorsConverter
    {
        private readonly Dictionary<char, Color> r_CharToColor = new Dictionary<char, Color>()
        {
        { 'A', Color.Purple },
        { 'B', Color.Red },
        { 'C', Color.ForestGreen },
        { 'D', Color.Aqua },
        { 'E', Color.Blue },
        { 'F', Color.Yellow },
        { 'G', Color.Brown },
        { 'H', Color.White }
        };

        private readonly Dictionary<Color, char> r_ColorToChar = new Dictionary<Color, char>()
        {
        { Color.Purple, 'A' },
        { Color.Red, 'B' },
        { Color.ForestGreen, 'C' },
        { Color.Aqua, 'D' },
        { Color.Blue, 'E' },
        { Color.Yellow, 'F' },
        { Color.Brown, 'G' },
        { Color.White, 'H' }
        };
        
        public char ColorToChar(Color i_color)
        {
            if(!r_ColorToChar.TryGetValue(i_color, out char result))
            {
                result = ' ';
            }

            return result;
        }

        public Color CharToColor(char i_char)
        {
            if (!r_CharToColor.TryGetValue(i_char, out Color o_result))
            {
                o_result = Color.AliceBlue; // Some different color to indicate something went wrong
            }

            return o_result;
        }
    }
}
