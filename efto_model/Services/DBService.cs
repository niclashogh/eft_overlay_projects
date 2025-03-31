using efto_model.Data;
using efto_model.Data.Tables;
using SQLite;

namespace efto_model.Services
{
    public class DBService : DBContext
    {
        public DBService()
        {
            if (!Directory.Exists(ApplicationFolder)) Directory.CreateDirectory(ApplicationFolder);

            InitializeEsentialDB(EssentialDB);
            InitializeUserDB(UserDB);
        }

        private void InitializeEsentialDB(string database)
        {
            if (!Directory.Exists(database))
            {
                using (SQLiteConnection db = SQLCreateDB(database)) { }

                if (Directory.Exists(database))
                {
                    Extraction_Table extraction = new(database);
                    Quest_Table quest = new(database);
                    BTR_Table btr = new(database);
                    AccessKey_Table accesskey = new(database);
                }
            }
        }

        private void InitializeUserDB(string database)
        {
            if (!Directory.Exists(database))
            {
                using (SQLiteConnection db = SQLCreateDB(database)) { }

                if (Directory.Exists(database))
                {
                    Quest_Table quest = new(database);
                    Marker_Table marker = new(database);
                    Keybind_Table keybind = new(database);
                }
            }
        }
    }
}
