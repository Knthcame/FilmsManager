using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsManager.Logging
{
    public interface ILogger
    {
        void LogObject(object obj, string message = "");

        void LogError(string message);

        void LogMessage(string message);
    }
}
