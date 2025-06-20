using efto_model.Models.Quests;

namespace efto_model.Repositories.Quests
{
    public class Quest_Reward_Category_Repo : Generic_Repo
    {
        private string tableName { get; } = "Quest_Reward_Category";

        public async Task AddAsync(Quest_Reward_Category model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, EssentialDB);
        public async Task<Quest_Reward_Category> LoadSingleAsync(int id) => LoadSingle<Quest_Reward_Category>(id, this.tableName, EssentialDB);
        public async Task<Quest_Reward_Category> LoadLastAsync() => LoadLast<Quest_Reward_Category>(this.tableName, EssentialDB);

        public async Task UpdateAsync(Quest_Reward_Category model)
        {
            string[] propertyNames = new[] { nameof(Quest_Reward_Category.Category) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
