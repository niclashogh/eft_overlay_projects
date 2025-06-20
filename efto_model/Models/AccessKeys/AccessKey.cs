using efto_model.Records;
using efto_model.Services;

namespace efto_model.Models.AccessKeys
{
    public class AccessKey : NotifyChangedService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MapId { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public bool Show { get; set; }

        public AccessKey(string name, int mapId) : this(new PositionRecord<double, double>(.5, .5))
        {
            this.Name = name;
            this.MapId = mapId;

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
