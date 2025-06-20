using efto_model.Models.Base;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;

namespace efto_window.Views.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter is string)
            {
                if (value is Map)
                {
                    string? imageName = value.ToString();
                    string? folderPath = parameter.ToString();

                    if (!string.IsNullOrEmpty(imageName) && !string.IsNullOrEmpty(folderPath))
                    {
                        return new BitmapImage(new Uri($"ms-appdata://{folderPath}{imageName}.png")); // Expecting "Local/Maps/image.png"
                    }
                    else throw new Exception("[ImageSourceConverter] BindableValue or ConverterParameter is null or empty");
                }
                else if (value is Trader)
                {
                    string? imageName = value.ToString();
                    string? folderPath = parameter.ToString();

                    if (!string.IsNullOrEmpty(imageName) && !string.IsNullOrEmpty(folderPath))
                    {
                        return $"{folderPath}{imageName}.png"; // Expecting "Assets/Traders/image.png"
                    }
                    else throw new Exception("[ImageSourceConverter] BindableValue or ConverterParameter is null or empty");
                }
                else throw new Exception("[ImageSourceConverter] BindableValue not regonized as Map or Trader");
            }
            else throw new Exception("[ImageSourceConverter] ConverterParameter not regonized as an string");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
