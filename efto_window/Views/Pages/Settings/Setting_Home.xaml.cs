using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using efto_window.ViewModels.Pages.Settings;

namespace efto_window.Views.Pages.Settings
{
    public sealed partial class Setting_Home : Page
    {
        private Setting_HomeVM viewModel { get; set; } = new();

        public Setting_Home()
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
