using efto_menu.ViewModels;
using efto_model.Models.Enums;
using System.Windows;

namespace efto_menu.Views
{
    public partial class Menu : Window
    {
        private MenuVM viewModel { get; set; } = new();

        public Menu()
        {
            InitializeComponent();
            this.DataContext = this.viewModel;

            this.Height = SystemParameters.PrimaryScreenHeight;
            this.MaxWidth = 50;
            this.Left = 0;
            this.Top = 0;
        }

        private void MapTab_Click(object sender, RoutedEventArgs e) => this.viewModel.SendCom(InterProcessComs.Map);

        private void SearchTab_Click(object sender, RoutedEventArgs e) => this.viewModel.SendCom(InterProcessComs.Search);

        private void SettingTab_Click(object sender, RoutedEventArgs e) => this.viewModel.SendCom(InterProcessComs.Setting);

        private void BrowserTab_Click(object sender, RoutedEventArgs e) => this.viewModel.SendCom(InterProcessComs.Browser);

        private void CloseApp_Click(object sender, RoutedEventArgs e) => this.viewModel.SendCom(InterProcessComs.Close);
    }
}
