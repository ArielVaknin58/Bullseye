using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class LettersAndColorsConverter
    {
        private readonly Dictionary<char, Color> m_charToColor = new Dictionary<char, Color>()
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

        private readonly Dictionary<Color, char> m_colorToChar = new Dictionary<Color, char>()
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
            if(!m_colorToChar.TryGetValue(i_color, out char result))
            {
                result = 'Z';
            }
            return result;
        }

        public Color CharToColor(char i_char)
        {
            if (!m_charToColor.TryGetValue(i_char, out Color result))
            {
                result = Color.Black;
            }
            return result;
        }
    }
}
