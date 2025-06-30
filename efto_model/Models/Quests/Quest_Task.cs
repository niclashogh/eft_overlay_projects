using efto_model.Interfaces;
using efto_model.Models.Base;
using efto_model.Models.Enums;
using efto_model.Records;
using efto_model.Services;

namespace efto_model.Models.Quests
{
    public class Quest_Task : NotifyChangedService, IPosition
    {
        public int Id { get; set; }
        public string MapName { get; set; }
        public string TraderName { get; set; }
        public int QuestId { get; set; }

        public string Desc { get; set; }
        public int Sequence { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public Quest_Task(string mapName, string traderName, int questId, string desc, int sequence) : this(mapName, traderName, desc, sequence) => this.QuestId = questId;

        public Quest_Task(string mapName, string traderName, string desc, int sequence) : this(new PositionRecord<double, double>(.5, .5))
        {
            this.MapName = mapName;
            this.TraderName = traderName;

            this.Desc = desc;
            this.Sequence = sequence;
        }

        public Quest_Task(PositionRecord<double, double> pos)
        {
            this.X = pos.HorizontalPlacement;
            this.Y = pos.VerticalPlacement;
        }

        public Quest_Task() { }
    }

    public static partial class Quest_SQLContext
    {
        public static string Task_Table_Name { get; } = "Quest_Task";

        public static List<SQLProperty> Task_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("MapName", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.ForeignKey, new(nameof(Map), nameof(Map.Name))),
            new("TraderName", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.ForeignKey, new(nameof(Trader), nameof(Trader.Name))),
            new("QuestId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id))),
            new("Desc", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull),
            new("Sequence", SQLPropertyTypes.INTEGER, SQLPropertyNotations.NotNull),
            new("X", SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
            new("Y", SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull)
        };
    }
}
