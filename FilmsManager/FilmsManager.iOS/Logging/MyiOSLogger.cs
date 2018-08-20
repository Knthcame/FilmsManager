using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FilmsManager.Logging;
using Foundation;
using Newtonsoft.Json;
using UIKit;

namespace FilmsManager.iOS.Logging
{
    public class MyiOSLogger : ILogger
    {
        public void LogError(Exception error)
        {
            Console.WriteLine(error);
        }

        public void LogMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void LogObject(object obj, string message = "")
        {
            Console.WriteLine($"{message} {JsonConvert.SerializeObject(obj)}");
        }
    }
}