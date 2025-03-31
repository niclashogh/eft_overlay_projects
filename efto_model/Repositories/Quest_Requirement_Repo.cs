using efto_model.Data;
using efto_model.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace efto_model.Repositories
{
    public class Quest_Requirement_Repo : DBContext
    {
        public async Task<bool> Add(Quest_Requirement model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string insertQuery = $"INSERT INTO Quest_Requirement (QuestId, Requirement) VALUES (?, ?)";
                    db.Execute(insertQuery, model.QuestId, model.Requirement);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<ObservableCollection<Quest_Requirement>?> LoadFromQuest(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Quest_Requirement WHERE QuestId = ?";
                    List<Quest_Requirement>? queryResult = db.Query<Quest_Requirement>(selectQuery, id) ?? null;

                    return queryResult != null ? new ObservableCollection<Quest_Requirement>(queryResult) : null;
                }
            }
            catch { return null; }
        }

        public async Task<Quest_Requirement?> LoadLast()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Quest_Requirement ORDER BY Id DESC LIMIT 1";
                    Quest_Requirement? item = db.Query<Quest_Requirement>(selectQuery).FirstOrDefault() ?? null;

                    return item != null ? item : null;
                }
            }
            catch { return null; }
        }

        public async Task<Quest_Requirement?> LoadSingle(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Quest_Requirement WHERE Id = ?";
                    Quest_Requirement? item = db.Query<Quest_Requirement>(selectQuery, id).FirstOrDefault() ?? null;

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
                    string deleteQuery = $"DELETE FROM Quest_Requirement WHERE Id = ?";
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
                    string deleteQuery = $"DELETE FROM Quest_Requirement WHERE QuestId = ?";
                    db.Execute(deleteQuery, id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Update(Quest_Requirement model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE Quest_Requirement SET Requirement = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.Requirement, model.Id);
                }

                return true;
            }
            catch { return false; }
        }
    }
}
