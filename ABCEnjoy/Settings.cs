using System;
namespace ABCEnjoy
{
    public class Settings
    {
        public static string dbPath = SQLite_Android.GetDatabasePath("ItemsOfCategory.db");
    }
}
