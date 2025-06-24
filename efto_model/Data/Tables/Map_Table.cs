using efto_model.Models;
using efto_model.Models.Base;
using efto_model.Models.Enums;
using efto_model.Services;
using SQLite;

namespace efto_model.Data.Tables
{
    public class Map_Table : DBContext
    {
        public Map_Table(string database)
        {
            List<SQLProperty> maps = new List<SQLProperty>
            {
                new(nameof(Map.Name), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.PrimaryKey),
                new(nameof(Map.UpdatedToVersion), SQLPropertyTypes.DOUBLE, SQLPropertyNotations.Nullable)
            };
            string mapQuery = DBQueryBuilder.CreateTable(maps, nameof(Map));

            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(mapQuery);
            }

            PopulateDefault(database);
        }

        private void PopulateDefault(string database)
        {
            using (SQLiteConnection db = SQLConnection(database))
            {
                List<Map> mapValues = new List<Map>
                    {
                        new("Customs", 0),
                        new("Factory", 0),
                        new("Ground Zero", 0),
                        new("Interchange", 0),
                        new("Labs", 0),
                        new("Lighthouse", 0),
                        new("Reserve", 0),
                        new("Shoreline", 0),
                        new("Streets of Tarkov", 0),
                        new("Woods", 0),
                    };

                string insertQuery = $"INSERT INTO Map (Name, UpdatedToVersion) VALUES (?, ?)";

                foreach (Map map in mapValues)
                {
                    db.Execute(insertQuery, map.Name, map.UpdatedToVersion);
                }
            }
        }
    }
}
