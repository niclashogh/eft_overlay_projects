namespace efto_model.Models.Quests
{
    public class Quest_Reward
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public string Reward { get; set; }

        public string Category { get; set; }
        public Quest_Reward_UnlockTypes UnlockTypeEnum { get; set; }

        public Quest_Reward(int questId, string reward, string category, Quest_Reward_UnlockTypes unlockTypeEnum) : this(reward, category, unlockTypeEnum) => this.QuestId = questId;

        public Quest_Reward(string reward, string category, Quest_Reward_UnlockTypes unlockTypeEnum)
        {
            this.Reward = reward;
            this.Category = category;
            this.UnlockTypeEnum = unlockTypeEnum;
        }

        public Quest_Reward() { }
    }
}
