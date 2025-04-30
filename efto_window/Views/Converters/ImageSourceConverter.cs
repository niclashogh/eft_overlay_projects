using efto_model.Models.Enums;
using Microsoft.UI.Xaml.Data;
using System;

namespace efto_window.Views.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Maps || value is Traders)
            {
                if (parameter is string)
                {
                    string? imageName = value.ToString();
                    string? folderPath = parameter.ToString();

                    if (!string.IsNullOrEmpty(imageName) && !string.IsNullOrEmpty(folderPath))
                    {
                        return $"{folderPath}{imageName}.png";
                    }
                    else throw new Exception("[ImageSourceConverter] BindableValue or ConverterParameter is null or empty");
                }
                else throw new Exception("[ImageSourceConverter] ConverterParameter not regonized as an string");
            }
            else throw new Exception("[ImageSourceConverter] BindableValue not regonized as Map or Trader");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
