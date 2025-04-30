using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using efto_window.ViewModels.Windows;
using efto_model.Models.Enums;
using efto.Services;
using WinRT.Interop;

namespace efto_window.Views.Windows
{
    public sealed partial class Setting_Window : Window
    {
        private SettingVM viewModel { get; set; } = new();
        private WindowController controller;

        public Setting_Window()
        {
            this.InitializeComponent();
            this.PARENT_GRID.DataContext = this.viewModel;

            this.controller = new(this.AppWindow, new(1550, 880), new(Horizontal_Alignments.Center, Vertical_Alignments.Center));
            this.controller.ConfigureTitleBar(this);
            this.controller.SetTopMost(WindowNative.GetWindowHandle(this));

            this.Closed += (sender, e) => this.controller.OnClose(InterProcessComs.Setting);
            this.controller.InitialNavigation(this.CONTENT_FRAME, this.viewModel.Pages.First().View);
        }

        private void Menu_Btn_Click(object sender, RoutedEventArgs e) => this.controller.Menu_Toggle(this.MENU_SPLITVIEW);

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e) => this.controller.Menu_SelectionChanged(sender, this.CONTENT_FRAME);
    }
}
