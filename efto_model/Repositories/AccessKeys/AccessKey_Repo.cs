using efto_model.Models.AccessKeys;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.AccessKeys
{
    public class AccessKey_Repo : Generic_Repo
    {
        private string tableName { get; } = AccessKey_SQLContext.AccessKey_Table_Name;

        public async Task AddAsync(AccessKey model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<AccessKey> LoadSingleAsync(int id) => LoadSingleById<AccessKey>(id, this.tableName, EssentialDB);
        public async Task<AccessKey> LoadLastAsync() => LoadLastById<AccessKey>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<AccessKey>> FindAsync(string? searchWord)
        {
            if (!string.IsNullOrEmpty(searchWord))
            {
                string propertyName = nameof(AccessKey.Name);
                return FindBySearchWord<AccessKey>((searchWord, propertyName), this.tableName, EssentialDB);
            }
            else return new ObservableCollection<AccessKey>();
        }

        public async Task<ObservableCollection<AccessKey>> LoadByMapAsync(string map) => LoadByKey<AccessKey>((map, nameof(AccessKey.MapName)), this.tableName, EssentialDB);

        public async Task UpdateAsync(AccessKey model)
        {
            string[] propertyNames = new[] { nameof(AccessKey.MapName), nameof(AccessKey.Name) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task UpdateCoordinatesAsync(AccessKey model)
        {
            string[] propertyNames = new[] { nameof(AccessKey.X), nameof(AccessKey.Y) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task UpdateShowAsync(AccessKey model)
        {
            string[] propertyNames = new[] { nameof(AccessKey.Show) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
