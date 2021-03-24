using System;
using System.Collections.Generic;
using System.Text;

namespace Charger_Functionality_Library.Help_Interfaces
{
    public interface IConsoleWriter
    {
        public void write(string str);
    }

    public class ConsoleWriter : IConsoleWriter
    {
        public void write(string str)
        {
            Console.WriteLine(str);
        }
    }
}
