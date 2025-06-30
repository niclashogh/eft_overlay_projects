using efto_model.Models.Extractions;

namespace efto_model.Repositories.Extractions
{
    public class Extraction_Type_Repo : Generic_Repo
    {
        private string tableName { get; } = Extraction_SQLContext.Type_Table_Name;

        public async Task AddAsync(Extraction_Type model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(string type) => DeleteByKey((type, nameof(Extraction_Type.Type)), this.tableName, EssentialDB);
        public async Task<Extraction_Type> LoadSingleAsync(string type) => LoadSingleByKey<Extraction_Type>((type, nameof(Extraction_Type.Type)), this.tableName, EssentialDB);
        public async Task<Extraction_Type> LoadLastAsync() => LoadLastByKey<Extraction_Type>(nameof(Extraction_Type.Type), this.tableName, EssentialDB);

        public async Task UpdateAsync(Extraction_Type model)
        {
            string[] propertyNames = new[] { nameof(Extraction_Type.Type) };
            UpdateByKey(model, GetProperties(model, propertyNames), nameof(Extraction_Type.Type), this.tableName, EssentialDB);
        }
    }
}
