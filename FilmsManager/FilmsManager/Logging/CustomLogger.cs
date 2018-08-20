using Prism.Logging;
using System;

namespace FilmsManager.Logging
{
    public class CustomLogger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            string messageToLog =
                String.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "{1}: {2}. Priority: {3}. Timestamp:{0:u}.",
                    DateTime.Now,
                    category.ToString().ToUpperInvariant(),
                    message,
                    priority.ToString());

            Console.WriteLine(messageToLog);
        }
    }

}
