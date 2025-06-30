using efto_model.Models.Quests;
using SQLite;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace efto_model.Repositories.Quests
{
    public class Quest_Reward_Repo : Generic_Repo
    {
        private string tableName { get; } = Quest_SQLContext.Reward_Table_Name;

        public async Task AddAsync(Quest_Reward model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<Quest_Reward> LoadSingleAsync(int id) => LoadSingleById<Quest_Reward>(id, this.tableName, EssentialDB);
        public async Task<Quest_Reward> LoadLastAsync() => LoadLastById<Quest_Reward>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<Quest_Reward>> FindAsync(string? searchWord, string category)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchWord))
                {
                    using (SQLiteConnection db = SQLConnection(EssentialDB))
                    {
                        List<Quest_Reward> queryResult;

                        if (category == "None")
                        {
                            string selectQuery = $"SELECT * FROM {tableName} WHERE Reward LIKE ?";
                            queryResult = db.Query<Quest_Reward>(selectQuery, $"%{searchWord}%") ?? new List<Quest_Reward>();
                        }
                        else
                        {
                            string selectQuery = $"SELECT * FROM {tableName} WHERE Reward LIKE ? AND Category = ?";
                            queryResult = db.Query<Quest_Reward>(selectQuery, $"%{searchWord}%", category) ?? new List<Quest_Reward>();
                        }

                        if (searchWord.Length == 1 && queryResult.Count > 0)
                        {
                            queryResult = queryResult.Where(filter => filter.Reward.StartsWith(searchWord, StringComparison.OrdinalIgnoreCase)).ToList();
                        }

                        return new ObservableCollection<Quest_Reward>(queryResult);
                    }
                }
                else return new ObservableCollection<Quest_Reward>();
            }
            catch(Exception e)
            {
                Debug.WriteLine("Quest_Reward.FindAsync: " + e);
                return new ObservableCollection<Quest_Reward>();
            }
        }

        public async Task<ObservableCollection<Quest_Reward>> LoadByQuestAsync(int id) => LoadById<Quest_Reward>(id, this.tableName, EssentialDB);

        public async Task DeleteAllByQuestAsync(int id) => DeleteAllByKey((id, nameof(Quest_Reward.QuestId)), this.tableName, EssentialDB);

        public async Task UpdateAsync(Quest_Reward model)
        {
            string[] propertyNames = new[] { nameof(Quest_Reward.Reward), nameof(Quest_Reward.Category), nameof(Quest_Reward.UnlockType) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
