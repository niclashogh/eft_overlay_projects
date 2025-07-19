using efto_model.Records;
using efto_window.Views.Pages.Settings;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

namespace efto_window.ViewModels.Windows
{
    public class SettingVM : WindowVM
    {
        #region Variables & Properties
        public List<ViewRecord<Page>> Pages { get; }
        #endregion

        public SettingVM(nint windowHandle)
        {
            Pages = new List<ViewRecord<Page>>
            {
                new ViewRecord<Page>(new Setting_Home(), "Home"),
                new ViewRecord<Page>(new Setting_Map(windowHandle), "Maps"),
                new ViewRecord<Page>(new Setting_Trader(windowHandle), "Traders"),
                new ViewRecord<Page>(new Setting_Extraction(windowHandle), "Extractions"),
                new ViewRecord<Page>(new Setting_Marker(windowHandle), "Markers"),
                new ViewRecord<Page>(new Setting_BTR(windowHandle), "BTR"),

                new ViewRecord<Page>(new Setting_Quest(windowHandle), "Quests"),
                new ViewRecord<Page>(new Setting_Quest_Reward_Category(), "Quest Reward Categories"),

                new ViewRecord<Page>(new Setting_AccessKey(windowHandle), "Access Keys"),
                new ViewRecord<Page>(new Setting_AccessKey_Loot(), "Access Key Loot")
            };
        }
    }
}
