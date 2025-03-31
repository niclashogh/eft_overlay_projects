using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using efto_window.ViewModels.Windows;
using efto.Services;
using WinRT.Interop;
using efto_model.Models.Enums;
using efto_model.Models.DataTransferObjects;

namespace efto_window.Views.Windows
{
    public sealed partial class Setting_Window : Window
    {
        private SettingVM viewModel { get; set; } = new();

        public Setting_Window()
        {
            this.InitializeComponent();
            this.ParentGrid.DataContext = viewModel;

            ConfigureWindowService CWS = new(new(1520, 880), new(100, Vertical_Alignments.Center), this.AppWindow);
            CWS.ConfigureTitleBar(this);
            CWS.SetTopMost(WindowNative.GetWindowHandle(this));

            this.ContentFrame.Navigate(this.viewModel.Pages.First().View.GetType());
        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e) => this.MenuSplitView.IsPaneOpen = !this.MenuSplitView.IsPaneOpen;

        private void SplitViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox list)
            {
                if (list != null)
                {
                    ViewRecord<Page> page = (ViewRecord<Page>)list.SelectedItem;
                    this.ContentFrame.Navigate(page.View.GetType());
                }
            }
        }
    }
}
