using efto_model.Models;
using efto_model.Models.Base;
using efto_model.Models.Enums;
using efto_model.Models.Quests;
using efto_model.Services;
using SQLite;

namespace efto_model.Data.Tables
{
    public class Quest_Table : DBContext
    {
        public Quest_Table(string database)
        {
            List<SQLProperty> quests = new List<SQLProperty>
            {
                new(nameof(Quest.Id), SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new(nameof(Quest.Name), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.NotNull),
                new(nameof(Quest.TraderId), SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Trader), nameof(Trader.Id))),
                new(nameof(Quest.AccessEnum), SQLPropertyTypes.TINTYINT, SQLPropertyNotations.NotNull),
                new(nameof(Quest.IsActive), SQLPropertyTypes.BIT, SQLPropertyNotations.NotNull),
                new(nameof(Quest.IsComplete), SQLPropertyTypes.BIT, SQLPropertyNotations.NotNull)
            };
            string questQuery = DBQueryBuilder.CreateTable(quests, nameof(Quest));

            List<SQLProperty> requirements = new List<SQLProperty>
            {
                new(nameof(Quest_Requirement.Id), SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new(nameof(Quest_Requirement.QuestId), SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id))),
                new(nameof(Quest_Requirement.Requirement), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.NotNull)
            };
            string requirementQuery = DBQueryBuilder.CreateTable(requirements, nameof(Quest_Requirement));

            List<SQLProperty> rewardCategories = new List<SQLProperty>
            {
                new(nameof(Quest_Reward_Category.Id), SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new(nameof(Quest_Reward_Category.Category), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.NotNull)
            };
            string rewardCategoryQuery = DBQueryBuilder.CreateTable(rewardCategories, nameof(Quest_Reward_Category));

            List<SQLProperty> rewardAccess = new List<SQLProperty>
            {
                new(nameof(Quest_Reward_Access.Id), SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new(nameof(Quest_Reward_Access.Access), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.NotNull)
            };
            string rewardAccessQuery = DBQueryBuilder.CreateTable(rewardAccess, nameof(Quest_Reward_Access));

            List<SQLProperty> rewards = new List<SQLProperty>
            {
                new(nameof(Quest_Reward.Id), SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new(nameof(Quest_Reward.QuestId), SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id))),
                new(nameof(Quest_Reward.Reward), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.NotNull),
                new(nameof(Quest_Reward.CategoryId), SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest_Reward_Category), nameof(Quest_Reward_Category.Id))),
                new(nameof(Quest_Reward.AccessId), SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest_Reward_Access), nameof(Quest_Reward_Access.Id)))
            };
            string rewardQuery = DBQueryBuilder.CreateTable(rewards, nameof(Quest_Reward));

            List<SQLProperty> tasks = new List<SQLProperty>
            {
                new(nameof(Quest_Task.Id), SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new(nameof(Quest_Task.MapId), SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Map), nameof(Map.Id))),
                new(nameof(Quest_Task.TraderId), SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Trader), nameof(Trader.Id))),
                new(nameof(Quest_Task.QuestId), SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id))),
                new(nameof(Quest_Task.Desc), SQLPropertyTypes.VARCHAR, SQLPropertyNotations.NotNull),
                new(nameof(Quest_Task.Sequence), SQLPropertyTypes.INTEGER, SQLPropertyNotations.NotNull),
                new(nameof(Quest_Task.X), SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull),
                new(nameof(Quest_Task.Y), SQLPropertyTypes.DOUBLE, SQLPropertyNotations.NotNull)
            };
            string taskQuery = DBQueryBuilder.CreateTable(tasks, nameof(Quest_Task));

            List<SQLProperty> questToQuestJunctionTable = new List<SQLProperty>
            {
                new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new("QuestId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id))),
                new("RequiredQuestId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id)))
            };
            string questToQuestJunctionTableQuery = DBQueryBuilder.CreateTable(questToQuestJunctionTable, "Quest_ToQuest_JunctionTable");

            List<SQLProperty> questMultiChoiceJunctionTable = new List<SQLProperty>
            {
                new("Id", SQLPropertyTypes.INTEGER, SQLPropertyNotations.PrimaryKey),
                new("GroupGUID", SQLPropertyTypes.TEXT, SQLPropertyNotations.NotNull),
                new("QuestId", SQLPropertyTypes.INTEGER, SQLPropertyNotations.ForeignKey, new(nameof(Quest), nameof(Quest.Id)))
            };
            string questMultiChoiceJunctionTableQuery = DBQueryBuilder.CreateTable(questMultiChoiceJunctionTable, "Quest_MultiChoice_JunctionTable");

            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(questQuery);
                db.Execute(requirementQuery);
                db.Execute(rewardCategoryQuery);
                db.Execute(rewardAccessQuery);
                db.Execute(rewardQuery);
                db.Execute(taskQuery);
                db.Execute(questToQuestJunctionTableQuery);
                db.Execute(questMultiChoiceJunctionTableQuery);
            }

            PopulateDefault(database);
        }

        private void PopulateDefault(string database)
        {
            using (SQLiteConnection db = SQLConnection(database))
            {
                List<Quest_Reward_Access> accessValues = new List<Quest_Reward_Access>
                {
                    new("Handover"),
                    new("Byable"),
                    new("Craftable")
                };

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

                string accessQuery = $"INSERT INTO Quest_Reward_Access (Access) VALUES (?)";
                string categoryQuery = $"INSERT INTO Quest_Reward_Category (Category) VALUES (?)";

                foreach (Quest_Reward_Access item in accessValues)
                {
                    db.Execute(accessQuery, item.Access);
                }

                foreach (Quest_Reward_Category item in categoryValues)
                {
                    db.Execute(accessQuery, item.Category);
                }
            }
        }
    }
}
