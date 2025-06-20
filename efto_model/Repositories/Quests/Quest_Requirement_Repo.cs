using efto_model.Models.Quests;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Quests
{
    public class Quest_Requirement_Repo : Generic_Repo
    {
        private string tableName { get; } = "Quest_Requirement";

        public async Task AddAsync(Quest_Requirement model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, EssentialDB);
        public async Task<Quest_Requirement> LoadSingleAsync(int id) => LoadSingle<Quest_Requirement>(id, this.tableName, EssentialDB);
        public async Task<Quest_Requirement> LoadLastAsync() => LoadLast<Quest_Requirement>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<Quest_Requirement>> LoadByQuestAsync(int id)
        {
            string propertyName = nameof(Quest_Requirement.QuestId);
            return LoadById<Quest_Requirement>(id, propertyName, this.tableName, EssentialDB);
        }

        public async Task DeleteAllByQuestAsync(int id)
        {
            string propertyName = nameof(Quest_Requirement.QuestId);
            DeleteAllById(id, propertyName, this.tableName, EssentialDB);
        }

        public async Task Update(Quest_Requirement model)
        {
            string[] propertyNames = new[] { nameof(Quest_Requirement.Requirement) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
