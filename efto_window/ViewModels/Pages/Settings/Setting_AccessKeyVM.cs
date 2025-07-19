using efto_model.Models.AccessKeys;
using efto_model.Models.Enums;
using efto_model.Repositories.AccessKeys;
using efto_window.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace efto_window.ViewModels.Pages.Settings
{
    public class Setting_AccessKeyVM : PageVM
    {
        private AccessKey_Icon_Repo iconRepo = new();

        #region Variables & Properties
        private ObservableCollection<AccessKey_Icon> icons = new();
        public ObservableCollection<AccessKey_Icon> Icons
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

        private AccessKey_Icon selectedIcon = new();
        public AccessKey_Icon SelectedIcon
        {
            get { return this.selectedIcon; }
            set
            {
                this.selectedIcon = value;
                OnPropertyChanged(nameof(this.SelectedIcon));
                _ = UpdateIconDateAsync();
            }
        }

        public string IconDateFeedback { get; set; } = string.Empty;

        public string NewAccessKeyIcon { get; set; } = string.Empty;
        #endregion

        public Setting_AccessKeyVM() => _ = LoadIconsAsync();

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
                this.IconDateFeedback = await ImageService.GetImageDateFeedback(ImageFolders.AccessKeys, this.SelectedIcon.Icon);
                OnPropertyChanged(nameof(this.IconDateFeedback));
            }
        }

        internal async Task UpdateAsync()
        {
            if (this.SelectedIcon != null)
            {
                await this.iconRepo.UpdateAsync(this.SelectedIcon);

                AccessKey_Icon? old = this.Icons.FirstOrDefault(sorting => sorting.Id == this.SelectedIcon.Id);

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
            if (!string.IsNullOrEmpty(this.NewAccessKeyIcon))
            {
                await iconRepo.AddAsync(new(this.NewAccessKeyIcon));

                this.NewAccessKeyIcon = string.Empty;
                OnPropertyChanged(nameof(this.NewAccessKeyIcon));

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
