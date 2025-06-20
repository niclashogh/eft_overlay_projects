using efto_model.Models.AccessKeys;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.AccessKeys
{
    public class AccessKey_Repo : Generic_Repo
    {
        private string tableName { get; } = "AccessKey";

        public async Task AddAsync(AccessKey model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, EssentialDB);
        public async Task<AccessKey> LoadSingleAsync(int id) => LoadSingle<AccessKey>(id, this.tableName, EssentialDB);
        public async Task<AccessKey> LoadLastAsync() => LoadLast<AccessKey>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<AccessKey>> FindAsync(string? searchWord)
        {
            if (!string.IsNullOrEmpty(searchWord))
            {
                string propertyName = nameof(AccessKey.Name);
                return FindBySearchWord<AccessKey>(propertyName, searchWord, this.tableName, EssentialDB);
            }
            else return new ObservableCollection<AccessKey>();
        }

        public async Task<ObservableCollection<AccessKey>> LoadByMapAsync(int id)
        {
            string propertyName = nameof(AccessKey.MapId);
            return LoadById<AccessKey>(id, propertyName, this.tableName, EssentialDB);
        }

        public async Task UpdateAsync(AccessKey model)
        {
            string[] propertyNames = new[] { nameof(AccessKey.MapId), nameof(AccessKey.Name) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task UpdateCoordinatesAsync(AccessKey model)
        {
            string[] propertyNames = new[] { nameof(AccessKey.X), nameof(AccessKey.Y) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task UpdateShowAsync(AccessKey model)
        {
            string[] propertyNames = new[] { nameof(AccessKey.Show) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
