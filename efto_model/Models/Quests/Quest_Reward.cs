using efto_model.Models.Enums;

namespace efto_model.Models.Quests
{
    public class Quest_Reward
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public string Reward { get; set; }

        public string Category { get; set; }
        public Quest_Reward_UnlockTypes UnlockType { get; set; }

        public Quest_Reward(int questId, string reward, string category, Quest_Reward_UnlockTypes unlockType) : this(reward, category, unlockType) => this.QuestId = questId;

        public Quest_Reward(string reward, string category, Quest_Reward_UnlockTypes unlockType)
        {
            this.Reward = reward;
            this.Category = category;
            this.UnlockType = unlockType;
        }

        public Quest_Reward() { }
    }

    public static partial class Quest_SQLContext
    {
        public static string Reward_Table_Name { get; } = "Quest_Reward";

        public static List<SQLProperty> Reward_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("QuestId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id))),
            new("Reward", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull),
            new("Category", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.ForeignKey, new(nameof(Quest_Reward_Category), nameof(Quest_Reward_Category.Category))),
            new("UnlockType", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull) // TINYINT OR nVARCHAR ???
        };
    }
}
