using SQLite;

namespace efto_model.Data.Tables
{
    public class Keybind_Table : DBContext
    {
        public Keybind_Table(string database)
        {
            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(@"CREATE TABLE IF NOT EXISTS Keybind (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Category VARCHAR(50) NOT NULL,
                            PressType VARCHAR(30) NOT NULL,
                            Desc nVARCHAR(50) NOT NULL,
                            DescOverrige nVARCHAR(50),
                            Key1 nVARCHAR(15),
                            Key2 nVARCHAR(15),
                            Show BIT NOT NULL
                            );");
            }
        }
    }
}
