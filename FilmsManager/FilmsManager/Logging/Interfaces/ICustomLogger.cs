using Prism.Logging;
using System;

namespace FilmsManager.Logging.Interfaces
{
    public interface ICustomLogger :ILoggerFacade
    {
        void Log(string str, Object obj, Category category, Priority priority);
    }
}
