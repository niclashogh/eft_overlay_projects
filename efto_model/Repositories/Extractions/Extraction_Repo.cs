using efto_model.Models.Extractions;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Extractions
{
    public class Extraction_Repo : Generic_Repo
    {
        private string tableName { get; } = Extraction_SQLContext.Extraction_Table_Name;

        public async Task AddAsync(Extraction model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<T> LoadSingleAsync<T>(int id) where T : class, new() => LoadSingleById<T>(id, this.tableName, EssentialDB);
        public async Task<T> LoadLastAsync<T>() where T : class, new() => LoadLastById<T>(this.tableName, EssentialDB);


        public async Task<ObservableCollection<T>> LoadByMapAsync<T>(string mapName) where T : class, new() => LoadByKey<T>((mapName, nameof(Extraction.MapName)), this.tableName, EssentialDB);


        public async Task UpdateAsync(Extraction model)
        {
            string[] propertyNames = new[] { nameof(Extraction.Name), nameof(Extraction.Type), nameof(Extraction.MapName) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task UpdateCoordinatesAsync(Extraction model)
        {
            string[] propertyNames = new[] { nameof(Extraction.X), nameof(Extraction.Y) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
