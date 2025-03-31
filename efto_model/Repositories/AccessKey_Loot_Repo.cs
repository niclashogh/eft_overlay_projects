using efto_model.Data;
using efto_model.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace efto_model.Repositories
{
    public class AccessKey_Loot_Repo : DBContext
    {
        public async Task<bool> Add(AccessKey_Loot model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string insertQuery = $"INSERT INTO AccessKey_Loot (AccessKeyId, Type, Quantity) VALUES (?, ?, ?)";
                    db.Execute(insertQuery, model.AccessKeyId, model.Type, model.Quantity);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<ObservableCollection<AccessKey_Loot>?> LoadFromAccessKey(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuesy = $"SELECT * FROM AccessKey_Loot WHERE AccessKeyId = ?";
                    List<AccessKey_Loot>? queryResult = db.Query<AccessKey_Loot>(selectQuesy, id) ?? null;

                    return queryResult != null ? new ObservableCollection<AccessKey_Loot>(queryResult) : null;
                }
            }
            catch { return null; }
        }

        public async Task<AccessKey_Loot?> LoadLast()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM AccessKey_Loot ORDER BY Id DESC LIMIT 1";
                    AccessKey_Loot? item = db.Query<AccessKey_Loot>(selectQuery).FirstOrDefault() ?? null;

                    return item != null ? item : null;
                }
            }
            catch { return null; }
        }

        public async Task<AccessKey_Loot?> LoadSingle(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM AccessKey_Loot WHERE Id = ?";
                    AccessKey_Loot? item = db.Query<AccessKey_Loot>(selectQuery, id).FirstOrDefault() ?? null;

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
                    string deleteQuery = $"DELETE FROM AccessKey_Loot WHERE Id = ?";
                    db.Execute(deleteQuery, id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> RemoveAllFromAccessKey(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string deleteQuery = $"DELETE FROM AccessKey_Loot WHERE AccessKeyId = ?";
                    db.Execute(deleteQuery, id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Update(AccessKey_Loot model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE AccessKey_Loot SET Type = ?, Quantity = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.Type, model.Quantity, model.Id);
                }

                return true;
            }
            catch { return false; }
        }
    }
}
