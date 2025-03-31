using efto_model.Models.Enums;
using System.Collections.ObjectModel;

namespace efto_model.Models.DataTransferObjects
{
    public class Quest_DTO : Quest
    {
        #region Requirements
        private ObservableCollection<Quest_Requirement>? requirements;
        public ObservableCollection<Quest_Requirement>? Requirements
        {
            get { return requirements; }
            set
            {
                requirements = value;
                OnPropertyChanged(nameof(Requirements));
            }
        }
        public bool AnyRequirements
        {
            get
            {
                if (Requirements == null)
                {
                    return false;
                }
                else return Requirements.Any() ? true : false;
            }
        }
        #endregion

        #region Rewards
        private ObservableCollection<Quest_Reward>? rewards;
        public ObservableCollection<Quest_Reward>? Rewards
        {
            get { return rewards; }
            set
            {
                rewards = value;
                OnPropertyChanged(nameof(Rewards));
            }
        }
        public bool AnyRewards
        {
            get
            {
                if (Rewards == null)
                {
                    return false;
                }
                else return Rewards.Any() ? true : false;
            }
        }
        #endregion

        #region Tasks
        private ObservableCollection<Quest_Task>? tasks;
        public ObservableCollection<Quest_Task>? Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public bool AnyTasks
        {
            get
            {
                if (Tasks == null)
                {
                    return false;
                }
                else return Tasks.Any() ? true : false;
            }
        }
        #endregion

        #region AccessKeys
        private ObservableCollection<AccessKey>? accessKeys;
        public ObservableCollection<AccessKey>? AccessKeys
        {
            get { return accessKeys; }
            set
            {
                accessKeys = value;
                OnPropertyChanged(nameof(AccessKeys));
            }
        }
        public bool AnyAccessKeys
        {
            get
            {
                if (AccessKeys == null)
                {
                    return false;
                }
                else return AccessKeys.Any() ? true : false;
            }
        }
        #endregion

        public bool AnyDetails
        {
            get
            {
                if (Requirements != null || Rewards != null || Tasks != null || AccessKeys != null)
                {
                    return true;
                }
                else return false;
            }
        }

        public Quest_DTO(string name, Traders trader, Quest_Access eAccess) : base(name, trader, eAccess) { }

        public Quest_DTO(bool isActive, bool isComplete) : base(isActive, isComplete) { }

        public Quest_DTO() : base() { }
    }
}
