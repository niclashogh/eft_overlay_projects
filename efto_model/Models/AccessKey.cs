using efto_model.Models.DataTransferObjects;
using efto_model.Models.Enums;
using efto_model.Services;

namespace efto_model.Models
{
    public class AccessKey : NotifyChangedService
    {
        public int Id { get; set; }
        public Maps Map { get; set; }
        public string Name { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public bool Show { get; set; }

        public AccessKey(Maps map, string name)
        {
            this.Map = map;
            this.Name = name;

            this.X = .5;
            this.Y = .5;

            this.Show = false;
        }

        public AccessKey(PositionRecord<double, double> pos)
        {
            this.X = pos.HorizontalPlacement;
            this.Y = pos.VerticalPlacement;
        }

        public AccessKey(bool show)
        {
            this.Show = show;
        }

        public AccessKey() { }
    }
}
