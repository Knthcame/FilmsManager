using Android.Util;
using FilmsManager.Logging;
using Newtonsoft.Json;

namespace FilmsManager.Droid.Logging
{
    public class MyAndroidLogger : ILogger
    {
        private string _tag = "FilmsManager"; 

        public void LogError(string message)
        {
            Log.Error(_tag, message);
        }

        public void LogMessage(string message)
        {
            Log.Info(_tag, message);
        }

        public void LogObject(object obj, string message = "")
        {
            Log.Info(_tag, $"{message}{JsonConvert.SerializeObject(obj)}");
        }
    }
}