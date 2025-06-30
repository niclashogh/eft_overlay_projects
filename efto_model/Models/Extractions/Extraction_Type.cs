using efto_model.Models.Enums;

namespace efto_model.Models.Extractions
{
    public class Extraction_Type
    {
        public string Type { get; set; }

        public Extraction_Type(string type) => this.Type = type;

        public Extraction_Type() { }
    }

    public static partial class Extraction_SQLContext
    {
        public static string Type_Table_Name { get; } = "Extraction_Type";

        public static List<SQLProperty> Type_Table { get; } = new List<SQLProperty>
        {
            new("Type", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.PrimaryKeyName)
        };
    }
}
