using efto_model.Data;
using efto_model.Models.Enums;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;

namespace efto_window.Views.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string image && !string.IsNullOrEmpty(image))
            {
                if (parameter is string folder && !string.IsNullOrEmpty(folder))
                {
                    if (folder == "Maps")
                    {
                        return new BitmapImage(new Uri($"{AssetContext.ApplicationFolder}/{ImageFolders.Maps.ToString()}/{image}.png"));
                    }
                    else if (folder == "Traders")
                    {
                        return new BitmapImage(new Uri($"{AssetContext.ApplicationFolder}/{ImageFolders.Traders.ToString()}/{image}.png"));
                    }
                    else return value;
                }
                else return value;
            }
            else return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
