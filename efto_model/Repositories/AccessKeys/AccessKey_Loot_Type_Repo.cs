using efto_model.Models.AccessKeys;

namespace efto_model.Repositories.AccessKeys
{
    public class AccessKey_Loot_Type_Repo : Generic_Repo
    {
        private string tableName { get; } = "AccessKey_Loot_Type";

        public async Task AddAsync(AccessKey_Loot_Type model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, EssentialDB);
        public async Task<AccessKey_Loot_Type> LoadSingleAsync(int id) => LoadSingle<AccessKey_Loot_Type>(id, this.tableName, EssentialDB);
        public async Task<AccessKey_Loot_Type> LoadLastAsync() => LoadLast<AccessKey_Loot_Type>(this.tableName, EssentialDB);

        public async Task Update(AccessKey_Loot_Type model)
        {
            string[] propertyNames = new[] { nameof(AccessKey_Loot_Type.Type) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
