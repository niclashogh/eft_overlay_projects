using efto_model.Models.Enums;

namespace efto_model.Models.AccessKeys
{
    public class AccessKey_Loot_Type
    {
        public string Type { get; set; }

        public AccessKey_Loot_Type(string type) => this.Type = type;

        public AccessKey_Loot_Type() { }
    }

    public static partial class AccessKey_SQLContext
    {
        public static string Type_Table_Name { get; } = "AccessKey_Loot_Type";

        public static List<SQLProperty> Type_Table { get; } = new List<SQLProperty>
        {
            new("Type", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.PrimaryKeyName)
        };
    }
}
