namespace efto_model.Models.Base
{
    public class Map
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double? UpdatedToVersion { get; set; }

        public Map(string name, double updateToVersion)
        {
            Name = name;
            UpdatedToVersion = updateToVersion;
        }

        public Map(string name) => Name = name;

        public Map() { }
    }
}
