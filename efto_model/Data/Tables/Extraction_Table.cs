using SQLite;

namespace efto_model.Data.Tables
{
    public class Extraction_Table : DBContext
    {
        public Extraction_Table(string database)
        {
            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(@"CREATE TABLE IF NOT EXISTS Extraction (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            DP TINYINT NOT NULL,
                            Map TINYINT NOT NULL,
                            Type TINYINT NOT NULL,
                            Name VARCHAR(50) NOT NULL,
                            X DOUBLE NOT NULL,
                            Y DOUBLE NOT NULL
                            );");

                db.Execute(@"CREATE TABLE IF NOT EXISTS Extraction_Requirement (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            ExtractionId INTEGER,
                            Requirement VARCHAR(50) NOT NULL,
                            FOREIGN KEY (ExtractionId) REFERENCES Extraction(Id)
                            );");
            }
        }
    }
}
