using efto_model.Data;
using efto_model.Models;
using efto_model.Models.Enums;
using SQLite;
using System.Collections.ObjectModel;

namespace efto_model.Repositories
{
    public class Extraction_Repo : DBContext
    {
        public async Task<bool> Add(Extraction model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string insertQuery = $"INSERT INTO Extraction (DP, Map, Type, Name, X, Y) VALUES (?, ?, ?, ?, ?, ?)";
                    db.Execute(insertQuery, model.DP, model.Map, model.Type, model.Name, model.X, model.Y);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<ObservableCollection<T>?> LoadFromMap<T>(Maps map) where T : class, new()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Extraction WHERE Map = ?";
                    List<T>? queryResult = db.Query<T>(selectQuery, map) ?? null;

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
                    string selectQuery = $"SELECT * FROM Extraction ORDER BY Id DESC LIMIT 1";
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
                    string selectQuery = $"SELECT * FROM Extraction WHERE Id = ?";
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
                    string deleteQuery = $"DELETE FROM Extraction WHERE Id = ?";
                    db.Execute(deleteQuery, id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Update(Extraction model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE Extraction SET DP = ?, Map = ?, Type = ?, Name = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.DP, model.Map, model.Type, model.Name, model.Id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateCoordinates(Extraction model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE Extraction SET X = ?, Y = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.X, model.Y, model.Id);
                }

                return true;
            }
            catch { return false; }
        }
    }
}
