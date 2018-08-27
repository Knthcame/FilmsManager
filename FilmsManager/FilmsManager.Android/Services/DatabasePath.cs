using System.IO;
using FilmsManager.Services.Interfaces;

namespace FilmsManager.Droid.Services
{
    public class DatabasePath : IDatabasePath
    {
        public string GetDatabasePath() => System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
    }
}