using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace efto_window.Views.Converters
{
    public class BorderPaddingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string)
            {
                string? text = value.ToString();

                if (text == null || string.IsNullOrEmpty(text))
                {
                    return new Thickness(0);
                }
                else return new Thickness(5);
            }
            else throw new Exception("[BorderPaddingConverter] Bindable Value not regonized as an string");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
