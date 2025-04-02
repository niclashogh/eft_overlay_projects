using efto_model.Models.DataTransferObjects;
using efto_model.Models.Enums;

namespace efto_model.Models
{
    public class Marker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public Dragging_Privileges DP { get; set; }
        public Maps Map { get; set; }
        public Marker_Types Type { get; set; }
        public Palette Color { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Marker(string name, string desc, Dragging_Privileges dp, Maps map, Marker_Types type, Palette color, DimensionRecord<double> size)
        {
            this.Name = name;
            this.Desc = desc;

            this.DP = dp;
            this.Map = map;
            this.Type = type;
            this.Color = color;

            this.Width = size.Width;
            this.Height = size.Height;
            this.X = .5;
            this.Y = .5;
        }

        public Marker(PositionRecord<double, double> pos)
        {
            this.X = pos.HorizontalPlacement;
            this.Y = pos.HorizontalPlacement;
        }

        public Marker(DimensionRecord<double> size)
        {
            this.Width = size.Width;
            this.Height = size.Height;
        }

        public Marker() { }
    }
}
