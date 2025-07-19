using efto_model.Models.Quests;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Quests
{
    public class Quest_Reward_Category_Repo : Generic_Repo
    {
        private string tableName { get; } = Quest_SQLContext.Reward_Category_Table_Name;

        public async Task AddAsync(Quest_Reward_Category model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<Quest_Reward_Category> LoadSingleAsync(int id) => LoadSingleById<Quest_Reward_Category>(id, this.tableName, EssentialDB);
        public async Task<Quest_Reward_Category> LoadLastAsync() => LoadLastById<Quest_Reward_Category>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<Quest_Reward_Category>> LoadAllAsync() => LoadAll<Quest_Reward_Category>(this.tableName, EssentialDB);

        public async Task UpdateAsync(Quest_Reward_Category model)
        {
            string[] propertyNames = new[] { nameof(Quest_Reward_Category.Category) };
            UpdateByKey(model, GetProperties(model, propertyNames), nameof(Quest_Reward_Category.Category), this.tableName, EssentialDB);
        }
    }
}
