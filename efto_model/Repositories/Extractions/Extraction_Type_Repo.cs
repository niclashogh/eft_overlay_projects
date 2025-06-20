using efto_model.Models.Extractions;

namespace efto_model.Repositories.Extractions
{
    public class Extraction_Type_Repo : Generic_Repo
    {
        private string tableName { get; } = "Extraction_Type";

        public async Task AddAsync(Extraction_Type model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, EssentialDB);
        public async Task<Extraction_Type> LoadSingleAsync(int id) => LoadSingle<Extraction_Type>(id, this.tableName, EssentialDB);
        public async Task<Extraction_Type> LoadLastAsync() => LoadLast<Extraction_Type>(this.tableName, EssentialDB);

        public async Task UpdateAsync(Extraction_Type model)
        {
            string[] propertyNames = new[] { nameof(Extraction_Type.Desc) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
