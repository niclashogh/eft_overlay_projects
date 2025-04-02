using efto_model.Data;
using efto_model.Models;
using efto_model.Models.Enums;
using SQLite;
using System.Collections.ObjectModel;

namespace efto_model.Repositories
{
    public class Marker_Repo : DBContext
    {
        public async Task<bool> Add(Marker model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(UserDB))
                {
                    string insertQuery = $"INSERT INTO Marker (Name, Desc, DP, Map, Type, Color, Width, Height, X, Y) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    db.Execute(insertQuery, model.Name, model.Desc, model.DP, model.Map, model.Type, model.Color, model.Width, model.Height, model.X, model.Y);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<ObservableCollection<Marker>?> LoadFromMap(Maps map)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(UserDB))
                {
                    string selectQuery = $"SELECT * FROM Marker WHERE Map = ?";
                    List<Marker>? queryResult = db.Query<Marker>(selectQuery, map) ?? null;

                    return queryResult != null ? new ObservableCollection<Marker>(queryResult) : null;
                }
            }
            catch { return null; }

        }

        public async Task<Marker?> LoadLast()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(UserDB))
                {
                    string selectQuery = $"SELECT * FROM Marker ORDER BY Id DESC LIMIT 1";
                    Marker? item = db.Query<Marker>(selectQuery).FirstOrDefault() ?? null;

                    return item != null ? item : null;
                }
            }
            catch { return null; }
        }

        public async Task<Marker?> LoadSingle(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(UserDB))
                {
                    string selectQuery = $"SELECT * FROM Marker WHERE Id = ?";
                    Marker? item = db.Query<Marker>(selectQuery, id).FirstOrDefault() ?? null;

                    return item != null ? item : null;
                }
            }
            catch { return null; }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(UserDB))
                {
                    string deleteQuery = $"DELETE FROM Marker WHERE Id = ?";
                    db.Execute(deleteQuery, id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Update(Marker model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(UserDB))
                {
                    string updateQuery = $"UPDATE Marker SET Name = ?, Desc = ?, DP = ?, Map = ?, Type = ?, Color = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.Name, model.Desc, model.DP, model.Map, model.Type, model.Color, model.Id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateCoordinates(Marker model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(UserDB))
                {
                    string updateQuery = $"UPDATE Marker SET X = ?, Y = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.X, model.Y, model.Id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateSize(Marker model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(UserDB))
                {
                    string updateQuery = $"UPDATE Marker SET Width = ?, Height = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.Width, model.Height, model.Id);
                }

                return true;
            }
            catch { return false; }
        }
    }
}
