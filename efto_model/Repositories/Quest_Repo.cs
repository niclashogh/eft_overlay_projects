using efto_model.Data;
using efto_model.Models;
using efto_model.Models.Enums;
using SQLite;
using System.Collections.ObjectModel;

namespace efto_model.Repositories
{
    public class Quest_Repo : DBContext
    {
        #region Standard
        public async Task<bool> Add(Quest model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string insertQuery = $"INSERT INTO Quest (Name, Trader, Access, IsActive, IsComplete) VALUES (?, ?, ?, ?, ?)";
                    db.Execute(insertQuery, model.Name, model.Trader, model.Access, model.IsActive, model.IsComplete);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<ObservableCollection<Quest>?> Find(string? searchWord)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchWord))
                {
                    using (SQLiteConnection db = SQLConnection(EssentialDB))
                    {
                        List<Quest>? queryResult = null;
                        string selectQuery = $"SELECT * FROM Quest WHERE Name LIKE ?";

                        if (searchWord.Length == 1)
                        {
                            queryResult = db.Query<Quest>(selectQuery, $"%{searchWord}%").Where(filter => filter.Name.StartsWith(searchWord, StringComparison.OrdinalIgnoreCase)).ToList();
                        }
                        else
                        {
                            queryResult = db.Query<Quest>(selectQuery, $"%{searchWord}%");
                        }

                        return queryResult != null ? new ObservableCollection<Quest>(queryResult) : null;
                    }
                }
                else return null;
            }
            catch { return null; }
        }

        public async Task<ObservableCollection<T>?> LoadFromTrader<T>(Traders trader) where T : class, new()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Quest WHERE Trader = ?";
                    List<T>? queryResult = db.Query<T>(selectQuery, trader) ?? null;

                    return queryResult != null ? new ObservableCollection<T>(queryResult) : null;
                }
            }
            catch { return null; }
        }

        public async Task<T?> LoadLast<T>() where T : class, new()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Quest ORDER BY Id DESC LIMIT 1";
                    T? item = db.Query<T>(selectQuery).FirstOrDefault() ?? null;

                    return item != null ? item : null;
                }
            }
            catch { return null; }
        }

        public async Task<T?> LoadSingle<T>(int id) where T : class, new()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Quest WHERE Id = ?";
                    T? item = db.Query<T>(selectQuery, id).FirstOrDefault() ?? null;

                    return item != null ? item : null;
                }
            }
            catch { return null; }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string deleteQuery = $"DELETE FROM Quest WHERE Id = ?";
                    db.Execute(deleteQuery, id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Update(Quest model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE Quest SET Name = ?, Trader = ?, Access = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.Name, model.Trader, model.Access, model.Id);
                }

                return true;
            }
            catch { return false; }

        }

        public async Task<bool> UpdateCompletion(Quest model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE Quest SET IsComplete = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.IsComplete, model.Id);
                }

                return true;
            }
            catch { return false; }

        }

        public async Task<bool> UpdateActive(Quest model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE Quest SET IsActive = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.IsActive, model.Id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> ResetProgression()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE Quest SET IsComplete = 0 WHERE IsComplete = 1";
                    db.Execute(updateQuery);
                }

                return true;
            }
            catch { return false; }
        }
        #endregion

        #region JunctionTables
        public async Task<bool> AddQuestToQuestDependency(int questId, int requiredQuestId)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string insertQuery = $"INSERT INTO Quest_ToQuest_JunctionTable (QuestId, RequiredQuestId) VALUES (?, ?)";
                    db.Execute(insertQuery, questId, requiredQuestId);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> RemoveQuestToQuestDependency(int questId, int requiredQuestId)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string insertQuery = $"DELETE INTO Quest_ToQuest_JunctionTable WHERE QuestId = ? AND RequiredQuestId = ?";
                    db.Execute(insertQuery, questId, requiredQuestId);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<ObservableCollection<Quest>?> LoadQuestDependencies(int questId)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Quest_ToQuest_JunctionTable WHERE QuestId = ?";
                    List<Quest>? queryResult = db.Query<Quest>(selectQuery, questId) ?? null;

                    return queryResult != null ? new ObservableCollection<Quest>(queryResult) : null;
                }
            }
            catch { return null; }
        }
        #endregion
    }
}
