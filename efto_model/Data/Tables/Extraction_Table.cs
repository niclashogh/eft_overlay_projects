using efto_model.Models;
using efto_model.Models.Enums;
using efto_model.Models.Extractions;
using efto_model.Services;
using SQLite;

namespace efto_model.Data.Tables
{
    public class Extraction_Table : DBContext
    {
        public Extraction_Table(string database)
        {
            List<SQLProperty> types = new List<SQLProperty>
            {
                new(nameof(Extraction_Type.Id), SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new(nameof(Extraction_Type.Desc), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.Nullable)
            };
            string typeQuery = DBQueryBuilder.CreateTable(types, nameof(Extraction_Type));

            List<SQLProperty> extractions = new List<SQLProperty>
            {
                new(nameof(Extraction.Id), SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new(nameof(Extraction.Name), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.NotNull),
                new(nameof(Extraction.TypeId), SQLPropertyTypes.INTEGER, SQLPropertyNotations.NotNull),
                new(nameof(Extraction.MapId), SQLPropertyTypes.INTEGER, SQLPropertyNotations.NotNull),
                new(nameof(Extraction.X), SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
                new(nameof(Extraction.Y), SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull)
            };
            string extractionQuery = DBQueryBuilder.CreateTable(extractions, nameof(Extraction));

            List<SQLProperty> requirements = new List<SQLProperty>
            {
                new(nameof(Extraction_Requirement.Id), SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new(nameof(Extraction_Requirement.ExtractionId), SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Extraction), nameof(Extraction.Id))),
                new(nameof(Extraction_Requirement.Requirement), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.NotNull)
            };
            string requirementQuery = DBQueryBuilder.CreateTable(requirements, nameof(Extraction_Requirement));

            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(typeQuery);
                db.Execute(extractionQuery);
                db.Execute(requirementQuery);
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

                string insertQuery = $"INSERT INTO Extraction_Type (Desc) VALUES (?)";

                foreach (Extraction_Type type in typeValues)
                {
                    db.Execute(insertQuery, type.Desc);
                }
            }
        }
    }
}
