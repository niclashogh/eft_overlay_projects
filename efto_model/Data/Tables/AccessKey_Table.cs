using efto_model.Models.AccessKeys;
using efto_model.Services;
using SQLite;

namespace efto_model.Data.Tables
{
    public class AccessKey_Table : DBContext
    {
        public AccessKey_Table(string database)
        {
            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(DBQueryBuilder.CreateTable(AccessKey_SQLContext.Icon_Table, AccessKey_SQLContext.Icon_Table_Name));
                db.Execute(DBQueryBuilder.CreateTable(AccessKey_SQLContext.AccessKey_Table, AccessKey_SQLContext.AccessKey_Table_Name));

                db.Execute(DBQueryBuilder.CreateTable(AccessKey_SQLContext.Type_Table, AccessKey_SQLContext.Type_Table_Name));
                db.Execute(DBQueryBuilder.CreateTable(AccessKey_SQLContext.Loot_Table, AccessKey_SQLContext.Loot_Table_Name));

                db.Execute(DBQueryBuilder.CreateTable(AccessKey_SQLContext.AccessKey_Quest_JunctionTable, AccessKey_SQLContext.AccessKey_Quest_JunctionTable_Name));
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

                string insertTypeQuery = $"INSERT INTO AccessKey_Loot_Type (Type) VALUES (?)";

                foreach (AccessKey_Loot_Type type in typeValues)
                {
                    db.Execute(insertTypeQuery, type.Type);
                }

                List<AccessKey_Icon> iconValues = new List<AccessKey_Icon>
                {
                    new("Keycard"),
                    new("Marked Key"),
                    new("Room Key"),
                    new("Container Key")
                };

                string insertIconQuery = $"INSERT INTO AccessKey_Icon (Icon) VALUES (?)";

                foreach (AccessKey_Icon icon in iconValues)
                {
                    db.Execute(insertIconQuery, icon.Icon);
                }
            }
        }
    }
}
