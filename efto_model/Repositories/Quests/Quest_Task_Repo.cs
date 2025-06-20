using efto_model.Models.Quests;
using SQLite;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Quests
{
    public class Quest_Task_Repo : Generic_Repo
    {
        private string tableName { get; } = "Quest_Task";

        public async Task AddAsync(Quest_Task model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, EssentialDB);
        public async Task<Quest_Task> LoadSingleAsync(int id) => LoadSingle<Quest_Task>(id, this.tableName, EssentialDB);
        public async Task<Quest_Task> LoadLastAsync() => LoadLast<Quest_Task>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<Quest_Task>> LoadByQuestAsync(int id)
        {
            string propertyName = nameof(Quest_Task.QuestId);
            return LoadById<Quest_Task>(id, propertyName, this.tableName, EssentialDB);
        }

        public async Task<ObservableCollection<Quest_Task>> LoadActiveByMapAsync(int id)
        {
            string propertyName = nameof(Quest_Task.MapId);
            return LoadById<Quest_Task>(id, propertyName, this.tableName, EssentialDB);
        }

        public async Task DeleteAllByQuestAsync(int id)
        {
            string propertyName = nameof(Quest_Task.QuestId);
            DeleteAllById(id, propertyName, this.tableName, EssentialDB);
        }

        public async Task UpdateAsync(Quest_Task model)
        {
            string[] propertyNames = new[] { nameof(Quest_Task.MapId), nameof(Quest_Task.TraderId), nameof(Quest_Task.Desc), nameof(Quest_Task.Sequence) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task UpdateCoordiantesAsync(Quest_Task model)
        {
            string[] propertyNames = new[] { nameof(Quest_Task.X), nameof(Quest_Task.Y) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
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
