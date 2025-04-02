using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using efto_model.Models;
using efto_model.Models.DataTransferObjects;
using efto_model.Models.Enums;
using efto_model.Repositories;

namespace efto_window.ViewModels.Windows
{
    public class MapVM : WindowVM
    {
        private Extraction_Repo extractionRepository = new();
        private Extraction_Requirement_Repo extractionRequirementRepository = new();
        private Quest_Task_Repo questTaskRepository = new();
        private BTR_Repo btrRepository = new();
        private Marker_Repo markerRepository = new();

        #region [Extraction] Variables & Properties
        private ObservableCollection<Extraction_DTO>? extractions;
        public ObservableCollection<Extraction_DTO>? Extractions
        {
            get { return this.extractions; }
            private set
            {
                this.extractions = value;
                OnPropertyChanged(nameof(this.Extractions));
            }
        }
        #endregion

        #region [Quest, Task] Variables & Properties
        private ObservableCollection<Quest_Task>? tasks;
        public ObservableCollection<Quest_Task>? Tasks
        {
            get { return this.tasks; }
            private set
            {
                value = this.tasks;
                OnPropertyChanged(nameof(this.Tasks));
            }
        }
        #endregion

        #region [BTR] Variables & Properties
        private ObservableCollection<BTR>? btr;
        public ObservableCollection<BTR>? BTR
        {
            get { return this.btr; }
            private set
            {
                value = this.btr;
                OnPropertyChanged(nameof(this.BTR));
            }
        }
        #endregion

        #region [Marker] Variables & Properties
        private ObservableCollection<Marker>? markers;
        public ObservableCollection<Marker>? Markers
        {
            get { return this.markers; }
            private set
            {
                this.markers = value;
                OnPropertyChanged(nameof(this.Markers));
            }
        }
        #endregion

        public PaletteRecord Colors = new();

        public List<Maps> MapList { get; } = new(Enum.GetValues<Maps>());
        private Maps selectedMap = 0;
        public Maps SelectedMap
        {
            get { return this.selectedMap; }
            private set
            {
                this.selectedMap = value;
                OnPropertyChanged(nameof(this.SelectedMap));
                // Reload
            }
        }

        public MapVM() // Make references to kill the threads if they still are running and SelectedMap changes?
        {
            //_ = LoadExtractions();
            //_ = LoadQuestTasks();
            //_ = LoadBTR();
            //_ = LoadMarkers();
        }

        #region Extraction Controls
        internal async Task LoadExtractions()
        {
            this.Extractions = await this.extractionRepository.LoadFromMap<Extraction_DTO>(this.SelectedMap);

            if (this.Extractions != null && this.Extractions.Any())
            {
                IEnumerable<Task> enumerateTask = this.Extractions.Select(async extraction =>
                {
                    extraction.Requirements = await this.extractionRequirementRepository.LoadFromExtraction(extraction.Id);
                });

                await Task.WhenAll(enumerateTask);
            }
        }

        internal async Task UpdateExtraction(Extraction selected)
        {
            if (selected != null && selected.Id! <= 0 && this.Extractions != null)
            {
                await this.extractionRepository.UpdateCoordinates(selected);

                Extraction_DTO? old = this.Extractions.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = this.Extractions.IndexOf(old);

                    this.Extractions[index] = await this.extractionRepository.LoadSingle<Extraction_DTO>(old.Id);
                    OnPropertyChanged(nameof(this.Extractions));
                }
            }
        }
        #endregion

        #region QuestTask Controls
        internal async Task LoadQuestTasks() => this.Tasks = await this.questTaskRepository.LoadActiveFromMap(this.SelectedMap);

        internal async Task UpdateQuestTask(Quest_Task selected)
        {
            if (selected != null && selected.Id! <= 0 && this.Tasks != null)
            {
                await this.questTaskRepository.UpdateCoordiantes(selected);

                Quest_Task? old = this.Tasks.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = this.Tasks.IndexOf(old);

                    this.Tasks[index] = await this.questTaskRepository.LoadSingle(selected.Id);
                }
            }
        }
        #endregion

        #region BTR Controls
        internal async Task LoadBTR() => await this.btrRepository.LoadFromMap(this.SelectedMap);

        internal async Task UpdateBTR(BTR selected)
        {
            if (selected != null && selected.Id! <= 0)
            {
                await this.btrRepository.UpdateCoordinates(selected);

                BTR? old = this.BTR.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = this.BTR.IndexOf(old);

                    this.BTR[index] = await this.btrRepository.LoadSingle(old.Id);
                }
            }
        }
        #endregion

        #region Marker Controls
        internal async Task LoadMarkers() => await this.markerRepository.LoadFromMap(this.SelectedMap);

        internal async Task UpdateMarkerCoordinates(Marker selected)
        {
            if (selected != null && selected.Id! <= 0)
            {
                await this.markerRepository.UpdateCoordinates(selected);

                Marker? old = this.Markers.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = this.Markers.IndexOf(old);

                    this.Markers[index] = await this.markerRepository.LoadSingle(selected.Id);
                }
            }
        }

        internal async Task UpdateMarkerSize(Marker selected)
        {
            if (selected != null && selected.Id! <= 0)
            {
                await this.markerRepository.UpdateSize(selected);

                Marker? old = this.Markers.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = this.Markers.IndexOf(old);

                    this.Markers[index] = await this.markerRepository.LoadSingle(selected.Id);
                }
            }
        }
        #endregion
    }
}
