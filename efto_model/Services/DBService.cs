using efto_model.Data;
using efto_model.Data.Tables;
using SQLite;
using System.Diagnostics;

namespace efto_model.Services
{
    public class DBService : DBContext
    {
        public DBService()
        {
            Directory.CreateDirectory(ApplicationFolder);

            InitializeEsentialDB(EssentialDB);
            InitializeUserDB(UserDB);
        }

        private void InitializeEsentialDB(string database)
        {
            if (!File.Exists(database))
            {
                Debug.WriteLine("CREATING DB FILES");
                using (SQLiteConnection db = SQLCreateDB(database)) { }

                Map_Table map = new(database);
                Trader_Table trader = new(database);

                Extraction_Table extraction = new(database);
                Quest_Table quest = new(database);
                BTR_Table btr = new(database);
                AccessKey_Table accesskey = new(database);
            }
        }

        private void InitializeUserDB(string database)
        {
            if (!File.Exists(database))
            {
                using (SQLiteConnection db = SQLCreateDB(database)) { }

                Marker_Table marker = new(database);
            }
        }
    }
}
