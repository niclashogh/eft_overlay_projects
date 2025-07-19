using efto_model.Models.AccessKeys;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.AccessKeys
{
    public class AccessKey_Loot_Type_Repo : Generic_Repo
    {
        private string tableName { get; } = AccessKey_SQLContext.Type_Table_Name;

        public async Task AddAsync(AccessKey_Loot_Type model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<AccessKey_Loot_Type> LoadSingleAsync(int id) => LoadSingleById<AccessKey_Loot_Type>(id, this.tableName, EssentialDB);
        public async Task<AccessKey_Loot_Type> LoadLastAsync() => LoadLastById<AccessKey_Loot_Type>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<AccessKey_Loot_Type>> LoadAllAsync() => LoadAll<AccessKey_Loot_Type>(this.tableName, EssentialDB);

        public async Task UpdateAsync(AccessKey_Loot_Type model)
        {
            string[] propertyNames = new[] { nameof(AccessKey_Loot_Type.Type) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
