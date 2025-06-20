using efto_model.Models.AccessKeys;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.AccessKeys
{
    public class AccessKey_Loot_Repo : Generic_Repo
    {
        private string tableName { get; } = "AccessKey_Loot";

        public async Task AddAsync(AccessKey_Loot model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, EssentialDB);
        public async Task<AccessKey_Loot> LoadSingle(int id) => LoadSingle<AccessKey_Loot>(id, this.tableName, EssentialDB);
        public async Task<AccessKey_Loot> LoadLastAsync() => LoadLast<AccessKey_Loot>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<AccessKey_Loot>> LoadByAccessKeyAsync(int id)
        {
            string propertyName = nameof(AccessKey_Loot.AccessKeyId);
            return LoadById<AccessKey_Loot>(id, propertyName, this.tableName, EssentialDB);
        }

        public async Task DeleteAllByAccessKeyAsync(int id)
        {
            string propertyName = nameof(AccessKey_Loot.AccessKeyId);
            DeleteAllById(id, propertyName, this.tableName, EssentialDB);
        }

        public async Task Update(AccessKey_Loot model)
        {
            string[] propertyNames = new[] { nameof(AccessKey_Loot.TypeId), nameof(AccessKey_Loot.Quantity) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
