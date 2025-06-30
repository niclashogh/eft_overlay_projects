using efto_model.Models.Extractions;
using efto_model.Services;
using SQLite;

namespace efto_model.Data.Tables
{
    public class Extraction_Table : DBContext
    {
        public Extraction_Table(string database)
        {
            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(DBQueryBuilder.CreateTable(Extraction_SQLContext.Type_Table, Extraction_SQLContext.Type_Table_Name));
                db.Execute(DBQueryBuilder.CreateTable(Extraction_SQLContext.Extraction_Table, Extraction_SQLContext.Extraction_Table_Name));
                db.Execute(DBQueryBuilder.CreateTable(Extraction_SQLContext.Requirement_Table, Extraction_SQLContext.Requirement_Table_Name));
            }

            PopulateDefault(database);
        }

        private void PopulateDefault(string database)
        {
            using (SQLiteConnection db = SQLConnection(database))
            {
                List<Extraction_Type> typeValues = new List<Extraction_Type>
                    {
                        new("PMC"),
                        new("Scav"),
                        new("Shared"),
                        new("Hidden"),
                        new("Transit")
                    };

                string insertQuery = $"INSERT INTO Extraction_Type (Type) VALUES (?)";

                foreach (Extraction_Type type in typeValues)
                {
                    db.Execute(insertQuery, type.Type);
                }
            }
        }
    }
}
