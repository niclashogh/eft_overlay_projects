using efto_model.Interfaces;
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
}
