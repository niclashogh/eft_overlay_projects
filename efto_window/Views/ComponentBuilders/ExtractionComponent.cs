using efto_model.Models.Extractions;
using efto_model.Records;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;
using Windows.UI;

namespace efto_window.Views.ComponentBuilders
{
    public class ExtractionComponent
    {
        #region Variables & Properties
        private int width { get; } = 20;
        private int height { get; } = 30;
        private int strokeThickness { get; } = 3;

        public DimensionRecord<int> Size
        {
            get { return new DimensionRecord<int>(this.width + this.strokeThickness, this.height + this.strokeThickness);}
        }
        #endregion

        public Grid GRID { get; private set; }

        public ExtractionComponent(Extraction_DTO extraction)
        {
            this.GRID = CustomGrid(extraction.Id);
            ToolTipService.SetToolTip(this.GRID, CustomToolTip(extraction));

            this.GRID.Children.Add(CustomIcon(extraction.Type));
        }

        private Grid CustomGrid(int id)
        {
            return new Grid
            {
                Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0)),
                Tag = id
            };
        }

        private ToolTip CustomToolTip(Extraction_DTO extraction)
        {
            TextBlock header = new TextBlock
            {
                Text = extraction.Name,
                FontSize = 18,
                FontWeight = FontWeights.SemiBold
            };

            StackPanel toolTipContent = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Children =
                {
                    header
                }
            };

            if (extraction.Requirements != null && extraction.Requirements.Count > 0)
            {
                foreach (Extraction_Requirement requirement in extraction.Requirements)
                {
                    toolTipContent.Children.Add(new TextBlock
                    {
                        Text = $"{requirement.Requirement}",
                        FontSize = 12
                    });
                }
            }

            return new ToolTip
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 30, 30, 30)),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                Padding = new Thickness(8),
                Content = toolTipContent
            };
        }

        private Path CustomIcon(Extraction_Types type)
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
                        Point = new Point(this.width, 20)
                    },

                    new LineSegment // Bottom right, angled line
                    {
                        Point = new Point(10, this.height)
                    },

                    new LineSegment // Bottom left, angled line
                    {
                        Point = new Point(0, 20)
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

            (byte R, byte G, byte B) color = Color_DTO.GetFromEnum(type);

            return new Path
            {
                Stroke = new SolidColorBrush(Color.FromArgb(255, color.R, color.G, color.B)),
                StrokeThickness = this.strokeThickness,
                Fill = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0)),
                IsHitTestVisible = false,
                IsTabStop = false,
                Data = geometry
            };
        }
    }
}
