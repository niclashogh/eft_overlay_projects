using efto_model.Data;
using efto_model.Models;
using efto_model.Models.Enums;
using SQLite;
using System.Collections.ObjectModel;

namespace efto_model.Repositories
{
    public class Quest_Task_Repo : DBContext
    {
        public async Task<bool> Add(Quest_Task model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string insertQuery = $"INSERT INTO Quest_Task (DP, Map, Trader, QuestId, Desc, Sequence, X, Y) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                    db.Execute(insertQuery, model.DP, model.Map, model.Trader, model.QuestId, model.Desc, model.Sequence, model.X, model.Y);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<ObservableCollection<Quest_Task>?> LoadFromQuest(int questId)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Quest_Task WHERE QuestId = ?";
                    List<Quest_Task> queryResult = db.Query<Quest_Task>(selectQuery, questId);

                    return queryResult != null ? new ObservableCollection<Quest_Task>(queryResult) : null;
                }
            }
            catch { return null; }
        }

        public async Task<Quest_Task?> LoadLast()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Quest_Task ORDER BY Id DESC LIMIT 1";
                    Quest_Task? item = db.Query<Quest_Task>(selectQuery).FirstOrDefault() ?? null;

                    return item != null ? item : null;
                }
            }
            catch { return null; }
        }

        public async Task<Quest_Task?> LoadSingle(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Quest_Task WHERE Id = ?";
                    Quest_Task? item = db.Query<Quest_Task>(selectQuery, id).FirstOrDefault() ?? null;

                    return item != null ? item : null;
                }
            }
            catch { return null; }
        }

        public async Task<ObservableCollection<Quest_Task>?> LoadActiveFromMap(Maps map)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = @$"SELECT qt.* FROM Quest_Task qt JOIN Quest q ON qt.QuestId = q.Id WHERE q.IsActive = 1 AND Map = ?";
                    List<Quest_Task>? queryResult = db.Query<Quest_Task>(selectQuery, map) ?? null;

                    return queryResult != null ? new ObservableCollection<Quest_Task>(queryResult) : null;
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
                    string deleteQuery = $"DELETE FROM Quest_Task WHERE Id = ?";
                    db.Execute(deleteQuery, id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> RemoveAllFromQuest(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string deleteQuery = $"DELETE FROM Quest_Task WHERE QuestId = ?";
                    db.Execute(deleteQuery, id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Update(Quest_Task model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE Quest_Task SET DP = ?, Map = ?, Trader = ?, Desc = ?, Sequence = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.DP, model.Map, model.Trader, model.Desc, model.Sequence, model.Id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateCoordiantes(Quest_Task model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE Quest_Task SET X = ?, Y = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.X, model.Y, model.Id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> IsAnyActive(Maps map)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = @$"SELECT qt.* FROM Quest_Task qt JOIN Quest q ON qt.QuestId = q.Id WHERE q.IsActive = 1 AND qt.Map = ? LIMIT 1";
                    List<Quest_Task> queryResult = db.Query<Quest_Task>(selectQuery, map);

                    return queryResult.Count > 0 ? true : false;
                }
            }
            catch { return false; }
        }
    }
}
