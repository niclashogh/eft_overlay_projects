using efto_model.Models.AccessKeys;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.AccessKeys
{
    public class AccessKey_Icon_Repo : Generic_Repo
    {
        private string tableName { get; } = AccessKey_SQLContext.Icon_Table_Name;

        public async Task AddAsync(AccessKey_Icon model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<AccessKey_Icon> LoadSingleAsync(int id) => LoadSingleById<AccessKey_Icon>(id, this.tableName, EssentialDB);
        public async Task<AccessKey_Icon> LoadLastAsync() => LoadLastById<AccessKey_Icon>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<AccessKey_Icon>> LoadAllAsync() => LoadAll<AccessKey_Icon>(this.tableName, EssentialDB);

        public async Task UpdateAsync(AccessKey_Icon model)
        {
            string[] propertyNames = new[] { nameof(AccessKey_Icon.Icon) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
