using efto_model.Models.Base;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Base
{
    public class Trader_Repo : Generic_Repo
    {
        private string tableName { get; } = Trader_SQLContext.Trader_Table_Name;

        public async Task AddAsync(Trader model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(string trader) => DeleteByKey((trader, nameof(Trader.Name)), this.tableName, EssentialDB);
        public async Task<Trader> LoadSingleAsync(string trader) => LoadSingleByKey<Trader>((trader, nameof(Trader.Name)), this.tableName, EssentialDB);
        public async Task<Trader> LoadLastAsync() => LoadLastByKey<Trader>(nameof(Trader.Name), this.tableName, EssentialDB);
        public async Task<ObservableCollection<Trader>> LoadAllAsync() => LoadAll<Trader>(this.tableName, EssentialDB);

        public async Task UpdateAsync(Trader model)
        {
            string[] propertyNames = new[] { nameof(Trader.Name) };
            UpdateByKey(model, GetProperties(model, propertyNames), nameof(Trader.Name), this.tableName, EssentialDB);
        }
    }
}
