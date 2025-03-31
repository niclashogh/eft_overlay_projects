using Microsoft.UI.Xaml.Data;
using System;

namespace efto_window.Views.Converters
{
    public class WidthConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double width)
            {
                if (width != double.NaN)
                {
                    if (int.TryParse(parameter.ToString(), out int remove))
                    {
                        return width - remove;
                    }
                    else throw new Exception("[WidthConverter] ConverterParameter not regonized as an int");
                }
                else throw new Exception("[WidthConverter] Bindable Value is NaN");

            }
            else throw new Exception("[WidthConverter] Bindable Value not regonized as an double");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
