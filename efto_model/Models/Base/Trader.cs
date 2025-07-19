using efto_model.Models.Enums;

namespace efto_model.Models.Base
{
    public class Trader
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Trader(string name) => this.Name = name;

        public Trader() { }
    }

    public static class Trader_SQLContext
    {
        public static string Trader_Table_Name { get; } = "Trader";

        public static List<SQLProperty> Trader_Trable { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("Name", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.Unique)
        };
    }
}
