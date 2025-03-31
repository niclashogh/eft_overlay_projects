using efto_model.Models.Enums;
using System.Collections.ObjectModel;

namespace efto_model.Models.DataTransferObjects
{
    public class Extraction_DTO : Extraction
    {
        private ObservableCollection<Extraction_Requirement>? requirements;
        public ObservableCollection<Extraction_Requirement>? Requirements
        {
            get { return requirements; }
            set
            {
                requirements = value;
                OnPropertyChanged(nameof(Requirements));
            }
        }

        public Extraction_DTO(Dragging_Privileges edp, Maps eMap, Extraction_Types eType, string name) : base(edp, eMap, eType, name) { }

        public Extraction_DTO(PositionRecord<double, double> pos) : base(pos) { }

        public Extraction_DTO() : base() { }
    }
}
