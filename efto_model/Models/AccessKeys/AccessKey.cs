using efto_model.Interfaces;
using efto_model.Records;
using efto_model.Services;

namespace efto_model.Models.AccessKeys
{
    public class AccessKey : NotifyChangedService, IPosition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MapName { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public bool Show { get; set; }

        public AccessKey(string name, string mapName) : this(new PositionRecord<double, double>(.5, .5))
        {
            this.Name = name;
            this.MapName = mapName;

            this.Show = false;
        }

        public AccessKey(PositionRecord<double, double> pos)
        {
            X = pos.HorizontalPlacement;
            Y = pos.VerticalPlacement;
        }

        public AccessKey(bool show)
        {
            Show = show;
        }

        public AccessKey() { }
    }
}
