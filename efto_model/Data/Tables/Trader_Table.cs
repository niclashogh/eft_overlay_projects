using efto_model.Models;
using efto_model.Models.Base;
using efto_model.Models.Enums;
using efto_model.Services;
using SQLite;

namespace efto_model.Data.Tables
{
    public class Trader_Table : DBContext
    {
        public Trader_Table(string database)
        {
            List<SQLProperty> traders = new List<SQLProperty>
            {
                new(nameof(Trader.Id), SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new(nameof(Trader.Name), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.NotNull)
            };
            string traderQuery = DBQueryBuilder.CreateTable(traders, nameof(Trader));

            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(traderQuery);
            }

            PopulateDefault(database);
        }

        private void PopulateDefault(string database)
        {
            using (SQLiteConnection db = SQLConnection(database))
            {
                List<Trader> traderValues = new List<Trader>
                {
                    new("Prapor"),
                    new("Therapist"),
                    new("Fence"),
                    new("Skier"),
                    new("Peacekeeper"),
                    new("Mechanic"),
                    new("Ragman"),
                    new("Jaeger"),
                    new("Lightkeeper"),
                    new("Ref"),
                    new("BTR Driver"),
                    new("Events"),
                };

                string insertQuery = $"INSERT INTO Trader (Name) VALUES (?)";

                foreach (Trader trader in traderValues)
                {
                    db.Execute(insertQuery, trader.Name);
                }
            }
        }
    }
}
