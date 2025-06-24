using efto_model.Models;
using efto_model.Models.AccessKeys;
using efto_model.Models.Base;
using efto_model.Models.Enums;
using efto_model.Models.Quests;
using efto_model.Services;
using SQLite;

namespace efto_model.Data.Tables
{
    public class AccessKey_Table : DBContext
    {
        public AccessKey_Table(string database)
        {
            List<SQLProperty> accessKeys = new List<SQLProperty>
            {
                new(nameof(AccessKey.Id), SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new(nameof(AccessKey.Name), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.NotNull),
                new(nameof(AccessKey.MapName), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.ForeignKey, new(nameof(Map), nameof(Map.Name))),
                new(nameof(AccessKey.X), SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
                new(nameof(AccessKey.Y), SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
                new(nameof(AccessKey.Show), SQLPropertyTypes.BIT, SQLPropertyNotations.NotNull)
            };
            string accessKeyQuery = DBQueryBuilder.CreateTable(accessKeys, nameof(AccessKey));

            List<SQLProperty> lootTypes = new List<SQLProperty>
            {
                new(nameof(AccessKey_Loot_Type.Type), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.PrimaryKey)
            };
            string lootTypeQuery = DBQueryBuilder.CreateTable(lootTypes, nameof(AccessKey_Loot_Type));

            List<SQLProperty> loot = new List<SQLProperty>
            {
                new(nameof(AccessKey_Loot.Id), SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new(nameof(AccessKey_Loot.AccessKeyId), SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(AccessKey), nameof(AccessKey.Id))),
                new(nameof(AccessKey_Loot.Type), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.ForeignKey, new(nameof(AccessKey_Loot_Type), nameof(AccessKey_Loot_Type.Type))),
                new(nameof(AccessKey_Loot.Quantity), SQLPropertyTypes.INTEGER, SQLPropertyNotations.NotNull)
            };
            string lootQuery = DBQueryBuilder.CreateTable(loot, nameof(AccessKey_Loot));

            List<SQLProperty> accessKeyQuestJunctionTable = new List<SQLProperty>
            {
                new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new("AccessKeyId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(AccessKey), nameof(AccessKey.Id))),
                new("QuestId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id)))
            };
            string accessKeyQuestJunctionTableQuery = DBQueryBuilder.CreateTable(accessKeyQuestJunctionTable, "AccessKey_Quest_Junction_Table");

            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(accessKeyQuery);
                db.Execute(lootTypeQuery);
                db.Execute(lootQuery);
                db.Execute(accessKeyQuestJunctionTableQuery);
            }

            PopulateDefault(database);
        }

        private void PopulateDefault(string database)
        {
            using (SQLiteConnection db = SQLConnection(database))
            {
                List<AccessKey_Loot_Type> typeValues = new List<AccessKey_Loot_Type>
                {
                    new("Keycard"),
                    new("Key"),
                    new("Bitcoin"),

                    new("Intelligence folder"),
                    new("Blue folder"),

                    new("LEDX"),
                    new("GPU"),
                    new("COFDM"),
                    new("Microcontroller board"),

                    new("Streamer item"),
                    new("Figures"),
                    new("Rare"),
                };

                string insertQuery = $"INSERT INTO AccessKey_Loot_Type (Type) VALUES (?)";

                foreach (AccessKey_Loot_Type type in typeValues)
                {
                    db.Execute(insertQuery, type.Type);
                }
            }
        }
    }
}
