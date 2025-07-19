using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using efto_window.ViewModels.Pages.Settings;

namespace efto_window.Views.Pages.Settings
{
    public sealed partial class Setting_AccessKey_Loot : Page
    {
        private Setting_AccessKey_LootVM viewmodel { get; set; } = new();

        public Setting_AccessKey_Loot()
        {
            this.InitializeComponent();
            this.SETTING_ACCESSKEY_LOOT_GRID.DataContext = this.viewmodel;
        }

        private void Update_Click(object sender, RoutedEventArgs e) => _ = this.viewmodel.UpdateAsync();

        private void Add_Click(object sender, RoutedEventArgs e) => _ = this.viewmodel.AddAsync();

        private void Remove_Click(object sender, RoutedEventArgs e) => _ = this.viewmodel.RemoveAsync();
    }
}
