using efto_model.Models.DataTransferObjects;
using efto_window.Views.Pages.Settings;
using efto_window.Views.Pages.Settings.AccessKey;
using efto_window.Views.Pages.Settings.BTR;
using efto_window.Views.Pages.Settings.Extraction;
using efto_window.Views.Pages.Settings.Marker;
using efto_window.Views.Pages.Settings.Quest;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace efto_window.ViewModels.Windows
{
    public class SettingVM : BaseVM
    {
        #region Variables & Properties
        public List<ViewRecord<Page>> Pages { get; } = new List<ViewRecord<Page>>
        {
            new ViewRecord<Page>(new Setting_Home(), "Home"),
            new ViewRecord<Page>(new Setting_Extraction_Preview(), "Extractions"),
            new ViewRecord<Page>(new Setting_Marker_Preview(), "Markers"),
            new ViewRecord<Page>(new Setting_BTR_Preview(), "BTR"),
            new ViewRecord<Page>(new Setting_Quest_Preview(), "Quests"),
            new ViewRecord<Page>(new Setting_AccessKey_Preview(), "Access Keys"),
        };

        public MetadataRecord MetaData { get; private set; } = new("MADE BY Grannice", "VERSION 0.9", "EFT 0.15");

        private bool disableNavigation = false;
        public bool DisableNavigation
        {
            get { return !this.disableNavigation; }
            set
            {
                this.disableNavigation = value;
                OnPropertyChanged(nameof(this.DisableNavigation));
            }
        }
        #endregion

        public SettingVM() { }
    }
}
