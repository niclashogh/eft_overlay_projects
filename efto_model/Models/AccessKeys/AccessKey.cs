using efto_model.Interfaces;
using efto_model.Models.Base;
using efto_model.Models.Enums;
using efto_model.Models.Quests;
using efto_model.Records;
using efto_model.Services;

namespace efto_model.Models.AccessKeys
{
    public class AccessKey : NotifyChangedService, IPosition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MapName { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public bool Show { get; set; }

        public AccessKey(string name, string mapName) : this(new PositionRecord<double, double>(.5, .5))
        {
            this.Name = name;
            this.MapName = mapName;

            this.Show = false;
        }

        public AccessKey(PositionRecord<double, double> pos)
        {
            X = pos.HorizontalPlacement;
            Y = pos.VerticalPlacement;
        }

        public AccessKey(bool show)
        {
            Show = show;
        }

        public AccessKey() { }
    }

    public static partial class AccessKey_SQLContext
    {
        public static string AccessKey_Table_Name { get; } = "AccessKey";

        public static List<SQLProperty> AccessKey_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("Name", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull),
            new("MapName", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.ForeignKey, new(nameof(Map), nameof(Map.Name))),
            new("X", SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
            new("Y", SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
            new("Show", SQLPropertyTypes.BIT, SQLPropertyNotations.NotNull)
        };

        public static string AccessKey_Quest_JunctionTable_Name { get; } = "AccessKey_Quest_JunctionTable";

        public static List<SQLProperty> AccessKey_Quest_JunctionTable { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("AccessKeyId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new("AccessKey", "Id")),
            new("QuestId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id)))
        };
    }
}
