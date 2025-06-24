using efto_model.Models.Base;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Base
{
    public class BTR_Repo : Generic_Repo
    {
        private string tableName { get; } = "BTR";

        public async Task AddAsync(BTR model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<BTR> LoadSingleAsync(int id) => LoadSingleById<BTR>(id, this.tableName, EssentialDB);
        public async Task<BTR> LoadLastAsync() => LoadLastById<BTR>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<BTR>> LoadByMapAsync(string map) => LoadByKey<BTR>((map, nameof(BTR.MapName)), this.tableName, EssentialDB);

        public async Task UpdateAsync(BTR model)
        {
            string[] propertyNames = new[] { nameof(BTR.Location) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task UpdateCoordinates(BTR model)
        {
            string[] propertyNames = new[] { nameof(BTR.X), nameof(BTR.Y) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
