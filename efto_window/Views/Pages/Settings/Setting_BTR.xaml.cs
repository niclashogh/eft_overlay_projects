using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using efto_window.ViewModels.Pages.Settings;
using efto_model.Models.Enums;
using efto_window.Services;

namespace efto_window.Views.Pages.Settings
{
    public sealed partial class Setting_BTR : Page
    {
        private Setting_BTRVM viewmodel { get; set; } = new();
        private nint windowHandle;

        public Setting_BTR(nint windowHandle)
        {
            this.InitializeComponent();
            this.windowHandle = windowHandle;
            this.SETTING_BTR_GRID.DataContext = this.viewmodel;
        }

        private async void Import_Click(object sender, RoutedEventArgs e)
        {
            await ImageService.PickImage(ImageFolders.BTR, this.windowHandle);
            _ = this.viewmodel.UpdateBTRDateAsync();
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e) => ImageService.OpenAssestsFolder(ImageFolders.BTR);
    }
}
