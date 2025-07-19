using efto_model.Models.Enums;
using efto_model.Models.Extractions;
using efto_model.Repositories.Extractions;
using efto_window.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace efto_window.ViewModels.Pages.Settings
{
    public class Setting_ExtractionVM : PageVM
    {
        private Extraction_Type_Repo typeRepo = new();

        #region Variables & Properties
        private ObservableCollection<Extraction_Type> types = new();
        public ObservableCollection<Extraction_Type> Types
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

        private Extraction_Type selectedType = new();
        public Extraction_Type SelectedType
        {
            get { return this.selectedType; }
            set
            {
                this.selectedType = value;
                OnPropertyChanged(nameof(this.SelectedType));
                _ = UpdateTypeDateAsync();
            }
        }

        public string TypeDateFeedback { get; set; } = string.Empty;

        public string NewExtractionType { get; set; } = string.Empty;
        #endregion

        public Setting_ExtractionVM() => _ = LoadTypesAsync();

        private async Task LoadTypesAsync()
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

        private async Task UpdateTypeDateAsync()
        {
            if (this.SelectedType != null)
            {
                this.TypeDateFeedback = await ImageService.GetImageDateFeedback(ImageFolders.Extractions, this.SelectedType.Type);
                OnPropertyChanged(nameof(this.TypeDateFeedback));
            }
        }

        internal async Task UpdateAsync()
        {
            if (this.SelectedType != null)
            {
                await this.typeRepo.UpdateAsync(this.SelectedType);

                Extraction_Type? old = this.Types.FirstOrDefault(sorting => sorting.Id == this.SelectedType.Id);

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
            if (!string.IsNullOrEmpty(this.NewExtractionType))
            {
                await typeRepo.AddAsync(new(this.NewExtractionType));

                this.NewExtractionType = string.Empty;
                OnPropertyChanged(nameof(this.NewExtractionType));

                this.Types.Add(await typeRepo.LoadLastAsync());

                if (this.Types.Count == 1)
                {
                    this.SelectedType = this.Types.FirstOrDefault();
                    OnPropertyChanged(nameof(this.AnyTypes));
                }
            }
        }

        internal async Task RemoveAsync()
        {
            if (this.SelectedType != null)
            {
                await typeRepo.DeleteAsync(this.SelectedType.Id);
                _ = LoadTypesAsync();
            }
        }
    }
}
