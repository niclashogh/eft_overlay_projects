using efto_model.Models.DataTransferObjects;
using efto_model.Models.Enums;
using efto_model.Services;

namespace efto_model.Models
{
    public class Extraction : NotifyChangedService
    {

        public int Id { get; set; }
        public Dragging_Privileges DP { get; set; }
        public Maps Map { get; set; }
        public Extraction_Types Type { get; set; }
        public string Name { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public Extraction(Dragging_Privileges dp, Maps map, Extraction_Types type, string name)
        {
            this.DP = dp;
            this.Map = map;
            this.Type = type;
            this.Name = name;

            this.X = .5;
            this.Y = .5;
        }

        public Extraction(PositionRecord<double, double> pos)
        {
            this.X = pos.HorizontalPlacement;
            this.Y = pos.VerticalPlacement;
        }

        public Extraction() { }
    }
}
