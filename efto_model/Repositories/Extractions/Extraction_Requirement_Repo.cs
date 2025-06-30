using efto_model.Models.Extractions;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Extractions
{
    public class Extraction_Requirement_Repo : Generic_Repo
    {
        private string tableName { get; } = Extraction_SQLContext.Requirement_Table_Name;

        public async Task AddAsync(Extraction_Requirement model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<Extraction_Requirement> LoadSingleAsync(int id) => LoadSingleById<Extraction_Requirement>(id, this.tableName, EssentialDB);
        public async Task<Extraction_Requirement> LoadLastAsync() => LoadLastById<Extraction_Requirement>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<Extraction_Requirement>> LoadByExtractionAsync(int id) => LoadById<Extraction_Requirement>(id, this.tableName, EssentialDB);

        public async Task DeleteAllByExtractionAsync(int id) => DeleteAllByKey((id, nameof(Extraction_Requirement.ExtractionId)), this.tableName, EssentialDB);

        public async Task UpdateAsync(Extraction_Requirement model)
        {
            string[] propertyNames = new[] { nameof(Extraction_Requirement.Requirement) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
