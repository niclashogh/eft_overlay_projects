using efto_model.Data;
using efto_model.Models;
using efto_model.Models.Enums;
using efto_model.Records;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI;

namespace efto_window.Views.ComponentBuilders
{
    public class MarkerComponent
    {
        public DimensionRecord<int> IconSize
        {
            get { return new(26, 26); }
        }

        public Grid GRID { get; set; }

        public MarkerComponent(Marker marker, Action<object, RangeBaseValueChangedEventArgs> callback)
        {
            this.GRID = CreateGrid(marker.Id);

            _ = CreateToolTipAsync(marker.Desc);

            switch (marker.ExpandableArea)
            {
                case Marker_Expandable_Areas.None:
                    _ = CreateIconAsync(marker.Icon); break;
                case Marker_Expandable_Areas.Ellipse:
                    _ = CreateEllipseAsync(marker.Width, marker.Height, marker.Icon); break;
                case Marker_Expandable_Areas.Rectangle:
                    _ = CreateRectangleAsync(marker.Width, marker.Height, marker.Icon); break;

                default:
                    _ = CreateIconAsync(marker.Icon); break;
            }

            this.GRID.ContextFlyout = CreateContextMenu(marker, callback);
        }

        #region Local methods
        private Grid CreateGrid(int id) => new Grid { Tag = id };

        private async Task CreateToolTipAsync(string desc)
        {
            ToolTip tooltip = new ToolTip
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 30, 30, 30)),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                FontSize = 16,
                FontWeight = FontWeights.SemiBold,
                Padding = new Thickness(8),
                Content = desc
            };

            ToolTipService.SetToolTip(this.GRID, tooltip);
        }

        private async Task CreateIconAsync(string icon)
        {
            string imagePath = Path.Combine(AssetContext.ApplicationFolder, ImageFolders.Markers.ToString(), icon);
            BitmapImage bitmap = new();

            using (FileStream stream = File.OpenRead(imagePath))
            {
                using (IRandomAccessStream rndAccessStream = stream.AsRandomAccessStream())
                {
                    await bitmap.SetSourceAsync(rndAccessStream);
                }
            }

            Image image = new Image
            {
                Source = bitmap,
                Width = this.IconSize.Width,
                Height = this.IconSize.Height,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            this.GRID.Children.Add(image);
        }

        private async Task CreateRectangleAsync(double width, double height, string icon)
        {
            Microsoft.UI.Xaml.Shapes.Rectangle rectangle = new Microsoft.UI.Xaml.Shapes.Rectangle
            {
                Width = width,
                Height = height,
                StrokeThickness = 4,
                Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                Fill = new SolidColorBrush(Color.FromArgb(10, 0, 0, 0)),
                Tag = "MARKER_ELEMENT"
            };

            this.GRID.Children.Add(rectangle);
            _ = CreateIconAsync(icon);
        }

        private async Task CreateEllipseAsync(double width, double height, string icon)
        {
            Microsoft.UI.Xaml.Shapes.Ellipse ellipse = new Microsoft.UI.Xaml.Shapes.Ellipse
            {
                Width = width,
                Height = height,
                StrokeThickness = 4,
                Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)),
                Fill = new SolidColorBrush(Color.FromArgb(10, 0, 0, 0)),
                Tag = "MARKER_ELEMENT"
            };

            this.GRID.Children.Add(ellipse);
            _ = CreateIconAsync(icon);
        }

        private Flyout CreateContextMenu(Marker marker, Action<object, RangeBaseValueChangedEventArgs> callback)
        {
            StackPanel widthPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    CreateSliderHeader("Width"),
                    CreateSlider(marker.Width, Size_Parameters.Width, callback)
                }
            };

            StackPanel heightPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    CreateSliderHeader("Height"),
                    CreateSlider(marker.Height, Size_Parameters.Height, callback)
                }
            };

            StackPanel content = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Children =
                {
                    widthPanel,
                    heightPanel
                }
            };

            return new Flyout
            {
                Content = content
            };
            
        }

        private Slider CreateSlider(double size, Size_Parameters sizeOrientation, Action<object, RangeBaseValueChangedEventArgs> callback)
        {
            Slider slider = new Slider
            {
                Minimum = 50,
                Maximum = 800,
                Width = 250,
                Height = 20,
                Value = size,
                Margin = new Thickness(10, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Tag = sizeOrientation == Size_Parameters.Width ? "MARKER_SLIDER_WIDTH" : "MARKER_SLIDER_HEIGHT"
            };

            slider.ValueChanged += (sender, e) => callback.Invoke(sender, e);

            return slider;
        }

        private TextBlock CreateSliderHeader(string header)
        {
            return new TextBlock
            {
                Text = header,
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14,
                FontWeight = FontWeights.SemiBold
            };
        }
        #endregion
    }
}
