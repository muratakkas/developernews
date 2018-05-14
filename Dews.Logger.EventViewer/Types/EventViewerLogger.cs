using Dews.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.Logger.ConsoleLogger.Types
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Log(Exception ex)
        {
            Console.WriteLine("Exception : " + ex.Message);
        }
    }
}
