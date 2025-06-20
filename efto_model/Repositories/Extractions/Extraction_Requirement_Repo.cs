using efto_model.Models.Extractions;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Extractions
{
    public class Extraction_Requirement_Repo : Generic_Repo
    {
        private string tableName { get; } = "Extraction_Requirement";

        public async Task AddAsync(Extraction_Requirement model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => Delete(id, this.tableName, EssentialDB);
        public async Task<Extraction_Requirement> LoadSingleAsync(int id) => LoadSingle<Extraction_Requirement>(id, this.tableName, EssentialDB);
        public async Task<Extraction_Requirement> LoadLastAsync() => LoadLast<Extraction_Requirement>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<Extraction_Requirement>> LoadByExtractionAsync(int id)
        {
            string propertyName = nameof(Extraction_Requirement.ExtractionId);
            return LoadById<Extraction_Requirement>(id, propertyName, this.tableName, EssentialDB);
        }

        public async Task DeleteAllByExtractionAsync(int id)
        {
            string propertyName = nameof(Extraction_Requirement.ExtractionId);
            DeleteAllById(id, propertyName, this.tableName, EssentialDB);
        }

        public async Task UpdateAsync(Extraction_Requirement model)
        {
            string[] propertyNames = new[] { nameof(Extraction_Requirement.Requirement) };
            Update(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
