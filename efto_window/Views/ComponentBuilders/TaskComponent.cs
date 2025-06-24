using efto_model.Data;
using efto_model.Models.Enums;
using efto_model.Models.Quests;
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
    public class TaskComponent
    {
        public DimensionRecord<int> Size
        {
            get { return new DimensionRecord<int>(26, 26); }
        }

        public Grid GRID { get; set; }

        public TaskComponent(Quest_Task task, Color color)
        {
            this.GRID = CreateGrid(task.Id);

            _ = CreateToolTipAsync(task.Desc);
            _ = CreateIconAsync(task.Sequence);
        }

        #region Local methods
        private Grid CreateGrid(int id)
        {
            return new Grid
            {
                Background = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0)),
                Tag = id
            };
        }

        private async Task CreateToolTipAsync(string desc)
        {
            ToolTip tooltip = new ToolTip
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 30, 30, 30)),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                Padding = new Thickness(8),
                FontSize = 12,
                Content = desc
            };

            ToolTipService.SetToolTip(this.GRID, tooltip);
        }

        private TextBlock CreateTextBlock(int sequence)
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

        private async Task CreateIconAsync(int sequence)
        {
            string imagePath = Path.Combine(AssetContext.ApplicationFolder, ImageFolders.BTR.ToString(), "BTR.png");
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

            this.GRID.Children.Add(CreateTextBlock(sequence));
            this.GRID.Children.Add(image);
        }
        #endregion
    }
}
