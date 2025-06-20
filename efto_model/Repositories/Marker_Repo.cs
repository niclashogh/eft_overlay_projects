using efto_model.Models;
using System.Collections.ObjectModel;

namespace efto_model.Repositories
{
    public class Marker_Repo : Generic_Repo
    {
        private string tableName { get; } = "Marker";

        public async Task AddAsync(Marker model) => Add(model, this.tableName, UserDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, UserDB);
        public async Task<Marker> LoadSingleAsync(int id) => LoadSingle<Marker>(id, this.tableName, UserDB);
        public async Task<Marker> LoadLastAsync() => LoadLast<Marker>(this.tableName, UserDB);

        public async Task<ObservableCollection<Marker>> LoadByMapAsync(int id)
        {
            string propertyName = nameof(Marker.MapId);
            return LoadById<Marker>(id, propertyName, this.tableName, UserDB);
        }

        public async Task UpdateAsync(Marker model)
        {
            string[] propertyNames = new[] { nameof(Marker.Name), nameof(Marker.Desc), nameof(Marker.Icon) };
            Update(model, GetProperties(model, propertyNames), this.tableName, UserDB);
        }

        public async Task UpdateCoordinatesAsync(Marker model)
        {
            string[] propertyNames = new[] { nameof(Marker.X), nameof(Marker.Y) };
            Update(model, GetProperties(model, propertyNames), this.tableName, UserDB);
        }

        public async Task UpdateSizeAsync(Marker model)
        {
            string[] propertyNames = new[] { nameof(Marker.Width), nameof(Marker.Height) };
            Update(model, GetProperties(model, propertyNames), this.tableName, UserDB);
        }
    }
}
