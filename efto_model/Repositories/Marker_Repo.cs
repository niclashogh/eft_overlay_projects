using efto_model.Models;
using System.Collections.ObjectModel;

namespace efto_model.Repositories
{
    public class Marker_Repo : Generic_Repo
    {
        private string tableName { get; } = "Marker";

        public async Task AddAsync(Marker model) => Add(model, this.tableName, UserDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, UserDB);
        public async Task<Marker> LoadSingleAsync(int id) => LoadSingleById<Marker>(id, this.tableName, UserDB);
        public async Task<Marker> LoadLastAsync() => LoadLastById<Marker>(this.tableName, UserDB);

        public async Task<ObservableCollection<Marker>> LoadByMapAsync(string map) => LoadByKey<Marker>((map, nameof(Marker.MapName)), this.tableName, UserDB);

        public async Task UpdateAsync(Marker model)
        {
            string[] propertyNames = new[] { nameof(Marker.Name), nameof(Marker.Desc), nameof(Marker.Icon) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, UserDB);
        }

        public async Task UpdateCoordinatesAsync(Marker model)
        {
            string[] propertyNames = new[] { nameof(Marker.X), nameof(Marker.Y) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, UserDB);
        }

        public async Task UpdateSizeAsync(Marker model)
        {
            string[] propertyNames = new[] { nameof(Marker.Width), nameof(Marker.Height) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, UserDB);
        }
    }
}
