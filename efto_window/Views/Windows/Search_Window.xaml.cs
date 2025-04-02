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
using efto.Services;

namespace efto_window.Views.Windows
{
    public sealed partial class Search_Window : Window
    {
        private SearchVM searchVM { get; set; } = new();
        private WindowController controller;

        public Search_Window()
        {
            this.InitializeComponent();

            this.controller = new(this.AppWindow);

            this.Closed += (sender, e) => this.controller.OnClose(InterProcessComs.Search);
        }
    }
}
