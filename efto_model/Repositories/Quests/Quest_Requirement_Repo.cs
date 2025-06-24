using efto_model.Models.Quests;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Quests
{
    public class Quest_Requirement_Repo : Generic_Repo
    {
        private string tableName { get; } = "Quest_Requirement";

        public async Task AddAsync(Quest_Requirement model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<Quest_Requirement> LoadSingleAsync(int id) => LoadSingleById<Quest_Requirement>(id, this.tableName, EssentialDB);
        public async Task<Quest_Requirement> LoadLastAsync() => LoadLastById<Quest_Requirement>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<Quest_Requirement>> LoadByQuestAsync(int id) => LoadById<Quest_Requirement>(id, this.tableName, EssentialDB);

        public async Task DeleteAllByQuestAsync(int id) => DeleteAllByKey((id, nameof(Quest_Requirement.QuestId)), this.tableName, EssentialDB);

        public async Task UpdateAsync(Quest_Requirement model)
        {
            string[] propertyNames = new[] { nameof(Quest_Requirement.Requirement) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
