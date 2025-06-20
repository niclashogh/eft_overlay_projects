using efto_model.Interfaces;
using efto_model.Records;

namespace efto_model.Models.Base
{
    public class BTR : IPosition
    {
        public int Id { get; set; }
        public int MapId { get; set; }
        public string Location { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public BTR(string location, int mapId) : this(new PositionRecord<double, double>(.5, .5))
        {
            MapId = mapId;
            Location = location;
        }

        public BTR(PositionRecord<double, double> pos)
        {
            X = pos.HorizontalPlacement;
            Y = pos.VerticalPlacement;
        }

        public BTR() { }
    }
}
