using efto_model.Models;
using efto_model.Models.Base;
using efto_model.Models.Enums;
using efto_model.Services;
using SQLite;

namespace efto_model.Data.Tables
{
    public class Marker_Table : DBContext
    {
        public Marker_Table(string database)
        {
            List<SQLProperty> markers = new List<SQLProperty>
            {
                new(nameof(Marker.Id), SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new(nameof(Marker.MapName), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.ForeignKey, new(nameof(Map), nameof(Map.Name))),
                new(nameof(Marker.Name), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.NotNull),
                new(nameof(Marker.Desc), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.NotNull),
                new(nameof(Marker.Icon), SQLPropertyTypes.nVARCHAR, SQLPropertyNotations.NotNull),
                new(nameof(Marker.Width), SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
                new(nameof(Marker.Height), SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
                new(nameof(Marker.X), SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
                new(nameof(Marker.Y), SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull)
            };
            string markerQuery = DBQueryBuilder.CreateTable(markers, nameof(Marker));

            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(markerQuery);
            }
        }
    }
}
