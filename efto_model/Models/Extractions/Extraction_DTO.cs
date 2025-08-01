﻿using efto_model.Records;
using System.Collections.ObjectModel;

namespace efto_model.Models.Extractions
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

        public Extraction_DTO(string name, string mapName, string type) : base(name, mapName, type) { }

        public Extraction_DTO(PositionRecord<double, double> pos) : base(pos) { }

        public Extraction_DTO() : base() { }
    }
}
