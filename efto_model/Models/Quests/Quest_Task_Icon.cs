using efto_model.Models.Enums;

namespace efto_model.Models.Quests
{
    public class Quest_Task_Icon
    {
        public int Id { get; set; }
        public string Icon { get; set; }

        public Quest_Task_Icon(string icon) => this.Icon = icon;

        public Quest_Task_Icon() { }
    }

    public static partial class Quest_SQLContext
    {
        public static string Task_Icon_Table_Name { get; } = "Quest_Task_Icon";

        public static List<SQLProperty> Task_Icon_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("Icon", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.Unique)
        };
    }
}
