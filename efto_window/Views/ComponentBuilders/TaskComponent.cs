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
    public class TaskComponent
    {
        #region Variables & Properties
        private int width { get; } = 26;
        private int height { get; } = 26;
        private int strokeThickness { get; } = 3;

        public DimensionRecord<int> Size
        {
            get { return new DimensionRecord<int>(this.width + this.strokeThickness, this.height + this.strokeThickness); }
        }
        #endregion

        public Grid GRID { get; set; }

        public TaskComponent(Quest_Task task, Color color)
        {
            this.GRID = CustomGrid(task.Id);
            ToolTipService.SetToolTip(this.GRID, CustomToolTip(task.Desc));

            this.GRID.Children.Add(CustomTextBlock(task.Sequence));
            this.GRID.Children.Add(CustomIcon(color));
        }

        private Grid CustomGrid(int id)
        {
            return new Grid
            {
                Background = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0)),
                Tag = id
            };
        }

        private ToolTip CustomToolTip(string desc)
        {
            return new ToolTip
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 30, 30, 30)),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                Padding = new Thickness(8),
                FontSize = 12,
                Content = desc
            };
        }

        private Path CustomIcon(Color color)
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
                Stroke = new SolidColorBrush(color),
                StrokeThickness = this.strokeThickness,
                IsHitTestVisible = false,
                IsTabStop = false,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(1.5, 1.5, 0, 0),
                Data = geometry
            };
        }

        private TextBlock CustomTextBlock(int sequence)
        {
            return new TextBlock
            {
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                FontWeight = FontWeights.SemiBold,
                Text = sequence.ToString()
            };
        }
    }
}
