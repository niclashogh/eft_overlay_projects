using efto_model.Models;
using efto_model.Models.DataTransferObjects;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace efto_window.Views.ComponentBuilders
{
    public class BTRComponent
    {
        #region Variables & Properties
        private int width { get; } = 20;
        private int height { get; } = 20;
        private int strokeThickness { get; } = 3;

        public DimensionRecord<int> Size
        {
            get { return new DimensionRecord<int>(this.width + this.strokeThickness, this.height + this.strokeThickness); }
        }
        #endregion

        public Grid GRID { get; set; }

        public BTRComponent(BTR btr)
        {
            this.GRID = CustomGrid(btr.Id);
            ToolTipService.SetToolTip(this.GRID, CustomToolTip(btr.Location));

            this.GRID.Children.Add(CustomIcon());
        }

        private Grid CustomGrid(int id)
        {
            return new Grid
            {
                Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0)),
                Tag = id
            };
        }

        private ToolTip CustomToolTip(string location)
        {
            return new ToolTip
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 30, 30, 30)),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                Padding = new Thickness(8),
                FontSize = 16,
                FontWeight = FontWeights.SemiBold,
                Content = location
            };
        }

        private Path CustomIcon()
        {
            PathFigure figure = new PathFigure
            {
                StartPoint = new Point(0, 0),
                Segments =
                {
                    new LineSegment // Top line
                    {
                        Point = new Point(this.width, 0)
                    },

                    new LineSegment // Right line
                    {
                        Point = new Point(this.width, this.height)
                    },

                    new LineSegment // Bottom line
                    {
                        Point = new Point(0, this.height)
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

            return new Path
            {
                Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                StrokeThickness = this.strokeThickness,
                Fill = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0)),
                IsHitTestVisible = false,
                IsTabStop = false,
                Data = geometry
            };
        }
    }
}
