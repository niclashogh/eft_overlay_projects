using efto_model.Models.Enums;

namespace efto_model.Models.Markers
{
    public class Marker_Icon
    {
        public int Id {  get; set; }
        public string Icon { get; set; }

        public Marker_Icon(string icon) => this.Icon = icon;

        public Marker_Icon() { }
    }

    public static partial class Marker_SQLContext
    {
        public static string Icon_Table_Name { get; } = "Marker_Icon";

        public static List<SQLProperty> Icon_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("Icon", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.Unique)
        };
    }
}
