using efto_model.Models.Enums;

namespace efto_model.Models.AccessKeys
{
    public class AccessKey_Icon
    {
        public int Id { get; set; }
        public string Icon {  get; set; }

        public AccessKey_Icon(string icon) => this.Icon = icon;

        public AccessKey_Icon() { }
    }

    public static partial class AccessKey_SQLContext
    {
        public static string Icon_Table_Name { get; } = "AccessKey_Icon";

        public static List<SQLProperty> Icon_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("Icon", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.Unique)
        };
    }
}
