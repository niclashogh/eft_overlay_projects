using efto_model.Interfaces;
using efto_model.Models.Enums;
using efto_model.Records;

namespace efto_model.Models.Base
{
    public class BTR : IPosition
    {
        public int Id { get; set; }
        public string MapName { get; set; }
        public string Location { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public BTR(string location, string mapName) : this(new PositionRecord<double, double>(.5, .5))
        {
            this.MapName = mapName;
            this.Location = location;
        }

        public BTR(PositionRecord<double, double> pos)
        {
            X = pos.HorizontalPlacement;
            Y = pos.VerticalPlacement;
        }

        public BTR() { }
    }

    public static class BTR_SQLContext
    {
        public static string BTR_Table_Name { get; } = "BTR";

        public static List<SQLProperty> BTR_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("MapName", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.ForeignKey, new(nameof(Map), nameof(Map.Name))),
            new("Location", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull),
            new("X", SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
            new("Y", SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull)
        };
    }
}
