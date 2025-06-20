using efto_model.Models.Base;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Base
{
    public class Map_Repo : Generic_Repo
    {
        private string tableName { get; } = "Map";

        public async Task AddAsync(Map model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, EssentialDB);
        public async Task<Map> LoadSingleAsync(int id) => LoadSingle<Map>(id, this.tableName, EssentialDB);
        public async Task<Map> LoadLastAsync() => LoadLast<Map>(this.tableName, EssentialDB);
        public async Task<ObservableCollection<Map>> LoadAllAsync() => LoadAll<Map>(this.tableName, EssentialDB);

        public async Task UpdateAsync(Map model)
        {
            string[] propertyNames = new[] { nameof(Map.Name), nameof(Map.UpdatedToVersion) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
