using efto_model.Models.Enums;

namespace efto_model.Models
{
    public class Quest_Reward
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public string Reward { get; set; }
        public Quest_Reward_Categories Category { get; set; }
        public Quest_Reward_Types Type { get; set; }

        public Quest_Reward(int questId, string reward, Quest_Reward_Categories category, Quest_Reward_Types type)
        {
            this.QuestId = questId;
            this.Reward = reward;
            this.Category = category;
            this.Type = type;
        }

        public Quest_Reward(string reward, Quest_Reward_Categories category, Quest_Reward_Types type)
        {
            this.Reward = reward;
            this.Category = category;
            this.Type = type;
        }

        public Quest_Reward() { }
    }
}
