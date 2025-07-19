using efto_model.Models.Quests;
using System.Collections.ObjectModel;

namespace efto_model.Repositories.Quests
{
    public class Quest_Task_Icon_Repo : Generic_Repo
    {
        private string tableName { get; } = Quest_SQLContext.Task_Icon_Table_Name;

        public async Task AddAsync(Quest_Task_Icon model) => Add(model, this.tableName, EssentialDB);
        public async Task DeleteAsync(int id) => DeleteById(id, this.tableName, EssentialDB);
        public async Task<Quest_Task_Icon> LoadSingleAsync(int id) => LoadSingleById<Quest_Task_Icon>(id, this.tableName, EssentialDB);
        public async Task<Quest_Task_Icon> LoadLastAsync() => LoadLastById<Quest_Task_Icon>(this.tableName, EssentialDB);

        public async Task<ObservableCollection<Quest_Task_Icon>> LoadAllAsync() => LoadAll<Quest_Task_Icon>(this.tableName, EssentialDB);

        public async Task UpdateAsync(Quest_Task_Icon model)
        {
            string[] propertyNames = new[] { nameof(Quest_Task_Icon.Icon) };
            UpdateById(model, GetProperties(model, propertyNames), this.tableName, EssentialDB);
        }
    }
}
