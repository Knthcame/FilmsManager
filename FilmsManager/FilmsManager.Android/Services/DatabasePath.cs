using System.IO;
using FilmsManager.Services.Interfaces;

namespace FilmsManager.Droid.Services
{
    public class DatabasePath : IDatabasePath
    {
        public string GetDatabasePath() => Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "databases");
    }
}