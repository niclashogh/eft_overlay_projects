using efto_model.Models.Base;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Base
{
    public class Trader_Repo : Generic_Repo
    {
        private string tableName { get; } = "Trader";

        public async Task AddAsync(Trader model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, EssentialDB);
        public async Task<Trader> LoadSingleAsync(int id) => LoadSingle<Trader>(id, this.tableName, EssentialDB);
        public async Task<Trader> LoadLastAsync() => LoadLast<Trader>(this.tableName, EssentialDB);
        public async Task<ObservableCollection<Trader>> LoadAllAsync() => LoadAll<Trader>(this.tableName, EssentialDB);

        public async Task Update(Trader model)
        {
            string[] propertyNames = new[] { nameof(Trader.Name) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
