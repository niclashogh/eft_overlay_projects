using efto_model.Models.Quests;
using SQLite;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace efto_model.Repositories.Quests
{
    public class Quest_Repo : Generic_Repo
    {
        private string tableName { get; } = Quest_SQLContext.Quest_Table_Name;

        #region Standard
        public async Task AddAsync(Quest model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<T> LoadSingleAsync<T>(int id) where T : class, new() => LoadSingleById<T>(id, this.tableName, EssentialDB);
        public async Task<T> LoadLastAsync<T>() where T : class, new() => LoadLastById<T>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<Quest>> FindAsync(string? searchWord)
        {
            if (!string.IsNullOrEmpty(searchWord))
            {
                string propertyName = nameof(Quest.Name);
                return FindBySearchWord<Quest>((searchWord, propertyName), this.tableName, EssentialDB);
            }
            else return new ObservableCollection<Quest>();
        }

        public async Task<ObservableCollection<T>> LoadByTraderAsync<T>(string trader) where T : class, new() => LoadByKey<T>((trader, nameof(Quest.TraderName)), this.tableName, EssentialDB);

        public async Task UpdateAsync(Quest model)
        {
            string[] propertyNames = new[] { nameof(Quest.Name), nameof(Quest.Access), nameof(Quest.TraderName) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task UpdateCompletionAsync(Quest model)
        {
            string[] propertyNames = new[] { nameof(Quest.IsComplete) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task UpdateActiveAsync(Quest model)
        {
            string[] propertyNames = new[] { nameof(Quest.IsActive) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task ResetProgressionAsync()
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string updateQuery = $"UPDATE {this.tableName} SET IsComplete = 0 WHERE IsComplete = 1";
                    db.Execute(updateQuery);
                }
            }
            catch(Exception e) { Debug.WriteLine("Quest_Repo.ResetProgressionAsync: " + e); }
        }
        #endregion

        #region JunctionTables
        public async Task AddQuestToQuestDependency(int questId, int requiredQuestId)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string insertQuery = $"INSERT INTO Quest_ToQuest_JunctionTable (QuestId, RequiredQuestId) VALUES (?, ?)";
                    db.Execute(insertQuery, questId, requiredQuestId);
                }
            }
            catch { }
        }

        public async Task RemoveQuestToQuestDependency(int questId, int requiredQuestId)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string insertQuery = $"DELETE INTO Quest_ToQuest_JunctionTable WHERE QuestId = ? AND RequiredQuestId = ?";
                    db.Execute(insertQuery, questId, requiredQuestId);
                }
            }
            catch { }
        }

        public async Task<ObservableCollection<Quest>> LoadQuestDependencies(int questId)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = $"SELECT * FROM Quest_ToQuest_JunctionTable WHERE QuestId = ?";
                    List<Quest>? queryResult = db.Query<Quest>(selectQuery, questId) ?? new List<Quest>();

                    return new ObservableCollection<Quest>(queryResult);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("Quest_Repo.LoadQuestDependencies: " + e);
                return new ObservableCollection<Quest>();
            }
        }
        #endregion
    }
}
