using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bullseye
{
    internal class ConsoleHandler
    {
        public void PrintLine(string i_Message = "\n")
        {
            Console.WriteLine(i_Message);
        }

        public void Print(string i_Message = "\n")
        { 
            Console.Write(i_Message); 
        }
        public string ReadLine()
        {
            return Console.ReadLine();
        }


    }
}
