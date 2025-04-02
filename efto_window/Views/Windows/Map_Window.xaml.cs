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
using efto_window.ViewModels.Windows;
using efto_model.Models.Enums;
using efto_model.Models.DataTransferObjects;
using efto.Services;
using WinRT.Interop;

namespace efto_window.Views.Windows
{
    public sealed partial class Map_Window : Window
    {
        private MapVM mapVM { get; set; } = new();
        private WindowController controller;

        public Map_Window()
        {
            this.InitializeComponent();
            this.MENU_SPLITVIEW.DataContext = this.mapVM;

            this.controller = new(1800, this.AppWindow);
            this.controller.ConfigureTitleBar(this);
            this.controller.SetTopMost(WindowNative.GetWindowHandle(this));

            this.Closed += (sender, e) => this.controller.OnClose(InterProcessComs.Map);
        }

        private void Menu_Btn_Click(object sender, RoutedEventArgs e) => this.controller.Menu_Toggle(this.MENU_SPLITVIEW);
    }
}
