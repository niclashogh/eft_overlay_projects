using efto_model.Models.Base;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Base
{
    public class Trader_Repo : Generic_Repo
    {
        private string tableName { get; } = Trader_SQLContext.Trader_Table_Name;

        public async Task AddAsync(Trader model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<Trader> LoadSingleAsync(int id) => LoadSingleById<Trader>(id, this.tableName, EssentialDB);
        public async Task<Trader> LoadLastAsync() => LoadLastById<Trader>(this.tableName, EssentialDB);
        public async Task<ObservableCollection<Trader>> LoadAllAsync() => LoadAll<Trader>(this.tableName, EssentialDB);

        public async Task UpdateAsync(Trader model)
        {
            string[] propertyNames = new[] { nameof(Trader.Name) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
