using efto_model.Models.Enums;

namespace efto_model.Models.Quests
{
    public class Quest_Requirement
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public string Requirement {  get; set; }

        public Quest_Requirement(int questId, string requirement) : this(requirement) => this.QuestId = questId;

        public Quest_Requirement(string requirement) => this.Requirement = requirement;

        public Quest_Requirement() { }
    }

    public static partial class Quest_SQLContext
    {
        public static string Requirement_Table_Name { get; } = "Quest_Requirement";

        public static List<SQLProperty> Requirement_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("QuestId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id))),
            new("Requirement", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull)
        };
    }
}
