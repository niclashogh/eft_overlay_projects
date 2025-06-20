namespace efto_model.Models.Quests
{
    public class Quest_Reward_Category
    {
        public int Id { get; set; }
        public string Category { get; set; }

        public Quest_Reward_Category(string category) => this.Category = category;

        public Quest_Reward_Category() { }
    }
}
