using efto_model.Models.Enums;
using Microsoft.UI.Xaml.Data;
using System;

namespace efto_window.Views.Converters
{
    public class TextReplaceUnderscore : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Maps || value is Traders)
            {
                string? text = value.ToString();

                if (text != null)
                {
                    return text.Replace('_', ' ');
                }
                else throw new Exception("[TextReplaceUnderscore] BindableValue is null or empty");
            }
            else throw new Exception("[TextReplaceUnderscore] BindableValue not regonized as Map or Trader");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
