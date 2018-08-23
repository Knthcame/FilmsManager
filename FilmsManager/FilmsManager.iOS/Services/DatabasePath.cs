using System;
using System.IO;
using FilmsManager.Services.Interfaces;

namespace FilmsManager.iOS.Services
{
    public class DatabasePath : IDatabasePath
    {
        public string GetDatabasePath() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library");
    }
}