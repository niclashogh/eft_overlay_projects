using efto_model.Models.Base;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Base
{
    public class BTR_Repo : Generic_Repo
    {
        private string tableName { get; } = "BTR";

        public async Task AddAsync(BTR model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, EssentialDB);
        public async Task<BTR> LoadSingleAsync(int id) => LoadSingle<BTR>(id, this.tableName, EssentialDB);
        public async Task<BTR> LoadLastAsync() => LoadLast<BTR>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<BTR>> LoadByMapAsync(int id)
        {
            string propertyName = nameof(BTR.MapId);
            return LoadById<BTR>(id, propertyName, this.tableName, EssentialDB);
        }

        public async Task UpdateAsync(BTR model)
        {
            string[] propertyNames = new[] { nameof(BTR.Location) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }

        public async Task UpdateCoordinates(BTR model)
        {
            string[] propertyNames = new[] { nameof(BTR.X), nameof(BTR.Y) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
