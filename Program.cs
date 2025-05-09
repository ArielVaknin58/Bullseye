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
            for (int i = 0; i < 10; i++) 
            {
                Console.WriteLine(game.GuessString());
                Console.ReadLine();

            }
            Console.ReadLine();

        }
    }
}
