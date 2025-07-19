using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using efto_window.ViewModels.Pages.Settings;

namespace efto_window.Views.Pages.Settings
{
    public sealed partial class Setting_Quest_Reward_Category : Page
    {
        private Setting_Quest_Reward_CategoryVM viewmodel { get; set; } = new();

        public Setting_Quest_Reward_Category()
        {
            this.InitializeComponent();
            this.SETTING_REWARD_CATEGORY.DataContext = this.viewmodel;
        }

        private void Update_Click(object sender, RoutedEventArgs e) => _ = this.viewmodel.UpdateAsync();

        private void Add_Click(object sender, RoutedEventArgs e) => _ = this.viewmodel.AddAsync();

        private void Remove_Click(object sender, RoutedEventArgs e) => _ = this.viewmodel.RemoveAsync();
    }
}
