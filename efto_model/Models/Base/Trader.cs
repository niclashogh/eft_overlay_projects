namespace efto_model.Models.Base
{
    public class Trader
    {
        public string Name { get; set; }

        public Trader(string name) => this.Name = name;

        public Trader() { }
    }
}
