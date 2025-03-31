using SQLite;

namespace efto_model.Data
{
    public class DBContext
    {
        private static string roamingFolder { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string ApplicationFolder { get; } = Path.Combine(roamingFolder, "EFT Overlay");
        public static string EssentialDB { get; } = Path.Combine(ApplicationFolder, "esential_data.db");
        public static string UserDB { get; } = Path.Combine(ApplicationFolder, "user_data.db");

        internal SQLiteConnection SQLConnection(string database)
        {
            SQLiteConnection db = new(database, SQLiteOpenFlags.ReadWrite);
            //db.Execute("PRAGMA foreign_keys = ON;");
            return db;
        }

        internal SQLiteConnection SQLCreateTable(string database)
        {
            SQLiteConnection db = new(database, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);
            //db.Execute("PRAGMA foreign_keys = ON;");
            return db;
        }

        internal SQLiteConnection SQLCreateDB(string database)
        {
            SQLiteConnection db = new(database, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);
            //db.EnableWriteAheadLogging();
            db.ExecuteScalar<string>("PRAGMA foreign_keys = ON;"); // ON, OFF
            db.ExecuteScalar<string>("PRAGMA journal_mode = DELETE;"); // WAL, DELETE
            db.ExecuteScalar<string>("PRAGMA synchronous = OFF;"); // OFF, NORMAL, FULL
            return db;
        }
    }
}
