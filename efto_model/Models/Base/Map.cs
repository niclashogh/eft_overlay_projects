namespace efto_model.Models.Base
{
    public class Map
    {
        public string Name { get; set; }

        public double? UpdatedToVersion { get; set; }

        public Map(string name, double updateToVersion) : this(updateToVersion) => this.Name = name;

        public Map(double updateToVersion) => this.UpdatedToVersion = updateToVersion;

        public Map() { }
    }
}
