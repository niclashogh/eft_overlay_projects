using efto_model.Models.DataTransferObjects;
using efto_model.Models.Enums;

namespace efto_model.Models
{
    public class BTR
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public Dragging_Privileges DP { get; set; }
        public Maps Map { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public BTR(string location, Dragging_Privileges dp, Maps map)
        {
            this.Location = location;
            this.DP = dp;
            this.Map = map;

            this.X = .5;
            this.Y = .5;
        }

        public BTR(PositionRecord<double, double> pos)
        {
            this.X = pos.HorizontalPlacement;
            this.Y = pos.VerticalPlacement;
        }

        public BTR() { }
    }
}
