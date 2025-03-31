using efto_model.Data;
using efto_model.Models;
using efto_model.Models.Enums;
using SQLite;
using System.Collections.ObjectModel;

namespace efto_model.Repositories
{
    public class BTR_Repo : DBContext
    {
        public async Task<bool> Add(BTR model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string insertQuery = $"INSERT INTO BTR (DP, Map, X, Y) VALUES (?, ?, ?, ?)";
                    db.Execute(insertQuery, model.DP, model.Map, model.X, model.Y);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<ObservableCollection<BTR>?> LoadFromMap(Maps map)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM BTR WHERE Map = ?";
                    List<BTR>? queryResult = db.Query<BTR>(selectQuery, map) ?? null;

                    return queryResult != null ? new ObservableCollection<BTR>(queryResult) : null;
                }
            }
            catch { return null; }
        }

        public async Task<BTR?> LoadLast()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM BTR ORDER BY Id DESC LIMIT 1";
                    BTR? item = db.Query<BTR>(selectQuery).FirstOrDefault() ?? null;

                    return item != null ? item : null;
                }
            }
            catch { return null; }
        }

        public async Task<BTR?> LoadSingle(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM BTR WHERE Id = ?";
                    BTR? item = db.Query<BTR>(selectQuery, id).FirstOrDefault() ?? null;

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
                    string deleteQuery = $"DELETE FROM BTR WHERE Id = ?";
                    db.Execute(deleteQuery, id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Update(BTR model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE BTR SET DP = ?, Map = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.DP, model.Map, model.Id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateCoordinates(BTR model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE BTR SET X = ?, Y = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.X, model.Y, model.Id);
                }

                return true;
            }
            catch { return false; }
        }
    }
}
