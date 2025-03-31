using efto_model.Data;
using efto_model.Models;
using efto_model.Models.Enums;
using SQLite;
using System.Collections.ObjectModel;

namespace efto_model.Repositories
{
    public class Quest_Reward_Repo : DBContext
    {
        public async Task<bool> Add(Quest_Reward model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string insertQuery = $"INSERT INTO Quest_Reward (QuestId, Reward, Category, Type) VALUES (?, ?, ?, ?)";
                    db.Execute(insertQuery, model.QuestId, model.Reward, model.Category, model.Type);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<ObservableCollection<Quest_Reward>?> Find(string? searchWord, Quest_Reward_Categories category)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchWord))
                {
                    List<Quest_Reward>? queryResult = null;

                    using (SQLiteConnection db = SQLConnection(EssentialDB))
                    {
                        if (category == Quest_Reward_Categories.None)
                        {
                            string selectQuery = $"SELECT * FROM Quest_Reward WHERE Reward LIKE ?";

                            if (searchWord.Length == 1)
                            {
                                queryResult = db.Query<Quest_Reward>(selectQuery, $"%{searchWord}%").Where(filter => filter.Reward.StartsWith(searchWord, StringComparison.OrdinalIgnoreCase)).ToList();
                            }
                            else
                            {
                                queryResult = db.Query<Quest_Reward>(selectQuery, $"%{searchWord}%");
                            }
                        }
                        else
                        {
                            string selectQuery = $"SELECT * FROM Quest_Reward WHERE Reward LIKE ? AND Category = ?";

                            if (searchWord.Length == 1)
                            {
                                queryResult = db.Query<Quest_Reward>(selectQuery, $"%{searchWord}%", category).Where(filter => filter.Reward.StartsWith(searchWord, StringComparison.OrdinalIgnoreCase)).ToList();
                            }
                            else
                            {
                                queryResult = db.Query<Quest_Reward>(selectQuery, $"%{searchWord}%", category);
                            }
                        }
                    }

                    return queryResult != null ? new ObservableCollection<Quest_Reward>(queryResult) : null;
                }
                else return null;
            }
            catch { return null; }
        }

        public async Task<ObservableCollection<Quest_Reward>?> LoadFromQuest(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Quest_Reward WHERE QuestId = ?";
                    List<Quest_Reward>? queryResult = db.Query<Quest_Reward>(selectQuery, id) ?? null;

                    return queryResult != null ? new ObservableCollection<Quest_Reward>(queryResult) : null;
                }
            }
            catch { return null; }
        }

        public async Task<Quest_Reward?> LoadLast()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Quest_Reward ORDER BY Id DESC LIMIT 1";
                    Quest_Reward? item = db.Query<Quest_Reward>(selectQuery).FirstOrDefault() ?? null;

                    return item != null ? item : null;
                }
            }
            catch { return null; }
        }

        public async Task<Quest_Reward?> LoadSingle(int id)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT FROM Quest_Reward WHERE Id = ?";
                    Quest_Reward? item = db.Query<Quest_Reward>(selectQuery, id).FirstOrDefault() ?? null;

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
                    string deleteQuery = $"DELETE FROM Quest_Reward WHERE Id = ?";
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
                    string deleteQuery = $"DELETE FROM Quest_Reward WHERE QuestId = ?";
                    db.Execute(deleteQuery, id);
                }

                return true;
            }
            catch { return false; }
        }

        public async Task<bool> Update(Quest_Reward model)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE Quest_Reward SET Reward = ?, Category = ?, Type = ? WHERE Id = ?";
                    db.Execute(updateQuery, model.Reward, model.Category, model.Type, model.Id);
                }

                return true;
            }
            catch { return false; }
        }
    }
}
