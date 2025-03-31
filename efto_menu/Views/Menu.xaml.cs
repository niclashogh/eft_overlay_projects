using efto_menu.ViewModels;
using System.Windows;

namespace efto_menu.Views
{
    public partial class Menu : Window
    {
        private MenuVM ViewModel { get; set; } = new();

        public Menu()
        {
            InitializeComponent();
            this.DataContext = ViewModel;

            this.Height = SystemParameters.PrimaryScreenHeight;
            this.MaxWidth = 50;
            this.Left = 0;
            this.Top = 0;
        }

        private void MapTab_Click(object sender, RoutedEventArgs e)
        {
            // Open Window
        }

        private void SearchTab_Click(object sender, RoutedEventArgs e)
        {
            // Open Window
        }

        private void KeybindTab_Click(object sender, RoutedEventArgs e)
        {
            // Open Window
        }


        private void SettingTab_Click(object sender, RoutedEventArgs e)
        {
            // Open Window
        }

        private void BrowserTab_Click(object sender, RoutedEventArgs e)
        {
            // Open Window
        }
    }
}
