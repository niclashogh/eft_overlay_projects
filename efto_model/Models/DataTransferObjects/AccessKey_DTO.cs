using efto_model.Models.Enums;
using System.Collections.ObjectModel;

namespace efto_model.Models.DataTransferObjects
{
    public class AccessKey_DTO : AccessKey
    {
        private ObservableCollection<AccessKey_Loot>? loot;
        public ObservableCollection<AccessKey_Loot>? Loot
        {
            get { return loot; }
            set
            {
                loot = value;
                OnPropertyChanged(nameof(Loot));
            }
        }

        public AccessKey_DTO(Maps map, string name) : base(map, name) { }

        public AccessKey_DTO(PositionRecord<double, double> pos) : base(pos) { }

        public AccessKey_DTO(bool show) : base(show) { }

        public AccessKey_DTO() { }
    }
}
