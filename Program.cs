using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Bullseye
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            Console.WriteLine("~~Hello ! Welcome to Bullseye.~~");
            Console.WriteLine("Please choose the number of max guesses (4-10):");
            string GuessNumInput = Console.ReadLine();
            while (!int.TryParse(GuessNumInput, out int numericGuess) || numericGuess > 10 || numericGuess < 4)
            {
                Console.WriteLine("Please enter a legal input - a number between 4 and 10 !");
                GuessNumInput = Console.ReadLine();
            }
            Console.WriteLine(game.GenerateString());

            char[] myGuess = { 'A', 'B', 'C', 'D' };
            Console.WriteLine(myGuess);

            List<char> list = game.GuessCurrentString(myGuess);
            foreach (char c in list)
            {
                Console.Write(c);
            }

            
            Console.ReadLine();

        }
    }
}
