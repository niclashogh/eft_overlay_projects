using efto_model.Models.Quests;
using efto_model.Services;
using SQLite;

namespace efto_model.Data.Tables
{
    public class Quest_Table : DBContext
    {
        public Quest_Table(string database)
        {
            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(DBQueryBuilder.CreateTable(Quest_SQLContext.Quest_Table, Quest_SQLContext.Quest_Table_Name));

                db.Execute(DBQueryBuilder.CreateTable(Quest_SQLContext.Requirement_Table, Quest_SQLContext.Requirement_Table_Name));

                db.Execute(DBQueryBuilder.CreateTable(Quest_SQLContext.Reward_Category_Table, Quest_SQLContext.Reward_Category_Table_Name));
                db.Execute(DBQueryBuilder.CreateTable(Quest_SQLContext.Reward_Table, Quest_SQLContext.Reward_Table_Name));

                db.Execute(DBQueryBuilder.CreateTable(Quest_SQLContext.Task_Table, Quest_SQLContext.Task_Table_Name));
                db.Execute(DBQueryBuilder.CreateTable(Quest_SQLContext.Task_Icon_Table, Quest_SQLContext.Task_Icon_Table_Name));

                db.Execute(DBQueryBuilder.CreateTable(Quest_SQLContext.Quest_RequiredByQuest_JunctionTable, Quest_SQLContext.Quest_RequiredByQuest_JunctionTable_Name));
                db.Execute(DBQueryBuilder.CreateTable(Quest_SQLContext.Quest_MultiChoice_JunctionTable, Quest_SQLContext.Quest_MultiChoice_JunctionTable_Name));
            }

            PopulateDefault(database);
        }

        private void PopulateDefault(string database)
        {
            using (SQLiteConnection db = SQLConnection(database))
            {
                List<Quest_Reward_Category> categoryValues = new List<Quest_Reward_Category>
                {
                    new("Ammo"),
                    new("Weapons"),
                    new("Attatchments"),
                    new("Armor"),
                    new("Medic"),
                    new("Money"),
                    new("Other")
                };
                string categoryQuery = $"INSERT INTO Quest_Reward_Category (Category) VALUES (?)";

                foreach (Quest_Reward_Category item in categoryValues)
                {
                    db.Execute(categoryQuery, item.Category);
                }
            }
        }
    }
}
