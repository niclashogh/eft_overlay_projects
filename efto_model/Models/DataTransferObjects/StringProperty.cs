using efto_model.Services;

namespace efto_model.Models.DataTransferObjects
{
    public class StringProperty : NotifyChangedService
    {
        public string DefaultValue { get; }

        private string property = string.Empty;
        public string Property
        {
            get { return this.property; }
            set
            {
                this.property = value;
                OnPropertyChanged(nameof(this.Property));
            }
        }

        public StringProperty(string defaultValue, string property)
        {
            this.DefaultValue = defaultValue;
            this.Property = property;
        }

        public StringProperty(string defaultValue)
        {
            this.DefaultValue = defaultValue;
            this.Property = defaultValue;
        }
    }
}
