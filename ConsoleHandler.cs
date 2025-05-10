using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullseye
{
    internal class ConsoleHandler
    {
        public void PrintLine(string message = "\n")
        {
            Console.WriteLine(message);
        }

        public void Print(string message = "\n")
        { 
            Console.Write(message); 
        }
        public string ReadLine()
        {
            return Console.ReadLine();
        }


    }
}
