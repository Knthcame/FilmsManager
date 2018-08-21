using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsManager.Logging.Interfaces
{
    public interface ICustomLogger :ILoggerFacade
    {
        void Log(string str, Object obj, Category category, Priority priority);
    }
}
