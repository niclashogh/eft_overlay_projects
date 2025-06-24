using efto_model.Models;
using efto_model.Models.Base;
using efto_model.Services;
using SQLite;

namespace efto_model.Data.Tables
{
    public class BTR_Table : DBContext
    {
        public BTR_Table(string database)
        {
            List<SQLProperty> btr = new List<SQLProperty>
            {
                new(nameof(BTR.Id), Models.Enums.SQLPropertyTypes.INTEGER, Models.Enums.SQLPropertyNotations.PrimaryKey),
                new(nameof(BTR.MapName), Models.Enums.SQLPropertyTypes.INTEGER, Models.Enums.SQLPropertyNotations.ForeignKey, new(nameof(Map), nameof(Map.Name))),
                new(nameof(BTR.Location), Models.Enums.SQLPropertyTypes.VARCHAR, Models.Enums.SQLPropertyNotations.NotNull),
                new(nameof(BTR.X), Models.Enums.SQLPropertyTypes.DOUBLE, Models.Enums.SQLPropertyNotations.NotNull),
                new(nameof(BTR.Y), Models.Enums.SQLPropertyTypes.DOUBLE, Models.Enums.SQLPropertyNotations.NotNull)
            };
            string btrQuery = DBQueryBuilder.CreateTable(btr, nameof(BTR));

            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(btrQuery);
            }
        }
    }
}
