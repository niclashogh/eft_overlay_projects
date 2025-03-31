using SQLite;

namespace efto_model.Data.Tables
{
    public class Marker_Table : DBContext
    {
        public Marker_Table(string database)
        {
            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(@"CREATE TABLE IF NOT EXISTS Marker (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            DP TINYINT NOT NULL,
                            Map TINYINT NOT NULL,
                            Type TINYINT NOT NULL,
                            GroupId INT NOT NULL,
                            Desc VARCHAR(40) NOT NULL,
                            Width DOUBLE NOT NULL,
                            Height DOUBLE NOT NULL,
                            X DOUBLE NOT NULL,
                            Y DOUBLE NOT NULL
                            );");
            }
        }
    }
}
