using efto_model.Interfaces;
using efto_model.Records;
using efto_model.Services;

namespace efto_model.Models.Extractions
{
    public class Extraction : NotifyChangedService, IPosition
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public int MapId { get; set; }
        public int TypeId { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public Extraction(string name, int mapId, int typeId) : this(new PositionRecord<double, double>(.5, .5))
        {
            Name = name;
            MapId = mapId;
            TypeId = typeId;
        }

        public Extraction(PositionRecord<double, double> pos)
        {
            X = pos.HorizontalPlacement;
            Y = pos.VerticalPlacement;
        }

        public Extraction() { }
    }
}
