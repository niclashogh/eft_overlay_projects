using SQLite;

namespace efto_model.Data.Tables
{
    public class BTR_Table : DBContext
    {
        public BTR_Table(string database)
        {
            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(@"CREATE TABLE IF NOT EXISTS BTR (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Location VARCHAR(50) NOT NULL,
                            DP TINYINT NOT NULL,
                            Map TINYINT NOT NULL,
                            X DOUBLE NOT NULL,
                            Y DOUBLE NOT NULL
                            );");
            }
        }
    }
}
