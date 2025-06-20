using efto_model.Models.Extractions;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Extractions
{
    public class Extraction_Repo : Generic_Repo
    {
        private string tableName { get; } = "Extraction";

        public async Task AddAsync(Extraction model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, EssentialDB);
        public async Task<T> LoadSingleAsync<T>(int id) where T : class, new() => LoadSingle<T>(id, this.tableName, EssentialDB);
        public async Task<T> LoadLastAsync<T>() where T : class, new() => LoadLast<T>(this.tableName, EssentialDB);


        public async Task<ObservableCollection<T>> LoadByMapAsync<T>(int mapId) where T : class, new()
        {
            string propertyName = nameof(Extraction.MapId);
            return LoadById<T>(mapId, propertyName, this.tableName, EssentialDB);
        }


        public async Task UpdateAsync(Extraction model)
        {
            string[] propertyNames = new[] { nameof(Extraction.Name), nameof(Extraction.TypeId), nameof(Extraction.MapId) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task UpdateCoordinatesAsync(Extraction model)
        {
            string[] propertyNames = new[] { nameof(Extraction.X), nameof(Extraction.Y) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
