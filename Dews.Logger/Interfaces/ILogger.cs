using System;
using System.Collections.Generic;
using System.Text;

namespace Dews.Logger.Interfaces
{
    public interface ILogger
    {
        void Log(string message);

        void Log(Exception message);
    }
}
