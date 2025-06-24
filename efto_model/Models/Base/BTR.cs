using efto_model.Interfaces;
using efto_model.Records;

namespace efto_model.Models.Base
{
    public class BTR : IPosition
    {
        public int Id { get; set; }
        public string MapName { get; set; }
        public string Location { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public BTR(string location, string mapName) : this(new PositionRecord<double, double>(.5, .5))
        {
            this.MapName = mapName;
            this.Location = location;
        }

        public BTR(PositionRecord<double, double> pos)
        {
            X = pos.HorizontalPlacement;
            Y = pos.VerticalPlacement;
        }

        public BTR() { }
    }
}
