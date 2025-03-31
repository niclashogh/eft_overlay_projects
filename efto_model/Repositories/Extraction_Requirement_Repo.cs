using efto_model.Data;
using efto_model.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace efto_model.Repositories
{
    public class Extraction_Requirement_Repo : DBContext
    {
        public async Task<bool> Add(Extraction_Requirement model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string insertQuery = $"INSERT INTO Extraction_Requirement (ExtractionId, Requirement) VALUES (?, ?, ?)";
                    db.Execute(insertQuery, model.ExtractionId, model.Requirement);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<ObservableCollection<Extraction_Requirement>?> LoadFromExtraction(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = "SELECT * FROM Extraction_Requirement WHERE ExtractionId = ?";
                    List<Extraction_Requirement>? queryResult = db.Query<Extraction_Requirement>(selectQuery, id);

                    return queryResult != null ? new ObservableCollection<Extraction_Requirement>(queryResult) : null;
                }
            }
            catch { return null; }
        }

        public async Task<Extraction_Requirement?> LoadLast()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Extraction_Requirement ORDER BY Id DESC LIMIT 1";
                    Extraction_Requirement? item = db.Query<Extraction_Requirement>(selectQuery).FirstOrDefault() ?? null;

                    return item != null ? item : null;
                }
            }
            catch { return null; }
        }

        public async Task<Extraction_Requirement?> LoadSingle(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Extraction_Requirement WHERE Id = ?";
                    Extraction_Requirement? item = db.Query<Extraction_Requirement>(selectQuery, id).FirstOrDefault() ?? null;

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
                    string deleteQuery = $"DELETE FROM Extraction_Requirement WHERE Id = ?";
                    db.Execute(deleteQuery, id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> RemoveAllFromExtraction(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string deleteQuery = $"DELETE FROM Extraction_Requirement WHERE ExtractionId = ?";
                    db.Execute(deleteQuery, id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Update(Extraction_Requirement model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE Extraction_Requirement SET Requirement = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.Requirement, model.Id);
                }

                return true;
            }
            catch { return false; }
        }
    }
}
