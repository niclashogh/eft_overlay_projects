using efto_model.Models.Enums;

namespace efto_model.Models.Quests
{
    public class Quest_Reward_Category
    {
        public int Id { get; set; }
        public string Category { get; set; }

        public Quest_Reward_Category(string category) => this.Category = category;

        public Quest_Reward_Category() { }
    }

    public static partial class Quest_SQLContext
    {
        public static string Reward_Category_Table_Name { get; } = "Quest_Reward_Category";

        public static List<SQLProperty> Reward_Category_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("Category", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.Unique)
        };
    }
}
