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
    public class MapVM : BaseVM
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
            get { return extractions; }
            private set
            {
                extractions = value;
                OnPropertyChanged(nameof(Extractions));
            }
        }
        #endregion

        #region [Quest, Task] Variables & Properties
        private ObservableCollection<Quest_Task>? tasks;
        public ObservableCollection<Quest_Task>? Tasks
        {
            get { return tasks; }
            private set
            {
                value = tasks;
                OnPropertyChanged(nameof(Tasks));
            }
        }
        #endregion

        #region [BTR] Variables & Properties
        private ObservableCollection<BTR>? btr;
        public ObservableCollection<BTR>? BTR
        {
            get { return btr; }
            private set
            {
                value = btr;
                OnPropertyChanged(nameof(BTR));
            }
        }
        #endregion

        #region [Marker] Variables & Properties
        private ObservableCollection<Marker>? markers;
        public ObservableCollection<Marker>? Markers
        {
            get { return markers; }
            private set
            {
                markers = value;
                OnPropertyChanged(nameof(Markers));
            }
        }
        #endregion

        public ColorPalette Palette = new();
        public Maps SelectedMap { get; private set; }

        public MapVM(Maps map = 0)
        {
            this.SelectedMap = map;

            _ = LoadExtractions();
            _ = LoadQuestTasks();
            _ = LoadBTR();
            _ = LoadMarkers();
        }

        #region Extraction Controls
        internal async Task LoadExtractions()
        {
            Extractions = await extractionRepository.LoadFromMap<Extraction_DTO>(this.SelectedMap);

            if (Extractions != null && Extractions.Any())
            {
                IEnumerable<Task> enumerateTask = Extractions.Select(async extraction =>
                {
                    extraction.Requirements = await extractionRequirementRepository.LoadFromExtraction(extraction.Id);
                });

                await Task.WhenAll(enumerateTask);
            }
        }

        internal async Task UpdateExtraction(Extraction selected)
        {
            if (selected != null && selected.Id! <= 0 && Extractions != null)
            {
                await extractionRepository.UpdateCoordinates(selected);

                Extraction_DTO? old = Extractions.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = Extractions.IndexOf(old);

                    Extractions[index] = await extractionRepository.LoadSingle<Extraction_DTO>(old.Id);
                    OnPropertyChanged(nameof(Extractions));
                }
            }
        }
        #endregion

        #region QuestTask Controls
        internal async Task LoadQuestTasks() => Tasks = await questTaskRepository.LoadActiveFromMap(this.SelectedMap);

        internal async Task UpdateQuestTask(Quest_Task selected)
        {
            if (selected != null && selected.Id! <= 0 && Tasks != null)
            {
                await questTaskRepository.UpdateCoordiantes(selected);

                Quest_Task? old = Tasks.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = Tasks.IndexOf(old);

                    Tasks[index] = await questTaskRepository.LoadSingle(selected.Id);
                }
            }
        }
        #endregion

        #region BTR Controls
        internal async Task LoadBTR() => await btrRepository.LoadFromMap(this.SelectedMap);

        internal async Task UpdateBTR(BTR selected)
        {
            if (selected != null && selected.Id! <= 0)
            {
                await btrRepository.UpdateCoordinates(selected);

                BTR? old = BTR.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = BTR.IndexOf(old);

                    BTR[index] = await btrRepository.LoadSingle(old.Id);
                }
            }
        }
        #endregion

        #region Marker Controls
        internal async Task LoadMarkers() => await markerRepository.LoadFromMap(this.SelectedMap);

        internal async Task UpdateMarkerCoordinates(Marker selected)
        {
            if (selected != null && selected.Id! <= 0)
            {
                await markerRepository.UpdateCoordinates(selected);

                Marker? old = Markers.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = Markers.IndexOf(old);

                    Markers[index] = await markerRepository.LoadSingle(selected.Id);
                }
            }
        }

        internal async Task UpdateMarkerSize(Marker selected)
        {
            if (selected != null && selected.Id! <= 0)
            {
                await markerRepository.UpdateSize(selected);

                Marker? old = Markers.FirstOrDefault(sorting => sorting.Id == selected.Id);

                if (old != null)
                {
                    int index = Markers.IndexOf(old);

                    Markers[index] = await markerRepository.LoadSingle(selected.Id);
                }
            }
        }
        #endregion
    }
}
