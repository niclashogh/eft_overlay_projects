using efto_model.Interfaces;
using efto_model.Models.Base;
using efto_model.Models.Enums;
using efto_model.Records;

namespace efto_model.Models
{
    public class Marker : IPosition
    {
        public int Id { get; set; }
        public string MapName { get; set; }

        public string Name { get; set; }
        public string Desc { get; set; }
        public string Icon { get; set; }
        public Marker_Expandable_Areas ExpandableArea { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Marker(string mapName, string name, string desc, string icon, Marker_Expandable_Areas exspandableArea) : this(new PositionRecord<double, double>(.5, .5))
        {
            this.Name = name;
            this.Desc = desc;
            this.Icon = icon;
            this.ExpandableArea = exspandableArea;

            this.MapName = mapName;

            this.Width = 50;
            this.Height = 50;
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

    public static class Marker_SQLContext
    {
        public static string Marker_Table_Name { get; } = "Marker";

        public static List<SQLProperty> Marker_Table { get; } = new List<SQLProperty>
        {
            new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKeyId),
            new("MapName", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.ForeignKey, new(nameof(Map), nameof(Map.Name))),
            new("Name", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull),
            new("Desc", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull),
            new("Icon", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull),
            new("ExpandableArea", SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull), // TINYINT OR nVARCHAR ???
            new("Width", SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
            new("Height", SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
            new("X", SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
            new("Y", SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull)
        };
    }
}
