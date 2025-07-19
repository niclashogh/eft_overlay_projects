using efto_model.Models.Enums;
using efto_window.Services;
using System.Threading.Tasks;

namespace efto_window.ViewModels.Pages.Settings
{
    public class Setting_BTRVM : PageVM
    {
        public string BTRDateFeedback { get; set; } = string.Empty;

        public Setting_BTRVM() { }

        internal async Task UpdateBTRDateAsync()
        {
            this.BTRDateFeedback = await ImageService.GetImageDateFeedback(ImageFolders.BTR, "BTR");
            OnPropertyChanged(nameof(this.BTRDateFeedback));
        }
    }
}
