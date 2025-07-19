using efto_model.Models.AccessKeys;
using efto_model.Repositories.AccessKeys;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace efto_window.ViewModels.Pages.Settings
{
    public class Setting_AccessKey_LootVM : PageVM
    {
        private AccessKey_Loot_Type_Repo typeRepo = new();

        #region Variables & Properties
        private ObservableCollection<AccessKey_Loot_Type> types = new();
        public ObservableCollection<AccessKey_Loot_Type> Types
        {
            get { return this.types; }
            private set
            {
                this.types = value;
                OnPropertyChanged(nameof(this.Types));
                OnPropertyChanged(nameof(this.AnyTypes));
            }
        }
        public bool AnyTypes
        {
            get { return this.Types.Any(); }
        }

        private AccessKey_Loot_Type selectedType = new();
        public AccessKey_Loot_Type SelectedType
        {
            get { return this.selectedType; }
            private set
            {
                this.selectedType = value;
                OnPropertyChanged(nameof(this.SelectedType));
            }
        }

        public string NewLootType { get; set; } = string.Empty;
        #endregion

        public Setting_AccessKey_LootVM() => _ = LoadTypeAsync();

        private async Task LoadTypeAsync()
        {
            this.Types = await typeRepo.LoadAllAsync();

            if (this.Types.Any())
            {
                this.SelectedType = this.Types.FirstOrDefault();
            }
            else
            {
                this.SelectedType = new();
            }
        }

        internal async Task UpdateAsync()
        {
            if (this.SelectedType != null)
            {
                await this.typeRepo.UpdateAsync(this.SelectedType);

                AccessKey_Loot_Type? old = this.Types.FirstOrDefault(sorting => sorting.Id == this.SelectedType.Id);

                if (old != null)
                {
                    int index = this.Types.IndexOf(old);

                    this.Types[index] = await this.typeRepo.LoadSingleAsync(old.Id);
                    this.SelectedType = this.Types[index];
                }
            }
        }

        internal async Task AddAsync()
        {
            if (!string.IsNullOrEmpty(this.NewLootType))
            {
                await typeRepo.AddAsync(new(this.NewLootType));

                this.NewLootType = string.Empty;
                OnPropertyChanged(nameof(this.NewLootType));

                this.Types.Add(await typeRepo.LoadLastAsync());

                if (this.Types.Count == 1)
                {
                    this.SelectedType = this.Types.FirstOrDefault();
                    OnPropertyChanged(nameof(this.SelectedType));
                }
            }
        }

        internal async Task RemoveAsync()
        {
            if (this.SelectedType != null)
            {
                await typeRepo.DeleteAsync(this.SelectedType.Id);
                _ = LoadTypeAsync();
            }
        }
    }
}
