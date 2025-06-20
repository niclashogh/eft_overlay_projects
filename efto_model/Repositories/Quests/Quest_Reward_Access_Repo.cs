using efto_model.Models.Quests;

namespace efto_model.Repositories.Quests
{
    public class Quest_Reward_Access_Repo : Generic_Repo
    {
        private string tableName { get; } = "Quest_Reward_Access";

        public async Task AddAsync(Quest_Reward_Access model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, EssentialDB);
        public async Task<Quest_Reward_Access> LoadSingleAsync(int id) => LoadSingle<Quest_Reward_Access>(id, this.tableName, EssentialDB);
        public async Task<Quest_Reward_Access> LoadLastAsync() => LoadLast<Quest_Reward_Access>(this.tableName, EssentialDB);

        public async Task UpdateAsync(Quest_Reward_Access model)
        {
            string[] propertyNames = new[] { nameof(Quest_Reward_Access.Access) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
