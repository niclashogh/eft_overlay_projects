using efto_model.Interfaces;
using efto_model.Models.Base;
using efto_model.Models.Enums;
using efto_model.Records;
using efto_model.Services;

namespace efto_model.Models.Extractions
{
    public class Extraction : NotifyChangedService, IPosition
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string MapName { get; set; }
        public string Type { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        public Extraction(string name, string mapName, string type) : this(new PositionRecord<double, double>(.5, .5))
        {
            this.Name = name;
            this.MapName = mapName;
            this.Type = type;
        }

        public Extraction(PositionRecord<double, double> pos)
        {
            X = pos.HorizontalPlacement;
            Y = pos.VerticalPlacement;
        }

        public Extraction() { }
    }

    public static partial class Extraction_SQLContext
    {
        public static string Extraction_Table_Name { get; } = "Extraction";

        public static List<SQLProperty> Extraction_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("Name", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull),
            new("MapName", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.ForeignKey, new(nameof(Map), nameof(Map.Name))),
            new("Type", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.ForeignKey, new(nameof(Extraction_Type), nameof(Extraction_Type.Type))),
            new("X", SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
            new("Y", SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull)
        };
    }
}
