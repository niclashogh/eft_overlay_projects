using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace efto_menu.View.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string && parameter is string)
            {
                string? imageName = value.ToString();
                string? folderPath = parameter.ToString();

                if (!string.IsNullOrEmpty(imageName) && !string.IsNullOrEmpty(folderPath))
                {
                    return new BitmapImage(new Uri($"{folderPath}/{imageName}.png", UriKind.Relative));
                }
                else throw new Exception("[ImageSourceConverter] Bindable Value or ConverterParameter is null or empty");
            }
            else throw new Exception("[ImageSourceConverter] Bindable Value or ConverterParameter not regonized as an string");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
