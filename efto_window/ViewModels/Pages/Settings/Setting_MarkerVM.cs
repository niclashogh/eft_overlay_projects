using efto_model.Models.Enums;
using efto_model.Models.Markers;
using efto_model.Repositories.Markers;
using efto_window.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace efto_window.ViewModels.Pages.Settings
{
    public class Setting_MarkerVM : PageVM
    {
        private Marker_Icon_Repo iconRepo = new();

        #region Variables & Properties
        private ObservableCollection<Marker_Icon> icons = new();
        public ObservableCollection<Marker_Icon> Icons
        {
            get { return this.icons; }
            private set
            {
                this.icons = value;
                OnPropertyChanged(nameof(this.Icons));
                OnPropertyChanged(nameof(this.AnyIcons));
            }
        }
        public bool AnyIcons
        {
            get { return this.Icons.Any(); }
        }

        private Marker_Icon selectedIcon = new();
        public Marker_Icon SelectedIcon
        {
            get {  return this.selectedIcon; }
            set
            {
                this.selectedIcon = value;
                OnPropertyChanged(nameof(this.SelectedIcon));
                _ = UpdateIconDateAsync();
            }
        }

        public string IconDateFeedback { get; set; } = string.Empty;

        public string NewMarkerIcon { get; set; } = string.Empty;
        #endregion

        public Setting_MarkerVM() => LoadIconsAsync();

        private async Task LoadIconsAsync()
        {
            this.Icons = await iconRepo.LoadAllAsync();

            if (this.Icons.Any())
            {
                this.SelectedIcon = this.Icons.FirstOrDefault();
            }
            else
            {
                this.SelectedIcon = new();
            }
        }

        private async Task UpdateIconDateAsync()
        {
            if (this.SelectedIcon != null)
            {
                this.IconDateFeedback = await ImageService.GetImageDateFeedback(ImageFolders.Markers, this.SelectedIcon.Icon);
                OnPropertyChanged(nameof(this.IconDateFeedback));
            }
        }

        internal async Task UpdateAsync()
        {
            if (this.SelectedIcon != null)
            {
                await this.iconRepo.UpdateAsync(this.SelectedIcon);

                Marker_Icon? old = this.Icons.FirstOrDefault(sorting => sorting.Id == this.SelectedIcon.Id);

                if (old != null)
                {
                    int index = this.Icons.IndexOf(old);

                    this.Icons[index] = await this.iconRepo.LoadSingleAsync(old.Id);
                    this.SelectedIcon = this.Icons[index];
                }
            }
        }

        internal async Task AddAsync()
        {
            if (!string.IsNullOrEmpty(this.NewMarkerIcon))
            {
                await iconRepo.AddAsync(new(this.NewMarkerIcon));

                this.NewMarkerIcon = string.Empty;
                OnPropertyChanged(nameof(this.NewMarkerIcon));

                this.Icons.Add(await iconRepo.LoadLastAsync());

                if (this.Icons.Count == 1)
                {
                    this.SelectedIcon = this.Icons.FirstOrDefault();
                    OnPropertyChanged(nameof(this.AnyIcons));
                }
            }
        }

        internal async Task RemoveAsync()
        {
            if (this.SelectedIcon != null)
            {
                await iconRepo.DeleteAsync(this.SelectedIcon.Id);
                _ = LoadIconsAsync();
            }
        }
    }
}
