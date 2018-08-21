using Prism.Logging;
using System;

namespace FilmsManager.Logging.Interfaces
{
    public interface ICustomLogger : ILoggerFacade
    {
        void Log(string message, object obj, Category category, Priority priority);
    }
}
