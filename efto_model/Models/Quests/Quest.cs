using efto_model.Models.Base;
using efto_model.Models.Enums;
using efto_model.Services;

namespace efto_model.Models.Quests
{
    public class Quest : NotifyChangedService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TraderName { get; set; }
        public Quest_Access Access { get; set; }

        public bool IsActive { get; set; }
        public bool IsComplete { get; set; }

        public Quest(string name, string traderName, Quest_Access access) : this(false, false)
        {
            this.Name = name;
            this.TraderName = traderName;
            this.Access = access;
        }

        public Quest(bool isActive, bool isComplete)
        {
            IsActive = isActive;
            IsComplete = isComplete;
        }

        public Quest() { }
    }

    public static partial class Quest_SQLContext
    {
        public static string Quest_Table_Name { get; } = "Quest";

        public static List<SQLProperty> Quest_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("Name", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull),
            new("TraderName", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.ForeignKey, new(nameof(Trader), nameof(Trader.Name))),
            new("Access", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull), // TINYINT OR nVARCHAR ???
            new("IsActive", SQLPropertyTypes.BIT, SQLPropertyNotations.NotNull),
            new("IsComplete", SQLPropertyTypes.BIT, SQLPropertyNotations.NotNull)
        };

        public static string Quest_RequiredByQuest_JunctionTable_Name { get; } = "Quest_RequiredByQuest_JunctionTable";

        public static List<SQLProperty> Quest_RequiredByQuest_JunctionTable { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("QuestId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id))),
            new("RequiredQuestId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id)))
        };

        public static string Quest_MultiChoice_JunctionTable_Name { get; } = "Quest_MultiChoice_JunctionTable";

        public static List<SQLProperty> Quest_MultiChoice_JunctionTable { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("GroupGUID", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull),
            new("QuestId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id)))
        };
    }
}
