namespace WerfLogDal
{
    // All the code in this file is included in all platforms.
    public static class Constants
    {

        public const string DatabaseFileName = "WerfLogSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =

            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;


        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
    }
}
