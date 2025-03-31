using efto_model.Models.DataTransferObjects;
using efto_model.Models.Enums;
using efto_model.Services;

namespace efto_model.Models
{
    public class Quest_Task : NotifyChangedService
    {
        public int Id { get; set; }
        public Dragging_Privileges DP { get; set; }
        public Maps Map { get; set; }
        public Traders Trader { get; set; }
        public int QuestId { get; set; }
        public string Desc { get; set; }
        public int Sequence { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public Quest_Task(Dragging_Privileges dp, Maps map, Traders trader, int questId, string desc, int sequence)
        {
            this.DP = dp;
            this.Map = map;
            this.Trader = trader;
            this.QuestId = questId;
            this.Desc = desc;
            this.Sequence = sequence;

            this.X = .5;
            this.Y = .5;
        }

        public Quest_Task(Dragging_Privileges dp, Maps map, Traders trader, string desc, int sequence)
        {
            this.DP = dp;
            this.Map = map;
            this.Trader = trader;
            this.Desc = desc;
            this.Sequence = sequence;

            this.X = .5;
            this.Y = .5;
        }

        public Quest_Task(PositionRecord<double, double> pos)
        {
            this.X = pos.HorizontalPlacement;
            this.Y = pos.VerticalPlacement;
        }

        public Quest_Task() { }
    }
}
