using SQLite;

namespace efto_model.Data.Tables
{
    public class AccessKey_Table : DBContext
    {
        public AccessKey_Table(string database)
        {
            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(@"CREATE TABLE IF NOT EXISTS AccessKey (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Map TINYINT NOT NULL,
                            Name VARCHAR(150) NOT NULL,
                            X DOUBLE NOT NULL,
                            Y DOUBLE NOT NULL,
                            Show BIT NOT NULL
                            );");

                db.Execute(@"CREATE TABLE IF NOT EXISTS AccessKey_Loot (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            AccessKeyId INTEGER,
                            Type TINYINT NOT NULL,
                            Quantity TINYINT NOT NULL,
                            FOREIGN KEY (AccessKeyId) REFERENCES AccessKey(Id)
                            );");

                db.Execute(@"CREATE TABLE IF NOT EXISTS AccessKey_Quest_JunctionTable (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            AccessKeyId INTEGER,
                            QuestId INTEGER,
                            FOREIGN KEY (AccessKeyId) REFERENCES AccessKey(Id),
                            FOREIGN KEY (QuestId) REFERENCES Quest(Id)
                            );");
            }
        }
    }
}
