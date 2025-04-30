using efto_model.Models;
using efto_model.Models.DataTransferObjects;
using efto_model.Models.Enums;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using Windows.Foundation;
using Windows.UI;

namespace efto_window.Views.ComponentBuilders
{
    public class MarkerComponent
    {
        public Grid GRID { get; set; }

        public MarkerComponent(Marker marker, Action<object, RangeBaseValueChangedEventArgs> callback)
        {
            this.GRID = CustomGrid(marker.Id);
            ToolTipService.SetToolTip(this.GRID, CustomToolTip(marker.Desc));
            this.GRID.ContextFlyout = CustomContextMenu(marker, callback);

            this.GRID.Children.Add(marker.Type switch
            {
                Marker_Types.Rectangle => CustomRectangle(marker),
                Marker_Types.Ellipse => CustomEllipse(marker),
                Marker_Types.Icon => CustomIcon(marker)
            });
        }

        private Grid CustomGrid(int id) => new Grid { Tag = id };

        private ToolTip CustomToolTip(string desc)
        {
            return new ToolTip
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 30, 30, 30)),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                FontSize = 16,
                FontWeight = FontWeights.SemiBold,
                Padding = new Thickness(8),
                Content = desc
            };
        }

        private Rectangle CustomRectangle(Marker marker)
        {
            (byte R, byte G, byte B) color = Color_DTO.GetFromEnum(marker.Color);

            return new Rectangle
            {
                Width = marker.Width,
                Height = marker.Height,
                StrokeThickness = 4,
                Stroke = new SolidColorBrush(Color.FromArgb(255, color.R, color.G, color.B)),
                Fill = new SolidColorBrush(Color.FromArgb(80, 0, 0, 0)),
                Tag = "MARKER_SHAPE"
            };
        }

        private Ellipse CustomEllipse(Marker marker)
        {
            (byte R, byte G, byte B) color = Color_DTO.GetFromEnum(marker.Color);

            return new Ellipse
            {
                Width = marker.Width,
                Height = marker.Height,
                StrokeThickness = 4,
                Stroke = new SolidColorBrush(Color.FromArgb(255, color.R, color.G, color.B)),
                Fill = new SolidColorBrush(Color.FromArgb(80, 0, 0, 0)),
                Tag = "MARKER_SHAPE"
            };
        }

        private Path CustomIcon(Marker marker)
        {
            PathFigure figure = new PathFigure
            {
                StartPoint = new Point(0, 0),
                Segments =
                {
                    new LineSegment // Top line
                    {
                        Point = new Point(marker.Width - 3, 0)
                    },

                    new LineSegment // Right line
                    {
                        Point = new Point(marker.Width - 3, marker.Height - 3)
                    },

                    new LineSegment // Bottom line
                    {
                        Point = new Point(0, marker.Height - 3)
                    }
                },
                IsClosed = true // Left line
            };

            PathGeometry geometry = new PathGeometry
            {
                Figures =
                {
                    figure
                }
            };

            (byte R, byte G, byte B) color = Color_DTO.GetFromEnum(marker.Color);

            return new Path
            {
                Stroke = new SolidColorBrush(Color.FromArgb(255, color.R, color.G, color.B)),
                StrokeThickness = 3,
                Fill = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0)),
                IsHitTestVisible = false,
                IsTabStop = false,
                Data = geometry,
                Tag = "MARKER_SHAPE"
            };
        }

        private Flyout CustomContextMenu(Marker marker, Action<object, RangeBaseValueChangedEventArgs> callback)
        {
            StackPanel widthPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    CustomSliderHeader("Width"),
                    CustomSlider(marker.Width, Size_Orientations.Width, callback)
                }
            };

            StackPanel heightPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    CustomSliderHeader("Height"),
                    CustomSlider(marker.Height, Size_Orientations.Height, callback)
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

        private Slider CustomSlider(double size, Size_Orientations sizeOrientation, Action<object, RangeBaseValueChangedEventArgs> callback)
        {
            Slider slider = new Slider
            {
                Minimum = 30,
                Maximum = 800,
                Width = 250,
                Height = 20,
                Value = size,
                Margin = new Thickness(10, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Tag = sizeOrientation == Size_Orientations.Width ? "MARKER_SLIDER_WIDTH" : "MARKER_SLIDER_HEIGHT"
            };

            slider.ValueChanged += (sender, e) => callback.Invoke(sender, e);

            return slider;
        }

        private TextBlock CustomSliderHeader(string header)
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
    }
}
