using efto_model.Models.Enums;

namespace efto_model.Models.Base
{
    public class Map
    {
        public string Name { get; set; }

        public double? UpdatedToVersion { get; set; }

        public Map(string name, double updateToVersion) : this(updateToVersion) => this.Name = name;

        public Map(double updateToVersion) => this.UpdatedToVersion = updateToVersion;

        public Map() { }
    }

    public static class Map_SQLContext
    {
        public static string Map_Table_Name { get; } = "Map";

        public static List<SQLProperty> Map_Table { get; } = new List<SQLProperty>
        {
            new("Name", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.PrimaryKeyName),
            new("UpdatedToVersion", SQLPropertyTypes.DOUBLE, SQLPropertyNotations.Nullable)
        };
    }
}
