using efto_model.Models.Enums;

namespace efto_model.Models.Base
{
    public class Map
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? UpdatedToVersion { get; set; }

        public Map(string name, string updateToVersion) : this(updateToVersion) => this.Name = name;

        public Map(string updateToVersion) => this.UpdatedToVersion = updateToVersion;

        public Map() { }
    }

    public static class Map_SQLContext
    {
        public static string Map_Table_Name { get; } = "Map";

        public static List<SQLProperty> Map_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("Name", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.Unique),
            new("UpdatedToVersion", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.Nullable)
        };
    }
}
