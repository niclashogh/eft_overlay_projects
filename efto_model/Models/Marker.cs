using efto_model.Records;

namespace efto_model.Models
{
    public class Marker
    {
        public int Id { get; set; }
        public string MapName { get; set; }

        public string Name { get; set; }
        public string Desc { get; set; }
        public string Icon { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Marker(string mapName, string name, string desc, string icon, DimensionRecord<double> size) : this(new PositionRecord<double, double>(.5, .5))
        {
            this.Name = name;
            this.Desc = desc;

            this.MapName = mapName;

            this.Width = size.Width;
            this.Height = size.Height;
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
