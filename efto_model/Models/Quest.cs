using efto_model.Models.Enums;
using efto_model.Services;

namespace efto_model.Models
{
    public class Quest : NotifyChangedService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Traders Trader { get; set; }
        public Quest_Access Access { get; set; }

        public bool IsActive { get; set; }
        public bool IsComplete { get; set; }

        public Quest(string name, Traders trader, Quest_Access access)
        {
            this.Name = name;
            this.Trader = trader;
            this.Access = access;

            this.IsActive = false;
            this.IsComplete = false;
        }

        public Quest(bool isActive, bool isComplete)
        {
            this.IsActive = isActive;
            this.IsComplete = isComplete;
        }

        public Quest() { }
    }
}
