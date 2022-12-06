using System;
using System.Collections.Generic;
using System.Text;

namespace LogServices
{
    class ConsoleLogProvider : ILogProvider
    {
        public void LogError(string msg)
        {
            Console.WriteLine($"ERROR:{msg}");
        }

        public void LogInfo(string msg)
        {
            Console.WriteLine($"info:{msg}");
        }
    }
}
