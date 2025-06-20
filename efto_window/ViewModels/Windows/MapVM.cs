using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using efto_model.Models;
using efto_model.Models.Base;
using efto_model.Models.Extractions;
using efto_model.Models.Quests;
using efto_model.Repositories;
using efto_model.Repositories.Base;
using efto_model.Repositories.Extractions;
using efto_model.Repositories.Quests;

namespace efto_window.ViewModels.Windows
{
    public class MapVM : WindowVM
    {
        private Map_Repo mapReposiroty = new();
        private Extraction_Repo extractionRepository = new();
        private Extraction_Requirement_Repo extractionRequirementRepository = new();
        private Quest_Task_Repo questTaskRepository = new();
        private BTR_Repo btrRepository = new();
        private Marker_Repo markerRepository = new();

        #region [Extraction] Variables & Properties
        public bool FinishedLoadingExtractions { get; private set; } = false;

        private ObservableCollection<Extraction_DTO> extractions;
        public ObservableCollection<Extraction_DTO> Extractions
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
        public bool FinishedLoadingTasks { get; private set; } = false;

        private ObservableCollection<Quest_Task> tasks;
        public ObservableCollection<Quest_Task> Tasks
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
        public bool FinishedLoadingBTR { get; private set; } = false;

        private ObservableCollection<BTR> btr;
        public ObservableCollection<BTR> BTR
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
        public bool FinishedLoadingMarkers { get; private set; } = false;

        private ObservableCollection<Marker> markers;
        public ObservableCollection<Marker> Markers
        {
            get { return this.markers; }
            private set
            {
                this.markers = value;
                OnPropertyChanged(nameof(this.Markers));
            }
        }
        #endregion

        #region [Map] Variables & Properties
        public ObservableCollection<Map> Maps { get; private set; }
        private Map selectedMap;
        public Map SelectedMap
        {
            get { return this.selectedMap; }
            private set
            {
                this.selectedMap = value;
                OnPropertyChanged(nameof(this.SelectedMap));

                ResetCollections();
            }
        }
        #endregion

        public MapVM()
        {
            LoadData();
        }

        private async Task LoadData() // Make references to kill the threads if they still are running and SelectedMap changes?
        {
            this.Maps = await mapReposiroty.LoadAllAsync();
            this.SelectedMap = this.Maps.FirstOrDefault();

            _ = LoadExtractions();
            _ = LoadQuestTasks();
            _ = LoadBTR();
            _ = LoadMarkers();
        }

        private void ResetCollections()
        {
            this.FinishedLoadingExtractions = false;
            this.FinishedLoadingTasks = false;
            this.FinishedLoadingBTR = false;
            this.FinishedLoadingMarkers = false;

            //this.Extractions = new();
            //this.Tasks = new();
            //this.BTR = new();
            //this.Markers = new();

            LoadData();
        }

        #region Extraction Controls
        internal async Task LoadExtractions()
        {
            this.Extractions = await this.extractionRepository.LoadByMapAsync<Extraction_DTO>(this.SelectedMap.Id);

            if (this.Extractions != null && this.Extractions.Any())
            {
                IEnumerable<Task> enumerateTask = this.Extractions.Select(async extraction =>
                {
                    extraction.Requirements = await this.extractionRequirementRepository.LoadByExtractionAsync(extraction.Id);
                });

                await Task.WhenAll(enumerateTask);
                this.FinishedLoadingExtractions = true;
            }
        }

        internal async Task UpdateExtraction(Extraction selected)
        {
            if (selected != null && selected.Id >= 0 && this.Extractions != null)
            {
                await this.extractionRepository.UpdateCoordinatesAsync(selected);

                Extraction_DTO? old = this.Extractions.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = this.Extractions.IndexOf(old);

                    this.Extractions[index] = await this.extractionRepository.LoadSingleAsync<Extraction_DTO>(old.Id);
                    OnPropertyChanged(nameof(this.Extractions));
                }
            }
        }
        #endregion

        #region QuestTask Controls
        internal async Task LoadQuestTasks()
        {
            this.Tasks = await this.questTaskRepository.LoadActiveByMapAsync(this.SelectedMap.Id);
            this.FinishedLoadingTasks = true;
        }

        internal async Task UpdateQuestTask(Quest_Task selected)
        {
            if (selected != null && selected.Id >= 0 && this.Tasks != null)
            {
                await this.questTaskRepository.UpdateCoordiantesAsync(selected);

                Quest_Task? old = this.Tasks.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = this.Tasks.IndexOf(old);

                    this.Tasks[index] = await this.questTaskRepository.LoadSingleAsync(selected.Id);
                    OnPropertyChanged(nameof(this.Tasks));
                }
            }
        }
        #endregion

        #region BTR Controls
        internal async Task LoadBTR()
        {
            this.BTR = await this.btrRepository.LoadByMapAsync(this.SelectedMap.Id);
            this.FinishedLoadingBTR = true;
        }

        internal async Task UpdateBTR(BTR selected)
        {
            if (selected != null && selected.Id >= 0)
            {
                await this.btrRepository.UpdateCoordinates(selected);

                BTR? old = this.BTR.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = this.BTR.IndexOf(old);

                    this.BTR[index] = await this.btrRepository.LoadSingleAsync(old.Id);
                    OnPropertyChanged(nameof(this.BTR));
                }
            }
        }
        #endregion

        #region Marker Controls
        internal async Task LoadMarkers()
        {
            this.Markers = await this.markerRepository.LoadByMapAsync(this.SelectedMap.Id);
            this.FinishedLoadingMarkers = true;
        }

        internal async Task UpdateMarkerCoordinates(Marker selected)
        {
            if (selected != null && selected.Id >= 0)
            {
                await this.markerRepository.UpdateCoordinatesAsync(selected);

                Marker? old = this.Markers.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = this.Markers.IndexOf(old);

                    this.Markers[index] = await this.markerRepository.LoadSingleAsync(selected.Id);
                    OnPropertyChanged(nameof(this.Markers));
                }
            }
        }

        internal async Task UpdateMarkerSize(Marker selected)
        {
            if (selected != null && selected.Id! <= 0)
            {
                await this.markerRepository.UpdateSizeAsync(selected);

                Marker? old = this.Markers.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = this.Markers.IndexOf(old);

                    this.Markers[index] = await this.markerRepository.LoadSingleAsync(selected.Id);
                    OnPropertyChanged(nameof(this.Markers));
                }
            }
        }
        #endregion
    }
}
