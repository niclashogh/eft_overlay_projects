using efto_model.Services;

namespace efto_model.Models.Quests
{
    public class Quest : NotifyChangedService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TraderId { get; set; }
        public Quest_Access AccessEnum { get; set; }

        public bool IsActive { get; set; }
        public bool IsComplete { get; set; }

        public Quest(string name, int traderId, Quest_Access accessEnum) : this(false, false)
        {
            this.Name = name;
            this.TraderId = traderId;
            this.AccessEnum = accessEnum;
        }

        public Quest(bool isActive, bool isComplete)
        {
            IsActive = isActive;
            IsComplete = isComplete;
        }

        public Quest() { }
    }
}
