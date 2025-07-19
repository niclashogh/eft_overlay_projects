using efto_model.Models.Extractions;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Extractions
{
    public class Extraction_Type_Repo : Generic_Repo
    {
        private string tableName { get; } = Extraction_SQLContext.Type_Table_Name;

        public async Task AddAsync(Extraction_Type model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<Extraction_Type> LoadSingleAsync(int id) => LoadSingleById<Extraction_Type>(id, this.tableName, EssentialDB);
        public async Task<Extraction_Type> LoadLastAsync() => LoadLastById<Extraction_Type>(this.tableName, EssentialDB);
        public async Task<ObservableCollection<Extraction_Type>> LoadAllAsync() => LoadAll<Extraction_Type>(this.tableName, EssentialDB);

        public async Task UpdateAsync(Extraction_Type model)
        {
            string[] propertyNames = new[] { nameof(Extraction_Type.Type) };
            UpdateByKey(model, GetProperties(model, propertyNames), nameof(Extraction_Type.Type), this.tableName, EssentialDB);
        }
    }
}
