using FilmsManager.Logging.Interfaces;
using Newtonsoft.Json;
using Prism.Logging;
using System;

namespace FilmsManager.Logging
{
    public class CustomLogger : ICustomLogger
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

        public void Log(string message, object obj, Category category, Priority priority)
        {
            try
            {
                Log($"{message} {JsonConvert.SerializeObject(obj)}", category, priority);
            }
            catch (JsonSerializationException ex)
            {
                if (ex.Message.Contains("loop"))
                    Log($"{message} {JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}", category, priority);
                else
                    Log(message, category, priority);
            }
            
        }
    }

}
