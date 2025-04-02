using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using efto_model.Models;
using efto_model.Models.DataTransferObjects;
using efto_model.Repositories;

namespace efto_window.ViewModels.Windows
{
    public class SearchVM : WindowVM
    {
        private Quest_Repo questRepository = new();
        private AccessKey_Repo accessKeyRepository = new();

        #region [Quest] Variables & Properties
        private ObservableCollection<Quest_DTO> quests = new();
        public ObservableCollection<Quest_DTO> Quests
        {
            get { return this.quests; }
            private set
            {
                this.quests = value;
                OnPropertyChanged(nameof(this.Quests));
            }
        }

        private List<Quest_DTO> selectedQuests = new();
        public List<Quest_DTO> SelectedQuests
        {
            get { return this.selectedQuests; }
            set
            {
                this.selectedQuests = value;
                OnPropertyChanged(nameof(this.SelectedQuests));
            }
        }

        private StringProperty questSearchWord = new("Search Quests ...", string.Empty);
        public StringProperty QuestSearchWord
        {
            get { return this.questSearchWord; }
            set
            {
                this.questSearchWord = value;
                OnPropertyChanged(nameof(this.QuestSearchWord));
            }
        }
        #endregion

        #region [AccessKey] Variables & Properties
        private ObservableCollection<AccessKey_DTO> accessKeys = new();
        public ObservableCollection<AccessKey_DTO> AccessKeys
        {
            get { return this.accessKeys; }
            set
            {
                this.accessKeys = value;
                OnPropertyChanged(nameof(this.AccessKeys));
            }
        }

        private List<AccessKey_DTO> selectedAccessKeys = new();
        public List<AccessKey_DTO> SelectedAccessKeys
        {
            get { return this.selectedAccessKeys; }
            set
            {
                this.selectedAccessKeys = value;
                OnPropertyChanged(nameof(this.SelectedAccessKeys));
            }
        }

        private StringProperty accessKeySearchWord = new("Search Accesskeys ...", string.Empty);
        public StringProperty AccessKeySearchWord
        {
            get { return this.accessKeySearchWord; }
            set
            {
                this.accessKeySearchWord = value;
                OnPropertyChanged(nameof(this.AccessKeySearchWord));
            }
        }
        #endregion

        public SearchVM() { }

        #region Quest Controls
        internal async Task FindQuests() => await questRepository.Find(this.QuestSearchWord.Property);

        internal async Task ToggleQuestActivation()
        {
            foreach (Quest quest in SelectedQuests)
            {
                quest.IsActive = !quest.IsActive;
                await questRepository.UpdateActive(quest);

                Quest_DTO? old = Quests.FirstOrDefault(sorting => sorting.Id == quest.Id);

                if (old != null)
                {
                    int index = Quests.IndexOf(old);

                    Quests[index] = await questRepository.LoadSingle<Quest_DTO>(old.Id);
                    //Quests[index].Requirements = await RequirementRepository.LoadFromQuest(old.Id, SelectedTrader);
                    //Quests[index].Rewards = await RewardRepository.LoadFromQuest(old.Id, SelectedTrader);
                    //Quests[index].Tasks = await TaskRepository.LoadFromQuest(old.Id, SelectedTrader);
                    //Quests[index].AccessKeys = await AccessKeyRepository.LoadFromQuest(old.Id);
                }
            }
        }

        internal async Task ToggleQuestCompletion()
        {
            foreach (Quest quest in SelectedQuests)
            {
                quest.IsComplete = !quest.IsComplete;
                await questRepository.UpdateCompletion(quest);

                Quest_DTO? old = Quests.FirstOrDefault(sorting => sorting.Id == quest.Id);

                if (old != null)
                {
                    int index = Quests.IndexOf(old);

                    Quests[index] = await questRepository.LoadSingle<Quest_DTO>(old.Id);
                    //Quests[index].Requirements = await RequirementRepository.LoadFromQuest(old.Id, SelectedTrader);
                    //Quests[index].Rewards = await RewardRepository.LoadFromQuest(old.Id, SelectedTrader);
                    //Quests[index].Tasks = await TaskRepository.LoadFromQuest(old.Id, SelectedTrader);
                    //Quests[index].AccessKeys = await AccessKeyRepository.LoadFromQuest(old.Id);
                }
            }
        }
        #endregion

        internal async Task FindAccessKeys() => await accessKeyRepository.Find(this.AccessKeySearchWord.Property);
    }
}
