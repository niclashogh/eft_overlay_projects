using ABI.System;
using efto_model.Models.Base;
using efto_model.Models.Enums;
using efto_model.Repositories.Base;
using efto_window.Services;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efto_window.ViewModels.Pages.Settings
{
    public class Setting_HomeVM : PageVM
    {
        private Map_Repo mapRepo = new();

        #region [Map] Variables & Properties
        public ObservableCollection<Map> Maps { get; private set; }

        private Map selectedMap = new();
        public Map SelectedMap
        {
            get { return this.selectedMap; }
            set
            {
                this.selectedMap = value;
                OnPropertyChanged(nameof(this.SelectedMap));
                _ = UpdateMapDate();
            }
        }

        public string MapDateFeedback { get; set; }
        #endregion

        #region [POI] Variables & Properties
        public List<ImageFolders> POIs { get; } = new(Enum.GetValues<ImageFolders>().Where(poi => poi != ImageFolders.Maps));

        private ImageFolders selectedPOI = Enum.GetValues<ImageFolders>().Where(poi => poi != ImageFolders.Maps).FirstOrDefault();
        public ImageFolders SelectedPOI
        {
            get { return this.selectedPOI; }
            set
            {
                this.selectedPOI = value;
                OnPropertyChanged(nameof(this.SelectedPOI));
            }
        }
        #endregion

        public Setting_HomeVM()
        {
            _ = LoadMaps();
        }

        private async Task LoadMaps()
        {
            this.Maps = await mapRepo.LoadAllAsync();
            this.SelectedMap = this.Maps.FirstOrDefault();
        }

        private async Task UpdateMapDate()
        {
            this.MapDateFeedback = await ImageService.GetImageDateFeedback(ImageFolders.Maps, this.SelectedMap.Name);
            OnPropertyChanged(nameof(this.MapDateFeedback));
        }
    }
}
