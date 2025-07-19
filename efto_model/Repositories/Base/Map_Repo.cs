using efto_model.Models.Base;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Base
{
    public class Map_Repo : Generic_Repo
    {
        private string tableName { get; } = Map_SQLContext.Map_Table_Name;

        public async Task AddAsync(Map model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<Map> LoadSingleAsync(int id) => LoadSingleById<Map>(id, this.tableName, EssentialDB);
        public async Task<Map> LoadLastAsync() => LoadLastById<Map>(this.tableName, EssentialDB);
        public async Task<ObservableCollection<Map>> LoadAllAsync() => LoadAll<Map>(this.tableName, EssentialDB);

        public async Task UpdateAsync(Map model)
        {
            string[] propertyNames = new[] { nameof(Map.Name), nameof(Map.UpdatedToVersion) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
