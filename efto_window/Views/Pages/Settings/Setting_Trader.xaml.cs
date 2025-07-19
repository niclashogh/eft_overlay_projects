using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using efto_window.ViewModels.Pages.Settings;
using efto_window.Services;
using efto_model.Models.Enums;

namespace efto_window.Views.Pages.Settings
{
    public sealed partial class Setting_Trader : Page
    {
        private Setting_TraderVM viewmodel { get; set; } = new();
        private nint windowHandle;

        public Setting_Trader(nint windowHandle)
        {
            this.InitializeComponent();
            this.windowHandle = windowHandle;
            this.SETTING_TRADER_GRID.DataContext = this.viewmodel;
        }

        private void Import_Click(object sender, RoutedEventArgs e) => ImageService.PickImage(ImageFolders.Traders, this.windowHandle);

        private void OpenFolder_Click(object sender, RoutedEventArgs e) => ImageService.OpenAssestsFolder(ImageFolders.Traders);

        private void Update_Click(object sender, RoutedEventArgs e) => _ = this.viewmodel.UpdateAsync();

        private void Add_Click(object sender, RoutedEventArgs e) => _ = this.viewmodel.AddAsync();

        private void Remove_Click(object sender, RoutedEventArgs e) => _ = this.viewmodel.RemoveAsync();
    }
}
