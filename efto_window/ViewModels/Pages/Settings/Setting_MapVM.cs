using efto_model.Models.Base;
using efto_model.Models.Enums;
using efto_model.Repositories.Base;
using efto_window.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace efto_window.ViewModels.Pages.Settings
{
    public class Setting_MapVM : PageVM
    {
        private Map_Repo mapRepo = new();

        #region Variables & Properties
        private ObservableCollection<Map> maps = new();
        public ObservableCollection<Map> Maps
        {
            get { return maps; }
            private set
            {
                this.maps = value;
                OnPropertyChanged(nameof(this.Maps));
                OnPropertyChanged(nameof(this.AnyMaps));
            }
        }
        public bool AnyMaps
        {
            get { return this.Maps.Any(); }
        }

        private Map selectedMap = new();
        public Map SelectedMap
        {
            get { return this.selectedMap; }
            set
            {
                this.selectedMap = value;
                OnPropertyChanged(nameof(this.SelectedMap));
                _ = UpdateMapDateAsync();
            }
        }

        public string MapDateFeedback { get; set; } = string.Empty;

        public string NewMapName { get; set; } = string.Empty;
        public string NewMapVersion { get; set; } = string.Empty;
        #endregion

        public Setting_MapVM() => _ = LoadMapsAsync();

        private async Task LoadMapsAsync()
        {
            this.Maps = await mapRepo.LoadAllAsync();

            if (this.Maps.Any())
            {
                this.SelectedMap = this.Maps.FirstOrDefault();
            }
            else
            {
                this.SelectedMap = new();
            }
        }

        private async Task UpdateMapDateAsync()
        {
            if (this.SelectedMap != null)
            {
                this.MapDateFeedback = await ImageService.GetImageDateFeedback(ImageFolders.Maps, this.SelectedMap.Name);
                OnPropertyChanged(nameof(this.MapDateFeedback));
            }
        }

        internal async Task UpdateAsync()
        {
            if (this.SelectedMap != null)
            {
                await this.mapRepo.UpdateAsync(this.SelectedMap);

                Map? old = this.Maps.FirstOrDefault(sorting => sorting.Id == this.SelectedMap.Id);

                if (old != null)
                {
                    int index = this.Maps.IndexOf(old);

                    this.Maps[index] = await this.mapRepo.LoadSingleAsync(old.Id);
                    this.SelectedMap = this.Maps[index];
                }
            }
        }

        internal async Task AddAsync()
        {
            if (!string.IsNullOrEmpty(this.NewMapName) && !string.IsNullOrEmpty(this.NewMapVersion))
            {
                await mapRepo.AddAsync(new(this.NewMapName, this.NewMapVersion));

                this.NewMapName = string.Empty;
                OnPropertyChanged(nameof(this.NewMapName));
                this.NewMapVersion = string.Empty;
                OnPropertyChanged(nameof(this.NewMapVersion));

                this.Maps.Add(await mapRepo.LoadLastAsync());

                if (this.Maps.Count == 1)
                {
                    this.SelectedMap = this.Maps.FirstOrDefault();
                    OnPropertyChanged(nameof(this.AnyMaps));
                }
            }
        }

        internal async Task RemoveAsync()
        {
            if (this.SelectedMap != null)
            {
                await mapRepo.DeleteAsync(this.SelectedMap.Id);
                _ = LoadMapsAsync();
            }
        }
    }
}
