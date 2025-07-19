using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using efto_window.ViewModels.Pages.Settings;
using efto_model.Models.Enums;
using efto_window.Services;

namespace efto_window.Views.Pages.Settings
{
    public sealed partial class Setting_Extraction : Page
    {
        private Setting_ExtractionVM viewmodel { get; set; } = new();
        private nint windowHandle;

        public Setting_Extraction(nint windowHandle)
        {
            this.InitializeComponent();
            this.windowHandle = windowHandle;
            this.SETTING_EXTRACTION_GRID.DataContext = this.viewmodel;
        }

        private void Import_Click(object sender, RoutedEventArgs e) => ImageService.PickImage(ImageFolders.Extractions, this.windowHandle);

        private void OpenFolder_Click(object sender, RoutedEventArgs e) => ImageService.OpenAssestsFolder(ImageFolders.Extractions);

        private void Update_Click(object sender, RoutedEventArgs e) => _ = this.viewmodel.UpdateAsync();

        private void Add_Click(object sender, RoutedEventArgs e) => _ = this.viewmodel.AddAsync();

        private void Remove_Click(object sender, RoutedEventArgs e) => _ = this.viewmodel.RemoveAsync();
    }
}
