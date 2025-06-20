using efto_model.Records;
using efto_model.Services;

namespace efto_model.Models.Quests
{
    public class Quest_Task : NotifyChangedService
    {
        public int Id { get; set; }
        public int MapId { get; set; }
        public int TraderId { get; set; }
        public int QuestId { get; set; }

        public string Desc { get; set; }
        public int Sequence { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public Quest_Task(int mapId, int traderId, int questId, string desc, int sequence) : this(mapId, traderId, desc, sequence) => this.QuestId = questId;

        public Quest_Task(int mapId, int traderId, string desc, int sequence) : this(new PositionRecord<double, double>(.5, .5))
        {
            this.MapId = mapId;
            this.TraderId = traderId;

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
