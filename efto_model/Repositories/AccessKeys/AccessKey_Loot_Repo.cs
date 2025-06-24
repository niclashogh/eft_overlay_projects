using efto_model.Models.AccessKeys;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.AccessKeys
{
    public class AccessKey_Loot_Repo : Generic_Repo
    {
        private string tableName { get; } = "AccessKey_Loot";

        public async Task AddAsync(AccessKey_Loot model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<AccessKey_Loot> LoadSingleAsync(int id) => LoadSingleById<AccessKey_Loot>(id, this.tableName, EssentialDB);
        public async Task<AccessKey_Loot> LoadLastAsync() => LoadLastById<AccessKey_Loot>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<AccessKey_Loot>> LoadByAccessKeyAsync(int id) => LoadById<AccessKey_Loot>(id, this.tableName, EssentialDB);

        public async Task DeleteAllByAccessKeyAsync(int id) => DeleteAllByKey((id, nameof(AccessKey_Loot.AccessKeyId)), this.tableName, EssentialDB);

        public async Task UpdateAsync(AccessKey_Loot model)
        {
            string[] propertyNames = new[] { nameof(AccessKey_Loot.Type), nameof(AccessKey_Loot.Quantity) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
