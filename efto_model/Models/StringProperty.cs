using efto_model.Services;

namespace efto_model.Models
{
    public class StringProperty : NotifyChangedService
    {
        public string DefaultValue { get; }

        private string property = string.Empty;
        public string Property
        {
            get { return property; }
            set
            {
                property = value;
                OnPropertyChanged(nameof(Property));
            }
        }

        public StringProperty(string defaultValue, string property)
        {
            DefaultValue = defaultValue;
            Property = property;
        }

        public StringProperty(string defaultValue)
        {
            DefaultValue = defaultValue;
            Property = defaultValue;
        }
    }
}
