using efto_model.Models.Quests;
using SQLite;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Quests
{
    public class Quest_Task_Repo : Generic_Repo
    {
        private string tableName { get; } = Quest_SQLContext.Task_Table_Name;

        public async Task AddAsync(Quest_Task model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<Quest_Task> LoadSingleAsync(int id) => LoadSingleById<Quest_Task>(id, this.tableName, EssentialDB);
        public async Task<Quest_Task> LoadLastAsync() => LoadLastById<Quest_Task>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<Quest_Task>> LoadByQuestAsync(int id) => LoadById<Quest_Task>(id, this.tableName, EssentialDB);

        public async Task<ObservableCollection<Quest_Task>> LoadActiveByMapAsync(string map) => LoadByKey<Quest_Task>((map, nameof(Quest_Task.MapName)), this.tableName, EssentialDB);

        public async Task DeleteAllByQuestAsync(int id) => DeleteAllByKey((id, nameof(Quest_Task.QuestId)), this.tableName, EssentialDB);

        public async Task UpdateAsync(Quest_Task model)
        {
            string[] propertyNames = new[] { nameof(Quest_Task.MapName), nameof(Quest_Task.TraderName), nameof(Quest_Task.Desc), nameof(Quest_Task.Sequence) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task UpdateCoordiantesAsync(Quest_Task model)
        {
            string[] propertyNames = new[] { nameof(Quest_Task.X), nameof(Quest_Task.Y) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task<int> GetActiveQuestsAsync(int mapId)
        {
            try
            {
                using (SQLiteConnection db = SQLConnection(EssentialDB))
                {
                    string selectQuery = @$"SELECT qt.* FROM Quest_Task qt JOIN Quest q ON qt.QuestId = q.Id WHERE q.IsActive = 1 AND qt.MapId = ?";
                    return db.Query<Quest_Task>(selectQuery, mapId).Count();
                }
            }
            catch { return 0; }
        }
    }
}
