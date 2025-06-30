using efto_model.Models.Base;
using efto_model.Services;
using SQLite;

namespace efto_model.Data.Tables
{
    public class Trader_Table : DBContext
    {
        public Trader_Table(string database)
        {
            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(DBQueryBuilder.CreateTable(Trader_SQLContext.Trader_Trable, Trader_SQLContext.Trader_Table_Name));
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
