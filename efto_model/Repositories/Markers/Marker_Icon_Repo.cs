using efto_model.Models.Markers;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Markers
{
    public class Marker_Icon_Repo : Generic_Repo
    {
        private string tableName { get; } = Marker_SQLContext.Icon_Table_Name;

        public async Task AddAsync(Marker_Icon model) => Add(model, tableName, UserDB);
        public async Task DeleteAsync(int id) => DeleteById(id, tableName, UserDB);
        public async Task<Marker_Icon> LoadSingleAsync(int id) => LoadSingleById<Marker_Icon>(id, tableName, UserDB);
        public async Task<Marker_Icon> LoadLastAsync() => LoadLastById<Marker_Icon>(tableName, UserDB);

        public async Task<ObservableCollection<Marker_Icon>> LoadAllAsync() => LoadAll<Marker_Icon>(tableName, UserDB);

        public async Task UpdateAsync(Marker_Icon model)
        {
            string[] propertyNames = new[] { nameof(Marker_Icon.Icon) };
            UpdateById(model, GetProperties(model, propertyNames), tableName, UserDB);
        }
    }
}
