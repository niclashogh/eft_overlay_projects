namespace efto_model.Models.Quests
{
    public class Quest_Reward
    {
        public int Id { get; set; }
        public int QuestId { get; set; }
        public string Reward { get; set; }

        public int CategoryId { get; set; }
        public int AccessId { get; set; }

        public Quest_Reward(int questId, string reward, int categoryId, int accessId) : this(reward, categoryId, accessId) => this.QuestId = questId;

        public Quest_Reward(string reward, int categoryId, int accessId)
        {
            this.Reward = reward;
            this.CategoryId = categoryId;
            this.AccessId = accessId;
        }

        public Quest_Reward() { }
    }
}
