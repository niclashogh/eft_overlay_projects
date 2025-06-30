using efto_model.Models.Quests;

namespace efto_model.Repositories.Quests
{
    public class Quest_Reward_Category_Repo : Generic_Repo
    {
        private string tableName { get; } = Quest_SQLContext.Reward_Category_Table_Name;

        public async Task AddAsync(Quest_Reward_Category model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(string category) => DeleteByKey((category, nameof(Quest_Reward_Category.Category)), this.tableName, EssentialDB);
        public async Task<Quest_Reward_Category> LoadSingleAsync(string category) => LoadSingleByKey<Quest_Reward_Category>((category, nameof(Quest_Reward_Category.Category)), this.tableName, EssentialDB);
        public async Task<Quest_Reward_Category> LoadLastAsync() => LoadLastByKey<Quest_Reward_Category>(nameof(Quest_Reward_Category.Category), this.tableName, EssentialDB);

        public async Task UpdateAsync(Quest_Reward_Category model)
        {
            string[] propertyNames = new[] { nameof(Quest_Reward_Category.Category) };
            UpdateByKey(model, GetProperties(model, propertyNames), nameof(Quest_Reward_Category.Category), this.tableName, EssentialDB);
        }
    }
}
