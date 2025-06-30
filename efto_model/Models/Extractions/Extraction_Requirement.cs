using efto_model.Models.Enums;

namespace efto_model.Models.Extractions
{
    public class Extraction_Requirement
    {
        public int Id { get; set; }
        public int ExtractionId { get; set; }
        public string Requirement { get; set; }

        public Extraction_Requirement(int extractionId, string requirement)
        {
            ExtractionId = extractionId;
            Requirement = requirement;
        }

        public Extraction_Requirement() { }
    }

    public static partial class Extraction_SQLContext
    {
        public static string Requirement_Table_Name { get; } = "Extraction_Requirement";

        public static List<SQLProperty> Requirement_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("ExtractionId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Extraction), nameof(Extraction.Id))),
            new("Requirement", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull)
        };
    }
}
