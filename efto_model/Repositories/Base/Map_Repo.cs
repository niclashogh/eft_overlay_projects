using efto_model.Models.Base;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Base
{
    public class Map_Repo : Generic_Repo
    {
        private string tableName { get; } = "Map";

        public async Task AddAsync(Map model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(string map) => DeleteByKey((map, nameof(Map.Name)), this.tableName, EssentialDB);
        public async Task<Map> LoadSingleAsync(string map) => LoadSingleByKey<Map>((map, nameof(Map.Name)), this.tableName, EssentialDB);
        public async Task<Map> LoadLastAsync() => LoadLastByKey<Map>(nameof(Map.Name), this.tableName, EssentialDB);
        public async Task<ObservableCollection<Map>> LoadAllAsync() => LoadAll<Map>(this.tableName, EssentialDB);

        public async Task UpdateAsync(Map model)
        {
            string[] propertyNames = new[] { nameof(Map.Name), nameof(Map.UpdatedToVersion) };
            UpdateByKey(model, GetProperties(model, propertyNames), nameof(Map.Name), this.tableName, EssentialDB);
        }
    }
}
