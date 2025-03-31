using SQLite;

namespace efto_model.Data.Tables
{
    public class Quest_Table : DBContext
    {
        public Quest_Table(string database)
        {
            using (SQLiteConnection db = SQLCreateTable(database))
            {
                db.Execute(@"CREATE TABLE IF NOT EXISTS Quest (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name VARCHAR(50) NOT NULL,
                            Trader TINYINT NOT NULL,
                            Access TINYINT NOT NULL,
                            IsActive BIT NOT NULL,
                            IsComplete BIT NOT NULL
                            );");

                db.Execute(@"CREATE TABLE IF NOT EXISTS Quest_Requirement (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            QuestId INTEGER,
                            Requirement VARCHAR(80) NOT NULL,
                            FOREIGN KEY (QuestId) REFERENCES Quest(Id)
                            );");

                db.Execute(@"CREATE TABLE IF NOT EXISTS Quest_Reward (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            QuestId INTEGER,
                            Reward VARCHAR(50) NOT NULL,
                            Category TINYINT NOT NULL,
                            Type TINYINT NOT NULL,
                            FOREIGN KEY (QuestId) REFERENCES Quest(Id)
                            );");

                db.Execute(@"CREATE TABLE IF NOT EXISTS Quest_Task (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            DP TINYINT NOT NULL,
                            Map TINYINT NOT NULL,
                            Trader TINYINT NOT NULL,
                            QuestId INTEGER,
                            Desc VARCHAR(50),
                            Sequence INT NOT NULL,
                            X DOUBLE NOT NULL,
                            Y DOUBLE NOT NULL,
                            FOREIGN KEY (QuestId) REFERENCES Quest(Id)
                            );");

                db.Execute(@"CREATE TABLE IF NOT EXISTS Quest_ToQuest_JunctionTable (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            QuestId INTEGER,
                            RequiredQuestId INTEGER,
                            FOREIGN KEY (QuestId) REFERENCES Quest(Id),
                            FOREIGN KEY (RequiredQuestId) REFERENCES Quest(Id)
                            );");

                db.Execute(@"CREATE TABLE IF NOT EXISTS Quest_MultiChoice_JunctionTable (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            GroupGUID TEXT NOT NULL,
                            QuestId INTEGER,
                            FOREIGN KEY (QuestId) REFERENCES Quest(Id)
                            );");
            }
        }
    }
}
