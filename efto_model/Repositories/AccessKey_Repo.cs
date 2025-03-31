using efto_model.Data;
using efto_model.Models;
using efto_model.Models.Enums;
using SQLite;
using System.Collections.ObjectModel;

namespace efto_model.Repositories
{
    public class AccessKey_Repo : DBContext
    {
        public async Task<bool> Add(AccessKey model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string insertQuery = $"INSERT INTO AccessKey (Map, Name, X, Y, Show) VALUES (?, ?, ?, ?, ?)";
                    db.Execute(insertQuery, model.Map, model.Name, model.X, model.Y, model.Show);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<ObservableCollection<AccessKey>?> Find(string? searchWord)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchWord))
                {
                    List<AccessKey>? queryResult;

                    using (SQLiteConnection db = SQLConnection(EssentialDB))
                    {
                        string selectQuery = $"SELECT * FROM Quest WHERE Name LIKE ?";

                        if (searchWord.Length == 1)
                        {
                            queryResult = db.Query<AccessKey>(selectQuery, $"%{searchWord}%").Where(filter => filter.Name.StartsWith(searchWord, StringComparison.OrdinalIgnoreCase)).ToList();
                        }
                        else
                        {
                            queryResult = db.Query<AccessKey>(selectQuery, $"%{searchWord}%");
                        }
                    }

                    return queryResult != null ? new ObservableCollection<AccessKey>(queryResult) : null;
                }
                else return null;
            }
            catch { return null; }
        }

        public async Task<ObservableCollection<AccessKey>?> LoadFromMap(Maps map)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM AccessKey WHERE Map = ?";
                    List<AccessKey>? queryResult = db.Query<AccessKey>(selectQuery, map) ?? null;

                    return queryResult != null ? new ObservableCollection<AccessKey>(queryResult) : null;
                }
            }
            catch { return null; }
        }

        public async Task<ObservableCollection<AccessKey>?> LoadFromQuest(int questId)
        {
            return null; // Missing
        }

        public async Task<AccessKey?> LoadLast()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM AccessKey ORDER BY Id DESC LIMIT 1";
                    AccessKey? item = db.Query<AccessKey>(selectQuery).FirstOrDefault() ?? null;

                    return item != null ? item : null;
                }
            }
            catch { return null; }
        }

        public async Task<AccessKey?> LoadSingle(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM AccessKey WHERE Id = ?";
                    AccessKey? item = db.Query<AccessKey>(selectQuery, id).FirstOrDefault() ?? null;

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
                    string deleteQuery = $"DELETE FROM AccessKey WHERE Id = ?";
                    db.Execute(deleteQuery, id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Update(AccessKey model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE AccessKey SET Map = ?, Name = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.Map, model.Name, model.Id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateCoordinates(AccessKey model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE AccessKey SET X = ?, Y = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.X, model.Y, model.Id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> UpdateShow(AccessKey model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE AccessKey SET Show = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.Show, model.Id);
                }

                return true;
            }
            catch { return false; }
        }
    }
}
