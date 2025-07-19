using efto_model.Models.Markers;
using efto_model.Services;
using SQLite;

namespace efto_model.Data.Tables
{
    public class Marker_Table : DBContext
    {
        public Marker_Table(string database)
        {
            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(DBQueryBuilder.CreateTable(Marker_SQLContext.Icon_Table, Marker_SQLContext.Icon_Table_Name));
                db.Execute(DBQueryBuilder.CreateTable(Marker_SQLContext.Marker_Table, Marker_SQLContext.Marker_Table_Name));
            }
        }
    }
}
