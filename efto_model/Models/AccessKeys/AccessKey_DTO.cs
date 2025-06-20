using efto_model.Records;
using System.Collections.ObjectModel;

namespace efto_model.Models.AccessKeys
{
    public class AccessKey_DTO : AccessKey
    {
        private ObservableCollection<AccessKey_Loot>? loot;
        public ObservableCollection<AccessKey_Loot>? Loot
        {
            get { return this.loot; }
            set
            {
                this.loot = value;
                OnPropertyChanged(nameof(Loot));
            }
        }

        public AccessKey_DTO(string name, int mapId) : base(name, mapId) { }

        public AccessKey_DTO(PositionRecord<double, double> pos) : base(pos) { }

        public AccessKey_DTO(bool show) : base(show) { }

        public AccessKey_DTO() { }
    }
}
