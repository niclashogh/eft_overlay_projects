using efto_model.Data;
using efto_model.Models.Enums;
using efto_model.Models.Extractions;
using efto_model.Records;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI;

namespace efto_window.Views.ComponentBuilders
{
    public class ExtractionComponent
    {
        public DimensionRecord<int> Size
        {
            get { return new DimensionRecord<int>(20, 30);}
        }

        public Grid GRID { get; private set; }

        public ExtractionComponent(Extraction_DTO extraction)
        {
            this.GRID = CreateGrid(extraction.Id);

            _ = CreateToolTipAsync(extraction);
            _ = CreateIconAsync(extraction.Type);
        }

        #region Local methods
        private Grid CreateGrid(int id)
        {
            return new Grid
            {
                Background = new SolidColorBrush(Color.FromArgb(1, 0, 0, 0)),
                Tag = id
            };
        }

        private async Task CreateToolTipAsync(Extraction_DTO extraction)
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

            ToolTip tooltip = new ToolTip
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 30, 30, 30)),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                Padding = new Thickness(8),
                Content = toolTipContent
            };

            ToolTipService.SetToolTip(this.GRID, tooltip);
        }

        private async Task CreateIconAsync(string type)
        {
            string imagePath = Path.Combine(AssetContext.ApplicationFolder, ImageFolders.Extractions.ToString(), type);
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
                Width = Size.Width,
                Height = Size.Height
            };

            this.GRID.Children.Add(image);
        }
        #endregion
    }
}
