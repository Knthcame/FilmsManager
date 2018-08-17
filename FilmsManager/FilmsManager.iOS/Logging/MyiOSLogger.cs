using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FilmsManager.Logging;
using Foundation;
using UIKit;

namespace FilmsManager.iOS.Logging
{
    public class MyiOSLogger : ILogger
    {
        public void LogError(string message)
        {
            throw new NotImplementedException();
        }

        public void LogMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void LogObject(object obj, string message = "")
        {
            throw new NotImplementedException();
        }
    }
}