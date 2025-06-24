using efto_model.Models.AccessKeys;
using System.Collections.ObjectModel;

namespace efto_model.Models.Quests
{
    public class Quest_DTO : Quest
    {
        #region Requirements
        private ObservableCollection<Quest_Requirement>? requirements;
        public ObservableCollection<Quest_Requirement>? Requirements
        {
            get { return this.requirements; }
            set
            {
                this.requirements = value;
                OnPropertyChanged(nameof(this.Requirements));
            }
        }
        public bool AnyRequirements
        {
            get
            {
                if (this.Requirements == null)
                {
                    return false;
                }
                else return this.Requirements.Any() ? true : false;
            }
        }
        #endregion

        #region Rewards
        private ObservableCollection<Quest_Reward>? rewards;
        public ObservableCollection<Quest_Reward>? Rewards
        {
            get { return this.rewards; }
            set
            {
                this.rewards = value;
                OnPropertyChanged(nameof(this.Rewards));
            }
        }
        public bool AnyRewards
        {
            get
            {
                if (this.Rewards == null)
                {
                    return false;
                }
                else return this.Rewards.Any() ? true : false;
            }
        }
        #endregion

        #region Tasks
        private ObservableCollection<Quest_Task>? tasks;
        public ObservableCollection<Quest_Task>? Tasks
        {
            get { return this.tasks; }
            set
            {
                this.tasks = value;
                OnPropertyChanged(nameof(this.Tasks));
            }
        }

        public bool AnyTasks
        {
            get
            {
                if (this.Tasks == null)
                {
                    return false;
                }
                else return this.Tasks.Any() ? true : false;
            }
        }
        #endregion

        #region AccessKeys
        private ObservableCollection<AccessKey>? accessKeys;
        public ObservableCollection<AccessKey>? AccessKeys
        {
            get { return this.accessKeys; }
            set
            {
                this.accessKeys = value;
                OnPropertyChanged(nameof(this.AccessKeys));
            }
        }
        public bool AnyAccessKeys
        {
            get
            {
                if (this.AccessKeys == null)
                {
                    return false;
                }
                else return this.AccessKeys.Any() ? true : false;
            }
        }
        #endregion

        public bool AnyDetails
        {
            get
            {
                if (this.Requirements != null || this.Rewards != null || this.Tasks != null || this.AccessKeys != null)
                {
                    return true;
                }
                else return false;
            }
        }

        public Quest_DTO(string name, string traderName, Quest_Access accessEnum) : base(name, traderName, accessEnum) { }

        public Quest_DTO(bool isActive, bool isComplete) : base(isActive, isComplete) { }

        public Quest_DTO() : base() { }
    }
}
