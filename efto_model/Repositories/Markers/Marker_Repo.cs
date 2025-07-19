using efto_model.Models.Markers;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Markers
{
    public class Marker_Repo : Generic_Repo
    {
        private string tableName { get; } = Marker_SQLContext.Marker_Table_Name;

        public async Task AddAsync(Marker model) => Add(model, tableName, UserDB);
        public async Task DeleteAsync(int id) => DeleteById(id, tableName, UserDB);
        public async Task<Marker> LoadSingleAsync(int id) => LoadSingleById<Marker>(id, tableName, UserDB);
        public async Task<Marker> LoadLastAsync() => LoadLastById<Marker>(tableName, UserDB);

        public async Task<ObservableCollection<Marker>> LoadByMapAsync(string map) => LoadByKey<Marker>((map, nameof(Marker.MapName)), tableName, UserDB);

        public async Task UpdateAsync(Marker model)
        {
            string[] propertyNames = new[] { nameof(Marker.Name), nameof(Marker.Desc), nameof(Marker.Icon), nameof(Marker.ExpandableArea) };
            UpdateById(model, GetProperties(model, propertyNames), tableName, UserDB);
        }

        public async Task UpdateCoordinatesAsync(Marker model)
        {
            string[] propertyNames = new[] { nameof(Marker.X), nameof(Marker.Y) };
            UpdateById(model, GetProperties(model, propertyNames), tableName, UserDB);
        }

        public async Task UpdateSizeAsync(Marker model)
        {
            string[] propertyNames = new[] { nameof(Marker.Width), nameof(Marker.Height) };
            UpdateById(model, GetProperties(model, propertyNames), tableName, UserDB);
        }
    }
}
